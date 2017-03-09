using Data;
using System.Collections.Generic;
using DataMember = Data.Data.Member;
using DataAlertCollection = Data.Data.Alert.AlertCollection;
using Data.Data;
using System;
using System.Linq.Expressions;

namespace Logic.Handler
{
	public class MemberHandler
	{
		private static readonly MemberHandler _memberHandler = new MemberHandler();
		private MongoConnection _mongoConnection;

		private MemberHandler()
		{
			_mongoConnection = MongoConnection.GetConnection("card");
		}

		public static MemberHandler Instance
		{
			get
			{
				return _memberHandler;
			}
		}

		public List<DataMember> GetMembersByCardId(string cardId)
		{
			List<DataMember> members = AbstractData.Read<DataMember>(_mongoConnection, member => member.CardId == cardId);

			return members;
		}

		public List<DataMember> GetMembersByNickName(string nickName)
		{
			List<DataMember> members = AbstractData.Read<DataMember>(_mongoConnection, member => member.NickName == nickName);

			return members;
		}

		public List<DataMember> GetMembersToAlert()
		{
			List<DataMember> members = DataAlertCollection.ReadAlertCollections(_mongoConnection);

			return members;
		}

		public string InsertMember(DataMember member)
		{
			return member.Insert<DataMember>(_mongoConnection);
		}

		public void UpdateMember(DataMember member)
		{
			member.Update<DataMember>(_mongoConnection);
		}

		public void DeleteMember(DataMember member)
		{
			member.Delete(_mongoConnection);
		}
	}
}
