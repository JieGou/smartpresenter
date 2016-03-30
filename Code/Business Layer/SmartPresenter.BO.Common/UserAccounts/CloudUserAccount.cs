using SmartPresenter.BO.Common.Enums;
using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.Common;
using SmartPresenter.Common.Extensions;
using SmartPresenter.Common.Logger;
using SmartPresenter.Data.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Xml;
using System.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using SmartPresenter.BO.Common.UserAccounts.Factory;

namespace SmartPresenter.BO.Common.UserAccounts
{
    ///// <summary>
    ///// A cloud based user account, which will be synced across systems.
    ///// </summary>
    //[XmlRoot("UserAccount")]
    //public class CloudUserAccount : UserAccount
    //{
    //    #region Constructor

    //    public CloudUserAccount()
    //        : base()
    //    {            
    //    }

    //    public CloudUserAccount(Guid id)
    //        : base(id)
    //    {
    //    }

    //    public CloudUserAccount(Guid id, string eMail, string displayName, string password)
    //        : this(id)
    //    {
    //        Email = eMail;
            
    //        //Password = password;
    //    }

    //    #endregion

    //    #region Properties


    //    /// <summary>
    //    /// Gets the type.
    //    /// </summary>
    //    /// <value>
    //    /// The type.
    //    /// </value>
    //    public override UserAccountType Type
    //    {
    //        get
    //        {
    //            return UserAccountType.Cloud;
    //        }
    //    }

    //    #endregion

    //    #region Methods

    //    #region Public Methods        
    //    public override void Save()
    //    {
    //        Logger.LogEntry();

    //        Serializer<CloudUserAccount> serializer = new Serializer<CloudUserAccount>();
    //        string path = GetUserAccountPath();    
    //        serializer.Save(this, path);

    //        base.Save();

    //        Logger.LogExit();
    //    }

    //    public static IUserAccount Load(Guid id)
    //    {
    //        Logger.LogEntry();

    //        try
    //        {
    //            Serializer<CloudUserAccount> serializer = new Serializer<CloudUserAccount>();
    //            CloudUserAccount userAccount = serializer.Load(CloudUserAccount.GetUserAccountPath(id));
    //            foreach (IUserProfile userProfile in userAccount.UserProfiles)
    //            {
    //                userProfile.Load();
    //            }

    //            Logger.LogExit();
    //            return userAccount;
    //        }
    //        catch (FileNotFoundException)
    //        {
    //            Logger.LogExit();
    //            return null;
    //        }
    //    }

    //    protected override IUserProfile CreateUserProfile()
    //    {
    //        IUserProfileFactory userProfileFactory = new UserProfileFactory();
    //        return userProfileFactory.CreateCloudUserPrfoile(this.Id, string.Empty);            
    //    }

    //    #endregion

    //    #region IXmlSerializable Members

    //    public override XmlSchema GetSchema()
    //    {
    //        return null;
    //    }

    //    public override void ReadXml(XmlReader reader)
    //    {
    //        Guid defaultProfileId = Guid.Parse(reader["DefaultProfileId"].ToSafeString());
    //        Guid activeProfileId = Guid.Parse(reader["ActiveProfileId"].ToSafeString());
    //        ActiveProfile = UserProfiles.FirstOrDefault(profile => profile.Id.CompareTo(activeProfileId) == 0);
    //        DefaultProfile = UserProfiles.FirstOrDefault(profile => profile.Id.CompareTo(defaultProfileId) == 0);
    //        base.ReadXml(reader);
    //    }

    //    public override void WriteXml(XmlWriter writer)
    //    {
    //        base.WriteXml(writer);
    //    }

    //    #endregion

    //    #endregion
    //}
}

