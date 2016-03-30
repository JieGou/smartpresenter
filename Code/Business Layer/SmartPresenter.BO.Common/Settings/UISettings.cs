using SmartPresenter.Common;
using SmartPresenter.Common.Logger;
using SmartPresenter.Data.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SmartPresenter.BO.Common
{
    public sealed class UISettings : SettingsBase<UISettings>, IXmlSerializable
    {
        #region Private Members

        #endregion

        #region Constructor

        public UISettings()
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

            Serializer<UISettings> serializer = new Serializer<UISettings>();
            serializer.Save(this, path);

            Logger.LogExit();
        }

        public override UISettings Load(string path)
        {
            Logger.LogEntry();

            try
            {
                Serializer<UISettings> serializer = new Serializer<UISettings>();
                UISettings uiSettings = serializer.Load(path);
                if (uiSettings == null)
                {
                    uiSettings = new UISettings();
                }

                return uiSettings;
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
