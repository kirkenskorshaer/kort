using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace Data
{
	public class MongoConnection
	{
		private readonly IMongoClient _client;
		internal readonly IMongoDatabase Database;
		private readonly string _databaseName;

		private static readonly Dictionary<string, MongoConnection> Connections = new Dictionary<string, MongoConnection>();
		public static int TimeoutMilliSeconds = 10000;

		private MongoConnection(string databaseName)
		{
			string connectionString = ConfigurationManager.ConnectionStrings[databaseName].ConnectionString;
			_client = new MongoClient(connectionString);
			_databaseName = databaseName;
			Database = _client.GetDatabase(databaseName);
		}

		public static MongoConnection GetConnection(string databaseName)
		{
			if (Connections.ContainsKey(databaseName))
			{
				return Connections[databaseName];
			}

			MongoConnection connection = new MongoConnection(databaseName);

			Connections.Add(databaseName, connection);

			return connection;
		}

		public void CleanDatabase()
		{
			List<string> CollectionsToPreserve = new List<string>
			{
				"system.indexes",
			};

			ListCollectionsOptions options = new ListCollectionsOptions()
			{
				Filter = Builders<BsonDocument>.Filter.Nin("name", CollectionsToPreserve),
			};

			Task<IAsyncCursor<BsonDocument>> listTask = Database.ListCollectionsAsync(options);

			IAsyncCursor<BsonDocument> listCursor = listTask.Result;

			Action<BsonDocument> clearAction = (document) =>
			{
				string name = document.GetValue("name").AsString;

				IMongoCollection<BsonDocument> collection = Database.GetCollection<BsonDocument>(name);
				Task<DeleteResult> deleteTask = collection.DeleteManyAsync(lDocument => true);
				deleteTask.Wait();
			};

			Task clearTask = listCursor.ForEachAsync(clearAction);

			clearTask.Wait();
		}
	}
}
