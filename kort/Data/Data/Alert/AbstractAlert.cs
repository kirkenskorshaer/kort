namespace Data.Data.Alert
{
	public abstract class AbstractAlert : AbstractMemberConnectedData
	{
		public AbstractAlert(Member member)
		{
			MemberId = member._id;
		}

		public abstract Member GetMemberIfRaised(MongoConnection mongoConnection);
	}
}