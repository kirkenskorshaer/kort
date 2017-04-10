using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using WindowsServer.DataContract;
using WindowsServer.DataContract.Alert;
using Logic;
using Logic.Receivable.Member;
using Logic.Receivable;
using System.Text;
using Logic.Receivable.Alert;
using Logic.Receivable.Service;
using Logic.Receivable.Visit;
using Logic.Time;
using Logic.Receivable.Ticket;

namespace WindowsServer
{
	public class MembershipCard : IMembershipCard
	{
		public List<Member> GetMembersByCardId(string cardId)
		{
			Pipeline<string, List<Data.Data.Member>> pipeline = CreatePipelineFromHeader<string, List<Data.Data.Member>, GetMembersByCardId>(cardId);

			return pipeline.Execute().Select(dataMember => Member.FromData(dataMember)).ToList();
		}

		public List<Member> GetMembersByNickName(string nickName)
		{
			Pipeline<string, List<Data.Data.Member>> pipeline = CreatePipelineFromHeader<string, List<Data.Data.Member>, GetMembersByNickName>(nickName);

			return pipeline.Execute().Select(dataMember => Member.FromData(dataMember)).ToList();
		}

		public List<Member> GetMembersToAlert()
		{
			Pipeline<object, List<Data.Data.Member>> pipeline = CreatePipelineFromHeader<object, List<Data.Data.Member>, GetMembersToAlert>(null);

			return pipeline.Execute().Select(dataMember => Member.FromData(dataMember)).ToList();
		}

		public InsertResult InsertMember(Member member)
		{
			Pipeline<Data.Data.Member, string> pipeline = CreatePipelineFromHeader<Data.Data.Member, string, InsertMember>(member.ToData());

			return new InsertResult() { Id = pipeline.Execute() };
		}

		public void UpdateMember(Member member)
		{
			Pipeline<Data.Data.Member, object> pipeline = CreatePipelineFromHeader<Data.Data.Member, object, UpdateMember>(member.ToData());

			pipeline.Execute();
		}

		public void DeleteMember(Member member)
		{
			Pipeline<Data.Data.Member, object> pipeline = CreatePipelineFromHeader<Data.Data.Member, object, DeleteMember>(member.ToData());

			pipeline.Execute();
		}


		public List<VisitTimeAlert> GetAlertsOnMember(string memberid)
		{
			Pipeline<string, List<Data.Data.Alert.AbstractAlert>> pipeline = CreatePipelineFromHeader<string, List<Data.Data.Alert.AbstractAlert>, GetAlertsOnMember>(memberid);

			return pipeline.Execute().Select(alert => VisitTimeAlert.FromData(alert)).ToList();
		}

		public InsertResult InsertAlert(VisitTimeAlert alert)
		{
			Pipeline<Data.Data.Alert.AbstractAlert, string> pipeline = CreatePipelineFromHeader<Data.Data.Alert.AbstractAlert, string, InsertAlert>(alert.ToData());

			return new InsertResult() { Id = pipeline.Execute() };
		}

		public void DeleteAlert(VisitTimeAlert alert)
		{
			Pipeline<Data.Data.Alert.AbstractAlert, object> pipeline = CreatePipelineFromHeader<Data.Data.Alert.AbstractAlert, object, DeleteAlert>(alert.ToData());

			pipeline.Execute();
		}

		public List<Service> GetServicesOnMember(string memberId)
		{
			Pipeline<string, List<Data.Data.Service>> pipeline = CreatePipelineFromHeader<string, List<Data.Data.Service>, GetServicesOnMember>(memberId);

			return pipeline.Execute().Select(dataService => Service.FromData(dataService, memberId)).ToList();
		}

		public InsertResult InsertService(Service service)
		{
			Pipeline<Data.Data.Service, string> pipeline = CreatePipelineFromHeader<Data.Data.Service, string, InsertService>(service.ToData());

			return new InsertResult() { Id = pipeline.Execute() };
		}

		public void UpdateService(Service service)
		{
			Pipeline<Data.Data.Service, object> pipeline = CreatePipelineFromHeader<Data.Data.Service, object, UpdateService>(service.ToData());

			pipeline.Execute();
		}

		public void DeleteService(Service service)
		{
			Pipeline<Data.Data.Service, object> pipeline = CreatePipelineFromHeader<Data.Data.Service, object, DeleteService>(service.ToData());

			pipeline.Execute();
		}


		public List<Visit> GetVisitsOnMember(string memberId)
		{
			Pipeline<string, List<Data.Data.Visit>> pipeline = CreatePipelineFromHeader<string, List<Data.Data.Visit>, GetVisitsOnMember>(memberId);

			return pipeline.Execute().Select(dataVisit => Visit.FromData(dataVisit)).ToList();
		}

		public InsertResult InsertVisit(Visit visit)
		{
			Pipeline<Data.Data.Visit, string> pipeline = CreatePipelineFromHeader<Data.Data.Visit, string, InsertVisit>(visit.ToData());

			return new InsertResult() { Id = pipeline.Execute() };
		}


		public bool UpdateTicket(Ticket ticket)
		{
			Pipeline<Period, bool> pipeline = CreatePipelineFromHeader<Period, bool, CreateTicket>(new Period() { From = ticket.ValidFrom, To = ticket.ValidTo });

			return pipeline.Execute();
		}


		public string GetMyIp()
		{
			return GetIp();
		}

		private const string Authorization = "Authorization";

		private Pipeline<InputType, OutputType> CreatePipelineFromHeader<InputType, OutputType, ReceivableType>(InputType input) where
			ReceivableType : IReceivable<InputType, OutputType>, new()
		{
			Pipeline<InputType, OutputType> pipeline = new Pipeline<InputType, OutputType>(new ReceivableType(), input, SetResponse);

			pipeline.UserName = string.Empty;
			pipeline.Ticket = string.Empty;
			pipeline.Ip = GetIp();

			IncomingWebRequestContext request = WebOperationContext.Current.IncomingRequest;
			WebHeaderCollection headers = request.Headers;

			if (headers.AllKeys.Contains(Authorization) == false)
			{
				return pipeline;
			}

			string AuthorizationHeader = headers["Authorization"];

			string[] split = AuthorizationHeader.Split('|');

			if (split.Length != 2)
			{
				return pipeline;
			}

			pipeline.UserName = split[0];
			pipeline.Ticket = split[1];

			return pipeline;
		}

		private string GetIp()
		{
			OperationContext context = OperationContext.Current;
			MessageProperties messageProperties = context.IncomingMessageProperties;
			RemoteEndpointMessageProperty endpointProperty = messageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;

			return endpointProperty.Address;
		}

		private void SetResponse(bool isRejected, StringBuilder publicErrorBuilder)
		{
			if (isRejected)
			{
				OutgoingWebResponseContext response = WebOperationContext.Current.OutgoingResponse;
				response.StatusCode = HttpStatusCode.Unauthorized;
				response.StatusDescription = publicErrorBuilder.ToString();
			}
		}
	}
}
