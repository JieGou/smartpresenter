﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartPresenter.Service.LocalSyncService.CloudStorageServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CloudStorageServiceReference.ICloudSyncService")]
    public interface ICloudSyncService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICloudSyncService/Download", ReplyAction="http://tempuri.org/ICloudSyncService/DownloadResponse")]
        System.IO.Stream Download(string path);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICloudSyncService/Download", ReplyAction="http://tempuri.org/ICloudSyncService/DownloadResponse")]
        System.Threading.Tasks.Task<System.IO.Stream> DownloadAsync(string path);
        
        // CODEGEN: Generating message contract since the wrapper name (StreamData) of message StreamData does not match the default value (Upload)
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ICloudSyncService/Upload")]
        void Upload(SmartPresenter.Service.LocalSyncService.CloudStorageServiceReference.StreamData request);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/ICloudSyncService/Upload")]
        System.Threading.Tasks.Task UploadAsync(SmartPresenter.Service.LocalSyncService.CloudStorageServiceReference.StreamData request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICloudSyncService/QueueLibraryDocumentUpload", ReplyAction="http://tempuri.org/ICloudSyncService/QueueLibraryDocumentUploadResponse")]
        void QueueLibraryDocumentUpload(System.Guid userAccountId, System.Guid userProfileId, System.Guid libraryId, string fileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICloudSyncService/QueueLibraryDocumentUpload", ReplyAction="http://tempuri.org/ICloudSyncService/QueueLibraryDocumentUploadResponse")]
        System.Threading.Tasks.Task QueueLibraryDocumentUploadAsync(System.Guid userAccountId, System.Guid userProfileId, System.Guid libraryId, string fileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICloudSyncService/QueueLibraryDocumentDownload", ReplyAction="http://tempuri.org/ICloudSyncService/QueueLibraryDocumentDownloadResponse")]
        void QueueLibraryDocumentDownload(System.Guid userAccountId, System.Guid userProfileId, System.Guid libraryId, string fileName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICloudSyncService/QueueLibraryDocumentDownload", ReplyAction="http://tempuri.org/ICloudSyncService/QueueLibraryDocumentDownloadResponse")]
        System.Threading.Tasks.Task QueueLibraryDocumentDownloadAsync(System.Guid userAccountId, System.Guid userProfileId, System.Guid libraryId, string fileName);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="StreamData", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class StreamData {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public string Path;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public System.IO.Stream ByteStream;
        
        public StreamData() {
        }
        
        public StreamData(string Path, System.IO.Stream ByteStream) {
            this.Path = Path;
            this.ByteStream = ByteStream;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICloudSyncServiceChannel : SmartPresenter.Service.LocalSyncService.CloudStorageServiceReference.ICloudSyncService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CloudSyncServiceClient : System.ServiceModel.ClientBase<SmartPresenter.Service.LocalSyncService.CloudStorageServiceReference.ICloudSyncService>, SmartPresenter.Service.LocalSyncService.CloudStorageServiceReference.ICloudSyncService {
        
        public CloudSyncServiceClient() {
        }
        
        public CloudSyncServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CloudSyncServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CloudSyncServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CloudSyncServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.IO.Stream Download(string path) {
            return base.Channel.Download(path);
        }
        
        public System.Threading.Tasks.Task<System.IO.Stream> DownloadAsync(string path) {
            return base.Channel.DownloadAsync(path);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        void SmartPresenter.Service.LocalSyncService.CloudStorageServiceReference.ICloudSyncService.Upload(SmartPresenter.Service.LocalSyncService.CloudStorageServiceReference.StreamData request) {
            base.Channel.Upload(request);
        }
        
        public void Upload(string Path, System.IO.Stream ByteStream) {
            SmartPresenter.Service.LocalSyncService.CloudStorageServiceReference.StreamData inValue = new SmartPresenter.Service.LocalSyncService.CloudStorageServiceReference.StreamData();
            inValue.Path = Path;
            inValue.ByteStream = ByteStream;
            ((SmartPresenter.Service.LocalSyncService.CloudStorageServiceReference.ICloudSyncService)(this)).Upload(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task SmartPresenter.Service.LocalSyncService.CloudStorageServiceReference.ICloudSyncService.UploadAsync(SmartPresenter.Service.LocalSyncService.CloudStorageServiceReference.StreamData request) {
            return base.Channel.UploadAsync(request);
        }
        
        public System.Threading.Tasks.Task UploadAsync(string Path, System.IO.Stream ByteStream) {
            SmartPresenter.Service.LocalSyncService.CloudStorageServiceReference.StreamData inValue = new SmartPresenter.Service.LocalSyncService.CloudStorageServiceReference.StreamData();
            inValue.Path = Path;
            inValue.ByteStream = ByteStream;
            return ((SmartPresenter.Service.LocalSyncService.CloudStorageServiceReference.ICloudSyncService)(this)).UploadAsync(inValue);
        }
        
        public void QueueLibraryDocumentUpload(System.Guid userAccountId, System.Guid userProfileId, System.Guid libraryId, string fileName) {
            base.Channel.QueueLibraryDocumentUpload(userAccountId, userProfileId, libraryId, fileName);
        }
        
        public System.Threading.Tasks.Task QueueLibraryDocumentUploadAsync(System.Guid userAccountId, System.Guid userProfileId, System.Guid libraryId, string fileName) {
            return base.Channel.QueueLibraryDocumentUploadAsync(userAccountId, userProfileId, libraryId, fileName);
        }
        
        public void QueueLibraryDocumentDownload(System.Guid userAccountId, System.Guid userProfileId, System.Guid libraryId, string fileName) {
            base.Channel.QueueLibraryDocumentDownload(userAccountId, userProfileId, libraryId, fileName);
        }
        
        public System.Threading.Tasks.Task QueueLibraryDocumentDownloadAsync(System.Guid userAccountId, System.Guid userProfileId, System.Guid libraryId, string fileName) {
            return base.Channel.QueueLibraryDocumentDownloadAsync(userAccountId, userProfileId, libraryId, fileName);
        }
    }
}
