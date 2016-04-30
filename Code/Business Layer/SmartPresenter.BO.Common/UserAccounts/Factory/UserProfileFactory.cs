using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.Common.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace SmartPresenter.BO.Common.UserAccounts.Factory
{
    public class UserProfileFactory : IUserProfileFactory
    {
        #region IUserProfileFactory Members

        public virtual IUserProfile CreateLocalUserPrfoile(Guid userAccountId, string displayName)
        {
            return new LocalUserProfile(userAccountId, Guid.NewGuid()) { DisplayName = displayName };
        }

        public virtual IUserProfile CreateCloudUserPrfoile(Guid userAccountId, string displayName)
        {
            return new CloudUserProfile(userAccountId, Guid.NewGuid()) { DisplayName = displayName };
        }

        #endregion    
    
        #region Inner classes

        /// <summary>
        /// A User Profile class will hold various user profile settings. There could be multiple user profiles per user account.
        /// </summary>
        public abstract class UserProfile : IUserProfile
        {
            #region Private Data Members

            private GeneralSettings _generalSettings;
            private SocialMediaSettings _socialMediaSettings;
            private DisplaySettings _displaySettings;
            private SyncSettings _syncSettings;
            private UISettings _uiSettings;

            private Guid _id;
            private Guid _userAccountId;
            protected UserAccountData _userAccountData;

            #endregion

            #region Constructor

            public UserProfile()
            {
                Initialize();
            }

            public UserProfile(Guid userAccountId, Guid id)
            {
                _userAccountId = userAccountId;
                _id = id;
                Initialize();
            }

            #endregion

            #region Methods

            #region IUserProfile Members

            public virtual string DisplayName { get; set; }


            /// <summary>
            /// Gets or sets the identifier.
            /// </summary>
            /// <value>
            /// The identifier.
            /// </value>
            public virtual Guid Id { get { return _id; } }

            /// <summary>
            /// Gets or sets the profile picture path.
            /// </summary>
            /// <value>
            /// The profile picture path.
            /// </value>
            public virtual string ProfilePicturePath { get; set; }

            /// <summary>
            /// Gets the general settings.
            /// </summary>
            /// <value>
            /// The general settings.
            /// </value>
            public virtual GeneralSettings GeneralSettings
            {
                get
                {
                    return _generalSettings;
                }
            }

            /// <summary>
            /// Gets the social media settings.
            /// </summary>
            /// <value>
            /// The social media settings.
            /// </value>
            public virtual SocialMediaSettings SocialMediaSettings
            {
                get
                {
                    return _socialMediaSettings;
                }
            }

            /// <summary>
            /// Gets the display settings.
            /// </summary>
            /// <value>
            /// The display settings.
            /// </value>
            public virtual DisplaySettings DisplaySettings
            {
                get
                {
                    return _displaySettings;
                }
            }

            /// <summary>
            /// Gets the synchronization settings.
            /// </summary>
            /// <value>
            /// The synchronization settings.
            /// </value>
            public virtual SyncSettings SyncSettings
            {
                get
                {
                    return _syncSettings;
                }
            }

            /// <summary>
            /// Gets the UI settings.
            /// </summary>
            /// <value>
            /// The UI settings.
            /// </value>
            public virtual UISettings UISettings
            {
                get
                {
                    return _uiSettings;
                }
            }

            /// <summary>
            /// Gets the media thumbnails path.
            /// </summary>
            /// <value>
            /// The media thumbnails path.
            /// </value>
            public string MediaThumbnailsPath
            {
                get
                {
                    return _userAccountData.MediaThumbnailsPath;
                }
            }

            /// <summary>
            /// Gets the media libraries path.
            /// </summary>
            public string MediaLibrariesFolderPath
            {
                get
                {
                    return _userAccountData.MediaLibrariesFolderPath;
                }
            }

            /// <summary>
            /// Gets the document libraries folder path.
            /// </summary>
            /// <value>
            /// The document libraries folder path.
            /// </value>
            public string DocumentLibrariesFolderPath
            {
                get
                {
                    return _userAccountData.DocumentLibrariesFolderPath;
                }
            }

            /// <summary>
            /// Saves this instance.
            /// </summary>
            public virtual void Save()
            {
                VerifyPath();
                _generalSettings.Save(_userAccountData.GeneralSettingsPath);
                _displaySettings.Save(_userAccountData.DisplaySettingsPath);
                _socialMediaSettings.Save(_userAccountData.SocialMediaSettingsPath);
                _syncSettings.Save(_userAccountData.GetSettingsRootPath());
                _uiSettings.Save(_userAccountData.UISettingsPath);
            }

            /// <summary>
            /// Loads this instance.
            /// </summary>
            /// <returns></returns>
            public virtual void Load()
            {
                _generalSettings = GeneralSettings.Load(_userAccountData.GeneralSettingsPath);              

                _displaySettings = DisplaySettings.Load(_userAccountData.DisplaySettingsPath);
                _socialMediaSettings = SocialMediaSettings.Load(_userAccountData.SocialMediaSettingsPath);
                _syncSettings = SyncSettings.Load(_userAccountData.GetSettingsRootPath());
                _uiSettings = UISettings.Load(_userAccountData.UISettingsPath);
            }

            internal string GetUserAccountsFolderPath()
            {
                return _userAccountData.UserAccountsPath;
            }

            #endregion

            #region IXmlSerializable Members

            public virtual XmlSchema GetSchema()
            {
                return null;
            }

            public virtual void ReadXml(XmlReader reader)
            {
                if (reader.Name.Equals("Profile"))
                {
                    _id = Guid.Parse(reader["Id"].ToSafeString());
                    DisplayName = reader["DisplayName"].ToSafeString();
                    ProfilePicturePath = reader["ProfilePicturePath"].ToSafeString();
                }
            }

            public virtual void WriteXml(XmlWriter writer)
            {
                writer.WriteStartElement("Profile");
                writer.WriteAttributeString("Id", Id.ToString());
                writer.WriteAttributeString("DisplayName", DisplayName.ToSafeString());
                writer.WriteAttributeString("ProfilePicturePath", ProfilePicturePath.ToSafeString());
                writer.WriteEndElement();
            }

            #endregion

            #region Private Methods

            private void Initialize()
            {
                _userAccountData = new UserAccountData(_userAccountId);
                DisplayName = string.Empty;
                ProfilePicturePath = string.Empty;

                _generalSettings = new GeneralSettings();
                _socialMediaSettings = new SocialMediaSettings();
                _displaySettings = new DisplaySettings();
                _syncSettings = new SyncSettings();
                _uiSettings = new UISettings();
            }

            private void VerifyPath()
            {
                if (File.Exists(Path.GetDirectoryName(_userAccountData.GeneralSettingsPath)) == false)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(_userAccountData.GeneralSettingsPath));
                }
            }

            #endregion

            #endregion

            #region UserAccountData Inner Class

            public class UserAccountData
            {
                #region Constants

                private const string Root_Folder = "SmartPresenter";
                private const string Users_Folder = "Users";
                private const string Settings_Folder = "Settings";
                private const string Media_Folder = "Media";
                private const string Document_Folder = "Document";
                private const string Audio_Folder = "Audio";
                private const string Media_Thumbnails_Folder = "Thumbnails";
                private const string Playlist_Root_Folder = "Playlists";
                private const string Libraries_Root_Folder = "Libraries";
                private const string General_Settings = "GeneralSettings.sppref";
                private const string Display_Settings = "DisplaySettings.sppref";
                private const string Social_Media_Settings = "SocialMediaSettings.sppref";
                private const string Local_Sync_Settings = "LocalSyncSettings.sppref";
                private const string Cloud_Sync_Settings = "CloudSyncSettings.sppref";
                private const string UI_Settings = "UISettings.sppref";
                private const string Default_Library_Folder_Name = "SmartPresenter";

                #endregion

                #region Private Data Members

                Guid _id;

                #endregion

                #region Constructor

                public UserAccountData(Guid id)
                {
                    _id = id;
                    Initialize();
                }

                #endregion

                #region Properties

                /// <summary>
                /// Location of GeneralSettings file.
                /// </summary>
                public string GeneralSettingsPath
                {
                    get;
                    private set;
                }

                /// <summary>
                /// Gets the social media settings path.
                /// </summary>
                /// <value>
                /// The social media settings path.
                /// </value>
                public string SocialMediaSettingsPath
                {
                    get;
                    private set;
                }

                /// <summary>
                /// Gets the display settings path.
                /// </summary>
                /// <value>
                /// The display settings path.
                /// </value>
                public string DisplaySettingsPath
                {
                    get;
                    private set;
                }

                /// <summary>
                /// Gets the local synchronization settings path.
                /// </summary>
                /// <value>
                /// The local synchronization settings path.
                /// </value>
                public string LocalSyncSettingsPath
                {
                    get;
                    private set;
                }

                /// <summary>
                /// Gets the cloud synchronization settings path.
                /// </summary>
                /// <value>
                /// The cloud synchronization settings path.
                /// </value>
                public string CloudSyncSettingsPath
                {
                    get;
                    private set;
                }

                /// <summary>
                /// Gets the UI settings path.
                /// </summary>
                /// <value>
                /// The UI settings path.
                /// </value>
                public string UISettingsPath
                {
                    get;
                    private set;
                }

                /// <summary>
                /// Default Location to store Library.
                /// </summary>
                public string DefaultLibraryLocation
                {
                    get;
                    private set;
                }

                /// <summary>
                /// Gets the document playlists folder path.
                /// </summary>
                /// <value>
                /// The document playlists folder path.
                /// </value>
                public string DocumentLibrariesFolderPath
                {
                    get;
                    private set;
                }

                /// <summary>
                /// Gets the audio libraries folder path.
                /// </summary>
                /// <value>
                /// The audio libraries folder path.
                /// </value>
                public string AudioLibrariesFolderPath
                {
                    get;
                    private set;
                }

                /// <summary>
                /// Gets the media playlists folder path.
                /// </summary>
                /// <value>
                /// The media playlists folder path.
                /// </value>
                public string MediaLibrariesFolderPath
                {
                    get;
                    private set;
                }

                /// <summary>
                /// Gets the media thumbnails path.
                /// </summary>
                /// <value>
                /// The media thumbnails path.
                /// </value>
                public string MediaThumbnailsPath
                {
                    get;
                    private set;
                }

                /// <summary>
                /// Gets the user accounts path.
                /// </summary>
                /// <value>
                /// The user accounts path.
                /// </value>
                public string UserAccountsPath
                {
                    get;
                    private set;
                }

                #endregion

                #region Methods

                private void Initialize()
                {
                    GeneralSettingsPath = GetGeneralSettingsPath();
                    DisplaySettingsPath = GetDisplaySettingsPath();
                    SocialMediaSettingsPath = GetSocialMediaSettingsPath();
                    LocalSyncSettingsPath = GetLocalSyncSettingsPath();
                    CloudSyncSettingsPath = GetCloudSyncSettingsPath();
                    UISettingsPath = GetUISettingsPath();
                    DefaultLibraryLocation = GetDefaultLibraryLocation();
                    DocumentLibrariesFolderPath = GetDocumentLibrariesPath();
                    MediaThumbnailsPath = GetMediaThumbnailPath();
                    MediaLibrariesFolderPath = GetMediaLibrariesPath();
                    UserAccountsPath = GetUserAccountsPath();
                }

                /// <summary>
                /// Gets the settings folder root path.
                /// </summary>
                /// <returns></returns>
                internal string GetSettingsRootPath()
                {
                    string settingsRootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Root_Folder, Users_Folder, _id.ToString(), Settings_Folder);

                    return settingsRootPath;
                }

                /// <summary>
                /// Gets the media root path.
                /// </summary>
                /// <returns></returns>
                private string GetMediaRootPath()
                {
                    string mediaRootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Root_Folder, Media_Folder);
                    // If its not already there then create a new fodler for it.
                    if (Directory.Exists(mediaRootPath) == false)
                    {
                        Directory.CreateDirectory(mediaRootPath);
                    }

                    return mediaRootPath;
                }

                /// <summary>
                /// Gets the media thumbnail path.
                /// </summary>
                /// <returns></returns>
                private string GetMediaThumbnailPath()
                {
                    string mediaThumbnailPath = Path.Combine(GetMediaRootPath(), Media_Thumbnails_Folder); ;
                    // If its not already there then create a new fodler for it.
                    if (Directory.Exists(mediaThumbnailPath) == false)
                    {
                        Directory.CreateDirectory(mediaThumbnailPath);
                    }
                    return mediaThumbnailPath;
                }

                /// <summary>
                /// Gives the path of GeneralSettings, if folder for settings does not exists, then it creates one.
                /// </summary>
                /// <returns>path of General Settings file</returns>
                private string GetGeneralSettingsPath()
                {
                    string settingsRootPath = GetSettingsRootPath();

                    return Path.Combine(settingsRootPath, General_Settings);
                }

                /// <summary>
                /// Gets the display settings path.
                /// </summary>
                /// <returns></returns>
                private string GetDisplaySettingsPath()
                {
                    string settingsRootPath = GetSettingsRootPath();

                    return Path.Combine(settingsRootPath, Display_Settings);
                }

                /// <summary>
                /// Gets the social media settings path.
                /// </summary>
                /// <returns></returns>
                private string GetSocialMediaSettingsPath()
                {
                    string settingsRootPath = GetSettingsRootPath();

                    return Path.Combine(settingsRootPath, Social_Media_Settings);
                }

                /// <summary>
                /// Gets the local synchronization settings path.
                /// </summary>
                /// <returns></returns>
                private string GetLocalSyncSettingsPath()
                {
                    string settingsRootPath = GetSettingsRootPath();

                    return Path.Combine(settingsRootPath, Local_Sync_Settings);
                }

                /// <summary>
                /// Gets the cloud synchronization settings path.
                /// </summary>
                /// <returns></returns>
                private string GetCloudSyncSettingsPath()
                {
                    string settingsRootPath = GetSettingsRootPath();

                    return Path.Combine(settingsRootPath, Cloud_Sync_Settings);
                }

                /// <summary>
                /// Gets the UI settings path.
                /// </summary>
                /// <returns></returns>
                private string GetUISettingsPath()
                {
                    string settingsRootPath = GetSettingsRootPath();

                    return Path.Combine(settingsRootPath, UI_Settings);
                }

                /// <summary>
                /// Gives location of Default Library.
                /// </summary>
                /// <returns></returns>
                private string GetDefaultLibraryLocation()
                {
                    return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Default_Library_Folder_Name);
                }

                /// <summary>
                /// Gets the playlists root path.
                /// </summary>
                /// <returns></returns>
                private string GetDocumentLibrariesPath()
                {
                    string rootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Root_Folder, Libraries_Root_Folder, Document_Folder);

                    if (Directory.Exists(rootPath) == false)
                    {
                        Directory.CreateDirectory(rootPath);
                    }

                    return rootPath;
                }

                /// <summary>
                /// Gets the audio libraries path.
                /// </summary>
                /// <returns></returns>
                private string GetAudioLibrariesPath()
                {
                    string rootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Root_Folder, Libraries_Root_Folder, Document_Folder);

                    if (Directory.Exists(rootPath) == false)
                    {
                        Directory.CreateDirectory(rootPath);
                    }

                    return rootPath;
                }

                /// <summary>
                /// Gets the media playlists path.
                /// </summary>
                /// <returns></returns>
                private string GetMediaLibrariesPath()
                {
                    string rootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Root_Folder, Libraries_Root_Folder, Media_Folder);

                    if (Directory.Exists(rootPath) == false)
                    {
                        Directory.CreateDirectory(rootPath);
                    }

                    return rootPath;
                }

                /// <summary>
                /// Gets the user accounts path.
                /// </summary>
                /// <returns></returns>
                public static string GetUserAccountsPath()
                {
                    string rootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Root_Folder, Users_Folder);

                    return rootPath;
                }

                /// <summary>
                /// Gets the user profile path.
                /// </summary>
                /// <param name="userId">The user identifier.</param>
                /// <returns></returns>
                public string GetUserProfilePath()
                {
                    string path = Path.Combine(UserAccountsPath, _id.ToString());
                    if (Directory.Exists(path) == false)
                    {
                        Directory.CreateDirectory(path);
                    }
                    return path;
                }

                #endregion
            }

            #endregion
        }

        private class CloudUserProfile : UserProfile
        {
            public CloudUserProfile()
                : base()
            {
            }

            public CloudUserProfile(Guid userAccountId, Guid id)
                : base(userAccountId, id)
            {
            }
        }

        private class LocalUserProfile : UserProfile
        {
            public LocalUserProfile()
                : base()
            {
            }

            public LocalUserProfile(Guid userAccountId, Guid id)
                : base(userAccountId, id)
            {
            }
        }

        #endregion
    }
}
