using SmartPresenter.BO.Common.Enums;
using SmartPresenter.BO.Common.UserAccounts;
using System;
using System.Collections.Generic;
using System.Security;
using System.Xml.Serialization;

namespace SmartPresenter.BO.Common.Interfaces
{
    /// <summary>
    /// An interface representing a User Account.
    /// </summary>
    public interface IUserAccount : IXmlSerializable
    {
        /// <summary>
        /// Gets or sets the email Id of User.
        /// </summary>
        /// <value>
        /// The email Id.
        /// </value>
        string Email { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        Guid Id { get; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        SecureString Password { get; set; }

        /// <summary>
        /// Gets or sets the default profile.
        /// </summary>
        /// <value>
        /// The default profile.
        /// </value>
        IUserProfile DefaultProfile { get; set; }

        /// <summary>
        /// Gets or sets the active profile.
        /// </summary>
        /// <value>
        /// The active profile.
        /// </value>
        IUserProfile ActiveProfile { get; set; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        UserAccountType Type { get; }

        /// <summary>
        /// Gets or sets the list of user profiles.
        /// </summary>
        /// <value>
        /// The list of user profiles.
        /// </value>
        List<IUserProfile> UserProfiles { get; set; }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        void Save();

        /// <summary>
        /// Loads this instance.
        /// </summary>
        /// <returns></returns>
        //IUserAccount Load();

    }
}
