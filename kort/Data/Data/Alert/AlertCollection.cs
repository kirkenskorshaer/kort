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

			IEnumerable<AbstractAlert> alerts = ReadAllAlerts(mongoConnection, alertTypes);

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

		private static IEnumerable<AbstractAlert> ReadAllAlerts(MongoConnection mongoConnection, List<Type> alertTypes)
		{
			IEnumerable<AbstractAlert> alerts = new List<AbstractAlert>();

			foreach (Type alertType in alertTypes)
			{
				IMongoCollection<BsonDocument> alertcollection = mongoConnection.Database.GetCollection<BsonDocument>(alertType.Name);

				IFindFluent<BsonDocument, BsonDocument> findFluent = alertcollection.Find(alert => true);

				alerts = alerts.Concat(findFluent.ToEnumerable().Select(bson => (AbstractAlert)BsonSerializer.Deserialize(bson, alertType)));
			}

			return alerts;
		}
	}
}
