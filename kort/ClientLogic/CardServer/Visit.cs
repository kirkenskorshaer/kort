namespace ClientLogic.CardServer
{
	public partial class Visit
	{
		public override string ToString()
		{
			return $"{VisitTime.ToString("yyyy-MM-dd HH:mm")}";
		}
	}
}
