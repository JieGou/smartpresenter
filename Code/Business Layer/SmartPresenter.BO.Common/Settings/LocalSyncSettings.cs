using SmartPresenter.Common;
using SmartPresenter.Common.Extensions;
using SmartPresenter.Common.Logger;
using SmartPresenter.Data.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SmartPresenter.BO.Common
{
    public sealed class LocalSyncSettings : SettingsBase<LocalSyncSettings>, IXmlSerializable
    {
        #region Private Members

        #endregion

        #region Constructor

        public LocalSyncSettings()
        {
            Initialize();            
        }

        #endregion

        #region Properties

        public List<string> Libraries { get; set; }
        
        public string SelectedLibraryPath { get; set; }

        #endregion

        #region Methods

        #region Public Methods

        public override void Save(string path)
        {
            Logger.LogEntry();

            Serializer<LocalSyncSettings> serializer = new Serializer<LocalSyncSettings>();
            serializer.Save(this, path);

            Logger.LogExit();
        }

        public override LocalSyncSettings Load(string path)
        {
            Logger.LogEntry();

            try
            {
                Serializer<LocalSyncSettings> serializer = new Serializer<LocalSyncSettings>();
                LocalSyncSettings localSyncSettings = serializer.Load(path);
                if (localSyncSettings == null)
                {
                    localSyncSettings = new LocalSyncSettings();
                }

                return localSyncSettings;
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
            if (reader.Name.Equals("Libraries"))
            {
                reader.Read();
                if (reader.Name.Equals("Library"))
                {                    
                    Libraries.Add(reader["Path"].ToSafeString());
                }
            }
        }

        public void WriteXml(XmlWriter writer)
        {            
            writer.WriteStartElement("Libraries");
            foreach (string library in Libraries)
            {
                writer.WriteStartElement("Library");
                writer.WriteAttributeString("Path", library);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();            
        }

        #endregion

        private void Initialize()
        {
            Libraries = new List<string>();
            Libraries.Add(@"C:\LocalSync");
        }

        #endregion

        #endregion
        
    }
}
