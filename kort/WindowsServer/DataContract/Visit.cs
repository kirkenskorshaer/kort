using System;
using System.Runtime.Serialization;

namespace WindowsServer.DataContract
{
	[DataContract]
	public class Visit
	{
		[DataMember]
		public string Id { get; set; }

		[DataMember]
		public string MemberId { get; set; }

		[DataMember]
		public DateTime VisitTime { get; set; }

		internal static Visit FromData(Data.Data.Visit dataVisit)
		{
			return new Visit()
			{
				Id = dataVisit.Id,
				VisitTime = dataVisit.VisitTime,
			};
		}

		internal Data.Data.Visit ToData()
		{
			return new Data.Data.Visit(MemberId)
			{
				Id = Id,
				VisitTime = VisitTime,
			};
		}
	}
}
