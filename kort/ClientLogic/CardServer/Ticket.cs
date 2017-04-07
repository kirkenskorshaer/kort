using System;

namespace ClientLogic.CardServer
{
	public partial class Ticket
	{
		public int UseCount = 0;

		public bool AssumedValid()
		{
			DateTime currentTime = DateTime.Now;

			return ValidFrom <= currentTime && ValidTo > currentTime;
		}
	}
}
