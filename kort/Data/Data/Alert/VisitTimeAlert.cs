using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Data.Alert
{
	public class VisitTimeAlert : AbstractAlert
	{
		public TimeSpan TimeFromLatestVisitBeforeAlert { get; set; }

		public VisitTimeAlert(Member member) : base(member) { }

		public override Member GetMemberIfRaised(MongoConnection mongoConnection)
		{
			DateTime visitsAfterThisDatePreventsAlert = DateTime.Now - TimeFromLatestVisitBeforeAlert;

			List<Visit> visitsPreventingAlert = Read<Visit>(mongoConnection, visit =>
				visit.MemberId == MemberId &&
				visit.VisitTime > visitsAfterThisDatePreventsAlert);

			if (visitsPreventingAlert.Any())
			{
				return null;
			}

			return Read<Member>(mongoConnection, MemberId);
		}
	}
}
