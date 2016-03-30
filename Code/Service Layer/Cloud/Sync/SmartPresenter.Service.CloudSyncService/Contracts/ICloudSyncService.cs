using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SmartPresenter.Service.CloudSyncService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.    
    [ServiceContract]
    public interface ICloudSyncService
    {
        [OperationContract]
        LibraryStreamData DownloadDocument(LibraryStreamData dataStream);

        [OperationContract(IsOneWay = true)]
        void UploadDocument(LibraryStreamData dataStream);

        [OperationContract(IsOneWay = true)]
        void UploadImageFile(LibraryStreamData dataStream);

        [OperationContract]
        LibraryStreamData DownloadImageFile(LibraryStreamData dataStream);

        [OperationContract(IsOneWay = true)]
        void UploadAudioFile(LibraryStreamData dataStream);

        [OperationContract]
        LibraryStreamData DownloadAudioFile(LibraryStreamData dataStream);

        [OperationContract(IsOneWay = true)]
        void UploadVideoFile(LibraryStreamData dataStream);

        [OperationContract]
        LibraryStreamData DownloadVideoFile(LibraryStreamData dataStream);

        [OperationContract(IsOneWay = true)]
        void UploadSettingsFile(LibraryStreamData dataStream);

        [OperationContract]
        LibraryStreamData DownloadSettingsFile(LibraryStreamData dataStream);
    }

    [MessageContract]
    public class LibraryStreamData
    {
        [MessageBodyMember]
        public Stream ByteStream { get; set; }
        [MessageHeader]
        public string FileName { get; set; }
        [MessageHeader]
        public Guid LibraryId { get; set; }
        [MessageHeader]
        public Guid UserProfileId { get; set; }
        [MessageHeader]
        public Guid UserAccountId { get; set; }
    }
}
