using SmartPresenter.BO.Common.CloudSyncServiceReference;
using SmartPresenter.BO.Common.Entities;
using SmartPresenter.BO.Common.Interfaces.Sync;
using System;


namespace SmartPresenter.BO.Common.Sync
{
	public class CloudSyncServer : ISync
    {
        #region Private Data Members

        private static Object _lockObject = new Object();
        private static volatile CloudSyncServer _instance;
        private SyncServiceClient _client = new SyncServiceClient();

        #endregion

        #region Constructor

        private CloudSyncServer()
        {

        }

        #endregion

        #region Properties

        public static CloudSyncServer Instance
        {
            get
            {
                if(_instance == null)
                {
                    lock(_lockObject)
                    {
                        if(_instance == null)
                        {
                            _instance = new CloudSyncServer();
                        }
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region ISync Members

        public virtual void SyncAll()
		{
			throw new System.NotImplementedException();            
		}

		public virtual void SyncAudioLibraryDown()
		{
			throw new System.NotImplementedException();
		}

		public virtual void SyncMediaLibraryDown()
		{
			throw new System.NotImplementedException();
		}

		public virtual void SyncPresentationLibraryDown()
		{
            //foreach(PresentationLibrary library in Settings.GeneralSettings.PresentationLibraries)
            //{
                
            //}
		}

		public virtual void SyncSettingsUp()
		{
			throw new System.NotImplementedException();
		}

		public virtual void SyncPresentationLibraryUp()
		{
			throw new System.NotImplementedException();
		}

		public virtual void SyncMediaLibraryUp()
		{
			throw new System.NotImplementedException();
		}

		public virtual void SyncAudioLibraryUp()
		{
			throw new System.NotImplementedException();
		}

		public virtual void SyncAudioLibrary()
		{
			throw new System.NotImplementedException();
		}

		public virtual void SyncMediaLibrary()
		{
			throw new System.NotImplementedException();
		}

		public virtual void SyncPresentationLibrary()
		{
			throw new System.NotImplementedException();
		}

		public virtual void SyncSettings()
		{
			throw new System.NotImplementedException();
		}

		public virtual void SyncAllUp()
		{
			throw new System.NotImplementedException();
		}

		public virtual void SyncAllDown()
		{
			throw new System.NotImplementedException();
        }

        #endregion
    }
}

