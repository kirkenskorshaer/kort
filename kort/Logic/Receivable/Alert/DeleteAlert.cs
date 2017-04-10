using System.Collections.Generic;
using Data.Data.Alert;
using Logic.Security;

namespace Logic.Receivable.Alert
{
	public class DeleteAlert : IReceivable<AbstractAlert, object>
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
						PrivilegeLevel = Privilege.PrivilegeLevelEnum.Delete,
					},
				};
			}
		}

		public object Execute(Pipeline<AbstractAlert, object> pipeline, AbstractAlert alert)
		{
			alert.Delete(pipeline.MongoConnection);

			return null;
		}
	}
}
