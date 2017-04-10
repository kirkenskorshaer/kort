using System.Collections.Generic;
using Logic.Security;
using Data;

namespace Logic.Receivable.Member
{
	public class GetMembersByCardId : IReceivable<string, List<Data.Data.Member>>
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

		public List<Data.Data.Member> Execute(Pipeline<string, List<Data.Data.Member>> pipeline, string cardId)
		{
			return AbstractData.Read<Data.Data.Member>(pipeline.MongoConnection, member => member.CardId == cardId);
		}
	}
}
