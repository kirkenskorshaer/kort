namespace ClientLogic.CardServer
{
	public partial class Member
	{
		public override string ToString()
		{
			return $"{NickName} - ({CardId})";
		}
	}
}
