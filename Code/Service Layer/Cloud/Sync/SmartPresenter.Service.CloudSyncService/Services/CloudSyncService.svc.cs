using SmartPresenter.Data.Common.Cloud.Sync;
using SmartPresenter.Service.CloudSyncService.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Collections.Concurrent;

namespace SmartPresenter.Service.CloudSyncService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.    
    public class CloudSyncService : ICloudSyncService
    {
        #region Constants

        private readonly string UserAccountPath = "{0}//{1}//{2}";

        #endregion

        #region ICloudSyncService Members

        public LibraryStreamData DownloadDocument(LibraryStreamData dataStream)
        {
            string userLibraryPath = ServerApplicationData.Instance.GetUserDocumentLibraryPath(dataStream.UserAccountId, dataStream.UserProfileId, dataStream.LibraryId);
            string serverFilePath = Path.Combine(userLibraryPath, dataStream.FileName);
            if (string.IsNullOrEmpty(serverFilePath) == false && File.Exists(serverFilePath) == true)
            {
                FileStream fileStream = new FileStream(serverFilePath, FileMode.Open);
                return new LibraryStreamData() { ByteStream = fileStream };
            }
            return null;
        }

        public void UploadDocument(LibraryStreamData dataStream)
        {
            string userLibraryPath = ServerApplicationData.Instance.GetUserDocumentLibraryPath(dataStream.UserAccountId, dataStream.UserProfileId, dataStream.LibraryId);
            string serverFilePath = Path.Combine(userLibraryPath, dataStream.FileName);
            using (FileStream fileStream = new FileStream(serverFilePath, FileMode.OpenOrCreate))
            {
                dataStream.ByteStream.CopyTo(fileStream);
            }
        }

        public void UploadImageFile(LibraryStreamData dataStream)
        {
            string userImagePath = ServerApplicationData.Instance.GetUserImageLibraryPath(dataStream.UserAccountId, dataStream.UserProfileId, dataStream.LibraryId);
            string serverFilePath = Path.Combine(userImagePath, dataStream.FileName);
            using (FileStream fileStream = new FileStream(serverFilePath, FileMode.OpenOrCreate))
            {
                dataStream.ByteStream.CopyTo(fileStream);
            }
        }

        public LibraryStreamData DownloadImageFile(LibraryStreamData dataStream)
        {
            string userImagePath = ServerApplicationData.Instance.GetUserImageLibraryPath(dataStream.UserAccountId, dataStream.UserProfileId, dataStream.LibraryId);
            string serverFilePath = Path.Combine(userImagePath, dataStream.FileName);
            if (string.IsNullOrEmpty(serverFilePath) == false && File.Exists(serverFilePath) == true)
            {
                FileStream fileStream = new FileStream(serverFilePath, FileMode.Open);
                return new LibraryStreamData() { ByteStream = fileStream };
            }
            return null;
        }

        public void UploadAudioFile(LibraryStreamData dataStream)
        {
            string userAudioPath = ServerApplicationData.Instance.GetUserAudioLibraryPath(dataStream.UserAccountId, dataStream.UserProfileId, dataStream.LibraryId);
            string serverFilePath = Path.Combine(userAudioPath, dataStream.FileName);
            using (FileStream fileStream = new FileStream(serverFilePath, FileMode.OpenOrCreate))
            {
                dataStream.ByteStream.CopyTo(fileStream);
            }
        }

        public LibraryStreamData DownloadAudioFile(LibraryStreamData dataStream)
        {
            string userAudioPath = ServerApplicationData.Instance.GetUserAudioLibraryPath(dataStream.UserAccountId, dataStream.UserProfileId, dataStream.LibraryId);
            string serverFilePath = Path.Combine(userAudioPath, dataStream.FileName);
            if (string.IsNullOrEmpty(serverFilePath) == false && File.Exists(serverFilePath) == true)
            {
                FileStream fileStream = new FileStream(serverFilePath, FileMode.Open);
                return new LibraryStreamData() { ByteStream = fileStream };
            }
            return null;
        }

        public void UploadVideoFile(LibraryStreamData dataStream)
        {
            string userVideoPath = ServerApplicationData.Instance.GetUserVideoLibraryPath(dataStream.UserAccountId, dataStream.UserProfileId, dataStream.LibraryId);
            string serverFilePath = Path.Combine(userVideoPath, dataStream.FileName);
            using (FileStream fileStream = new FileStream(serverFilePath, FileMode.OpenOrCreate))
            {
                dataStream.ByteStream.CopyTo(fileStream);
            }
        }

        public LibraryStreamData DownloadVideoFile(LibraryStreamData dataStream)
        {
            string userVideoPath = ServerApplicationData.Instance.GetUserVideoLibraryPath(dataStream.UserAccountId, dataStream.UserProfileId, dataStream.LibraryId);
            string serverFilePath = Path.Combine(userVideoPath, dataStream.FileName);
            if (string.IsNullOrEmpty(serverFilePath) == false && File.Exists(serverFilePath) == true)
            {
                FileStream fileStream = new FileStream(serverFilePath, FileMode.Open);
                return new LibraryStreamData() { ByteStream = fileStream };
            }
            return null;
        }

        public void UploadSettingsFile(LibraryStreamData dataStream)
        {
            string userVideoPath = ServerApplicationData.Instance.GetUserSettingsPath(dataStream.UserAccountId, dataStream.UserProfileId);
            string serverFilePath = Path.Combine(userVideoPath, dataStream.FileName);
            using (FileStream fileStream = new FileStream(serverFilePath, FileMode.OpenOrCreate))
            {
                dataStream.ByteStream.CopyTo(fileStream);
            }
        }

        public LibraryStreamData DownloadSettingsFile(LibraryStreamData dataStream)
        {
            string userVideoPath = ServerApplicationData.Instance.GetUserSettingsPath(dataStream.UserAccountId, dataStream.UserProfileId);
            string serverFilePath = Path.Combine(userVideoPath, dataStream.FileName);
            if (string.IsNullOrEmpty(serverFilePath) == false && File.Exists(serverFilePath) == true)
            {
                FileStream fileStream = new FileStream(serverFilePath, FileMode.Open);
                return new LibraryStreamData() { ByteStream = fileStream };
            }
            return null;
        }

        #endregion

        #region Private Methods

        private byte[] GenerateHash(byte[] fileData)
        {
            byte[] hash = null;
            using (MD5 md5Hash = MD5.Create())
            {
                hash = md5Hash.ComputeHash(fileData);
            }
            return hash;
        }

        #endregion
        
    }
}

