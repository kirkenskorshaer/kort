using System;
using System.Runtime.Serialization;

namespace WindowsServer.DataContract
{
	[DataContract]
	public class Ticket
	{
		[DataMember]
		public string UserName { get; set; }

		[DataMember]
		public DateTime ValidFrom { get; set; }

		[DataMember]
		public DateTime ValidTo { get; set; }
	}
}
