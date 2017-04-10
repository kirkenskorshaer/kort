using System.Collections.Generic;
using Logic.Security;

namespace Logic.Receivable.Member
{
	public class InsertMember : IReceivable<Data.Data.Member, string>
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
						PrivilegeLevel = Privilege.PrivilegeLevelEnum.Insert,
					},
				};
			}
		}

		public string Execute(Pipeline<Data.Data.Member, string> pipeline, Data.Data.Member member)
		{
			return member.Insert<Data.Data.Member>(pipeline.MongoConnection);
		}
	}
}
