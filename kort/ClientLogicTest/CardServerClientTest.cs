using ClientLogic;
using ClientLogic.CardServer;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ClientLogicTest
{
	[TestFixture]
	public class CardServerClientTest
	{
		private CardServerClient _cardServerClient;
		private TimeSpan _ticketValidTime = TimeSpan.FromSeconds(5);

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_cardServerClient = new CardServerClient("userNameTest", "passwordTest", "::1", _ticketValidTime);
		}

		[Test]
		public void MembersCanBeInserted()
		{
			Member member = GetMember();

			_cardServerClient.InsertMember(member);

			Assert.IsNotNull(member.Id);
		}

		[Test]
		public void CallsAutomaticallyRenewTickets()
		{
			Member member = GetMember();
			_cardServerClient.InsertMember(member);

			Thread.Sleep(_ticketValidTime.Add(TimeSpan.FromSeconds(1)));

			member = GetMember();
			_cardServerClient.InsertMember(member);
		}

		[Test]
		public void ASecondCallCanBeMade()
		{
			Member member = GetMember();

			_cardServerClient.InsertMember(member);

			member = GetMember();

			_cardServerClient.InsertMember(member);
		}

		[Test]
		public void ASecondClientMayNotConnectWhileTheFirstIsConnected()
		{
			Member member = GetMember();

			_cardServerClient.InsertMember(member);

			CardServerClient cardServerClient = new CardServerClient("userNameTest", "passwordTest", "::1", _ticketValidTime);

			member = GetMember();

			Assert.Throws<Exception>(() => cardServerClient.InsertMember(member), "Unable to get ticket");
		}

		[Test]
		public void MembersToAlertCanBeRetrieved()
		{
			Member member = GetMember();
			_cardServerClient.InsertMember(member);

			VisitTimeAlert alert = new VisitTimeAlert()
			{
				Member = member,
				TimeFromLatestVisitBeforeAlert = TimeSpan.FromMilliseconds(1),
			};
			_cardServerClient.InsertAlert(alert);

			List<Member> members = _cardServerClient.GetMembersToAlert();

			Assert.True(members.Any(lMember => lMember.Id == member.Id));
		}

		[Test]
		public void MembersCanBeReadByCardId()
		{
			Member member = GetMember();
			_cardServerClient.InsertMember(member);

			Member memberRead = _cardServerClient.GetMembersByCardId(member.CardId).Single();

			Assert.AreEqual(member.Id, memberRead.Id);
		}

		[Test]
		public void MembersCanBeReadByNickName()
		{
			Member member = GetMember();
			_cardServerClient.InsertMember(member);

			Member memberRead = _cardServerClient.GetMembersByNickName(member.NickName).Single();

			Assert.AreEqual(member.Id, memberRead.Id);
		}

		private Member GetMember()
		{
			return new Member()
			{
				CardId = $"cardId {Guid.NewGuid()}",
				NickName = $"nickName {Guid.NewGuid()}",
			};
		}
	}
}