using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using WindowsServer.DataContract;

namespace WindowsServer
{
	[ServiceContract]
	public interface IMembershipCard
	{
		[OperationContract]
		[WebInvoke(Method = "Get", UriTemplate = "GetMembers/", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		List<Member> GetMembers(string id);
	}
}
