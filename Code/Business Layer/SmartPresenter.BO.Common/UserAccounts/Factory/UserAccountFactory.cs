using System;
using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.BO.Common.UserAccounts.Factory;
using SmartPresenter.Common.Logger;
using SmartPresenter.Common.Extensions;
using SmartPresenter.Data.Common;
using SmartPresenter.BO.Common.Enums;
using System.Xml.Serialization;
using System.Xml;
using System.Linq;
using System.Xml.Schema;
using System.IO;

namespace SmartPresenter.BO.Common.UserAccounts
{
    /// <summary>
    /// A Factory class to create different types of User Accounts.
    /// </summary>
    public class UserAccountFactory : IUserAccountFactory
    {
        public virtual IUserAccount CreateLocalUserAccount(string eMail, string displayName, string password)
        {
            IUserAccount userAccount = new LocalUserAccount(Guid.NewGuid(), eMail, displayName, password);
            
            IUserProfileFactory userProfileFactory = new UserProfileFactory();
            userAccount.UserProfiles.Add(userProfileFactory.CreateLocalUserPrfoile(userAccount.Id, displayName));
            userAccount.ActiveProfile = userAccount.DefaultProfile = userAccount.UserProfiles[0];

            return userAccount;
        }

        public virtual IUserAccount CreateCloudUserAccount(string eMail, string displayName, string password)
        {
            IUserAccount userAccount = new CloudUserAccount(Guid.NewGuid(), eMail, displayName, password);

            IUserProfileFactory userProfileFactory = new UserProfileFactory();
            userAccount.UserProfiles.Add(userProfileFactory.CreateCloudUserPrfoile(userAccount.Id, displayName));
            userAccount.ActiveProfile = userAccount.DefaultProfile = userAccount.UserProfiles[0];

            return userAccount;
        }

    }

    /// <summary>
    /// A cloud based user account, which will be synced across systems.
    /// </summary>
    [XmlRoot("UserAccount")]
    public class CloudUserAccount : UserAccount
    {
        #region Constructor

        public CloudUserAccount()
            : base()
        {
        }

        public CloudUserAccount(Guid id)
            : base(id)
        {
        }

        public CloudUserAccount(Guid id, string eMail, string displayName, string password)
            : this(id)
        {
            Email = eMail;

            //Password = password;
        }

        #endregion

        #region Properties


        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public override UserAccountType Type
        {
            get
            {
                return UserAccountType.Cloud;
            }
        }

        #endregion

        #region Methods

        #region Public Methods
        public override void Save()
        {
            Logger.LogEntry();

            Serializer<CloudUserAccount> serializer = new Serializer<CloudUserAccount>();
            string path = GetUserAccountPath();
            serializer.Save(this, path);

            base.Save();

            Logger.LogExit();
        }

        public static IUserAccount Load(Guid id)
        {
            Logger.LogEntry();

            try
            {
                Serializer<CloudUserAccount> serializer = new Serializer<CloudUserAccount>();
                CloudUserAccount userAccount = serializer.Load(CloudUserAccount.GetUserAccountPath(id));
                foreach (IUserProfile userProfile in userAccount.UserProfiles)
                {
                    userProfile.Load();
                }

                Logger.LogExit();
                return userAccount;
            }
            catch (FileNotFoundException)
            {
                Logger.LogExit();
                return null;
            }
        }

        protected override IUserProfile CreateUserProfile()
        {
            IUserProfileFactory userProfileFactory = new UserProfileFactory();
            return userProfileFactory.CreateCloudUserPrfoile(this.Id, string.Empty);
        }

        #endregion

        #region IXmlSerializable Members

        public override XmlSchema GetSchema()
        {
            return null;
        }

        public override void ReadXml(XmlReader reader)
        {
            Guid defaultProfileId = Guid.Parse(reader["DefaultProfileId"].ToSafeString());
            Guid activeProfileId = Guid.Parse(reader["ActiveProfileId"].ToSafeString());
            ActiveProfile = UserProfiles.FirstOrDefault(profile => profile.Id.CompareTo(activeProfileId) == 0);
            DefaultProfile = UserProfiles.FirstOrDefault(profile => profile.Id.CompareTo(defaultProfileId) == 0);
            base.ReadXml(reader);
        }

        public override void WriteXml(XmlWriter writer)
        {
            base.WriteXml(writer);
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// A Local user account which will be available only on local system.
    /// </summary>
    [XmlRoot("UserAccount")]
    public class LocalUserAccount : UserAccount
    {
        #region Constructor

        public LocalUserAccount()
            : base()
        {
            Initialize();
        }

        public LocalUserAccount(Guid id)
            : base(id)
        {
            Initialize();
        }

        public LocalUserAccount(Guid id, string eMail, string displayName, string password)
            : this(id)
        {
            Email = eMail;

            //Password = password;
        }

        #endregion

        #region Properties
        public override UserAccountType Type
        {
            get
            {
                return UserAccountType.Local;
            }
        }

        #endregion

        #region Methods

        #region Public Methods

        public override void Save()
        {
            Logger.LogEntry();

            Serializer<LocalUserAccount> serializer = new Serializer<LocalUserAccount>();
            string path = GetUserAccountPath();
            serializer.Save(this, path);

            base.Save();

            Logger.LogExit();
        }

        public static IUserAccount Load(Guid id)
        {
            Logger.LogEntry();

            try
            {
                Serializer<LocalUserAccount> serializer = new Serializer<LocalUserAccount>();
                LocalUserAccount userAccount = serializer.Load(LocalUserAccount.GetUserAccountPath(id));
                foreach (IUserProfile userProfile in userAccount.UserProfiles)
                {
                    userProfile.Load();
                }

                Logger.LogExit();
                return userAccount;
            }
            catch (FileNotFoundException)
            {
                Logger.LogExit();
                return null;
            }
        }

        #endregion

        #region Private Methods

        private void Initialize()
        {

        }

        protected override IUserProfile CreateUserProfile()
        {
            IUserProfileFactory userProfileFactory = new UserProfileFactory();
            return userProfileFactory.CreateLocalUserPrfoile(this.Id, string.Empty);
        }

        #endregion

        #region IXmlSerializable Members

        public override XmlSchema GetSchema()
        {
            return null;
        }

        public override void ReadXml(XmlReader reader)
        {
            Guid defaultProfileId = Guid.Parse(reader["DefaultProfileId"].ToSafeString());
            Guid activeProfileId = Guid.Parse(reader["ActiveProfileId"].ToSafeString());

            base.ReadXml(reader);

            ActiveProfile = UserProfiles.FirstOrDefault(profile => profile.Id.CompareTo(activeProfileId) == 0);
            DefaultProfile = UserProfiles.FirstOrDefault(profile => profile.Id.CompareTo(defaultProfileId) == 0);
        }

        public override void WriteXml(XmlWriter writer)
        {
            base.WriteXml(writer);
        }

        #endregion

        #endregion
    }
}

