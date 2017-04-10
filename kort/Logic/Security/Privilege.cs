namespace Logic.Security
{
	public class Privilege
	{
		public CollectionEnum Collection { get; set; }
		public PrivilegeLevelEnum PrivilegeLevel { get; set; }

		public enum PrivilegeLevelEnum
		{
			Read = 1,
			Update = 2,
			Insert = 4,
			Delete = 8,
		}

		public enum CollectionEnum
		{
			VisitTimeAlert = 1,
			Member = 2,
			Service = 4,
			User = 8,
			Visit = 16,
		}

		public Data.Data.Part.Privilege AsDataPriviledge
		{
			get
			{
				return new Data.Data.Part.Privilege()
				{
					Collection = Collection.ToString(),
					PrivilegeLevel = DataPrivilegeLevel,
				};
			}
		}

		public Data.Data.Part.Privilege.PrivilegeLevelEnum DataPrivilegeLevel
		{
			get
			{
				return (Data.Data.Part.Privilege.PrivilegeLevelEnum)PrivilegeLevel;
			}
		}

		public override string ToString()
		{
			return $"{PrivilegeLevel} on {Collection.ToString()}";
		}
	}
}
