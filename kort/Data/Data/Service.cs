using Data.Data.Part;
using System.Collections.Generic;

namespace Data.Data
{
	public class Service : AbstractMemberConnectedData
	{
		public string Name { get; set; }

		public int? MaxServiceUses { get; set; }

		public List<ServiceUse> ServiceUses = new List<ServiceUse>();
	}
}
