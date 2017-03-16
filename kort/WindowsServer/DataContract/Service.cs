using System.Collections.Generic;
using System.Runtime.Serialization;
using WindowsServer.DataContract.Part;
using System.Linq;

namespace WindowsServer.DataContract
{
	[DataContract]
	public class Service
	{
		[DataMember]
		public string Id { get; set; }

		[DataMember]
		public string MemberId { get; set; }

		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public int? MaxServiceUses { get; set; }

		[DataMember]
		public List<ServiceUse> ServiceUses { get; set; }

		internal static Service FromData(Data.Data.Service dataService, string memberId)
		{
			return new Service()
			{
				Id = dataService.Id,
				MemberId = memberId,
				MaxServiceUses = dataService.MaxServiceUses,
				Name = dataService.Name,
				ServiceUses = dataService.ServiceUses.Select(serviceUse => ServiceUse.FromData(serviceUse)).ToList(),
			};
		}

		internal Data.Data.Service ToData()
		{
			return new Data.Data.Service()
			{
				Id = Id,
				MemberIdString = MemberId,
				MaxServiceUses = MaxServiceUses,
				Name = Name,
				ServiceUses = ServiceUses.Select(serviceUse => serviceUse.ToData()).ToList(),
			};
		}
	}
}
