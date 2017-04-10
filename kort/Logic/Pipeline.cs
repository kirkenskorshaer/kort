using Data;
using Data.Data;
using Logic.Receivable;
using Logic.Receivable.Ticket;
using Logic.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logic
{
	public class Pipeline<InputType, OutputType>
	{
		private IReceivable<InputType, OutputType> _receivable;
		public MongoConnection MongoConnection { get; private set; }
		private InputType _input;

		public string UserName { get; set; }
		public string Ip { get; set; }
		public string Ticket { get; set; }

		public StringBuilder LogBuilder = new StringBuilder();
		public StringBuilder PublicErrorBuilder = new StringBuilder();

		public bool IsRejected { get; private set; }

		private Action<bool, StringBuilder> _afterExecute;

		public Pipeline(IReceivable<InputType, OutputType> receivable, InputType input, Action<bool, StringBuilder> afterExecute)
		{
			MongoConnection = MongoConnection.GetConnection("card");

			_input = input;
			_receivable = receivable;
			_afterExecute = afterExecute;
		}

		public OutputType Execute()
		{
			try
			{
				if (_receivable.GetType() != typeof(CreateTicket) && IsTicketValidForDataManipulation() == false)
				{
					LogRejected(MongoConnection, _receivable.GetType());
					return default(OutputType);
				}

				return _receivable.Execute(this, _input);
			}
			catch (Exception exception)
			{
				Log.Write(MongoConnection, _receivable.GetType(), exception);
				return default(OutputType);
			}
			finally
			{
				_afterExecute(IsRejected, PublicErrorBuilder);
			}
		}

		private void LogRejected(MongoConnection mongoConnection, Type handlerType)
		{
			IsRejected = true;

			Log.Write(mongoConnection, handlerType, $"failed authorization error for User: {UserName} ErrorLog: {LogBuilder.ToString()}");
		}

		private bool IsTicketValid()
		{
			User user = GetUser(UserName);

			if (user == null)
			{
				return false;
			}

			return IsTicketValid(user);
		}

		private bool IsTicketValid(User user)
		{
			DateTime currentTime = DateTime.Now;

			if (user.Ticket == null)
			{
				LogBuilder.AppendLine("User has no ticket");
				return false;
			}

			Data.Data.Part.Ticket ticket = user.Ticket;

			if (ticket.ValidFrom > currentTime || ticket.ValidTo < currentTime)
			{
				LogBuilder.AppendLine($"invalid ticket time CurrentTime: {currentTime} ValidFrom: {ticket.ValidFrom} ValidTo: {ticket.ValidTo}");
				return false;
			}

			string validTicket = Utilities.Ticket.CreateTicket(UserName, user.Password, ticket.Ip, ticket.UseCount, ticket.ValidFrom, ticket.ValidTo);

			if (Ticket != validTicket)
			{
				LogBuilder.AppendLine($"invalid ticket {Ticket} valid ticket was {validTicket} built with UserName: {UserName} Password: {user.Password} Ip: {ticket.Ip} Count: {ticket.UseCount} ValidFrom: {ticket.ValidFrom} ValidTo: {ticket.ValidTo}");
				return false;
			}

			user.Ticket.UseCount++;

			user.Update<User>(MongoConnection);

			return true;
		}

		private bool IsTicketValidForDataManipulation()
		{
			User user = GetUser(UserName);

			if (user == null)
			{
				return false;
			}

			LogBuilder.AppendLine("user found");

			if (IsTicketValid(user) == false)
			{
				return false;
			}

			if (IsPrivilegesValid(user) == false)
			{
				return false;
			}

			return true;
		}

		private bool IsPrivilegesValid(User user)
		{
			List<Privilege> privileges = _receivable.RequiredPrivileges;

			if (privileges == null)
			{
				return true;
			}

			foreach (Privilege privilege in privileges)
			{
				bool isThisPrivilegeGranted;

				if (user.Privileges == null)
				{
					isThisPrivilegeGranted = false;
				}
				else
				{
					isThisPrivilegeGranted = user.Privileges.Any(dataPrivilege =>
						dataPrivilege.Collection == privilege.Collection.ToString() &&
						dataPrivilege.PrivilegeLevel.HasFlag(privilege.DataPrivilegeLevel));
				}

				if (isThisPrivilegeGranted == false)
				{
					LogBuilder.AppendLine($"user {user.UserName} does not have privilege {privilege}");

					return false;
				}
			}

			return true;
		}

		private User GetUser(string userName)
		{
			return AbstractData.Read<User>(MongoConnection, userLinq => userLinq.UserName == userName).SingleOrDefault();
		}
	}
}
