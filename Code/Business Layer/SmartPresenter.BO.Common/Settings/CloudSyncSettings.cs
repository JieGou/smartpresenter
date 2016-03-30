using SmartPresenter.Common;
using SmartPresenter.Common.Logger;
using SmartPresenter.Data.Common;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SmartPresenter.BO.Common
{
    public sealed class CloudSyncSettings : SettingsBase<CloudSyncSettings>, IXmlSerializable
    {
        #region Private Members


        #endregion

        #region Constructor

        public CloudSyncSettings()
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        #region Public Methods

        public override void Save(string path)
        {
            Logger.LogEntry();

            Serializer<CloudSyncSettings> serializer = new Serializer<CloudSyncSettings>();
            serializer.Save(this, path);

            Logger.LogExit();
        }

        public override CloudSyncSettings Load(string path)
        {
            Logger.LogEntry();

            try
            {
                Serializer<CloudSyncSettings> serializer = new Serializer<CloudSyncSettings>();
                CloudSyncSettings cloudSyncSettings = serializer.Load(path);
                if (cloudSyncSettings == null)
                {
                    cloudSyncSettings = new CloudSyncSettings();
                }

                return cloudSyncSettings;
            }
            catch (FileNotFoundException)
            {
                return null;
            }
            finally
            {
                Logger.LogExit();
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
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Facebook");
            writer.WriteEndElement();
        }

        #endregion

        #endregion

        #endregion
        
    }
}
