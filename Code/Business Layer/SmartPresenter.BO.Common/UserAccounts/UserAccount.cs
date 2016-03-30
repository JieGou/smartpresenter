using SmartPresenter.BO.Common.Enums;
using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.Common;
using SmartPresenter.Common.Extensions;
using SmartPresenter.Common.Logger;
using SmartPresenter.Data.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SmartPresenter.BO.Common.UserAccounts
{
    public abstract class UserAccount : IUserAccount
    {
        #region Private Members

        private UserAccountType _type;
        private IUserProfile _activeProfile;
        private Guid _id;        

        #endregion

        #region Constructor

        public UserAccount()
        {
            Initialize();
        }

        public UserAccount(Guid id)
        {
            _id = id;
            Initialize();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public virtual Guid Id { get { return _id; } }

        /// <summary>
        /// Gets or sets the email Id of User.
        /// </summary>
        /// <value>
        /// The email Id.
        /// </value>
        public virtual string Email { get; set; }

        /// <summary>
        /// Gets or sets the default profile.
        /// </summary>
        /// <value>
        /// The default profile.
        /// </value>
        public virtual IUserProfile DefaultProfile { get; set; }

        /// <summary>
        /// Gets or sets the active profile.
        /// </summary>
        /// <value>
        /// The active profile.
        /// </value>
        public virtual IUserProfile ActiveProfile
        {
            get
            {
                return _activeProfile;
            }
            set
            {
                _activeProfile = value;                
            }
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public virtual UserAccountType Type { get { return _type; } }

        /// <summary>
        /// Gets or sets the list of user profiles.
        /// </summary>
        /// <value>
        /// The list of user profiles.
        /// </value>
        public virtual List<IUserProfile> UserProfiles { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public virtual SecureString Password { get; set; }

        #endregion

        #region Methods

        #region Public Methods

        public virtual void Save()
        {
            foreach(IUserProfile userProfile in UserProfiles)
            {
                userProfile.Save();
            }
        }

        public virtual string GetUserAccountPath()
        {
            string path = Path.Combine(ApplicationData.Instance.UserAccountsPath, this.Id.ToString());
            if(Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            path = Path.Combine(path, this.Id.ToString() + ".xml");
            return path;
        }

        public static string GetUserAccountPath(Guid id)
        {
            string path = Path.Combine(ApplicationData.Instance.UserAccountsPath, id.ToString());
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            path = Path.Combine(path, id.ToString() + ".xml");
            return path;
        }

        public static string GetUserMediaLibrariesFolderPath(Guid id)
        {
            string path = Path.Combine(ApplicationData.Instance.UserAccountsPath, id.ToString());
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            path = Path.Combine(path, "Media" + ".xml");
            return path;
        }

        public override bool Equals(object obj)
        {
            UserAccount userAccount = obj as UserAccount;
            if (userAccount == null)
            {
                return false;
            }
            return Id.Equals(userAccount.Id);
        }        

        #endregion

        #region Protected Methods

        protected virtual IUserProfile CreateUserProfile()
        {
            return null;
        }

        private void Initialize()
        {            
            Email = string.Empty;
            Password = new SecureString();
            UserProfiles = new List<IUserProfile>();
        }

        #endregion

        #region IXmlSerializable Members

        public virtual void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("Id", this.Id.ToString());
            writer.WriteAttributeString("Type", this.Type.ToString());
            writer.WriteAttributeString("DefaultProfileId", this.DefaultProfile.Id.ToString());
            writer.WriteAttributeString("ActiveProfileId", this.ActiveProfile.Id.ToString());
            writer.WriteStartElement("Profiles");
            foreach (IUserProfile userProfile in UserProfiles)
            {
                userProfile.WriteXml(writer);
            }
            writer.WriteEndElement();
        }

        public virtual XmlSchema GetSchema()
        {
            return null;
        }

        public virtual void ReadXml(XmlReader reader)
        {
            if (reader.Name.Equals("UserAccount"))
            {
                _id = Guid.Parse(reader["Id"].ToSafeString());
                string userType = reader["Type"].ToSafeString();
                UserAccountType type;
                Enum.TryParse(userType, out type);
                _type = type;
                reader.Read();
                if (reader.Name.Equals("Profiles"))
                {
                    reader.Read();
                    while (reader.Name.Equals("Profile"))
                    {
                        IUserProfile userProfile = CreateUserProfile();
                        userProfile.ReadXml(reader);
                        this.UserProfiles.Add(userProfile);
                        reader.Read();
                    }
                }
            }
        }

        #endregion

        #endregion        
    }
}
