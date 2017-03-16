using System.Runtime.Serialization;
using DataMember = Data.Data.Member;

namespace WindowsServer.DataContract
{
	[DataContract]
	public class Member
	{
		[DataMember]
		public string Id { get; set; }

		[DataMember]
		public string CardId { get; set; }

		[DataMember]
		public string NickName { get; set; }

		internal static Member FromData(DataMember dataMember)
		{
			return new Member
			{
				Id = dataMember.Id,
				CardId = dataMember.CardId,
				NickName = dataMember.NickName,
			};
		}

		internal DataMember ToData()
		{
			return new DataMember
			{
				Id = Id,
				CardId = CardId,
				NickName = NickName,
			};
		}
	}
}
