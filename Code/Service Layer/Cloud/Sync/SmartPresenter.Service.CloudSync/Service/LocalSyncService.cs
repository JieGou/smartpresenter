using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace SmartPresenter.Service.LocalSync
{
    public class LocalSyncService : SyncService
    {
        #region Constructor

        public LocalSyncService() : base(new LocalStore())
        {

        }

        #endregion
    }
}