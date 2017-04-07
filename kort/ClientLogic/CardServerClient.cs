using RestSharp;
using System.Collections.Generic;
using Ducksboard.Serializers;
using System.Configuration;
using System;
using ClientLogic.CardServer;

namespace ClientLogic
{
	public class CardServerClient
	{
		private RestClient _client;
		private Ticket _ticket = null;

		private string _userName;
		private string _password;
		private string _ip;
		private TimeSpan _ticketLifeSpan;

		public CardServerClient(string userName, string password, string ip, TimeSpan ticketLifeSpan)
		{
			string serverUrl = ConfigurationManager.AppSettings["serverUrl"];
			_client = new RestClient(serverUrl);

			_userName = userName;
			_password = password;
			_ip = ip;
			_ticketLifeSpan = ticketLifeSpan;
		}

		public List<Member> GetMembersToAlert()
		{
			Func<RestRequest> createRequest = () => new RestRequest("Members/ToAlert", Method.GET);

			Func<RestRequest, IRestResponse<List<Member>>> send = (request) => _client.Execute<List<Member>>(request);

			return SendWithRetry(createRequest, send);
		}

		public List<Member> GetMembersByCardId(string cardId)
		{
			Func<RestRequest> createRequest = () =>
			{
				RestRequest request = new RestRequest("Members/CardId/{cardId}", Method.GET);

				request.AddUrlSegment("cardId", cardId);

				return request;
			};

			Func<RestRequest, IRestResponse<List<Member>>> send = (request) => _client.Execute<List<Member>>(request);

			return SendWithRetry(createRequest, send);
		}

		public List<Member> GetMembersByNickName(string nickName)
		{
			Func<RestRequest> createRequest = () =>
			{
				RestRequest request = new RestRequest("Members/NickName/{nickName}", Method.GET);

				request.AddUrlSegment("nickName", nickName);

				return request;
			};

			Func<RestRequest, IRestResponse<List<Member>>> send = (request) => _client.Execute<List<Member>>(request);

			return SendWithRetry(createRequest, send);
		}

		public void InsertMember(Member member)
		{
			Func<RestRequest> createRequest = () =>
			{
				RestRequest request = new RestRequest("Member", Method.POST)
				{
					RequestFormat = DataFormat.Json,
					JsonSerializer = new RestSharpDataContractJsonSerializer(),
				};

				request.AddBody(member);

				return request;
			};

			Func<RestRequest, IRestResponse<InsertResult>> send = (request) => _client.Post<InsertResult>(request);

			InsertResult result = SendWithRetry(createRequest, send);

			member.Id = result.Id;
		}

		public List<VisitTimeAlert> GetAlertsOnMember(string memberId)
		{
			Func<RestRequest> createRequest = () =>
			{
				RestRequest request = new RestRequest("Alerts/VisitTimeAlert/{memberid}", Method.GET);

				request.AddUrlSegment("memberid", memberId);

				return request;
			};

			Func<RestRequest, IRestResponse<List<VisitTimeAlert>>> send = (request) => _client.Execute<List<VisitTimeAlert>>(request);

			return SendWithRetry(createRequest, send);
		}

		public List<Service> GetServicesOnMember(string memberId)
		{
			Func<RestRequest> createRequest = () =>
			{
				RestRequest request = new RestRequest("Services/{memberid}", Method.GET);

				request.AddUrlSegment("memberid", memberId);

				return request;
			};

			Func<RestRequest, IRestResponse<List<Service>>> send = (request) => _client.Execute<List<Service>>(request);

			return SendWithRetry(createRequest, send);
		}

		public List<Visit> GetVisitsOnMember(string memberId)
		{
			Func<RestRequest> createRequest = () =>
			{
				RestRequest request = new RestRequest("Visits/{memberid}", Method.GET);

				request.AddUrlSegment("memberid", memberId);

				return request;
			};

			Func<RestRequest, IRestResponse<List<Visit>>> send = (request) => _client.Execute<List<Visit>>(request);

			return SendWithRetry(createRequest, send);
		}

		public void InsertService(Service service)
		{
			Func<RestRequest> createRequest = () =>
			{
				RestRequest request = new RestRequest("Service", Method.POST)
				{
					RequestFormat = DataFormat.Json,
					JsonSerializer = new RestSharpDataContractJsonSerializer(),
				};

				request.AddBody(service);

				return request;
			};

			Func<RestRequest, IRestResponse<InsertResult>> send = (request) => _client.Post<InsertResult>(request);

			service.Id = SendWithRetry(createRequest, send).Id;
		}

