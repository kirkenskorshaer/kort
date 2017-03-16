using Logic.Handler;
using System.Collections.Generic;
using System.Linq;
using WindowsServer.DataContract;
using WindowsServer.DataContract.Alert;

namespace WindowsServer
{
	public class MembershipCard : IMembershipCard
	{
		public List<Member> GetMembersByCardId(string cardId)
		{
			return MemberHandler.Instance.GetMembersByCardId(cardId).Select(dataMember => Member.FromData(dataMember)).ToList();
		}

		public List<Member> GetMembersByNickName(string nickName)
		{
			return MemberHandler.Instance.GetMembersByNickName(nickName).Select(dataMember => Member.FromData(dataMember)).ToList();
		}

		public List<Member> GetMembersToAlert()
		{
			return MemberHandler.Instance.GetMembersToAlert().Select(dataMember => Member.FromData(dataMember)).ToList();
		}

		public InsertResult InsertMember(Member member)
		{
			return new InsertResult() { Id = MemberHandler.Instance.InsertMember(member.ToData()) };
		}

		public void UpdateMember(Member member)
		{
			MemberHandler.Instance.UpdateMember(member.ToData());
		}

		public void DeleteMember(Member member)
		{
			MemberHandler.Instance.DeleteMember(member.ToData());
		}


		public List<VisitTimeAlert> GetAlertsOnMember(string memberid)
		{
			return AlertHandler.Instance.GetAlertsOnMember(memberid).Select(alert => VisitTimeAlert.FromData(alert)).ToList();
		}

		public InsertResult InsertAlert(VisitTimeAlert alert)
		{
			return new InsertResult() { Id = AlertHandler.Instance.InsertAlert(alert.ToData()) };
		}

		public void DeleteAlert(VisitTimeAlert alert)
		{
			AlertHandler.Instance.Delete(alert.ToData());
		}

		public List<Service> GetServicesOnMember(string memberId)
		{
			return ServiceHandler.Instance.GetServicesOnMember(memberId).Select(dataService => Service.FromData(dataService, memberId)).ToList();
		}

		public InsertResult InsertService(Service service)
		{
			return new InsertResult() { Id = ServiceHandler.Instance.InsertService(service.ToData()) };
		}

		public void UpdateService(Service service)
		{
			ServiceHandler.Instance.UpdateService(service.ToData());
		}

		public void DeleteService(Service service)
		{
			ServiceHandler.Instance.DeleteService(service.ToData());
		}


		public List<Visit> GetVisitsOnMember(string memberId)
		{
			return VisitHandler.Instance.GetVisitsOnMember(memberId).Select(dataVisit => Visit.FromData(dataVisit)).ToList();
		}

		public InsertResult InsertVisit(Visit visit)
		{
			return new InsertResult() { Id = VisitHandler.Instance.InsertVisit(visit.ToData()) };
		}
	}
}
