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
    /// A class for holding audio playlist which will hold audio items.
    /// </summary>
    public class AudioPlaylist : IPlaylist<Audio>
    {

        #region Private Data Members

        private ObservableCollection<Audio> _items;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioPlaylist"/> class.
        /// </summary>
        public AudioPlaylist()
        {
            Id = Guid.NewGuid();
            _items = new ObservableCollection<Audio>();
        }

        #endregion

        #region IPlaylist Properties

        /// <summary>
        /// Name of playlist.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of Playlist.
        /// </summary>
        public PlaylistType Type
        {
            get { return PlaylistType.MediaPlaylist; }
        }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public ObservableCollection<Audio> Items
        {
            get
            {
                return _items;
            }
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has been changed.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance was changes; otherwise, <c>false</c>.
        /// </value>
        public bool IsDirty { get; private set; }

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public object Clone()
        {
            IPlaylist<Audio> clonnedAudio = new AudioPlaylist();

            try
            {
                using (var stream = new MemoryStream())
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(stream, this);
                    stream.Position = 0;

                    clonnedAudio = (IPlaylist<Audio>)binaryFormatter.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                Logger.LogMsg.Error(ex.Message, ex);
            }

            return clonnedAudio;
        }

        /// <summary>
        /// Marks this instance as dirty.
        /// </summary>
        public void MarkDirty()
        {
            IsDirty = true;
        }

        #endregion

        #region IPlaylist Methods

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
        private static ObservableCollection<Audio> ReadElements(XmlReader reader)
        {
            ObservableCollection<Audio> elements = new ObservableCollection<Audio>();

            if (!reader.IsEmptyElement && reader.Name.Equals("Elements"))
            {
                var elementsReader = reader.ReadSubtree();
                elementsReader.Read(); // start reading the subtree
                elementsReader.Read(); // go to first display shape node
                while (elementsReader.IsStartElement() || elementsReader.Read())
                {
                    IShapeFactory elementFactory = new AudioFactory();
                    Audio audio = (Audio)elementFactory.CreateElement();
                    //audio.ReadXml(elementsReader);
                    elements.Add(audio);
                }
            }

            return elements;
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
            this._items = ReadElements(reader);
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> stream to which the object is serialized.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Playlist");

            writer.WriteAttributeString("Id", Id.ToString());
            writer.WriteAttributeString("Name", Name);

            writer.WriteStartElement("Elements");

            //this.Items.ForEach(item => item.WriteXml(writer));

            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        #endregion

        #endregion

    }
}
