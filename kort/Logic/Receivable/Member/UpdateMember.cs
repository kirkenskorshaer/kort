using System.Collections.Generic;
using Logic.Security;

namespace Logic.Receivable.Member
{
	public class UpdateMember : IReceivable<Data.Data.Member, object>
	{
		public List<Privilege> RequiredPrivileges
		{
			get
			{
				return new List<Privilege>()
				{
					new Privilege()
					{
						Collection = Privilege.CollectionEnum.Member,
						PrivilegeLevel = Privilege.PrivilegeLevelEnum.Update,
					},
				};
			}
		}

		public object Execute(Pipeline<Data.Data.Member, object> pipeline, Data.Data.Member member)
		{
			member.Update<Data.Data.Member>(pipeline.MongoConnection);

			return null;
		}
	}
}
