using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Data
{
	[BsonIgnoreExtraElements]
	public abstract class AbstractData
	{
		[BsonId]
		internal ObjectId _id { get; set; }

		[BsonIgnore]
		public string Id
		{
			get
			{
				return _id.ToString();
			}
			set
			{
				if (_id == ObjectId.Empty)
				{
					if (value == null)
					{
						_id = ObjectId.Empty;
					}
					else
					{
						_id = ObjectId.Parse(value);
					}
				}
				else
				{
					throw new Exception($"trying to set id with value {_id} to {value}");
				}
			}
		}

		public string Insert<TDataType>(MongoConnection connection)
		where TDataType : AbstractData
		{
			IMongoCollection<TDataType> datas = connection.Database.GetCollection<TDataType>(GetType().Name);
			Task insertTask = datas.InsertOneAsync((TDataType)this);

			ExecuteTask(insertTask);

			return Id;
		}

		public static void Insert<TDataType>(MongoConnection connection, params TDataType[] dataToInsert)
		where TDataType : AbstractData
		{
			IMongoCollection<TDataType> datas = connection.Database.GetCollection<TDataType>(typeof(TDataType).Name);
			Task insertTask = datas.InsertManyAsync(dataToInsert);

			ExecuteTask(insertTask);
		}

		public static TDataType Read<TDataType>(MongoConnection connection, string id)
		where TDataType : AbstractData
		{
			return Read<TDataType>(connection, ObjectId.Parse(id));
		}

		internal static TDataType Read<TDataType>(MongoConnection connection, ObjectId id)
		where TDataType : AbstractData
		{
			Expression<Func<TDataType, bool>> filter = data => data._id == id;

			IMongoCollection<TDataType> datas = connection.Database.GetCollection<TDataType>(typeof(TDataType).Name);
			IFindFluent<TDataType, TDataType> find = datas.Find(filter);
			find.Limit(1);

			Task<TDataType> dataTask = find.SingleOrDefaultAsync();

			return GetValue(dataTask);
		}

		public static List<TDataType> Read<TDataType>(MongoConnection connection, Expression<Func<TDataType, bool>> criteria)
		where TDataType : AbstractData
		{
			IMongoCollection<TDataType> datas = connection.Database.GetCollection<TDataType>(typeof(TDataType).Name);

			IFindFluent<TDataType, TDataType> find = datas.Find(criteria);

			return find.ToList();
		}

		public void Update<TDataType>(MongoConnection connection)
		where TDataType : AbstractData
		{
			IMongoCollection<TDataType> datas = connection.Database.GetCollection<TDataType>(GetType().Name);

			FilterDefinition<TDataType> filter = Builders<TDataType>.Filter.Eq(data => data._id, _id);

			Task<ReplaceOneResult> result = datas.ReplaceOneAsync(filter, (TDataType)this);

			ExecuteTask(result);
		}

		public static void Update<TDataType, TField>(MongoConnection connection, Expression<Func<TDataType, bool>> criteria, Expression<Func<TDataType, TField>> setDefinition, TField value)
		where TDataType : AbstractData
		{
			IMongoCollection<TDataType> datas = connection.Database.GetCollection<TDataType>(typeof(TDataType).Name);

			UpdateDefinition<TDataType> updateDefinition = Builders<TDataType>.Update.Set(setDefinition, value);

			Task<UpdateResult> result = datas.UpdateManyAsync(criteria, updateDefinition);

			ExecuteTask(result);
		}

		public void Delete(MongoConnection connection)
		{
			IMongoCollection<AbstractData> datas = connection.Database.GetCollection<AbstractData>(GetType().Name);
			Task deleteTask = datas.DeleteOneAsync(data => data._id == _id);
			deleteTask.Wait();
		}

		protected static SearchedObjectType GetValue<SearchedObjectType>(Task<SearchedObjectType> task)
		{
			int timeoutMilliSeconds = MongoConnection.TimeoutMilliSeconds;

			task.Wait(timeoutMilliSeconds);

			if (task.IsCompleted == false)
			{
				throw new TimeoutException($"timeout after waiting {timeoutMilliSeconds} MilliSeconds to fetch {typeof(SearchedObjectType).Name}");
			}

			return task.Result;
		}

		protected static void ExecuteTask(Task task)
		{
			int timeoutMilliSeconds = MongoConnection.TimeoutMilliSeconds;

			task.Wait(timeoutMilliSeconds);

			if (task.IsCompleted == false)
			{
				throw new TimeoutException($"timeout after waiting {timeoutMilliSeconds} MilliSeconds");
			}
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			if (obj.GetType() != GetType())
			{
				return false;
			}

			Type compareType = obj.GetType();

			List<MemberInfo> memberInfos = compareType.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).ToList();

			memberInfos = memberInfos.Where(memberInfo => memberInfo is PropertyInfo || memberInfo is FieldInfo).ToList();

			foreach (MemberInfo memberInfo in memberInfos)
			{
				object value;
				object valueObj;

				if (memberInfo is PropertyInfo)
				{
					value = ((PropertyInfo)memberInfo).GetValue(this);
					valueObj = ((PropertyInfo)memberInfo).GetValue(obj);
				}
				else
				{
					value = ((FieldInfo)memberInfo).GetValue(this);
					valueObj = ((FieldInfo)memberInfo).GetValue(obj);
				}

				if (value.Equals(valueObj) == false)
				{
					return false;
				}
			}

			return true;
		}
	}
}
