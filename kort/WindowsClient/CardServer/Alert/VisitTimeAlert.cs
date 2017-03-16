namespace WindowsClient.CardServer
{
	public partial class VisitTimeAlert
	{
		public override string ToString()
		{
			return $"{TimeFromLatestVisitBeforeAlert.Days} D {TimeFromLatestVisitBeforeAlert.Hours} T {TimeFromLatestVisitBeforeAlert.Minutes} M {TimeFromLatestVisitBeforeAlert.Seconds} S";
		}
	}
}
