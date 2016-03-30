using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.Common.Extensions;
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
    /// Class to represent a Playlist which will contain a collection of presentation or media or presentation/media.
    /// </summary>
    public class PresentationPlaylist : IPlaylist<IPresentation>
    {

        #region Constants


        #endregion

        #region Private Data Members

        /// <summary>
        /// The collection of presentations
        /// </summary>
        ObservableCollection<IPresentation> _items;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new Playlist.
        /// </summary>
        public PresentationPlaylist()
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
        /// Type of Playlist.
        /// </summary>
        public PlaylistType Type
        {
            get { return PlaylistType.Playlist; }
        }

        /// <summary>
        /// Collection of Presentations.
        /// </summary>
        public ObservableCollection<IPresentation> Items
        {
            get
            {
                return _items;
            }
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
            _items = new ObservableCollection<IPresentation>();
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

            PresentationPlaylist playlist = obj as PresentationPlaylist;
            result = Id.Equals(playlist.Id);

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

        #region IPlaylistItem Members

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
        /// Reads the presentations.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        private static ObservableCollection<IPresentation> ReadPresentations(XmlReader reader)
        {
            ObservableCollection<IPresentation> presentations = new ObservableCollection<IPresentation>();

            if (!reader.IsEmptyElement && reader.Name.Equals("Presentations"))
            {
                var presentationsReader = reader.ReadSubtree();
                presentationsReader.Read(); // start reading the subtree
                presentationsReader.Read(); // go to first display shape node
                while (presentationsReader.IsStartElement() || presentationsReader.Read())
                {
                    IPresentationFactory slideFactory = new PresentationFactory();
                    IPresentation presentation = slideFactory.CreatePresentation();
                    //presentation.ReadXml(presentationsReader);
                    presentations.Add(presentation);
                }
            }

            return presentations;
        }

        /// <summary>
        /// Generates an object from its XML representation.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> stream from which the object is deserialized.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void ReadXml(XmlReader reader)
        {
            reader.ReadStartElement();

            // Attributes
            this.Id = reader["Id"].ToGuid();
            this.Name = reader["Name"].ToSafeString();

            // Slides
            this._items = ReadPresentations(reader);
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> stream to which the object is serialized.</param>
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Playlist");

            writer.WriteAttributeString("Id", Id.ToString());
            writer.WriteAttributeString("Name", Name);
            writer.WriteAttributeString("PlaylistType", Type.ToString());

            writer.WriteStartElement("Presentations");
            foreach (Presentation presentation in Items)
            {
                //presentation.WriteXml(writer);
            }
            writer.WriteEndElement();

            writer.WriteEndElement();
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
            IPlaylist<IPresentation> clonedPlaylist = new PresentationPlaylist();

            try
            {
                using (var stream = new MemoryStream())
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(stream, this);
                    stream.Position = 0;

                    clonedPlaylist = (IPlaylist<IPresentation>)binaryFormatter.Deserialize(stream);
                    return clonedPlaylist;
                }
            }
            catch (Exception ex)
            {
                Logger.LogMsg.Error(ex.Message, ex);
                return null;
            }            
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
