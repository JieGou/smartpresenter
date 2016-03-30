using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
namespace SmartPresenter.BO.Common
{
    /// <summary>
    /// Base class for Playlist types.
    /// </summary>
    public abstract class PlaylistBase : IXmlSerializable
    {
        #region Constructor

        protected PlaylistBase()
        {
            ID = Guid.NewGuid();
        }

        #endregion

        #region Properties

        public Guid ID { get; set; }

        /// <summary>
        /// Name of playlist.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of Playlist.
        /// </summary>
        public abstract PlaylistType PlaylistType { get; }

        #endregion

        public abstract void ReadPlaylistXml(XmlReader reader);

        public abstract void WritePlaylistXml(XmlWriter writer);

        #region IXmlSerializable Members

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            ReadPlaylistXml(reader);
        }

        public void WriteXml(XmlWriter writer)
        {
            WritePlaylistXml(writer);
        }

        #endregion
    }
}
