using SmartPresenter.BO.Common.Entities;
using SmartPresenter.BO.Common.Interfaces.Sync;
using SmartPresenter.BO.Common.LocalSyncServiceReference;
using System;
namespace SmartPresenter.BO.Common.Sync
{
	public class LocalSyncServer : ISync
    {
        #region Private Data Members

        private static Object _lockObject = new Object();
        private static volatile LocalSyncServer _instance;
        private SyncServiceClient _client = new SyncServiceClient();

        #endregion

        #region Constructor

        private LocalSyncServer()
        {
                      
        }

        #endregion

        #region Properties

        private string _location
		{
			get;
			set;
		}

        public static LocalSyncServer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new LocalSyncServer();
                        }
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Methods

        #region ISync Members

        public virtual void SyncAll()
		{
            SyncPresentationLibrary();
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
            Guid userId = ApplicationSettings.Instance.ActiveUserAccount.ActiveProfile.Id;
            foreach (PresentationLibrary library in ApplicationSettings.Instance.ActiveUserAccount.ActiveProfile.GeneralSettings.PresentationLibraries)
            {
                foreach (Presentation presentation in library.Items)
                {
                    _client.DownloadFile(userId, library.Id, presentation.Id, presentation.Path, SyncFileType.Presentation);
                }
            }	
		}

		public virtual void SyncSettingsUp()
		{
			throw new System.NotImplementedException();
		}

		public virtual void SyncPresentationLibraryUp()
		{
            Guid userId = ApplicationSettings.Instance.ActiveUserAccount.ActiveProfile.Id;
            foreach (PresentationLibrary library in ApplicationSettings.Instance.ActiveUserAccount.ActiveProfile.GeneralSettings.PresentationLibraries)
            {
                foreach(Presentation presentation in library.Items)
                {
                    _client.UploadFile(userId, library.Id, presentation.Id, presentation.Path, SyncFileType.Presentation);
                }
            }
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
            SyncPresentationLibraryUp();
            SyncPresentationLibraryDown();
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

        #endregion

    }
}

