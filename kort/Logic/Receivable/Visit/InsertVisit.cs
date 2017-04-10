using System.Collections.Generic;
using Logic.Security;

namespace Logic.Receivable.Visit
{
	public class InsertVisit : IReceivable<Data.Data.Visit, string>
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
						PrivilegeLevel = Privilege.PrivilegeLevelEnum.Insert,
					}
				};
			}
		}

		public string Execute(Pipeline<Data.Data.Visit, string> pipeline, Data.Data.Visit visit)
		{
			return visit.Insert<Data.Data.Visit>(pipeline.MongoConnection);
		}
	}
}
