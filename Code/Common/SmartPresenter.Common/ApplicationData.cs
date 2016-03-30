using System;
using System.IO;
namespace SmartPresenter.Common
{
    /// <summary>
    /// Class to hold information about various folders which stores application data.
    /// </summary>
    public sealed class ApplicationData
    {
        #region Constants

        private const string Root_Folder = "SmartPresenter";
        private const string Users_Folder = "Users";
        private const string Application_Settings = "ApplicationSettings.sppref";

        #endregion

        #region Private Data Members

        // Single Instance of ApplicationData class.
        private static ApplicationData _instance;
        // Synchronization Lock.
        private static volatile Object _lockObject = new Object();

        #endregion

        #region Private Constructor

        /// <summary>
        /// Creates a new ApplicationData object.
        /// </summary>
        private ApplicationData()
        {
            Initialize();
        }

        #endregion

        #region Properties

        public static ApplicationData Instance
        {
            get
            {
                if(_instance == null)
                {
                    lock(_lockObject)
                    {
                        if(_instance == null)
                        {
                            _instance = new ApplicationData();
                        }
                    }
                }
                return _instance;
            }
        }

        public string ApplicationSettingsPath { get; private set; }

        /// <summary>
        /// Gets the user accounts path.
        /// </summary>
        /// <value>
        /// The user accounts path.
        /// </value>
        public string UserAccountsPath { get; private set; }

        #endregion

        #region Methods

        private void Initialize()
        {
            ApplicationSettingsPath = GetApplicationSettingsPath();
            UserAccountsPath = GetUserAccountsPath();
        }

        private string GetApplicationSettingsPath()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Root_Folder);
            if(Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            path = Path.Combine(path, Application_Settings);
            return path;
        }

        /// <summary>
        /// Gets the user accounts path.
        /// </summary>
        /// <returns></returns>
        private static string GetUserAccountsPath()
        {
            string rootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Root_Folder, Users_Folder);

            return rootPath;
        }

        #endregion

    }
}
