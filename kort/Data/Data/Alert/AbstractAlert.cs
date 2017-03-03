using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data.Data.Alert
{
	public abstract class AbstractAlert : AbstractData
	{
		public AbstractAlert(Member member)
		{
			MemberId = member._id;
		}

		public ObjectId MemberId { get; set; }

		[BsonIgnore]
		public string MemberIdString => MemberId.ToString();

		public abstract Member GetMemberIfRaised(MongoConnection mongoConnection);
	}
}