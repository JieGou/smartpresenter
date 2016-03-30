using System.ServiceModel;

namespace SmartPresenter.Service.LocalSync
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class CloudSyncService : SyncService
    {
        #region Constructor

        public CloudSyncService()
            : base(new CloudStore())
        {

        }

        #endregion
    }
}
