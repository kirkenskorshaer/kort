using System;
using System.Security.Cryptography;

namespace Utilities
{
	public static class Ticket
	{
		private const string dateFromat = "yyyy-MM-dd HH:mm:ss";

		public static string CreateTicket(string UserName, string Password, string ip, int useCount, DateTime ValidFrom, DateTime ValidTo)
		{
			string unhashedString = $"{UserName}{Password}{ip}{useCount}{ValidFrom.ToString(dateFromat)}{ValidTo.ToString(dateFromat)}";

			string hashedString = HashToMd5(unhashedString);

			return hashedString;
		}

		public static string HashToMd5(string stringToHashMd5From)
		{
			byte[] unhashedBytes = System.Text.Encoding.UTF8.GetBytes(stringToHashMd5From);
			byte[] hashedBytes = MD5.Create().ComputeHash(unhashedBytes);
			//return BitConverter.ToString(hashedBytes);
			return Convert.ToBase64String(hashedBytes);
		}
	}
}
