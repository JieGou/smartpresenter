
using SmartPresenter.BO.Common.Enums;
using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.BO.Common.UserAccounts;
using SmartPresenter.Common;
using SmartPresenter.Common.Extensions;
using SmartPresenter.Common.Logger;
using SmartPresenter.Data.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml;
using System.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
namespace SmartPresenter.BO.Common
{
    [XmlRoot("ApplicationSettings")]
    public class ApplicationSettings : IXmlSerializable
    {
        #region Constants

        private const string Root_Folder = "SmartPresenter";
        private const string Users_Folder = "Users";

        #endregion

        #region Private Data Members

        private static Object _lockObject = new Object();
        private static volatile ApplicationSettings _instance;
        private volatile bool _isLoaded = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Prevents a default instance of the <see cref="ApplicationSettings"/> class from being created.
        /// </summary>
        private ApplicationSettings()
        {
            Initialize();            
        }

        #endregion

        #region Properties

        public static ApplicationSettings Instance
        {
            get
            {
                if(_instance == null)
                {
                    lock(_lockObject)
                    {
                        Load();
                        if(_instance == null)
                        {
                            _instance = new ApplicationSettings();
                        }
                    }
                }
                return _instance;
            }
        }

        public List<IUserAccount> UserAccounts { get; set; }

        public IUserAccount ActiveUserAccount { get; set; }        

        #endregion

        #region Methods

        #region Private Methods

        /// <summary>
        /// Gets the user accounts path.
        /// </summary>
        /// <returns></returns>
        private string GetUserAccountsPath()
        {
            string rootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Root_Folder, Users_Folder);

            if (Directory.Exists(rootPath) == false)
            {
                Directory.CreateDirectory(rootPath);
            }

            return rootPath;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            lock (_lockObject)
            {
                if (_isLoaded == false)
                {
                    UserAccounts = new List<IUserAccount>();
                    // In case of Unit Testing it will be null.
                    if (Application.Current != null)
                    {
                        Application.Current.Dispatcher.ShutdownStarted -= Dispatcher_ShutdownStarted;
                        Application.Current.Dispatcher.ShutdownStarted += Dispatcher_ShutdownStarted;
                    }                    
                    _isLoaded = true;
                }
            }
        }

        private static void ValidateUsers()
        {
            if(_instance != null)
            {
                if(_instance.UserAccounts.Count == 0)
                {
                    IUserAccountFactory userAccountFactory = new UserAccountFactory();
                    _instance.UserAccounts.Add(userAccountFactory.CreateLocalUserAccount(string.Empty, string.Empty, string.Empty));
                    _instance.ActiveUserAccount = _instance.UserAccounts[0];
                }
            }
        }
        
        /// <summary>
        /// Handles the ShutdownStarted event of the Dispatcher control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Dispatcher_ShutdownStarted(object sender, EventArgs e)
        {
            Save();
        }        

        #endregion

        #region Public Methods

        /// <summary>
        /// Loads this instance.
        /// </summary>
        public static void Load()
        {
            Logger.LogEntry();

            try
            {
                Serializer<ApplicationSettings> serializer = new Serializer<ApplicationSettings>();
                _instance = serializer.Load(ApplicationData.Instance.ApplicationSettingsPath);
                if (_instance == null)
                {
                    _instance = new ApplicationSettings();
                }
                ValidateUsers();
                Logger.LogExit();
            }
            catch (FileNotFoundException)
            {
                if (_instance == null)
                {
                    _instance = new ApplicationSettings();
                }
                ValidateUsers();
                Logger.LogExit();
            }
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            Logger.LogEntry();

            Serializer<ApplicationSettings> serializer = new Serializer<ApplicationSettings>();
            serializer.Save(this, ApplicationData.Instance.ApplicationSettingsPath);

            foreach (IUserAccount userAccount in UserAccounts)
            {
                userAccount.Save();
            }

            Logger.LogExit();
        }

        #endregion

        #region IXmlSerializable Members

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            if (reader.Name.Equals("ApplicationSettings"))
            {
                reader.Read();
                if (reader.Name.Equals("UserAccounts"))
                {
                    Guid activeUserAccountId = Guid.Parse(reader["ActiveUserAccountId"].ToSafeString());
                    reader.Read();
                    while (reader.Name.Equals("UserAccount"))
                    {
                        IUserAccount userAccount = null;
                        string type = reader["Type"].ToSafeString();
                        UserAccountType accountType;
                        Enum.TryParse(type, out accountType);
                        Guid id = Guid.Parse(reader["Id"].ToSafeString());
                        if (Guid.Empty.CompareTo(id) == 0)
                        {
                            IUserAccountFactory userAccountFactory = new UserAccountFactory();
                            if (accountType == UserAccountType.Cloud)
                            {
                                userAccount = userAccountFactory.CreateCloudUserAccount(string.Empty, string.Empty, string.Empty);
                            }
                            else if (accountType == UserAccountType.Local)
                            {
                                userAccount = userAccountFactory.CreateLocalUserAccount(string.Empty, string.Empty, string.Empty);
                            }
                        }
                        else
                        {
                            if (accountType == UserAccountType.Cloud)
                            {
                                userAccount = CloudUserAccount.Load(id);
                            }
                            else if (accountType == UserAccountType.Local)
                            {
                                userAccount = LocalUserAccount.Load(id);
                            }
                        }
                        userAccount.Email = reader["Email"].ToSafeString();
                        reader["Password"].ToSafeString().ToCharArray().ForEach(ch => userAccount.Password.AppendChar(ch));                        
                        UserAccounts.Add(userAccount);
                        reader.Read();
                    }                    
                    ActiveUserAccount = UserAccounts.FirstOrDefault(account => account.Id.CompareTo(activeUserAccountId) == 0);                    
                }
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("UserAccounts");
            writer.WriteAttributeString("ActiveUserAccountId", this.ActiveUserAccount.Id.ToString());
            foreach(IUserAccount userAccount in this.UserAccounts)
            {
                writer.WriteStartElement("UserAccount");
                writer.WriteAttributeString("Id", userAccount.Id.ToString());
                writer.WriteAttributeString("Email", userAccount.Email);
                writer.WriteAttributeString("Password", userAccount.Password.ToString());
                writer.WriteAttributeString("Type", userAccount.Type.ToString());                
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        #endregion

        #endregion
        
    }
}
