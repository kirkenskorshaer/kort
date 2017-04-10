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
	public class MemberHandlerTest : TestBase
	{
		[Test]
		public void InsertedMemberCanBeReadByCardId()
		{
			Member member = InsertMember(out DateTime validFrom);

			string ticket = CreateValidTicket(validFrom, 1);
			GetMembersByCardId getMembersByCardId = new GetMembersByCardId();
			Pipeline<string, List<Member>> pipeline = CreatePipeline(ticket, member.CardId, getMembersByCardId);

			List<Member> members = pipeline.Execute();

			CollectionAssert.AreEqual(members, new List<Member>() { member });
		}

		[Test]
		public void InsertedMemberCanBeReadByNickName()
		{
			Member member = InsertMember(out DateTime validFrom);

			string ticket = CreateValidTicket(validFrom, 1);
			GetMembersByNickName getMembersByNickName = new GetMembersByNickName();
			Pipeline<string, List<Member>> pipeline = CreatePipeline(ticket, member.NickName, getMembersByNickName);

			List<Member> members = pipeline.Execute();

			CollectionAssert.AreEqual(members, new List<Member>() { member });
		}

		[Test]
		public void AlertsCanBeRetreivedOnInsertedMember()
		{
			Member member = InsertMember(out DateTime validFrom);

			GrantTestUserPrivileges(Privilege.CollectionEnum.VisitTimeAlert, null, Privilege.PrivilegeLevelEnum.Read, Privilege.PrivilegeLevelEnum.Insert);

			VisitTimeAlert visitTimeAlert = new VisitTimeAlert(member) { TimeFromLatestVisitBeforeAlert = TimeSpan.FromMilliseconds(1) };
			string ticket = CreateValidTicket(validFrom, 1);
			InsertAlert insertAlert = new InsertAlert();
			Pipeline<AbstractAlert, string> pipeline = CreatePipeline(ticket, visitTimeAlert, insertAlert);
			pipeline.Execute();

			ticket = CreateValidTicket(validFrom, 2);
			GetMembersToAlert getMembersToAlert = new GetMembersToAlert();
			Pipeline<object, List<Member>> getMembersToAlertPipeline = CreatePipeline(ticket, null, getMembersToAlert);

			List<Member> members = getMembersToAlertPipeline.Execute();

			CollectionAssert.AreEqual(members, new List<Member>() { member });
		}

		[Test]
		public void AllowAll()
		{
			GrantTestUserPrivileges(Privilege.CollectionEnum.Member, null, Privilege.PrivilegeLevelEnum.Read | Privilege.PrivilegeLevelEnum.Insert | Privilege.PrivilegeLevelEnum.Update | Privilege.PrivilegeLevelEnum.Delete);
			GrantTestUserPrivileges(Privilege.CollectionEnum.Service, null, Privilege.PrivilegeLevelEnum.Read | Privilege.PrivilegeLevelEnum.Insert | Privilege.PrivilegeLevelEnum.Update | Privilege.PrivilegeLevelEnum.Delete);
			GrantTestUserPrivileges(Privilege.CollectionEnum.User, null, Privilege.PrivilegeLevelEnum.Read | Privilege.PrivilegeLevelEnum.Insert | Privilege.PrivilegeLevelEnum.Update | Privilege.PrivilegeLevelEnum.Delete);
			GrantTestUserPrivileges(Privilege.CollectionEnum.Visit, null, Privilege.PrivilegeLevelEnum.Read | Privilege.PrivilegeLevelEnum.Insert | Privilege.PrivilegeLevelEnum.Update | Privilege.PrivilegeLevelEnum.Delete);
			GrantTestUserPrivileges(Privilege.CollectionEnum.VisitTimeAlert, null, Privilege.PrivilegeLevelEnum.Read | Privilege.PrivilegeLevelEnum.Insert | Privilege.PrivilegeLevelEnum.Update | Privilege.PrivilegeLevelEnum.Delete);
		}

		private Member InsertMember(out DateTime validFrom)
		{
			validFrom = DateTime.Now;
			string ticket = CreateValidTicket(validFrom, 0);
			InsertMember insertMember = new InsertMember();

			GrantTestUserPrivileges(Privilege.CollectionEnum.Member, validFrom, Privilege.PrivilegeLevelEnum.Read, Privilege.PrivilegeLevelEnum.Insert);

			Member member = new Member()
			{
				CardId = $"cardid {Guid.NewGuid()}",
				NickName = $"NickName {Guid.NewGuid()}",
			};

			Pipeline<Member, string> pipeline = CreatePipeline(ticket, member, insertMember);

			pipeline.Execute();

			return member;
		}
	}
}
