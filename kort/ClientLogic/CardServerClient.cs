using RestSharp;
using System.Collections.Generic;
using Ducksboard.Serializers;
using System.Configuration;
using ClientLogic.CardServer;

namespace ClientLogic
{
	public class CardServerClient
	{
		private RestClient _client;

		public CardServerClient()
		{
			string serverUrl = ConfigurationManager.AppSettings["serverUrl"];
			_client = new RestClient(serverUrl);
		}

		public List<Member> GetMembersToAlert()
		{
			RestRequest request = new RestRequest("Members/ToAlert", Method.GET);
			IRestResponse<List<Member>> response = _client.Execute<List<Member>>(request);

			return response.Data;
		}

		public List<Member> GetMembersByCardId(string cardId)
		{
			RestRequest request = new RestRequest("Members/CardId/{cardId}", Method.GET);

			request.AddUrlSegment("cardId", cardId);

			IRestResponse<List<Member>> response = _client.Execute<List<Member>>(request);

			return response.Data;
		}

		public List<Member> GetMembersByNickName(string nickName)
		{
			RestRequest request = new RestRequest("Members/NickName/{nickName}", Method.GET);

			request.AddUrlSegment("nickName", nickName);

			IRestResponse<List<Member>> response = _client.Execute<List<Member>>(request);

			return response.Data;
		}


		public void InsertMember(Member member)
		{
			RestRequest request = new RestRequest("Member", Method.POST);
			request.RequestFormat = DataFormat.Json;

			request.AddBody(member);

			IRestResponse<InsertResult> response = _client.Post<InsertResult>(request);

			member.Id = response.Data.Id;
		}

		public List<VisitTimeAlert> GetAlertsOnMember(string memberId)
		{
			RestRequest request = new RestRequest("Alerts/VisitTimeAlert/{memberid}", Method.GET);

			request.AddUrlSegment("memberid", memberId);

			IRestResponse<List<VisitTimeAlert>> response = _client.Execute<List<VisitTimeAlert>>(request);

			return response.Data;
		}

		public List<Service> GetServicesOnMember(string memberId)
		{
			RestRequest request = new RestRequest("Services/{memberid}", Method.GET);

			request.AddUrlSegment("memberid", memberId);

			IRestResponse<List<Service>> response = _client.Execute<List<Service>>(request);

			return response.Data;
		}

		public List<Visit> GetVisitsOnMember(string memberId)
		{
			RestRequest request = new RestRequest("Visits/{memberid}", Method.GET);

			request.AddUrlSegment("memberid", memberId);

			IRestResponse<List<Visit>> response = _client.Execute<List<Visit>>(request);

			return response.Data;
		}

		public void InsertService(Service service)
		{
			RestRequest request = new RestRequest("Service", Method.POST);
			request.RequestFormat = DataFormat.Json;

			request.AddBody(service);

			IRestResponse<InsertResult> response = _client.Post<InsertResult>(request);

			service.Id = response.Data.Id;
		}

		public void InsertVisit(Visit visit)
		{
			RestRequest request = new RestRequest("Visit", Method.POST);
			request.RequestFormat = DataFormat.Json;
			request.JsonSerializer = new RestSharpDataContractJsonSerializer();

			request.AddBody(visit);

			IRestResponse<InsertResult> response = _client.Post<InsertResult>(request);

			visit.Id = response.Data.Id;
		}

		public void InsertAlert(VisitTimeAlert alert)
		{
			RestRequest request = new RestRequest("Alert/VisitTimeAlert", Method.POST);
			request.RequestFormat = DataFormat.Json;
			request.JsonSerializer = new RestSharpDataContractJsonSerializer();

			request.AddBody(alert);

			IRestResponse<InsertResult> response = _client.Post<InsertResult>(request);

			alert.Id = response.Data.Id;
		}

		public void UpdateMember(Member member)
		{
			RestRequest request = new RestRequest("Member", Method.PUT);
			request.RequestFormat = DataFormat.Json;

			request.AddBody(member);

			IRestResponse response = _client.Put(request);
		}

		public void UpdateService(Service service)
		{
			RestRequest request = new RestRequest("Service", Method.PUT);
			request.RequestFormat = DataFormat.Json;
			request.JsonSerializer = new RestSharpDataContractJsonSerializer();

			request.AddBody(service);

			IRestResponse response = _client.Put(request);
		}
	}
}