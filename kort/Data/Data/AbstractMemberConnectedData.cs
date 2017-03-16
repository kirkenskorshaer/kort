using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Data.Data
{
	public abstract class AbstractMemberConnectedData : AbstractData
	{
		public ObjectId MemberId { get; set; }

		[BsonIgnore]
		public string MemberIdString
		{
			get
			{
				return MemberId.ToString();
			}
			set
			{
				if (MemberId == ObjectId.Empty)
				{
					if (value == null)
					{
						MemberId = ObjectId.Empty;
					}
					else
					{
						MemberId = ObjectId.Parse(value);
					}
				}
				else
				{
					throw new Exception($"trying to set MemberId with value {MemberId} to {value}");
				}
			}
		}

		public static List<AbstractMemberConnectedDataType> ReadByMemberId<AbstractMemberConnectedDataType>(MongoConnection mongoConnection, string memberId) where AbstractMemberConnectedDataType : AbstractMemberConnectedData
		{
			return Read<AbstractMemberConnectedDataType>(mongoConnection, type => type.MemberId == ObjectId.Parse(memberId));
		}
	}
}
