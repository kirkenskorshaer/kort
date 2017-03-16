using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Data.Data
{
	public class Visit : AbstractMemberConnectedData
	{
		public Visit(string memberId)
		{
			MemberIdString = memberId;
		}

		[BsonDateTimeOptions(Kind = DateTimeKind.Local)]
		public DateTime VisitTime { get; set; }
	}
}
