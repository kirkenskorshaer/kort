namespace Data.Data.Part
{
	public class Privilege
	{
		public string Collection { get; set; }
		public PrivilegeLevelEnum PrivilegeLevel { get; set; }

		public enum PrivilegeLevelEnum
		{
			Read = 1,
			Update = 2,
			Insert = 4,
			Delete = 8,
		}
	}
}
