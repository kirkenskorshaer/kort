using System.Collections.Generic;
using Data.Data;
using Logic.Security;

namespace Logic.Receivable.Visit
{
	public class GetVisitsOnMember : IReceivable<string, List<Data.Data.Visit>>
	{
		public List<Privilege> RequiredPrivileges
		{
			get
			{
				return new List<Privilege>()
				{
					new Privilege()
					{
						Collection = Privilege.CollectionEnum.Visit,
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

		public List<Data.Data.Visit> Execute(Pipeline<string, List<Data.Data.Visit>> pipeline, string memberId)
		{
			return AbstractMemberConnectedData.ReadByMemberId<Data.Data.Visit>(pipeline.MongoConnection, memberId);
		}
	}
}
