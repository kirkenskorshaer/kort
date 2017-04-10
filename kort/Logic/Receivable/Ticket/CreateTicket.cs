using System;
using System.Collections.Generic;
using Data;
using Logic.Security;
using Data.Data;
using System.Linq;
using Logic.Time;
using System.Configuration;

namespace Logic.Receivable.Ticket
{
	public class CreateTicket : IReceivable<Period, bool>
	{
		private int _ticketMaxValidMinutes = 10;
		private int _ticketMinValidSeconds = 10;

		public CreateTicket()
		{
			string ticketMaxValidMinutes = ConfigurationManager.AppSettings["TicketMaxValidMinutes"];
			if (string.IsNullOrWhiteSpace(ticketMaxValidMinutes) == false)
			{
				_ticketMaxValidMinutes = int.Parse(ticketMaxValidMinutes);
			}

			string ticketMinValidSeconds = ConfigurationManager.AppSettings["TicketMinValidSeconds"];
			if (string.IsNullOrWhiteSpace(ticketMinValidSeconds) == false)
			{
				_ticketMinValidSeconds = int.Parse(ticketMinValidSeconds);
			}
		}

		public List<Privilege> RequiredPrivileges
		{
			get
			{
				return new List<Privilege>()
				{
				};
			}
		}

		public bool Execute(Pipeline<Period, bool> pipeline, Period period)
		{
			string ticketError = UpdateTicketOrError(pipeline, period.From.Value, period.To.Value);

			if (ticketError == null)
			{
				return true;
			}

			Log.Write(pipeline.MongoConnection, GetType(), ticketError);

			return false;
		}

		private string UpdateTicketOrError(Pipeline<Period, bool> pipeline, DateTime validFrom, DateTime validTo)
		{
			if (validTo < validFrom)
			{
				return $"Ticket span negative {validFrom} - {validTo}";
			}

			TimeSpan validSpan = validTo - validFrom;

			if (validSpan > TimeSpan.FromMinutes(_ticketMaxValidMinutes))
			{
				return $"Ticket span too long {validFrom} - {validTo} Max is {_ticketMaxValidMinutes} Minutes";
			}

			if (validSpan < TimeSpan.FromSeconds(_ticketMinValidSeconds))
			{
				return $"Ticket span too short {validFrom} - {validTo} Min is {_ticketMinValidSeconds} Seconds";
			}

			User user = AbstractData.Read<User>(pipeline.MongoConnection, userLinq => userLinq.UserName == pipeline.UserName).SingleOrDefault();

			if (user == null)
			{
				return $"User {pipeline.UserName} not found";
			}

			DateTime currentTime = DateTime.Now;

			if (user.Ticket != null && user.Ticket.ValidFrom < currentTime && user.Ticket.ValidTo > currentTime)
			{
				return $"There is already a valid ticket from {user.Ticket.ValidFrom} to {user.Ticket.ValidTo}";
			}

			user.Ticket = new Data.Data.Part.Ticket()
			{
				ValidFrom = validFrom,
				ValidTo = validTo,
				Ip = pipeline.Ip,
				UseCount = 0,
			};

			user.Update<User>(pipeline.MongoConnection);

			return null;
		}
	}
}
