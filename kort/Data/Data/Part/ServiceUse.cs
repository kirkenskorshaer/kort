using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Data.Data.Part
{
	public class ServiceUse
	{
		[BsonDateTimeOptions(Kind = DateTimeKind.Local)]
		public DateTime UsedTime { get; set; }
	}
}
