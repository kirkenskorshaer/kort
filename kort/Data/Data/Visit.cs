using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Data.Data
{
	public class Visit : AbstractData
	{
		public Visit(Member member)
		{
			MemberId = member._id;
		}

		public ObjectId MemberId { get; set; }

		[BsonIgnore]
		public string MemberIdString => MemberId.ToString();

		[BsonDateTimeOptions(Kind = DateTimeKind.Local)]
		public DateTime VisitTime { get; set; }
	}
}
