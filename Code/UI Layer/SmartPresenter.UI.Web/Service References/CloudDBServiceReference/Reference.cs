﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartPresenter.UI.Web.CloudDBServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UserAccount", Namespace="http://schemas.datacontract.org/2004/07/SmartPresenter.Service.CloudSyncService")]
    [System.SerializableAttribute()]
    public partial class UserAccount : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EMailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PasswordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int SerialField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TypeField;
        
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
        public string EMail {
            get {
                return this.EMailField;
            }
            set {
                if ((object.ReferenceEquals(this.EMailField, value) != true)) {
                    this.EMailField = value;
                    this.RaisePropertyChanged("EMail");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Password {
            get {
                return this.PasswordField;
            }
            set {
                if ((object.ReferenceEquals(this.PasswordField, value) != true)) {
                    this.PasswordField = value;
                    this.RaisePropertyChanged("Password");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Serial {
            get {
                return this.SerialField;
            }
            set {
                if ((this.SerialField.Equals(value) != true)) {
                    this.SerialField = value;
                    this.RaisePropertyChanged("Serial");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Type {
            get {
                return this.TypeField;
            }
            set {
                if ((object.ReferenceEquals(this.TypeField, value) != true)) {
                    this.TypeField = value;
                    this.RaisePropertyChanged("Type");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CloudDBServiceReference.ICloudDBService")]
    public interface ICloudDBService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICloudDBService/GetAllUsers", ReplyAction="http://tempuri.org/ICloudDBService/GetAllUsersResponse")]
        SmartPresenter.UI.Web.CloudDBServiceReference.UserAccount[] GetAllUsers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICloudDBService/GetAllUsers", ReplyAction="http://tempuri.org/ICloudDBService/GetAllUsersResponse")]
        System.Threading.Tasks.Task<SmartPresenter.UI.Web.CloudDBServiceReference.UserAccount[]> GetAllUsersAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICloudDBService/AddUser", ReplyAction="http://tempuri.org/ICloudDBService/AddUserResponse")]
        void AddUser(SmartPresenter.UI.Web.CloudDBServiceReference.UserAccount userAccount);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICloudDBService/AddUser", ReplyAction="http://tempuri.org/ICloudDBService/AddUserResponse")]
        System.Threading.Tasks.Task AddUserAsync(SmartPresenter.UI.Web.CloudDBServiceReference.UserAccount userAccount);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICloudDBService/UpdateUser", ReplyAction="http://tempuri.org/ICloudDBService/UpdateUserResponse")]
        void UpdateUser(SmartPresenter.UI.Web.CloudDBServiceReference.UserAccount userAccount);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICloudDBService/UpdateUser", ReplyAction="http://tempuri.org/ICloudDBService/UpdateUserResponse")]
        System.Threading.Tasks.Task UpdateUserAsync(SmartPresenter.UI.Web.CloudDBServiceReference.UserAccount userAccount);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICloudDBService/DeleteUser", ReplyAction="http://tempuri.org/ICloudDBService/DeleteUserResponse")]
        void DeleteUser(SmartPresenter.UI.Web.CloudDBServiceReference.UserAccount userAccount);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICloudDBService/DeleteUser", ReplyAction="http://tempuri.org/ICloudDBService/DeleteUserResponse")]
        System.Threading.Tasks.Task DeleteUserAsync(SmartPresenter.UI.Web.CloudDBServiceReference.UserAccount userAccount);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICloudDBServiceChannel : SmartPresenter.UI.Web.CloudDBServiceReference.ICloudDBService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CloudDBServiceClient : System.ServiceModel.ClientBase<SmartPresenter.UI.Web.CloudDBServiceReference.ICloudDBService>, SmartPresenter.UI.Web.CloudDBServiceReference.ICloudDBService {
        
        public CloudDBServiceClient() {
        }
        
        public CloudDBServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CloudDBServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CloudDBServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CloudDBServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public SmartPresenter.UI.Web.CloudDBServiceReference.UserAccount[] GetAllUsers() {
            return base.Channel.GetAllUsers();
        }
        
        public System.Threading.Tasks.Task<SmartPresenter.UI.Web.CloudDBServiceReference.UserAccount[]> GetAllUsersAsync() {
            return base.Channel.GetAllUsersAsync();
        }
        
        public void AddUser(SmartPresenter.UI.Web.CloudDBServiceReference.UserAccount userAccount) {
            base.Channel.AddUser(userAccount);
        }
        
        public System.Threading.Tasks.Task AddUserAsync(SmartPresenter.UI.Web.CloudDBServiceReference.UserAccount userAccount) {
            return base.Channel.AddUserAsync(userAccount);
        }
        
        public void UpdateUser(SmartPresenter.UI.Web.CloudDBServiceReference.UserAccount userAccount) {
            base.Channel.UpdateUser(userAccount);
        }
        
        public System.Threading.Tasks.Task UpdateUserAsync(SmartPresenter.UI.Web.CloudDBServiceReference.UserAccount userAccount) {
            return base.Channel.UpdateUserAsync(userAccount);
        }
        
        public void DeleteUser(SmartPresenter.UI.Web.CloudDBServiceReference.UserAccount userAccount) {
            base.Channel.DeleteUser(userAccount);
        }
        
        public System.Threading.Tasks.Task DeleteUserAsync(SmartPresenter.UI.Web.CloudDBServiceReference.UserAccount userAccount) {
            return base.Channel.DeleteUserAsync(userAccount);
        }
    }
}
