using System.Runtime.Serialization;

namespace WindowsServer.DataContract
{
	[DataContract]
	public class Member
	{
		[DataMember]
		public string Id { get; set; }

		[DataMember]
		public string Nickname { get; set; }
	}
}
