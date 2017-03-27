using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Data.Data.Part
{
	public class Ticket
	{
		[BsonDateTimeOptions(Kind = DateTimeKind.Local)]
		public DateTime ValidFrom { get; set; }

		[BsonDateTimeOptions(Kind = DateTimeKind.Local)]
		public DateTime ValidTo { get; set; }

		public int UseCount { get; set; }

		public string Ip { get; set; }
	}
}
