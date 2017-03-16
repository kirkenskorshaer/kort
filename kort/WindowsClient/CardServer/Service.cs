namespace WindowsClient.CardServer
{
	public partial class Service
	{
		public override string ToString()
		{
			string maxServiceUsesString = "-";
			if (MaxServiceUses.HasValue)
			{
				maxServiceUsesString = MaxServiceUses.Value.ToString();
			}

			int currentlyUsed = ServiceUses.Count;

			return $"{Name} - {currentlyUsed} / {maxServiceUsesString}";
		}
	}
}
