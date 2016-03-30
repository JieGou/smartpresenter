using System;
using System.Xml;
using SmartPresenter.Common.Extensions;
using SmartPresenter.Common.Enums;

namespace SmartPresenter.Data.Entities
{
    /// <summary>
    /// A class representing an audio object.
    /// </summary>
    [Serializable]
    public class AudioDTO : ShapeDTO
    {
        #region Private Data Members



        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioDTO"/> class.
        /// </summary>
        public AudioDTO() : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioDTO"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public AudioDTO(string path)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the path of Image.
        /// </summary>
        /// <value>
        /// The path of Image.
        /// </value>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        /// <value>
        /// The volume.
        /// </value>
        public int Volume { get; set; }

        /// <summary>
        /// Gets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        public string Duration { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is playing.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is playing; otherwise, <c>false</c>.
        /// </value>
        public bool IsPlaying { get; private set; }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public string State { get; private set; }

        /// <summary>
        /// Gets the current position.
        /// </summary>
        /// <value>
        /// The current position.
        /// </value>
        public string CurrentPosition { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is muted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is muted; otherwise, <c>false</c>.
        /// </value>
        public bool IsMuted { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Reads the XML internal.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override void ReadXmlInternal(XmlReader reader)
        {
            if (this.Type == ElementType.Audio)
            {
                // Attributes
                if (reader.Name.Equals("Audio"))
                {
                    base.ReadXmlInternal(reader);
                }
            }
            else
            {
                base.ReadXmlInternal(reader);
            }
        }

        /// <summary>
        /// Reads the attributes.
        /// </summary>
        /// <param name="reader">The reader.</param>
        protected override void ReadCommonAttributes(XmlReader reader)
        {
            base.ReadCommonAttributes(reader);
            this.Volume = reader["Volume"].ToInt();
            this.IsMuted = reader["Mute"].ToBool();
            this.Path = reader["Path"].ToSafeString();
        }

        /// <summary>
        /// Writes the XML internal.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override void WriteXmlInternal(XmlWriter writer)
        {
            if (this.Type == ElementType.Audio)
            {
                // Write Video shape.
                writer.WriteStartElement("Audio");
                base.WriteXmlInternal(writer);
                writer.WriteEndElement();
            }
            else
            {
                base.WriteXmlInternal(writer);
            }
        }

        /// <summary>
        /// Writes the common attributes.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected override void WriteCommonAttributes(XmlWriter writer)
        {
            base.WriteCommonAttributes(writer);
            writer.WriteAttributeString("Path", Path.ToString());
            writer.WriteAttributeString("Volume", Volume.ToString());
            writer.WriteAttributeString("Mute", IsMuted.ToString());
        }

        #endregion

    }
}
