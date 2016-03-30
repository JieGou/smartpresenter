using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.Common.Logger;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Schema;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// A class for a group of audio items.
    /// </summary>
    public class AudioPlaylistGroup : IPlaylist<IPlaylist<Audio>>
    {
        #region Private Data Members

        /// <summary>
        /// The collection of playlists
        /// </summary>
        private ObservableCollection<IPlaylist<Audio>> _items;

        #endregion

        #region Constants

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new playlist group.
        /// </summary>
        public AudioPlaylistGroup()
        {
            Initialize();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>
        /// The unique identifier.
        /// </value>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has been changed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance was changes; otherwise, <c>false</c>.
        /// </value>
        public bool IsDirty { get; private set; }

        /// <summary>
        /// Name of playlist.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public PlaylistType Type
        {
            get { return PlaylistType.PlaylistGroup; }
        }

        /// <summary>
        /// Collection of Playlists.
        /// </summary>
        public ObservableCollection<IPlaylist<Audio>> Items
        {
            get { return _items; }
        }

        #endregion

        #region Methods

        #region Private Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            Id = Guid.NewGuid();
            _items = new ObservableCollection<IPlaylist<Audio>>();
        }

        #endregion

        #region Protected Overridden Methods

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            bool result = false;

            AudioPlaylistGroup playlistGroup = obj as AudioPlaylistGroup;
            result = Id.Equals(playlistGroup.Id);

            return result;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        #region IPlaylistItem Methods

        /// <summary>
        /// This method is reserved and should not be used. When implementing the IXmlSerializable interface, you should return null (Nothing in Visual Basic) from this method, and instead, if specifying a custom schema is required, apply the <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute" /> to the class.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Xml.Schema.XmlSchema" /> that describes the XML representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)" /> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)" /> method.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public XmlSchema GetSchema()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generates an object from its XML representation.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> stream from which the object is deserialized.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> stream to which the object is serialized.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void WriteXml(XmlWriter writer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves this instance to default location.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Save()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves this instance to a specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void SaveTo(string path)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public object Clone()
        {
            IPlaylist<IPlaylist<Audio>> clonedPlaylistGroup = new AudioPlaylistGroup();

            try
            {
                using (var stream = new MemoryStream())
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(stream, this);
                    stream.Position = 0;

                    clonedPlaylistGroup = (IPlaylist<IPlaylist<Audio>>)binaryFormatter.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                Logger.LogMsg.Error(ex.Message, ex);
            }

            return clonedPlaylistGroup;
        }

        /// <summary>
        /// Marks this instance as dirty.
        /// </summary>
        public void MarkDirty()
        {
            IsDirty = true;
        }

        #endregion

        #endregion

    }
}
