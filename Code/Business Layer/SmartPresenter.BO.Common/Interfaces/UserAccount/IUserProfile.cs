
using System;
using System.Xml.Serialization;
namespace SmartPresenter.BO.Common.Interfaces
{
    /// <summary>
    /// Interface for creating User Profiles.
    /// </summary>
    public interface IUserProfile : IXmlSerializable
    {
        #region Properties

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>

        Guid Id { get; }
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>

        string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the profile picture path.
        /// </summary>
        /// <value>
        /// The profile picture path.
        /// </value>
        string ProfilePicturePath { get; set; }

        /// <summary>
        /// Gets the media thumbnails path.
        /// </summary>
        /// <value>
        /// The media thumbnails path.
        /// </value>
        string MediaThumbnailsPath { get; }

        /// <summary>
        /// Gets the media libraries path.
        /// </summary>
        string MediaLibrariesFolderPath { get; }

        /// <summary>
        /// Gets the document libraries folder path.
        /// </summary>
        /// <value>
        /// The document libraries folder path.
        /// </value>
        string DocumentLibrariesFolderPath { get; }

        /// <summary>
        /// Gets the general settings.
        /// </summary>
        /// <value>
        /// The general settings.
        /// </value>
        GeneralSettings GeneralSettings { get; }

        /// <summary>
        /// Gets the social media settings.
        /// </summary>
        /// <value>
        /// The social media settings.
        /// </value>
        SocialMediaSettings SocialMediaSettings { get; }

        /// <summary>
        /// Gets the display settings.
        /// </summary>
        /// <value>
        /// The display settings.
        /// </value>
        DisplaySettings DisplaySettings { get; }

        /// <summary>
        /// Gets the synchronization settings.
        /// </summary>
        /// <value>
        /// The synchronization settings.
        /// </value>
        SyncSettings SyncSettings { get; }

        /// <summary>
        /// Gets the UI settings.
        /// </summary>
        /// <value>
        /// The UI settings.
        /// </value>
        UISettings UISettings { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Saves this instance.
        /// </summary>
        void Save();

        /// <summary>
        /// Loads this instance.
        /// </summary>
        /// <returns></returns>
        void Load();

        #endregion
    }
}

