using Logic;
using Logic.Receivable.Member;
using Logic.Security;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;

namespace LogicTest.Handler
{
	[TestFixture]
	public class TicketHandlerTest : TestBase
	{
		[Test]
		public void UsersWithNoTicketWillBeRejected()
		{
			GetMembersToAlert getMembersToAlert = new GetMembersToAlert();
			Pipeline<object, List<Data.Data.Member>> pipeline = new Pipeline<object, List<Data.Data.Member>>(getMembersToAlert, null, (bool isRejected, StringBuilder stringBuilder) => { })
			{
				Ip = "127.0.0.1",
				Ticket = null,
				UserName = "testUserName",
			};

			pipeline.Execute();

			Assert.True(pipeline.IsRejected);
		}

		[Test]
		public void ATicketIsOnlyValidOnce()
		{
			DateTime validFrom = DateTime.Now;
			string ticket = CreateValidTicket(validFrom, 0);

			GrantTestUserPrivileges(Privilege.CollectionEnum.Member, validFrom, Privilege.PrivilegeLevelEnum.Read);
			GrantTestUserPrivileges(Privilege.CollectionEnum.VisitTimeAlert, validFrom, Privilege.PrivilegeLevelEnum.Read);

			GetMembersToAlert getMembersToAlert1 = new GetMembersToAlert();
			Pipeline<object, List<Data.Data.Member>> getMembersToAlert1Pipeline = CreatePipeline(ticket, null, getMembersToAlert1);

			getMembersToAlert1Pipeline.Execute();

			GetMembersToAlert getMembersToAlert2 = new GetMembersToAlert();
			Pipeline<object, List<Data.Data.Member>> getMembersToAlert2Pipeline = CreatePipeline(ticket, null, getMembersToAlert2);

			getMembersToAlert2Pipeline.Execute();

			Assert.False(getMembersToAlert1Pipeline.IsRejected);
			Assert.True(getMembersToAlert2Pipeline.IsRejected);
		}
	}
}
