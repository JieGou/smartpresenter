using SmartPresenter.Service.LocalSync;
using SmartPresenter.Service.LocalSyncService.CloudStorageServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPresenter.Service.LocalSync
{
    public sealed class CloudStorageServiceManager
    {
        #region Private Data Members

        private static Object _lockObject = new Object();
        private static volatile CloudStorageServiceManager _instance;
        private CloudSyncServiceClient _client;

        #endregion

        #region Constructor

        private CloudStorageServiceManager()
        {

        }

        #endregion

        #region Properties

        public static CloudStorageServiceManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    lock(_lockObject)
                    {
                        if(_instance == null)
                        {
                            _instance = new CloudStorageServiceManager();
                        }
                    }
                }
                return _instance;
            }
        }

        public CloudSyncServiceClient Client
        {
            get
            {
                if(_client == null)
                {
                    _client = new CloudSyncServiceClient();
                }
                return _client;
            }
        }

        #endregion
    }
}
