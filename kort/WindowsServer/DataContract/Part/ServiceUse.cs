using System;
using System.Runtime.Serialization;

namespace WindowsServer.DataContract.Part
{
	[DataContract]
	public class ServiceUse
	{
		[DataMember]
		public DateTime UsedTime { get; set; }

		internal static ServiceUse FromData(Data.Data.Part.ServiceUse serviceUse)
		{
			return new ServiceUse()
			{
				UsedTime = serviceUse.UsedTime,
			};
		}

		internal Data.Data.Part.ServiceUse ToData()
		{
			return new Data.Data.Part.ServiceUse()
			{
				UsedTime = UsedTime,
			};
		}
	}
}
