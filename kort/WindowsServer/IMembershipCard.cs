using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using WindowsServer.DataContract;
using WindowsServer.DataContract.Alert;

namespace WindowsServer
{
	[ServiceContract]
	public interface IMembershipCard
	{
		[OperationContract]
		[WebInvoke(Method = "GET", UriTemplate = "Members/CardId/{cardId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		List<Member> GetMembersByCardId(string cardId);

		[OperationContract]
		[WebInvoke(Method = "GET", UriTemplate = "Members/NickName/{nickName}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		List<Member> GetMembersByNickName(string nickName);

		[OperationContract]
		[WebInvoke(Method = "GET", UriTemplate = "Members/ToAlert", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		List<Member> GetMembersToAlert();

		[OperationContract]
		[WebInvoke(Method = "POST", UriTemplate = "Member", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		InsertResult InsertMember(Member member);

		[OperationContract]
		[WebInvoke(Method = "PUT", UriTemplate = "Member", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		void UpdateMember(Member member);

		[OperationContract]
		[WebInvoke(Method = "DELETE", UriTemplate = "Member", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		void DeleteMember(Member member);


		[OperationContract]
		[WebInvoke(Method = "GET", UriTemplate = "Alerts/VisitTimeAlert/{memberid}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		List<VisitTimeAlert> GetAlertsOnMember(string memberid);

		[OperationContract]
		[WebInvoke(Method = "POST", UriTemplate = "Alert/VisitTimeAlert", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		InsertResult InsertAlert(VisitTimeAlert alert);

		[OperationContract]
		[WebInvoke(Method = "DELETE", UriTemplate = "Alert/VisitTimeAlert", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		void DeleteAlert(VisitTimeAlert alert);


		[OperationContract]
		[WebInvoke(Method = "GET", UriTemplate = "Services/{memberid}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		List<Service> GetServicesOnMember(string memberid);

		[OperationContract]
		[WebInvoke(Method = "POST", UriTemplate = "Service", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		InsertResult InsertService(Service service);

		[OperationContract]
		[WebInvoke(Method = "PUT", UriTemplate = "Service", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		void UpdateService(Service service);

		[OperationContract]
		[WebInvoke(Method = "DELETE", UriTemplate = "Service", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		void DeleteService(Service service);


		[OperationContract]
		[WebInvoke(Method = "GET", UriTemplate = "Visits/{memberid}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		List<Visit> GetVisitsOnMember(string memberid);

		[OperationContract]
		[WebInvoke(Method = "POST", UriTemplate = "Visit", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		InsertResult InsertVisit(Visit visit);
	}
}
