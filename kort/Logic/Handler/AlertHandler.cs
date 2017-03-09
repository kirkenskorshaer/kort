using System.Collections.Generic;
using Data;
using DataAbstractAlert = Data.Data.Alert.AbstractAlert;
using DataVisitTimeAlert = Data.Data.Alert.VisitTimeAlert;
using DataAlertCollection = Data.Data.Alert.AlertCollection;
using Data.Data.Alert;
using System;

namespace Logic.Handler
{
	public class AlertHandler
	{
		private static readonly AlertHandler _alertHandler = new AlertHandler();
		private MongoConnection _mongoConnection;

		private AlertHandler()
		{
			_mongoConnection = MongoConnection.GetConnection("card");
		}

		public static AlertHandler Instance
		{
			get
			{
				return _alertHandler;
			}
		}

		public List<DataAbstractAlert> GetAlertsOnMember(string memberid)
		{
			List<DataAbstractAlert> alerts = DataAlertCollection.ReadAllAlerts(_mongoConnection, memberid);

			return alerts;
		}

		public string InsertAlert(DataAbstractAlert dataAlert)
		{
			return dataAlert.Insert<DataVisitTimeAlert>(_mongoConnection);
		}

		public string InsertAlert(DataVisitTimeAlert dataAlert)
		{
			return dataAlert.Insert<DataVisitTimeAlert>(_mongoConnection);
		}

		public void Delete(DataAbstractAlert dataAlert)
		{
			dataAlert.Delete(_mongoConnection);
		}
	}
}
