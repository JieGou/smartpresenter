using SmartPresenter.Common;
using SmartPresenter.Common.Extensions;
using SmartPresenter.Common.Logger;
using SmartPresenter.Data.Common;
using System;
using System.IO;
using System.Windows.Media;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SmartPresenter.BO.Common
{
    /// <summary>
    /// Class to store display settings.
    /// </summary>
    public sealed class DisplaySettings : SettingsBase<DisplaySettings>, IXmlSerializable
    {

        #region Private Data Members



        #endregion

        #region Contsructor

        public DisplaySettings()
        {
            Initialize();
        }

        #endregion

        #region Properties        

        /// <summary>
        /// Gets or sets the output x cordinate.
        /// </summary>
        /// <value>
        /// The output x cordinate.
        /// </value>
        public int OutputX { get; set; }

        /// <summary>
        /// Gets or sets the output y cordinate.
        /// </summary>
        /// <value>
        /// The output y cordinate.
        /// </value>
        public int OutputY { get; set; }

        /// <summary>
        /// Gets or sets the width of the output.
        /// </summary>
        /// <value>
        /// The width of the output.
        /// </value>
        public int OutputWidth { get; set; }

        /// <summary>
        /// Gets or sets the height of the output.
        /// </summary>
        /// <value>
        /// The height of the output.
        /// </value>
        public int OutputHeight { get; set; }

        /// <summary>
        /// Gets or sets the background.
        /// </summary>
        /// <value>
        /// The background.
        /// </value>
        public Brush Background { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is full screen.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is full screen; otherwise, <c>false</c>.
        /// </value>
        public bool IsFullScreen { get; set; }

        #endregion

        #region Commands

        #region Command Handlers



        #endregion

        #endregion

        #region Methods

        #region Private Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            this.Background = Brushes.Black;
        }

        #endregion

        #region Public Methods

        public override void Save(string path)
        {
            Logger.LogEntry();

            Serializer<DisplaySettings> serializer = new Serializer<DisplaySettings>();
            serializer.Save(this, path);

            Logger.LogExit();
        }

        public override DisplaySettings Load(string path)
        {
            Logger.LogEntry();

            try
            {
                Serializer<DisplaySettings> serializer = new Serializer<DisplaySettings>();
                DisplaySettings displaySettings = serializer.Load(path);
                if (displaySettings == null)
                {
                    displaySettings = new DisplaySettings();
                }
                
                return displaySettings;
            }
            catch (FileNotFoundException)
            {
                Logger.LogExit();
                return null;
            }
            finally
            {
                Logger.LogExit();
            }
        }

        #region IXmlSerializable Members

        /// <summary>
        /// This method is reserved and should not be used. When implementing the IXmlSerializable interface, you should return null (Nothing in Visual Basic) from this method, and instead, if specifying a custom schema is required, apply the <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute" /> to the class.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Xml.Schema.XmlSchema" /> that describes the XML representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)" /> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)" /> method.
        /// </returns>
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Generates an object from its XML representation.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> stream from which the object is deserialized.</param>
        public void ReadXml(XmlReader reader)
        {
            reader.Read();
            if (reader.Name.Equals("Output"))
            {
                OutputX = reader["X"].ToInt();
                OutputY = reader["Y"].ToInt();
                OutputWidth = reader["Width"].ToInt();
                OutputHeight = reader["Height"].ToInt();
                IsFullScreen = reader["IsFullScreen"].ToBool();
                reader.Read();
                if (reader.Name.Equals("Background"))
                {
                    this.Background = BrushExtension.LoadFromString(reader.ReadInnerXml());
                }
            }
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> stream to which the object is serialized.</param>
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Output");
            writer.WriteAttributeString("X", this.OutputX.ToString());
            writer.WriteAttributeString("Y", this.OutputY.ToString());
            writer.WriteAttributeString("Width", this.OutputWidth.ToString());
            writer.WriteAttributeString("Height", this.OutputHeight.ToString());
            writer.WriteAttributeString("IsFullScreen", this.IsFullScreen.ToString());
            writer.WriteStartElement("Background");
            writer.WriteRaw(Background.SaveToString());
            writer.WriteEndElement();
            writer.WriteEndElement();
        }

        #endregion

        #endregion

        #endregion

        #region Events



        #endregion

    }
}
