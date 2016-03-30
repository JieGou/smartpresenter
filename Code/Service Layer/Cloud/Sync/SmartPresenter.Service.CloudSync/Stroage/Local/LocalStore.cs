using System;
using System.IO;

namespace SmartPresenter.Service.LocalSync
{
    public class LocalStore : IStore
    {
        #region IStore Members

        public void StoreFile(TransferEntity transferEntity)
        {
            switch (transferEntity.FileType)
            {
                case SyncFileType.Audio:
                    UploadAudioFile(transferEntity.UserId, transferEntity.LibraryId, transferEntity.FileId, transferEntity.FileName);
                    break;
                case SyncFileType.Image:
                    UploadImageFile(transferEntity.UserId, transferEntity.LibraryId, transferEntity.FileId, transferEntity.FileName);
                    break;
                case SyncFileType.Presentation:
                    UploadLibraryDocument(transferEntity.UserId, transferEntity.LibraryId, transferEntity.FileId, transferEntity.FileName);
                    break;
                case SyncFileType.Video:
                    UploadVideoFile(transferEntity.UserId, transferEntity.LibraryId, transferEntity.FileId, transferEntity.FileName);
                    break;
            }
        }

        public byte[] RetrieveFile(TransferEntity transferEntity)
        {
            switch (transferEntity.FileType)
            {
                case SyncFileType.Audio:
                    return DownloadAudioFile(transferEntity.UserId, transferEntity.LibraryId, transferEntity.FileId);
                    break;
                case SyncFileType.Image:
                    return DownloadImageFile(transferEntity.UserId, transferEntity.LibraryId, transferEntity.FileId);
                    break;
                case SyncFileType.Presentation:
                    return DownloadLibraryDocument(transferEntity.UserId, transferEntity.LibraryId, transferEntity.FileId);
                    break;
                case SyncFileType.Video:
                    return DownloadVideoFile(transferEntity.UserId, transferEntity.LibraryId, transferEntity.FileId);
                    break;
            }
            return null;
        }

        #endregion

        #region Private Methods

        private void UploadLibraryDocument(Guid userId, Guid libraryId, Guid fileId, string fileName)
        {
            byte[] data = File.ReadAllBytes(fileName);            
        }

        private void UploadImageFile(Guid userId, Guid libraryId, Guid fileId, string fileName)
        {
            byte[] data = File.ReadAllBytes(fileName);
        }

        private void UploadVideoFile(Guid userId, Guid libraryId, Guid fileId, string fileName)
        {
            byte[] data = File.ReadAllBytes(fileName);
        }

        private void UploadAudioFile(Guid userId, Guid libraryId, Guid fileId, string fileName)
        {
            byte[] data = File.ReadAllBytes(fileName);
        }

        private byte[] DownloadLibraryDocument(Guid userId, Guid libraryId, Guid fileId)
        {
            //return CloudStorageServiceManager.Instance.Client.DownloadLibraryDocument(userId, libraryId, fileId);
            return null;
        }

        private byte[] DownloadImageFile(Guid userId, Guid libraryId, Guid fileId)
        {
            //return CloudStorageServiceManager.Instance.Client.DownloadImageFile(userId, libraryId, fileId);
            return null;
        }

        private byte[] DownloadVideoFile(Guid userId, Guid libraryId, Guid fileId)
        {
            //return CloudStorageServiceManager.Instance.Client.DownloadVideoFile(userId, libraryId, fileId);
            return null;
        }

        private byte[] DownloadAudioFile(Guid userId, Guid libraryId, Guid fileId)
        {
            //return CloudStorageServiceManager.Instance.Client.DownloadAudioFile(userId, libraryId, fileId);
            return null;
        }

        #endregion
        
    }
}