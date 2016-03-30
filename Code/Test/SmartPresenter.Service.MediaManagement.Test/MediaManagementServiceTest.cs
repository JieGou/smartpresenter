using System.Collections.Generic;
using System.Linq;
using System.Threading;
namespace SmartPresenter.Service.MediaManagement.Test
{
    public class MediaManagementServiceTest
    {
        public void GetAllMediaFilesTest()
        {
            MediaManagementServiceReference.MediaManagementServiceClient client = new MediaManagementServiceReference.MediaManagementServiceClient("httpEndpoint");
            List<string> allMediaFiles = client.GetAllMediaFiles().ToList();
            Thread.Sleep(1000);
            allMediaFiles = client.GetAllMediaFiles().ToList();
            Thread.Sleep(2000);
            allMediaFiles = client.GetAllMediaFiles().ToList();
            Thread.Sleep(2000);
            allMediaFiles = client.GetAllMediaFiles().ToList();
        }
    }
}
