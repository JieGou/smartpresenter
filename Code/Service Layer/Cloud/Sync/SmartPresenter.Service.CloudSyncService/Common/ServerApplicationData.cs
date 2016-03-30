using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SmartPresenter.Service.CloudSyncService.Common
{
    /// <summary>
        /// Class to hold information about various folders which stores application data.
        /// </summary>
    public sealed class ServerApplicationData
    {        
            #region Constants

            private const string Server_Root_Folder = @"E:\CloudServer";
            private const string Users_Folder = @"Users";
            private const string Profiles_Folder = @"Profiles";
            private const string Libraries_Folder = @"Libraries";
            private const string Document = @"Document";
            private const string User_Document_Library_Path_Format = @"{0}\Users\{1}\Profiles\{2}\Libraries\Document\{3}";
            private const string User_Media_Library_Path_Format = @"{0}\Users\{1}\Profiles\{2}\Libraries\Media\{3}";
            private const string User_Audio_Library_Path_Format = @"{0}\Users\{1}\Profiles\{2}\Libraries\Audio\{3}";
            
            private const string User_Image_Path_Format = @"{0}\Users\{1}\Profiles\{2}\Libraries\Image\{3}";
            private const string User_Video_Path_Format = @"{0}\Users\{1}\Profiles\{2}\Libraries\Video\{3}";
            private const string User_Audio_Path_Format = @"{0}\Users\{1}\Profiles\{2}\Libraries\Audio\{3}";
            private const string User_Settings_Path_Format = @"{0}\Users\{1}\Profiles\{2}\Settings";

            #endregion

            #region Private Data Members

            // Single Instance of ApplicationData class.
            private static ServerApplicationData _instance;
            // Synchronization Lock.
            private static volatile Object _lockObject = new Object();

            #endregion

            #region Private Constructor

            /// <summary>
            /// Creates a new ApplicationData object.
            /// </summary>
            private ServerApplicationData()
            {
                Initialize();
            }

            #endregion

            #region Properties

            public static ServerApplicationData Instance
            {
                get
                {
                    if (_instance == null)
                    {
                        lock (_lockObject)
                        {
                            if (_instance == null)
                            {
                                _instance = new ServerApplicationData();
                            }
                        }
                    }
                    return _instance;
                }
            }

            #endregion

            #region Methods

            #region Public Methods

            internal string GetUserDocumentLibraryPath(Guid userAccountId, Guid userProfileId, Guid documentLibraryId)
            {
                string userDocumentLibraryPath = string.Format(User_Document_Library_Path_Format, Server_Root_Folder, userAccountId, userProfileId, documentLibraryId);
                CheckOrCreateDirectory(userDocumentLibraryPath);
                return userDocumentLibraryPath;
            }

            internal string GetUserImageLibraryPath(Guid userAccountId, Guid userProfileId, Guid mediaLibraryId)
            {
                string userImageLibraryPath = string.Format(User_Image_Path_Format, Server_Root_Folder, userAccountId, userProfileId, mediaLibraryId);
                CheckOrCreateDirectory(userImageLibraryPath);
                return userImageLibraryPath;
            }
            internal string GetUserVideoLibraryPath(Guid userAccountId, Guid userProfileId, Guid mediaLibraryId)
            {
                string userVideoLibraryPath = string.Format(User_Video_Path_Format, Server_Root_Folder, userAccountId, userProfileId, mediaLibraryId);
                CheckOrCreateDirectory(userVideoLibraryPath);
                return userVideoLibraryPath;
            }

            internal string GetUserAudioLibraryPath(Guid userAccountId, Guid userProfileId, Guid audioLibraryId)
            {
                string userAudioLibraryPath = string.Format(User_Audio_Path_Format, Server_Root_Folder, userAccountId, userProfileId, audioLibraryId);
                CheckOrCreateDirectory(userAudioLibraryPath);
                return userAudioLibraryPath;
            }

            internal string GetUserSettingsPath(Guid userAccountId, Guid userProfileId)
            {
                string userSettingsPath = string.Format(User_Settings_Path_Format, Server_Root_Folder, userAccountId, userProfileId);

                return userSettingsPath;
            }

            #endregion

            #region Private Methods

            private void Initialize()
            {                                
            }

            private void CheckOrCreateDirectory(string directoryPath)
            {
                if (Directory.Exists(directoryPath) == false)
                {
                    Directory.CreateDirectory(directoryPath);
                }
            }

            #endregion

            #endregion

    }
}