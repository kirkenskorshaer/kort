using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

namespace Data.Data.Alert
{
	public static class AlertCollection
	{
		public static List<Member> ReadAlertCollections(MongoConnection mongoConnection)
		{
			List<Member> members = new List<Member>();

			List<Type> alertTypes = ReflectionHelper.GetChildTypes(typeof(AbstractAlert));

			IEnumerable<AbstractAlert> alerts = ReadAllAlerts(mongoConnection, alertTypes, null);

			foreach (AbstractAlert alert in alerts)
			{
				Member currentMember = alert.GetMemberIfRaised(mongoConnection);

				if (currentMember != null)
				{
					members.Add(currentMember);
				}
			}

			return members;
		}

		public static List<AbstractAlert> ReadAllAlerts(MongoConnection mongoConnection, string memberId)
		{
			List<Type> alertTypes = ReflectionHelper.GetChildTypes(typeof(AbstractAlert));

			Member member = AbstractData.Read<Member>(mongoConnection, memberId);

			return ReadAllAlerts(mongoConnection, alertTypes, member).ToList();
		}

		private static IEnumerable<AbstractAlert> ReadAllAlerts(MongoConnection mongoConnection, List<Type> alertTypes, Member member)
		{
			IEnumerable<AbstractAlert> alerts = new List<AbstractAlert>();

			FilterDefinition<BsonDocument> filter;
			if (member == null)
			{
				filter = Builders<BsonDocument>.Filter.Empty;
			}
			else
			{
				filter = Builders<BsonDocument>.Filter.Eq(bson => bson["MemberId"], member._id);
			}

			foreach (Type alertType in alertTypes)
			{
				IMongoCollection<BsonDocument> alertcollection = mongoConnection.Database.GetCollection<BsonDocument>(alertType.Name);

				IFindFluent<BsonDocument, BsonDocument> findFluent = alertcollection.Find(filter);

				alerts = alerts.Concat(findFluent.ToEnumerable().Select(bson => (AbstractAlert)BsonSerializer.Deserialize(bson, alertType)));
			}

			return alerts;
		}
	}
}
