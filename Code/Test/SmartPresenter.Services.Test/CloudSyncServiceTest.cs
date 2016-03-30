using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading.Tasks;

namespace SmartPresenter.Services.Test
{
    [TestClass]
    public class CloudSyncServiceTest
    {
        [TestMethod]
        public void UploadLibraryDocumentTest()
        {
            using (CloudSyncServiceReference.CloudSyncServiceClient client = new CloudSyncServiceReference.CloudSyncServiceClient())
            {
                client.UploadDocument("New Presentation.spd", Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), File.OpenRead(@"C:\Users\vtanwer\Desktop\kl\New Presentation.spd"));
            }
        }

        [TestMethod]
        public void DownloadLibraryDocumentTest()
        {
            using (CloudSyncServiceReference.CloudSyncServiceClient client = new CloudSyncServiceReference.CloudSyncServiceClient())
            {
                
            }
        }

        [TestMethod]
        public void GetAllUsersTest()
        {
            using(CloudDBServiceReference.CloudDBServiceClient client = new CloudDBServiceReference.CloudDBServiceClient())
            {
                var items = client.GetAllUsers();
            }
        }

        [TestMethod]
        public void AddUserTest()
        {
            using (CloudDBServiceReference.CloudDBServiceClient client = new CloudDBServiceReference.CloudDBServiceClient())
            {
                client.AddUser(new CloudDBServiceReference.UserAccount()
                {
                    Id = Guid.NewGuid(),
                    EMail = @"vibhore.tanwer@gmail.com",
                    Password = "passpass",
                    Type = "Local"
                });
            }
        }

        [TestMethod]
        public void UpdateUserTest()
        {
            using (CloudDBServiceReference.CloudDBServiceClient client = new CloudDBServiceReference.CloudDBServiceClient())
            {
                client.UpdateUser(new CloudDBServiceReference.UserAccount()
                {
                    Id = Guid.Parse(@"de0c5b8b-53ee-4ed8-bfdd-9b3d50391240"),
                    EMail = @"vibhore.tanwer@hotmail.com",
                    Password = "passmypass",
                    Type = "Local"
                });
            }
        }
    }
}
