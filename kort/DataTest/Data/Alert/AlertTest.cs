using Data.Data;
using Data.Data.Alert;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DataTest.Data.Alert
{
	[TestFixture]
	public class AlertTest : TestBase
	{
		[Test]
		public void AlertCanBeRaised()
		{
			Member member = InsertMember();
			VisitTimeAlert alert = InsertVisitTimeAlert(member);

			List<Member> membersToAlert = AlertCollection.ReadAlertCollections(_mongoConnection);

			CollectionAssert.AreEqual(new List<Member>() { member }, membersToAlert);
		}

		[Test]
		public void VisitTimeAlertWillNotBeRaisedIfThereIsAVisit()
		{
			Member member = InsertMember();
			VisitTimeAlert alert = InsertVisitTimeAlert(member);

			Visit visit = InsertVisit(member);

			List<Member> membersToAlert = AlertCollection.ReadAlertCollections(_mongoConnection);

			CollectionAssert.AreEqual(new List<Member>() { }, membersToAlert);
		}

		[Test]
		[Ignore("Performance test")]
		public void AlertsHaveAcceptablePerformance()
		{
			int insertGroupSize = 1000;
			int numberOfGroups = 10;

			for (int createMemberIndex = 0; createMemberIndex < numberOfGroups; createMemberIndex++)
			{
				Member[] members = InsertMembers(insertGroupSize);

				InsertVisits(members, insertGroupSize, 24);
				InsertVisitTimeAlerts(members, insertGroupSize, 24);
			}

			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();

			List<Member> membersToAlert = AlertCollection.ReadAlertCollections(_mongoConnection);

			stopwatch.Stop();

			Console.Out.WriteLine($"found {membersToAlert.Count} in {stopwatch.Elapsed.TotalSeconds} Seconds");
			Assert.Less(stopwatch.Elapsed, TimeSpan.FromMinutes(1));
		}
	}
}
