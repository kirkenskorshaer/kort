using Data;
using Data.Data;
using Data.Data.Alert;
using NUnit.Framework;
using System;

namespace DataTest
{
	[TestFixture]
	public class TestBase
	{
		protected MongoConnection _mongoConnection;
		protected Random _random = new Random();

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_mongoConnection = MongoConnection.GetConnection("card");
		}

		[SetUp]
		public void SetUp()
		{
			_mongoConnection.CleanDatabase();
		}

		protected Member InsertMember()
		{
			Member member = new Member()
			{
				CardId = Guid.NewGuid().ToString(),
				NickName = $"nickname {Guid.NewGuid()}",
			};

			member.Insert<Member>(_mongoConnection);

			return member;
		}

		protected Visit[] InsertVisits(Member[] members, int numberOfVisitsToCreate, int visitAgeHours)
		{
			Visit[] visits = new Visit[numberOfVisitsToCreate];

			DateTime visitTime = DateTime.Now - TimeSpan.FromHours(_random.NextDouble() * visitAgeHours);

			for (int currentVisitIndex = 0; currentVisitIndex < numberOfVisitsToCreate; currentVisitIndex++)
			{
				Visit visit = new Visit(members[_random.Next(0, members.Length)].Id)
				{
					VisitTime = visitTime,
				};

				visits[currentVisitIndex] = visit;
			}

			AbstractData.Insert(_mongoConnection, visits);

			return visits;
		}

		protected VisitTimeAlert[] InsertVisitTimeAlerts(Member[] members, int numberOfVisitTimeAlertsToCreate, int timeFromLatestVisitBeforeAlertHours)
		{
			VisitTimeAlert[] visitTimeAlerts = new VisitTimeAlert[numberOfVisitTimeAlertsToCreate];

			TimeSpan timeFromLatestVisitBeforeAlert = TimeSpan.FromHours(_random.NextDouble() * timeFromLatestVisitBeforeAlertHours);

			for (int currentVisitTimeAlertIndex = 0; currentVisitTimeAlertIndex < numberOfVisitTimeAlertsToCreate; currentVisitTimeAlertIndex++)
			{
				VisitTimeAlert visitTimeAlert = new VisitTimeAlert(members[_random.Next(0, members.Length)])
				{
					TimeFromLatestVisitBeforeAlert = timeFromLatestVisitBeforeAlert,
				};

				visitTimeAlerts[currentVisitTimeAlertIndex] = visitTimeAlert;
			}

			AbstractData.Insert(_mongoConnection, visitTimeAlerts);

			return visitTimeAlerts;
		}

		protected VisitTimeAlert InsertVisitTimeAlert(Member member)
		{
			VisitTimeAlert alert = new VisitTimeAlert(member)
			{
				TimeFromLatestVisitBeforeAlert = TimeSpan.FromHours(24),
			};

			alert.Insert<VisitTimeAlert>(_mongoConnection);

			return alert;
		}

		protected Visit InsertVisit(Member member)
		{
			DateTime visitTime = DateTime.Now - TimeSpan.FromHours(_random.NextDouble() * 24);

			Visit visit = new Visit(member.Id)
			{
				VisitTime = visitTime,
			};

			visit.Insert<Visit>(_mongoConnection);

			return visit;
		}

		protected Member[] InsertMembers(int numberOfMembersToCreate)
		{
			Member[] members = new Member[numberOfMembersToCreate];

			for (int currentMemberIndex = 0; currentMemberIndex < numberOfMembersToCreate; currentMemberIndex++)
			{
				Member member = new Member()
				{
					CardId = Guid.NewGuid().ToString(),
					NickName = $"nickname {Guid.NewGuid()}",
				};

				members[currentMemberIndex] = member;
			}

			AbstractData.Insert(_mongoConnection, members);

			return members;
		}

	}
}
