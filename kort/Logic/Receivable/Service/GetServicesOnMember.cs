using System.Collections.Generic;
using Data.Data;
using Logic.Security;

namespace Logic.Receivable.Service
{
	public class GetServicesOnMember : IReceivable<string, List<Data.Data.Service>>
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

		public List<Data.Data.Service> Execute(Pipeline<string, List<Data.Data.Service>> pipeline, string memberId)
		{
			return AbstractMemberConnectedData.ReadByMemberId<Data.Data.Service>(pipeline.MongoConnection, memberId);
		}
	}
}
