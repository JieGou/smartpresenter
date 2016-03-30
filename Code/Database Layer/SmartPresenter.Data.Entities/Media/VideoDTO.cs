using System;
using SmartPresenter.Common.Extensions;
using System.Xml;
using SmartPresenter.Common.Enums;

namespace SmartPresenter.Data.Entities
{
    /// <summary>
    /// class representing a video object.
    /// </summary>
    [Serializable]
    public class VideoDTO : ImageDTO
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoDTO"/> class.
        /// </summary>
        public VideoDTO()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoDTO"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public VideoDTO(string path)
            : base(path)
        {            
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the frames per second.
        /// </summary>
        /// <value>
        /// The frames per second.
        /// </value>
        public int FramesPerSecond { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail path.
        /// </summary>
        /// <value>
        /// The thumbnail path.
        /// </value>
        public string ThumbnailPath { get; set; }

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
        public TimeSpan Duration { get; private set; }

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
        public TimeSpan CurrentPosition { get; private set; }

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
            if (this.Type == ElementType.Video)
            {
                // Attributes
                if (reader.Name.Equals("Video"))
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
        /// Writes the XML internal.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override void WriteXmlInternal(XmlWriter writer)
        {
            if (this.Type == ElementType.Video)
            {
                // Write Image shape.
                writer.WriteStartElement("Video");
                base.WriteXmlInternal(writer);
                writer.WriteEndElement();
            }
            else
            {
                base.WriteXmlInternal(writer);
            }
        }

        /// <summary>
        /// Reads the common attributes.
        /// </summary>
        /// <param name="reader">The reader.</param>
        protected override void ReadCommonAttributes(XmlReader reader)
        {
            base.ReadCommonAttributes(reader);
            this.FramesPerSecond = reader["FramesPerSecond"].ToInt();
            this.Volume = reader["Volume"].ToInt();
            this.IsMuted = reader["IsMuted"].ToBool();
            this.IsPlaying = reader["IsPlaying"].ToBool();
            this.ThumbnailPath = reader["ThumbnailPath"].ToSafeString();
        }

        /// <summary>
        /// Writes the common attributes.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected override void WriteCommonAttributes(XmlWriter writer)
        {
            base.WriteCommonAttributes(writer);
            writer.WriteAttributeString("FramesPerSecond", FramesPerSecond.ToString());
            writer.WriteAttributeString("Volume", Volume.ToString());
            writer.WriteAttributeString("IsMuted", IsMuted.ToString());
            writer.WriteAttributeString("IsPlaying", IsPlaying.ToString());
            writer.WriteAttributeString("ThumbnailPath", this.ThumbnailPath);
        }

        #endregion
    }
}
