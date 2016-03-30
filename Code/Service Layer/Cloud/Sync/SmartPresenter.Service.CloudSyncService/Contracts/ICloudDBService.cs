using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SmartPresenter.Service.CloudSyncService
{
    [ServiceContract]
    public interface ICloudDBService
    {
        [OperationContract]
        List<UserAccount> GetAllUsers();

        [OperationContract]
        void AddUser(UserAccount userAccount);

        [OperationContract]
        void UpdateUser(UserAccount userAccount);

        [OperationContract]
        void DeleteUser(UserAccount userAccount);
    }
}
