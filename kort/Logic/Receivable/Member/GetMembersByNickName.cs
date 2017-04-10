using System.Collections.Generic;
using Data;
using Logic.Security;

namespace Logic.Receivable.Member
{
	public class GetMembersByNickName : IReceivable<string, List<Data.Data.Member>>
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
						PrivilegeLevel = Privilege.PrivilegeLevelEnum.Read,
					},
				};
			}
		}

		public List<Data.Data.Member> Execute(Pipeline<string, List<Data.Data.Member>> pipeline, string nickName)
		{
			return AbstractData.Read<Data.Data.Member>(pipeline.MongoConnection, member => member.NickName == nickName);
		}
	}
}
