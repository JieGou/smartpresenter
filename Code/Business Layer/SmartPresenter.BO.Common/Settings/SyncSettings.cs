using SmartPresenter.Common.Logger;
using System;
using System.IO;

namespace SmartPresenter.BO.Common
{
    public sealed class SyncSettings : SettingsBase<SyncSettings>
    {

        #region Constants

        private const string Local_Sync_Settings = "LocalSyncSettings.sppref";
        private const string Cloud_Sync_Settings = "CloudSyncSettings.sppref";

        #endregion

        #region Private Members

        #endregion

        #region Constructor

        public SyncSettings()
        {
            Initialize();
        }   

        #endregion

        #region Properties  

        public LocalSyncSettings LocalSyncSettings { get; set; }

        public CloudSyncSettings CloudSyncSettings { get; set; }

        #endregion

        #region Methods

        #region Public Methods

        public override void Save(string path)
        {
            Logger.LogEntry();

            try
            {
                string localSyncSettingsPath = Path.Combine(path, Local_Sync_Settings);
                string cloudSyncSettingsPath = Path.Combine(path, Cloud_Sync_Settings);

                LocalSyncSettings.Save(localSyncSettingsPath);
                CloudSyncSettings.Save(cloudSyncSettingsPath);
            }
            finally
            {
                Logger.LogExit();
            }
        }

        public override SyncSettings Load(string path)
        {
            Logger.LogEntry();
            
            try
            {
                string localSyncSettingsPath = Path.Combine(path, Local_Sync_Settings);
                string cloudSyncSettingsPath = Path.Combine(path, Cloud_Sync_Settings);

                SyncSettings syncSettings = new SyncSettings();

                syncSettings.LocalSyncSettings = LocalSyncSettings.Load(localSyncSettingsPath);
                syncSettings.CloudSyncSettings = CloudSyncSettings.Load(cloudSyncSettingsPath);

                return syncSettings;
            }
            finally
            {
                Logger.LogExit();
            }
        }

        private void Initialize()
        {
            LocalSyncSettings = new LocalSyncSettings();
            CloudSyncSettings = new CloudSyncSettings();
        }

        #endregion

        #endregion
        
    }
}
