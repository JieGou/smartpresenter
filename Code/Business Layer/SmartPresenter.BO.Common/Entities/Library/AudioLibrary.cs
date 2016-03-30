using SmartPresenter.BO.Common.Enums;
using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.Common;
using SmartPresenter.Common.Logger;
using SmartPresenter.Data.Common;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Schema;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// A library to hold audio items and related playlists.
    /// </summary>
    public class AudioLibrary : ILibrary<Audio, IPlaylist<Audio>>
    {
        #region Constants

        private const string Lib_File_Name = "Library.slf";

        #endregion

        #region Private Data Members

        /// <summary>
        /// The audio items.
        /// </summary>
        private ObservableCollection<Audio> _items;
        /// <summary>
        /// The audio playlists.
        /// </summary>
        private ObservableCollection<IPlaylist<Audio>> _playlists;

        private string _location;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioLibrary"/> class.
        /// </summary>
        public AudioLibrary()
        {
            Id = Guid.NewGuid();
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
                return LibraryType.Audio;
            }
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public ObservableCollection<Audio> Items
        {
            get
            {
                if (_items == null)
                {
                    _items = new ObservableCollection<Audio>();
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
        public ObservableCollection<IPlaylist<Audio>> Playlists
        {
            get
            {
                if (_playlists == null)
                {
                    _playlists = new ObservableCollection<IPlaylist<Audio>>();
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
        public Guid Id { get; private set; }

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
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public object Clone()
        {
            ILibrary<Audio, IPlaylist<Audio>> clonedLibrary = new AudioLibrary();

            try
            {
                using (var stream = new MemoryStream())
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(stream, this);
                    stream.Position = 0;

                    clonedLibrary = (ILibrary<Audio, IPlaylist<Audio>>)binaryFormatter.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                Logger.LogMsg.Error(ex.Message, ex);
            }

            return clonedLibrary;
        }

        #endregion

        #region Methods

        #region Private Methods

        public static AudioLibrary Load(string name)
        {
            Logger.LogEntry();

            //string path = System.IO.Path.Combine(ApplicationSettings.Instance.ActiveUserAccount.MediaLibrariesFolderPath, name, Lib_File_Name);

            //if (File.Exists(path) == false)
            //{
            //    throw new FileNotFoundException("Specified audio library not found", name);
            //}

            Serializer<AudioLibrary> serializer = new Serializer<AudioLibrary>();
            //AudioLibrary library = serializer.Load(path);
            AudioLibrary library = null;

            Logger.LogExit();
            return library;
        }

        /// <summary>
        /// Handles the CollectionChanged event of the Items.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handles the CollectionChanged event of the Playlists.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void Playlists_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion

    }
}
