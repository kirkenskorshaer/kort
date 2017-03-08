using Data;
using Data.Data;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataTest
{
	[TestFixture]
	public class MongoConnectionTest : TestBase
	{
		[Test]
		public void MongoConnectionCanReadData()
		{
			Member member = InsertMember();

			Member readMember = AbstractData.Read<Member>(_mongoConnection, member.Id);

			Assert.IsTrue(member.Equals(readMember));
		}

		[Test]
		public void MongoConnectionCanReadMultipleData()
		{
			Member member1 = InsertMember();
			Member member2 = InsertMember();
			IOrderedEnumerable<Member> members = new List<Member>() { member1, member2 }.OrderBy(member => member.Id);

			IOrderedEnumerable<Member> readMembers = AbstractData.Read<Member>(_mongoConnection, member => true).OrderBy(member => member.Id);

			CollectionAssert.AreEqual(readMembers, members);
		}

		[Test]
		public void MongoConnectionCanUpdateData()
		{
			Member member = InsertMember();
			member.CardId = $"new card id {Guid.NewGuid()}";

			member.Update<Member>(_mongoConnection);

			Member readMember = AbstractData.Read<Member>(_mongoConnection, member.Id);

			Assert.IsTrue(member.Equals(readMember));
		}

		[Test]
		public void MongoConnectionCanUpdateMultipleData()
		{
			Member member1 = InsertMember();
			Member member2 = InsertMember();

			string newId = $"new card id {Guid.NewGuid()}";

			AbstractData.Update<Member, string>(_mongoConnection, member => true, member => member.CardId, newId);

			IOrderedEnumerable<Member> readMembers = AbstractData.Read<Member>(_mongoConnection, member => true).OrderBy(member => member.Id);

			Assert.IsTrue(readMembers.All(member => member.CardId == newId));
		}

		[Test]
		public void MongoConnectionCanDeleteData()
		{
			Member member = InsertMember();

			member.Delete(_mongoConnection);

			Member readMember = AbstractData.Read<Member>(_mongoConnection, member.Id);

			Assert.IsNull(readMember);
		}
	}
}
