using NUnit.Framework;
using System;
using Data;
using Logic.Security;
using Data.Data;
using System.Collections.Generic;
using System.Linq;
using Logic;
using Logic.Receivable;
using System.Text;

namespace LogicTest
{
	[TestFixture]
	public class TestBase
	{
		protected MongoConnection _mongoConnection;
		protected Random _random = new Random();
		protected string _userId;
		protected string _ip = "127.0.0.1";

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_mongoConnection = MongoConnection.GetConnection("card");
		}

		[SetUp]
		public void SetUp()
		{
			_mongoConnection.CleanDatabase();
			CreateUser();
		}

		private void CreateUser()
		{
			User user = new User()
			{
				UserName = "userNameTest",
				Password = "passwordTest",
			};

			_userId = user.Insert<User>(_mongoConnection);
		}

		protected void GrantTestUserPrivileges(DateTime? validFrom, List<Privilege> privileges)
		{
			User user = AbstractData.Read<User>(_mongoConnection, _userId);

			IEnumerable<Data.Data.Part.Privilege> newPrivileges = privileges.Select(privilege => privilege.AsDataPriviledge);

			if (user.Privileges == null)
			{
				user.Privileges = newPrivileges.ToList();
			}
			else
			{
				user.Privileges = user.Privileges.Concat(newPrivileges).ToList();
			}

			if (validFrom.HasValue)
			{
				user.Ticket = new Data.Data.Part.Ticket()
				{
					Ip = _ip,
					UseCount = 0,
					ValidFrom = validFrom.Value,
					ValidTo = validFrom.Value.AddMinutes(10),
				};
			}

			user.Update<User>(_mongoConnection);
		}

		protected void GrantTestUserPrivileges(Privilege.CollectionEnum collection, DateTime? validFrom, params Privilege.PrivilegeLevelEnum[] privilegeLevels)
		{
			List<Privilege> privileges = privilegeLevels.Select(privilegeLevel =>
			new Privilege()
			{
				Collection = collection,
				PrivilegeLevel = privilegeLevel,
			}).ToList();

			GrantTestUserPrivileges(validFrom, privileges);
		}

		protected Pipeline<InputType, OutputType> CreatePipeline<InputType, OutputType>(string ticket, InputType input, IReceivable<InputType, OutputType> receivable)
		{
			return new Pipeline<InputType, OutputType>(receivable, input, (bool isRejected, StringBuilder stringBuilder) => { })
			{
				Ip = _ip,
				Ticket = ticket,
				UserName = "userNameTest",
			};
		}

		protected string CreateValidTicket(DateTime validFrom, int userCount)
		{
			string userName = "userNameTest";
			string password = "passwordTest";
			string ip = _ip;
			DateTime validTo = validFrom.AddMinutes(10);

			string ticket = Utilities.Ticket.CreateTicket(userName, password, ip, userCount, validFrom, validTo);

			return ticket;
		}
	}
}