using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Data.Data
{
	public class Log : AbstractData
	{
		[BsonDateTimeOptions(Kind = DateTimeKind.Local)]
		public DateTime WriteTime { get; set; }

		public string StackTrace { get; set; }

		public string Location { get; set; }

		public string Message { get; set; }

		public static void Write(MongoConnection mongoConnection, Type source, string message)
		{
			Write(mongoConnection, source, message, null);
		}

		public static void Write(MongoConnection mongoConnection, Type source, Exception exception)
		{
			Write(mongoConnection, source, exception.Message, exception.StackTrace);

			if (exception.InnerException != null)
			{
				Write(mongoConnection, source, exception.InnerException);
			}
		}

		private static void Write(MongoConnection mongoConnection, Type source, string message, string stackTrace = null)
		{
			Log log = new Log()
			{
				Location = source.Name,
				WriteTime = DateTime.Now,
				Message = message,
				StackTrace = stackTrace,
			};

			log.Insert<Log>(mongoConnection);
		}
	}
}
