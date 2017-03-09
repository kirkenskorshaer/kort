using System.Collections.Generic;
using Data;
using DataService = Data.Data.Service;
using Data.Data;

namespace Logic.Handler
{
	public class ServiceHandler
	{
		private static readonly ServiceHandler _serviceHandler = new ServiceHandler();
		private MongoConnection _mongoConnection;

		private ServiceHandler()
		{
			_mongoConnection = MongoConnection.GetConnection("card");
		}

		public static ServiceHandler Instance
		{
			get
			{
				return _serviceHandler;
			}
		}

		public List<DataService> GetServicesOnMember(string memberId)
		{
			List<DataService> services = AbstractMemberConnectedData.ReadByMemberId<DataService>(_mongoConnection, memberId);

			return services;
		}

		public string InsertService(DataService dataService)
		{
			return dataService.Insert<DataService>(_mongoConnection);
		}

		public void UpdateService(DataService service)
		{
			service.Update<DataService>(_mongoConnection);
		}

		public void DeleteService(DataService service)
		{
			service.Delete(_mongoConnection);
		}
	}
}
