using NUnit.Framework;
using System;
using Data;

namespace LogicTest
{
	[TestFixture]
	public class TestBase
	{
		protected MongoConnection _mongoConnection;
		protected Random _random = new Random();

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_mongoConnection = MongoConnection.GetConnection("card");
		}

		[SetUp]
		public void SetUp()
		{
			_mongoConnection.CleanDatabase();
		}
	}
}