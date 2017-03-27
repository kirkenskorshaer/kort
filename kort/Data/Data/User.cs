using Data.Data.Part;
using System.Collections.Generic;

namespace Data.Data
{
	public class User : AbstractData
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		public List<Privilege> Privileges { get; set; }
		public Ticket Ticket { get; set; }
	}
}
