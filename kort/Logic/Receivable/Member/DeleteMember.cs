using System.Collections.Generic;
using Logic.Security;

namespace Logic.Receivable.Member
{
	public class DeleteMember : IReceivable<Data.Data.Member, object>
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
						PrivilegeLevel = Privilege.PrivilegeLevelEnum.Delete,
					},
				};
			}
		}

		public object Execute(Pipeline<Data.Data.Member, object> pipeline, Data.Data.Member member)
		{
			member.Delete(pipeline.MongoConnection);

			return null;
		}
	}
}
