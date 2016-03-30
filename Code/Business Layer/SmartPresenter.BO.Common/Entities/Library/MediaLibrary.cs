using SmartPresenter.BO.Common.Enums;
using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.Common;
using SmartPresenter.Common.Enums;
using SmartPresenter.Common.Extensions;
using SmartPresenter.Common.Logger;
using SmartPresenter.Data.Common;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Schema;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// A library to hold media objects like images and videos.
    /// </summary>
    [DataContract]
    public class MediaLibrary : ILibrary<Image, IPlaylist<Image>>
    {
        #region Constants

        private const string Lib_File_Name = "Library.slf";

        #endregion

        #region Private Data Members

        /// <summary>
        /// The media items
        /// </summary>
        private ObservableCollection<Image> _items;
        /// <summary>
        /// The media playlists
        /// </summary>
        private ObservableCollection<IPlaylist<Image>> _playlists;

        private string _location;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaLibrary"/> class.
        /// </summary>
        public MediaLibrary()
        {
            Id = Guid.NewGuid();
            //_location = ApplicationSettings.Instance.MediaLibrariesFolderPath;

            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaLibrary"/> class.
        /// </summary>
        /// <param name="location">The location.</param>
        public MediaLibrary(string location)
        {
            _location = location;
            if (Directory.Exists(System.IO.Path.GetDirectoryName(location)) == false)
            {
                Directory.CreateDirectory(_location);
            }

            Initialize();
        }

        #endregion

        #region ILibrary Members

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public string Location
        {
            get
            {
                return _location;
            }
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public LibraryType Type
        {
            get
            {
                return LibraryType.Media;
            }
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public ObservableCollection<Image> Items
        {
            get
            {
                if (_items == null)
                {
                    _items = new ObservableCollection<Image>();
                    _items.CollectionChanged += Items_CollectionChanged;
                }
                return _items;
            }
        }

        /// <summary>
        /// Gets the playlists.
        /// </summary>
        /// <value>
        /// The playlists.
        /// </value>
        public ObservableCollection<IPlaylist<Image>> Playlists
        {
            get
            {
                if (_playlists == null)
                {
                    _playlists = new ObservableCollection<IPlaylist<Image>>();
                    _playlists.CollectionChanged += Playlists_CollectionChanged;
                }
                return _playlists;
            }
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has been changed.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance was changes; otherwise, <c>false</c>.
        /// </value>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool IsDirty { get; private set; }

        /// <summary>
        /// Marks this instance as dirty.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void MarkDirty()
        {
            this.IsDirty = true;
        }

        public static MediaLibrary Load(string path)
        {
            Logger.LogEntry();

            path = System.IO.Path.Combine(path, Lib_File_Name);

            if (File.Exists(path) == false)
            {
                throw new FileNotFoundException("Specified media library not found", path);
            }

            Serializer<MediaLibrary> serializer = new Serializer<MediaLibrary>();
            MediaLibrary library = serializer.Load(path);            

            Logger.LogExit();
            return library;
        }

        /// <summary>
        /// Saves this instance to default location.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Save()
        {
            Logger.LogEntry();

            Serializer<MediaLibrary> serializer = new Serializer<MediaLibrary>();
            string path = System.IO.Path.Combine(ApplicationSettings.Instance.ActiveUserAccount.ActiveProfile.MediaLibrariesFolderPath, Name);
            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            path = System.IO.Path.Combine(path, Lib_File_Name);
            serializer.Save(this, path);

            Logger.LogExit();
        }

        /// <summary>
        /// Saves this instance to a specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Save(string path)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method is reserved and should not be used. When implementing the IXmlSerializable interface, you should return null (Nothing in Visual Basic) from this method, and instead, if specifying a custom schema is required, apply the <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute" /> to the class.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Xml.Schema.XmlSchema" /> that describes the XML representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)" /> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)" /> method.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Generates an object from its XML representation.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> stream from which the object is deserialized.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void ReadXml(XmlReader reader)
        {
            Logger.LogEntry();

            Name = reader.GetAttribute("Name");
            _location = reader.GetAttribute("Location");

            reader.Read();
            if (reader.Name.Equals("MediaItems"))
            {
                reader.Read();
                while (reader.Name.Equals("Image") || reader.Name.Equals("Video"))
                {
                    if (reader.Name.Equals("Image"))
                    {
                        Image image = new Image();
                        image.Id = reader["Id"].ToGuid();
                        image.Path = reader["Path"].ToSafeString();
                        this.Items.Add(image);
                    }
                    if (reader.Name.Equals("Video"))
                    {
                        Video video = new Video();
                        video.Id = reader["Id"].ToGuid();
                        video.Path = reader["Path"].ToSafeString();
                        video.ThumbnailPath = reader["ThumbnailPath"].ToSafeString();
                        video.Opacity = reader["Opacity"].ToInt();
                        this.Items.Add(video);
                    }
                    reader.Read();
                }
            }

            Logger.LogExit();
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> stream to which the object is serialized.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void WriteXml(XmlWriter writer)
        {
            Logger.LogEntry();

            writer.WriteAttributeString("Name", Name);
            writer.WriteAttributeString("Location", Location);

            // Write MediaItems
            writer.WriteStartElement("MediaItems");
            foreach (Image media in Items)
            {
                if (media.Type == ElementType.Image)
                {
                    writer.WriteStartElement("Image");
                    writer.WriteAttributeString("Id", media.Id.ToString());
                    writer.WriteAttributeString("Path", media.Path);
                    writer.WriteEndElement();
                }
                if (media.Type == ElementType.Video)
                {
                    writer.WriteStartElement("Video");
                    writer.WriteAttributeString("Id", media.Id.ToString());
                    writer.WriteAttributeString("Path", media.Path);
                    writer.WriteAttributeString("ThumbnailPath", ((Video)media).ThumbnailPath);
                    //writer.WriteAttributeString("Width", media.Width.ToString());
                    //writer.WriteAttributeString("Height", media.Height.ToString());
                    writer.WriteAttributeString("Opacity", media.Opacity.ToString());
                    writer.WriteEndElement();
                }
            }
            writer.WriteEndElement();

            // Write Playlists
            writer.WriteStartElement("Playlists");
            foreach (IPlaylist<Image> playlist in Playlists)
            {
                playlist.WriteXml(writer);
            }
            writer.WriteEndElement();

            Logger.LogExit();
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public object Clone()
        {
            ILibrary<Image, IPlaylist<Image>> clonedLibrary = new MediaLibrary();

            try
            {
                using (var stream = new MemoryStream())
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(stream, this);
                    stream.Position = 0;

                    clonedLibrary = (ILibrary<Image, IPlaylist<Image>>)binaryFormatter.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                Logger.LogMsg.Error(ex.Message, ex);
            }

            return clonedLibrary;
        }

        public override bool Equals(object obj)
        {
            MediaLibrary library = obj as MediaLibrary;
            if (library != null)
            {
                return Id.Equals(library.Id);
            }
            return false;
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
            Playlists.Add(new MediaPlaylist() { Name = "Default" });
        }

        /// <summary>
        /// Handles the CollectionChanged event of the Items.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

        }

        /// <summary>
        /// Handles the CollectionChanged event of the Playlists.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void Playlists_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

        }

        #endregion

        #endregion
    }
}
