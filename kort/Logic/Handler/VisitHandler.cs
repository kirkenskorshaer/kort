using System.Collections.Generic;
using Data;
using DataVisit = Data.Data.Visit;
using Data.Data;

namespace Logic.Handler
{
	public class VisitHandler
	{
		private static readonly VisitHandler _visitHandler = new VisitHandler();
		private MongoConnection _mongoConnection;

		private VisitHandler()
		{
			_mongoConnection = MongoConnection.GetConnection("card");
		}

		public static VisitHandler Instance
		{
			get
			{
				return _visitHandler;
			}
		}

		public List<DataVisit> GetVisitsOnMember(string memberId)
		{
			List<DataVisit> visits = AbstractMemberConnectedData.ReadByMemberId<DataVisit>(_mongoConnection, memberId);
			return visits;
		}

		public string InsertVisit(DataVisit dataVisit)
		{
			return dataVisit.Insert<DataVisit>(_mongoConnection);
		}
	}
}
