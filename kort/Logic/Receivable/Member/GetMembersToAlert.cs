using System.Collections.Generic;
using Logic.Security;

namespace Logic.Receivable.Member
{
	public class GetMembersToAlert : IReceivable<object, List<Data.Data.Member>>
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

		public List<Data.Data.Member> Execute(Pipeline<object, List<Data.Data.Member>> pipeline, object input)
		{
			return Data.Data.Alert.AlertCollection.ReadAlertCollections(pipeline.MongoConnection);
		}
	}
}
