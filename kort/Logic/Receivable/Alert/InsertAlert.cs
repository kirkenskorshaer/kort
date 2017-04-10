using System.Collections.Generic;
using Data.Data.Alert;
using Logic.Security;
using System;

namespace Logic.Receivable.Alert
{
	public class InsertAlert : IReceivable<AbstractAlert, string>
	{
		public List<Privilege> RequiredPrivileges
		{
			get
			{
				return new List<Privilege>()
				{
					new Privilege()
					{
						Collection = Privilege.CollectionEnum.VisitTimeAlert,
						PrivilegeLevel = Privilege.PrivilegeLevelEnum.Insert,
					},
					new Privilege()
					{
						Collection = Privilege.CollectionEnum.Member,
						PrivilegeLevel = Privilege.PrivilegeLevelEnum.Read,
					},
				};
			}
		}

		public string Execute(Pipeline<AbstractAlert, string> pipeline, AbstractAlert alert)
		{
			if (alert is VisitTimeAlert visitTimeAlert)
			{
				if (visitTimeAlert.TimeFromLatestVisitBeforeAlert < TimeSpan.FromMilliseconds(1))
				{
					throw new ArgumentException($"must be at least 1 ms was {visitTimeAlert.TimeFromLatestVisitBeforeAlert}", "TimeFromLatestVisitBeforeAlert");
				}
			}

			return alert.Insert<VisitTimeAlert>(pipeline.MongoConnection);
		}

		/*
		public string InsertAlert(Pipeline pipeline, DataVisitTimeAlert dataAlert)
		{
			List<Privilege> requiredPrivileges = new List<Privilege>()
			{
				new Privilege()
				{
					Collection = Privilege.CollectionEnum.VisitTimeAlert,
					PrivilegeLevel = Privilege.PrivilegeLevelEnum.Insert,
				},
				new Privilege()
				{
					Collection = Privilege.CollectionEnum.Member,
					PrivilegeLevel = Privilege.PrivilegeLevelEnum.Read,
				},
			};

			return TryFunc(pipeline, requiredPrivileges, () => InsertAlert(dataAlert));
		}

		private string InsertAlert(DataVisitTimeAlert dataAlert)
		{
			return dataAlert.Insert<DataVisitTimeAlert>(_mongoConnection);
		}
		*/
	}
}
