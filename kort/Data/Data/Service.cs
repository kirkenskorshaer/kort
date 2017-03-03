using Data.Data.Part;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Data.Data
{
	public class Service : AbstractData
	{
		public ObjectId MemberId { get; set; }

		[BsonIgnore]
		public string MemberIdString => MemberId.ToString();

		public string Name { get; set; }

		public int? MaxServiceUses { get; set; }

		public List<ServiceUse> ServiceUses = new List<ServiceUse>();
	}
}
