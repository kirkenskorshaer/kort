using System.Collections.Generic;
using WindowsServer.DataContract;
using WindowsServer.Logic;

namespace WindowsServer
{
	public class MembershipCard : IMembershipCard
	{
		public List<Member> GetMembers(string id)
		{
			return MemberHandler.Instance.GetMembers();
		}
	}
}
