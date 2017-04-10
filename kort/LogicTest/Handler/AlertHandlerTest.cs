using Data.Data;
using Data.Data.Alert;
using Logic;
using Logic.Receivable.Alert;
using Logic.Receivable.Member;
using Logic.Security;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace LogicTest.Handler
{
	[TestFixture]
	public class AlertHandlerTest : TestBase
	{
		[Test]
		public void InsertedAlertCanBeRead()
		{
			ArrangeMemberWithAlert(out Member member, out VisitTimeAlert alert, out DateTime validFrom);

			string ticket = CreateValidTicket(validFrom, 2);
			GetAlertsOnMember getAlertsOnMember = new GetAlertsOnMember();
			Pipeline<string, List<AbstractAlert>> pipeline = CreatePipeline(ticket, member.Id, getAlertsOnMember);

			List<AbstractAlert> alerts = pipeline.Execute();

			CollectionAssert.AreEqual(alerts, new List<AbstractAlert>() { alert });
		}

		private void ArrangeMemberWithAlert(out Member member, out VisitTimeAlert alert, out DateTime validFrom)
		{
			member = new Member();
			validFrom = DateTime.Now;
			string ticket = CreateValidTicket(validFrom, 0);
			InsertMember insertMember = new InsertMember();
			Pipeline<Member, string> insertMemberPipeline = CreatePipeline(ticket, member, insertMember);

			GrantTestUserPrivileges(Privilege.CollectionEnum.Member, validFrom, Privilege.PrivilegeLevelEnum.Read, Privilege.PrivilegeLevelEnum.Insert);
			GrantTestUserPrivileges(Privilege.CollectionEnum.VisitTimeAlert, validFrom, Privilege.PrivilegeLevelEnum.Read, Privilege.PrivilegeLevelEnum.Insert);

			insertMemberPipeline.Execute();
			alert = new VisitTimeAlert(member) { TimeFromLatestVisitBeforeAlert = TimeSpan.FromMilliseconds(1) };
			ticket = CreateValidTicket(validFrom, 1);
			InsertAlert insertAlert = new InsertAlert();
			Pipeline<AbstractAlert, string> insertAlertPipeline = CreatePipeline(ticket, alert, insertAlert);

			insertAlertPipeline.Execute();
		}
	}
}
