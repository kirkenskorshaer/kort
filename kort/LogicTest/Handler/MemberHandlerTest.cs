using Data.Data;
using Data.Data.Alert;
using Logic.Handler;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace LogicTest.Handler
{
	[TestFixture]
	public class MemberHandlerTest : TestBase
	{
		[Test]
		public void InsertedMemberCanBeReadByCardId()
		{
			Member member = InsertMember();

			List<Member> members = MemberHandler.Instance.GetMembersByCardId(member.CardId);

			CollectionAssert.AreEqual(members, new List<Member>() { member });
		}

		[Test]
		public void InsertedMemberCanBeReadByNickName()
		{
			Member member = InsertMember();

			List<Member> members = MemberHandler.Instance.GetMembersByNickName(member.NickName);

			CollectionAssert.AreEqual(members, new List<Member>() { member });
		}

		[Test]
		public void AlertsCanBeRetreivedOnInsertedMember()
		{
			Member member = InsertMember();
			VisitTimeAlert visitTimeAlert = new VisitTimeAlert(member);
			AlertHandler.Instance.InsertAlert(visitTimeAlert);

			List<Member> members = MemberHandler.Instance.GetMembersToAlert();

			CollectionAssert.AreEqual(members, new List<Member>() { member });
		}

		private Member InsertMember()
		{
			Member member = new Member()
			{
				CardId = $"cardid {Guid.NewGuid()}",
				NickName = $"NickName {Guid.NewGuid()}",
			};
			MemberHandler.Instance.InsertMember(member);

			return member;
		}
	}
}
