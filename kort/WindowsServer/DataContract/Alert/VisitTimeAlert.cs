using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServer.DataContract.Alert
{
	[DataContract]
	public class VisitTimeAlert
	{
		[DataMember]
		public string Id { get; set; }

		[DataMember]
		public Member Member { get; set; }

		[DataMember]
		public TimeSpan TimeFromLatestVisitBeforeAlert { get; set; }

		internal Data.Data.Alert.VisitTimeAlert ToData()
		{
			return new Data.Data.Alert.VisitTimeAlert(Member.ToData())
			{
				Id = Id,
				TimeFromLatestVisitBeforeAlert = ((VisitTimeAlert)this).TimeFromLatestVisitBeforeAlert,
			};
		}

		internal static VisitTimeAlert FromData(Data.Data.Alert.AbstractAlert alert)
		{
			return new VisitTimeAlert()
			{
				Id = alert.Id,
				TimeFromLatestVisitBeforeAlert = ((Data.Data.Alert.VisitTimeAlert)alert).TimeFromLatestVisitBeforeAlert,
			};
		}
	}
}
