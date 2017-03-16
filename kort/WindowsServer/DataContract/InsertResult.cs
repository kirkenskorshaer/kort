using System.Runtime.Serialization;

namespace WindowsServer.DataContract
{
	[DataContract]
	public class InsertResult
	{
		[DataMember]
		public string Id { get; set; }
	}
}
