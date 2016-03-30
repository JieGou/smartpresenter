using SmartPresenter.Common;
using SmartPresenter.Common.Extensions;
using SmartPresenter.Common.Logger;
using SmartPresenter.Data.Common;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SmartPresenter.BO.Common
{
    /// <summary>
    /// Class for storing Social Media Settings
    /// </summary>
    public sealed class SocialMediaSettings : SettingsBase<SocialMediaSettings>, IXmlSerializable
    {
        #region Private Members

        #endregion

        #region Constructor

        /// <summary>
        /// Prevents a default instance of the <see cref="SocialMediaSettings"/> class from being created.
        /// </summary>
        public SocialMediaSettings()
        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>
        /// The access token.
        /// </value>
        public string FacebookAccessToken { get; set; }

        /// <summary>
        /// Gets or sets the is logged in.
        /// </summary>
        /// <value>
        /// The is logged in.
        /// </value>
        public bool IsLoggedIn { get; set; }

        #endregion

        #region Methods

        #region Public Methods

        public override void Save(string path)
        {
            Logger.LogEntry();

            Serializer<SocialMediaSettings> serializer = new Serializer<SocialMediaSettings>();
            serializer.Save(this, path);

            Logger.LogExit();
        }

        public override SocialMediaSettings Load(string path)
        {
            Logger.LogEntry();

            try
            {
                Serializer<SocialMediaSettings> serializer = new Serializer<SocialMediaSettings>();
                SocialMediaSettings SocialMediaSettings = serializer.Load(path);
                if (SocialMediaSettings == null)
                {
                    SocialMediaSettings = new SocialMediaSettings();
                }

                Logger.LogExit();
                return SocialMediaSettings;
            }
            catch (FileNotFoundException)
            {
                Logger.LogExit();
                return null;
            }
        }

        #endregion

        #region Private Methods

        #region IXmlSerializable Members

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.Read();
            FacebookAccessToken = reader["AccessToken"].ToSafeString();
            IsLoggedIn = reader["IsLoggedIn"].ToBool();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Facebook");
            writer.WriteAttributeString("AccessToken", FacebookAccessToken);
            writer.WriteAttributeString("IsLoggedIn", IsLoggedIn.ToString());
            writer.WriteEndElement();
        }

        #endregion

        #endregion

        #endregion
        
    }
}