		public void InsertVisit(Visit visit)
		{
			Func<RestRequest> createRequest = () =>
			{
				RestRequest request = new RestRequest("Visit", Method.POST)
				{
					RequestFormat = DataFormat.Json,
					JsonSerializer = new RestSharpDataContractJsonSerializer(),
				};

				request.AddBody(visit);

				return request;
			};

			Func<RestRequest, IRestResponse<InsertResult>> send = (request) => _client.Post<InsertResult>(request);

			visit.Id = SendWithRetry(createRequest, send).Id;
		}

		public void InsertAlert(VisitTimeAlert alert)
		{
			Func<RestRequest> createRequest = () =>
			{
				RestRequest request = new RestRequest("Alert/VisitTimeAlert", Method.POST)
				{
					RequestFormat = DataFormat.Json,
					JsonSerializer = new RestSharpDataContractJsonSerializer(),
				};

				request.AddBody(alert);

				return request;
			};

			Func<RestRequest, IRestResponse<InsertResult>> send = (request) => _client.Post<InsertResult>(request);

			alert.Id = SendWithRetry(createRequest, send).Id;
		}

		public void UpdateMember(Member member)
		{
			Func<RestRequest> createRequest = () =>
			{
				RestRequest request = new RestRequest("Member", Method.PUT)
				{
					RequestFormat = DataFormat.Json,
					JsonSerializer = new RestSharpDataContractJsonSerializer(),
				};
				request.AddBody(member);

				return request;
			};

			Func<RestRequest, IRestResponse> send = (request) => _client.Put(request);

			SendWithRetry(createRequest, send);
		}

		public void UpdateService(Service service)
		{
			Func<RestRequest> createRequest = () =>
			{
				RestRequest request = new RestRequest("Service", Method.PUT)
				{
					RequestFormat = DataFormat.Json,
					JsonSerializer = new RestSharpDataContractJsonSerializer(),
				};
				request.AddBody(service);

				return request;
			};

			Func<RestRequest, IRestResponse> send = (request) => _client.Put(request);

			SendWithRetry(createRequest, send);
		}

		public RestResponseType SendWithRetry<RestResponseType>(Func<RestRequest> createRequest, Func<RestRequest, IRestResponse<RestResponseType>> send, bool isRetry = false)
		{
			RestRequest request = createRequest();
			AddTicketHeader(request);
			IRestResponse<RestResponseType> response = send(request);

			if (isRetry)
			{
				return response.Data;
			}

			if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
			{
				_ticket = null;
				return SendWithRetry(createRequest, send, true);
			}

			return response.Data;
		}

		public void SendWithRetry(Func<RestRequest> createRequest, Func<RestRequest, IRestResponse> send, bool isRetry = false)
		{
			RestRequest request = createRequest();
			AddTicketHeader(request);
			IRestResponse response = send(request);

			if (isRetry)
			{
				return;
			}

			if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
			{
				_ticket = null;
				SendWithRetry(createRequest, send, true);
			}
		}

		private void AddTicketHeader(RestRequest request)
		{
			if (_ticket == null || _ticket.AssumedValid() == false)
			{
				if (MakeValidTicket() == false)
				{
					throw new Exception("Unable to get ticket");
				}
			}

			string ticket = Utilities.Ticket.CreateTicket(_userName, _password, _ip, _ticket.UseCount, _ticket.ValidFrom, _ticket.ValidTo);

			#if DEBUG
			Console.Out.WriteLine($"{request.Method}:{request.Resource} ticket:[{ticket}] -  UserName:{_userName}, Password:{_password}, Ip:{_ip}, UseCount:{_ticket.UseCount}, ValidFrom:{_ticket.ValidFrom}, ValidTo:{_ticket.ValidTo}");
			#endif

			request.AddHeader("Authorization", _userName + "|" + ticket);
			_ticket.UseCount++;
		}

		private bool MakeValidTicket()
		{
			DateTime currentTime = DateTime.Now;

			if (_ticket == null || _ticket.AssumedValid() == false)
			{
				_ticket = new Ticket()
				{
					ValidFrom = currentTime,
					ValidTo = currentTime + _ticketLifeSpan,
				};

				bool ticketResponse = UpdateTicket(_ticket);

				return ticketResponse;
			}

			return true;
		}

		private bool UpdateTicket(Ticket ticket)
		{
			RestRequest request = new RestRequest("Ticket", Method.PUT)
			{
				RequestFormat = DataFormat.Json,
				JsonSerializer = new RestSharpDataContractJsonSerializer(),
			};
			request.AddBody(ticket);

			request.AddHeader("Authorization", _userName + "|");

			IRestResponse<bool> response = _client.Put<bool>(request);

			if (response.StatusCode != System.Net.HttpStatusCode.OK)
			{
				Console.Out.WriteLine($"failed update ticket");
				return false;
			}

			#if DEBUG
			Console.Out.WriteLine($"ticket update {response.Data} {ticket.ValidFrom} - {ticket.ValidTo}");
			#endif

			return response.Data;
		}
	}
}