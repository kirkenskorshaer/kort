using System.Collections.Generic;
using Logic.Security;

namespace Logic.Receivable.Service
{
	public class UpdateService : IReceivable<Data.Data.Service, object>
	{
		public List<Privilege> RequiredPrivileges
		{
			get
			{
				return new List<Privilege>()
				{
					new Privilege()
					{
						Collection = Privilege.CollectionEnum.Service,
						PrivilegeLevel = Privilege.PrivilegeLevelEnum.Update,
					},
					new Privilege()
					{
						Collection = Privilege.CollectionEnum.Member,
						PrivilegeLevel = Privilege.PrivilegeLevelEnum.Read,
					},
				};
			}
		}

		public object Execute(Pipeline<Data.Data.Service, object> pipeline, Data.Data.Service service)
		{
			service.Update<Data.Data.Service>(pipeline.MongoConnection);

			return null;
		}
	}
}
