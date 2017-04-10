using System.Collections.Generic;
using Data.Data.Alert;
using Logic.Security;

namespace Logic.Receivable.Alert
{
	public class GetAlertsOnMember : IReceivable<string, List<AbstractAlert>>
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
						PrivilegeLevel = Privilege.PrivilegeLevelEnum.Read,
					},
					new Privilege()
					{
						Collection = Privilege.CollectionEnum.Member,
						PrivilegeLevel = Privilege.PrivilegeLevelEnum.Read,
					},
				};
			}
		}

		public List<AbstractAlert> Execute(Pipeline<string, List<AbstractAlert>> pipeline, string memberid)
		{
			return AlertCollection.ReadAllAlerts(pipeline.MongoConnection, memberid);
		}
	}
}
