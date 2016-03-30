using System;
using System.Collections.Concurrent;
using System.ServiceModel;

namespace SmartPresenter.Service.LocalSync
{    
    [ServiceContract]
    public interface ISyncService
    {
        [OperationContract]
        void UploadFile(Guid userId, Guid libraryId, Guid fileId, string fileName, SyncFileType fileType);

        [OperationContract]
        void DownloadFile(Guid userId, Guid libraryId, Guid fileId, string fileName, SyncFileType fileType);
    }
}
