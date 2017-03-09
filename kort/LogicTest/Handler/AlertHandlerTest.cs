using Data.Data;
using Data.Data.Alert;
using Logic.Handler;
using NUnit.Framework;
using System.Collections.Generic;

namespace LogicTest.Handler
{
	[TestFixture]
	public class AlertHandlerTest : TestBase
	{
		[Test]
		public void InsertedAlertCanBeRead()
		{
			Member member = new Member();
			MemberHandler.Instance.InsertMember(member);
			VisitTimeAlert alert = new VisitTimeAlert(member);

			AlertHandler.Instance.InsertAlert(alert);

			List<AbstractAlert> alerts = AlertHandler.Instance.GetAlertsOnMember(member.Id);

			CollectionAssert.AreEqual(alerts, new List<AbstractAlert>() { alert });
		}
	}
}
