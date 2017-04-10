using System.Collections.Generic;
using Logic.Security;

namespace Logic.Receivable.Service
{
	public class InsertService : IReceivable<Data.Data.Service, string>
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

		public string Execute(Pipeline<Data.Data.Service, string> pipeline, Data.Data.Service service)
		{
			return service.Insert<Data.Data.Service>(pipeline.MongoConnection);
		}
	}
}
