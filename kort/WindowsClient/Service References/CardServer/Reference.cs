﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WindowsClient.CardServer {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Member", Namespace="http://schemas.datacontract.org/2004/07/WindowsServer.DataContract")]
    [System.SerializableAttribute()]
    public partial class Member : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CardIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NickNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CardId {
            get {
                return this.CardIdField;
            }
            set {
                if ((object.ReferenceEquals(this.CardIdField, value) != true)) {
                    this.CardIdField = value;
                    this.RaisePropertyChanged("CardId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Id {
            get {
                return this.IdField;
            }
            set {
                if ((object.ReferenceEquals(this.IdField, value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string NickName {
            get {
                return this.NickNameField;
            }
            set {
                if ((object.ReferenceEquals(this.NickNameField, value) != true)) {
                    this.NickNameField = value;
                    this.RaisePropertyChanged("NickName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="InsertResult", Namespace="http://schemas.datacontract.org/2004/07/WindowsServer.DataContract")]
    [System.SerializableAttribute()]
    public partial class InsertResult : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Id {
            get {
                return this.IdField;
            }
            set {
                if ((object.ReferenceEquals(this.IdField, value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="VisitTimeAlert", Namespace="http://schemas.datacontract.org/2004/07/WindowsServer.DataContract.Alert")]
    [System.SerializableAttribute()]
    public partial class VisitTimeAlert : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private WindowsClient.CardServer.Member MemberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.TimeSpan TimeFromLatestVisitBeforeAlertField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Id {
            get {
                return this.IdField;
            }
            set {
                if ((object.ReferenceEquals(this.IdField, value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public WindowsClient.CardServer.Member Member {
            get {
                return this.MemberField;
            }
            set {
                if ((object.ReferenceEquals(this.MemberField, value) != true)) {
                    this.MemberField = value;
                    this.RaisePropertyChanged("Member");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.TimeSpan TimeFromLatestVisitBeforeAlert {
            get {
                return this.TimeFromLatestVisitBeforeAlertField;
            }
            set {
                if ((this.TimeFromLatestVisitBeforeAlertField.Equals(value) != true)) {
                    this.TimeFromLatestVisitBeforeAlertField = value;
                    this.RaisePropertyChanged("TimeFromLatestVisitBeforeAlert");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Service", Namespace="http://schemas.datacontract.org/2004/07/WindowsServer.DataContract")]
    [System.SerializableAttribute()]
    public partial class Service : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> MaxServiceUsesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MemberIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<WindowsClient.CardServer.ServiceUse> ServiceUsesField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Id {
            get {
                return this.IdField;
            }
            set {
                if ((object.ReferenceEquals(this.IdField, value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<int> MaxServiceUses {
            get {
                return this.MaxServiceUsesField;
            }
            set {
                if ((this.MaxServiceUsesField.Equals(value) != true)) {
                    this.MaxServiceUsesField = value;
                    this.RaisePropertyChanged("MaxServiceUses");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MemberId {
            get {
                return this.MemberIdField;
            }
            set {
                if ((object.ReferenceEquals(this.MemberIdField, value) != true)) {
                    this.MemberIdField = value;
                    this.RaisePropertyChanged("MemberId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<WindowsClient.CardServer.ServiceUse> ServiceUses {
            get {
                return this.ServiceUsesField;
            }
            set {
                if ((object.ReferenceEquals(this.ServiceUsesField, value) != true)) {
                    this.ServiceUsesField = value;
                    this.RaisePropertyChanged("ServiceUses");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ServiceUse", Namespace="http://schemas.datacontract.org/2004/07/WindowsServer.DataContract.Part")]
    [System.SerializableAttribute()]
    public partial class ServiceUse : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime UsedTimeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime UsedTime {
            get {
                return this.UsedTimeField;
            }
            set {
                if ((this.UsedTimeField.Equals(value) != true)) {
                    this.UsedTimeField = value;
                    this.RaisePropertyChanged("UsedTime");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Visit", Namespace="http://schemas.datacontract.org/2004/07/WindowsServer.DataContract")]
    [System.SerializableAttribute()]
    public partial class Visit : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MemberIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime VisitTimeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Id {
            get {
                return this.IdField;
            }
            set {
                if ((object.ReferenceEquals(this.IdField, value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MemberId {
            get {
                return this.MemberIdField;
            }
            set {
                if ((object.ReferenceEquals(this.MemberIdField, value) != true)) {
                    this.MemberIdField = value;
                    this.RaisePropertyChanged("MemberId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime VisitTime {
            get {
                return this.VisitTimeField;
            }
            set {
                if ((this.VisitTimeField.Equals(value) != true)) {
                    this.VisitTimeField = value;
                    this.RaisePropertyChanged("VisitTime");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CardServer.IMembershipCard")]
    public interface IMembershipCard {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMembershipCard/GetMembersByCardId", ReplyAction="http://tempuri.org/IMembershipCard/GetMembersByCardIdResponse")]
        System.Collections.Generic.List<WindowsClient.CardServer.Member> GetMembersByCardId(string cardId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMembershipCard/GetMembersByCardId", ReplyAction="http://tempuri.org/IMembershipCard/GetMembersByCardIdResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<WindowsClient.CardServer.Member>> GetMembersByCardIdAsync(string cardId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMembershipCard/GetMembersByNickName", ReplyAction="http://tempuri.org/IMembershipCard/GetMembersByNickNameResponse")]
        System.Collections.Generic.List<WindowsClient.CardServer.Member> GetMembersByNickName(string nickName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMembershipCard/GetMembersByNickName", ReplyAction="http://tempuri.org/IMembershipCard/GetMembersByNickNameResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<WindowsClient.CardServer.Member>> GetMembersByNickNameAsync(string nickName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMembershipCard/GetMembersToAlert", ReplyAction="http://tempuri.org/IMembershipCard/GetMembersToAlertResponse")]
        System.Collections.Generic.List<WindowsClient.CardServer.Member> GetMembersToAlert();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMembershipCard/GetMembersToAlert", ReplyAction="http://tempuri.org/IMembershipCard/GetMembersToAlertResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<WindowsClient.CardServer.Member>> GetMembersToAlertAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMembershipCard/InsertMember", ReplyAction="http://tempuri.org/IMembershipCard/InsertMemberResponse")]
        WindowsClient.CardServer.InsertResult InsertMember(WindowsClient.CardServer.Member member);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMembershipCard/InsertMember", ReplyAction="http://tempuri.org/IMembershipCard/InsertMemberResponse")]
        System.Threading.Tasks.Task<WindowsClient.CardServer.InsertResult> InsertMemberAsync(WindowsClient.CardServer.Member member);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMembershipCard/UpdateMember", ReplyAction="http://tempuri.org/IMembershipCard/UpdateMemberResponse")]
        void UpdateMember(WindowsClient.CardServer.Member member);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMembershipCard/UpdateMember", ReplyAction="http://tempuri.org/IMembershipCard/UpdateMemberResponse")]
        System.Threading.Tasks.Task UpdateMemberAsync(WindowsClient.CardServer.Member member);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMembershipCard/DeleteMember", ReplyAction="http://tempuri.org/IMembershipCard/DeleteMemberResponse")]
        void DeleteMember(WindowsClient.CardServer.Member member);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMembershipCard/DeleteMember", ReplyAction="http://tempuri.org/IMembershipCard/DeleteMemberResponse")]
        System.Threading.Tasks.Task DeleteMemberAsync(WindowsClient.CardServer.Member member);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMembershipCard/GetAlertsOnMember", ReplyAction="http://tempuri.org/IMembershipCard/GetAlertsOnMemberResponse")]
        System.Collections.Generic.List<WindowsClient.CardServer.VisitTimeAlert> GetAlertsOnMember(string memberid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMembershipCard/GetAlertsOnMember", ReplyAction="http://tempuri.org/IMembershipCard/GetAlertsOnMemberResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<WindowsClient.CardServer.VisitTimeAlert>> GetAlertsOnMemberAsync(string memberid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMembershipCard/InsertAlert", ReplyAction="http://tempuri.org/IMembershipCard/InsertAlertResponse")]
        WindowsClient.CardServer.InsertResult InsertAlert(WindowsClient.CardServer.VisitTimeAlert alert);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMembershipCard/InsertAlert", ReplyAction="http://tempuri.org/IMembershipCard/InsertAlertResponse")]
        System.Threading.Tasks.Task<WindowsClient.CardServer.InsertResult> InsertAlertAsync(WindowsClient.CardServer.VisitTimeAlert alert);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMembershipCard/DeleteAlert", ReplyAction="http://tempuri.org/IMembershipCard/DeleteAlertResponse")]
        void DeleteAlert(WindowsClient.CardServer.VisitTimeAlert alert);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMembershipCard/DeleteAlert", ReplyAction="http://tempuri.org/IMembershipCard/DeleteAlertResponse")]
        System.Threading.Tasks.Task DeleteAlertAsync(WindowsClient.CardServer.VisitTimeAlert alert);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMembershipCard/GetServicesOnMember", ReplyAction="http://tempuri.org/IMembershipCard/GetServicesOnMemberResponse")]
        System.Collections.Generic.List<WindowsClient.CardServer.Service> GetServicesOnMember(string memberid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMembershipCard/GetServicesOnMember", ReplyAction="http://tempuri.org/IMembershipCard/GetServicesOnMemberResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<WindowsClient.CardServer.Service>> GetServicesOnMemberAsync(string memberid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMembershipCard/InsertService", ReplyAction="http://tempuri.org/IMembershipCard/InsertServiceResponse")]
        WindowsClient.CardServer.InsertResult InsertService(WindowsClient.CardServer.Service service);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMembershipCard/InsertService", ReplyAction="http://tempuri.org/IMembershipCard/InsertServiceResponse")]
        System.Threading.Tasks.Task<WindowsClient.CardServer.InsertResult> InsertServiceAsync(WindowsClient.CardServer.Service service);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMembershipCard/UpdateService", ReplyAction="http://tempuri.org/IMembershipCard/UpdateServiceResponse")]
        void UpdateService(WindowsClient.CardServer.Service service);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMembershipCard/UpdateService", ReplyAction="http://tempuri.org/IMembershipCard/UpdateServiceResponse")]
        System.Threading.Tasks.Task UpdateServiceAsync(WindowsClient.CardServer.Service service);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMembershipCard/DeleteService", ReplyAction="http://tempuri.org/IMembershipCard/DeleteServiceResponse")]
        void DeleteService(WindowsClient.CardServer.Service service);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMembershipCard/DeleteService", ReplyAction="http://tempuri.org/IMembershipCard/DeleteServiceResponse")]
        System.Threading.Tasks.Task DeleteServiceAsync(WindowsClient.CardServer.Service service);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMembershipCard/GetVisitsOnMember", ReplyAction="http://tempuri.org/IMembershipCard/GetVisitsOnMemberResponse")]
        System.Collections.Generic.List<WindowsClient.CardServer.Visit> GetVisitsOnMember(string memberid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMembershipCard/GetVisitsOnMember", ReplyAction="http://tempuri.org/IMembershipCard/GetVisitsOnMemberResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<WindowsClient.CardServer.Visit>> GetVisitsOnMemberAsync(string memberid);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMembershipCard/InsertVisit", ReplyAction="http://tempuri.org/IMembershipCard/InsertVisitResponse")]
        WindowsClient.CardServer.InsertResult InsertVisit(WindowsClient.CardServer.Visit visit);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMembershipCard/InsertVisit", ReplyAction="http://tempuri.org/IMembershipCard/InsertVisitResponse")]
        System.Threading.Tasks.Task<WindowsClient.CardServer.InsertResult> InsertVisitAsync(WindowsClient.CardServer.Visit visit);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMembershipCardChannel : WindowsClient.CardServer.IMembershipCard, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MembershipCardClient : System.ServiceModel.ClientBase<WindowsClient.CardServer.IMembershipCard>, WindowsClient.CardServer.IMembershipCard {
        
        public MembershipCardClient() {
        }
        
        public MembershipCardClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public MembershipCardClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MembershipCardClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MembershipCardClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<WindowsClient.CardServer.Member> GetMembersByCardId(string cardId) {
            return base.Channel.GetMembersByCardId(cardId);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<WindowsClient.CardServer.Member>> GetMembersByCardIdAsync(string cardId) {
            return base.Channel.GetMembersByCardIdAsync(cardId);
        }
        
        public System.Collections.Generic.List<WindowsClient.CardServer.Member> GetMembersByNickName(string nickName) {
            return base.Channel.GetMembersByNickName(nickName);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<WindowsClient.CardServer.Member>> GetMembersByNickNameAsync(string nickName) {
            return base.Channel.GetMembersByNickNameAsync(nickName);
        }
        
        public System.Collections.Generic.List<WindowsClient.CardServer.Member> GetMembersToAlert() {
            return base.Channel.GetMembersToAlert();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<WindowsClient.CardServer.Member>> GetMembersToAlertAsync() {
            return base.Channel.GetMembersToAlertAsync();
        }
        
        public WindowsClient.CardServer.InsertResult InsertMember(WindowsClient.CardServer.Member member) {
            return base.Channel.InsertMember(member);
        }
        
        public System.Threading.Tasks.Task<WindowsClient.CardServer.InsertResult> InsertMemberAsync(WindowsClient.CardServer.Member member) {
            return base.Channel.InsertMemberAsync(member);
        }
        
        public void UpdateMember(WindowsClient.CardServer.Member member) {
            base.Channel.UpdateMember(member);
        }
        
        public System.Threading.Tasks.Task UpdateMemberAsync(WindowsClient.CardServer.Member member) {
            return base.Channel.UpdateMemberAsync(member);
        }
        
        public void DeleteMember(WindowsClient.CardServer.Member member) {
            base.Channel.DeleteMember(member);
        }
        
        public System.Threading.Tasks.Task DeleteMemberAsync(WindowsClient.CardServer.Member member) {
            return base.Channel.DeleteMemberAsync(member);
        }
        
        public System.Collections.Generic.List<WindowsClient.CardServer.VisitTimeAlert> GetAlertsOnMember(string memberid) {
            return base.Channel.GetAlertsOnMember(memberid);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<WindowsClient.CardServer.VisitTimeAlert>> GetAlertsOnMemberAsync(string memberid) {
            return base.Channel.GetAlertsOnMemberAsync(memberid);
        }
        
        public WindowsClient.CardServer.InsertResult InsertAlert(WindowsClient.CardServer.VisitTimeAlert alert) {
            return base.Channel.InsertAlert(alert);
        }
        
        public System.Threading.Tasks.Task<WindowsClient.CardServer.InsertResult> InsertAlertAsync(WindowsClient.CardServer.VisitTimeAlert alert) {
            return base.Channel.InsertAlertAsync(alert);
        }
        
        public void DeleteAlert(WindowsClient.CardServer.VisitTimeAlert alert) {
            base.Channel.DeleteAlert(alert);
        }
        
        public System.Threading.Tasks.Task DeleteAlertAsync(WindowsClient.CardServer.VisitTimeAlert alert) {
            return base.Channel.DeleteAlertAsync(alert);
        }
        
        public System.Collections.Generic.List<WindowsClient.CardServer.Service> GetServicesOnMember(string memberid) {
            return base.Channel.GetServicesOnMember(memberid);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<WindowsClient.CardServer.Service>> GetServicesOnMemberAsync(string memberid) {
            return base.Channel.GetServicesOnMemberAsync(memberid);
        }
        
        public WindowsClient.CardServer.InsertResult InsertService(WindowsClient.CardServer.Service service) {
            return base.Channel.InsertService(service);
        }
        
        public System.Threading.Tasks.Task<WindowsClient.CardServer.InsertResult> InsertServiceAsync(WindowsClient.CardServer.Service service) {
            return base.Channel.InsertServiceAsync(service);
        }
        
        public void UpdateService(WindowsClient.CardServer.Service service) {
            base.Channel.UpdateService(service);
        }
        
        public System.Threading.Tasks.Task UpdateServiceAsync(WindowsClient.CardServer.Service service) {
            return base.Channel.UpdateServiceAsync(service);
        }
        
        public void DeleteService(WindowsClient.CardServer.Service service) {
            base.Channel.DeleteService(service);
        }
        
        public System.Threading.Tasks.Task DeleteServiceAsync(WindowsClient.CardServer.Service service) {
            return base.Channel.DeleteServiceAsync(service);
        }
        
        public System.Collections.Generic.List<WindowsClient.CardServer.Visit> GetVisitsOnMember(string memberid) {
            return base.Channel.GetVisitsOnMember(memberid);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<WindowsClient.CardServer.Visit>> GetVisitsOnMemberAsync(string memberid) {
            return base.Channel.GetVisitsOnMemberAsync(memberid);
        }
        
        public WindowsClient.CardServer.InsertResult InsertVisit(WindowsClient.CardServer.Visit visit) {
            return base.Channel.InsertVisit(visit);
        }
        
        public System.Threading.Tasks.Task<WindowsClient.CardServer.InsertResult> InsertVisitAsync(WindowsClient.CardServer.Visit visit) {
            return base.Channel.InsertVisitAsync(visit);
        }
    }
}