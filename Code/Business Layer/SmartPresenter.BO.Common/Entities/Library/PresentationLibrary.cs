using SmartPresenter.BO.Common.Enums;
using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.Common;
using SmartPresenter.Common.Extensions;
using SmartPresenter.Common.Logger;
using SmartPresenter.Data.Common;
using SmartPresenter.Data.Common.Repositories;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Schema;
using AutoMapper;
using System.Collections.Generic;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// Class to represent a presentation library.
    /// </summary>
    [KnownType(typeof(Presentation))]
    [KnownType(typeof(PresentationPlaylist))]
    [KnownType(typeof(PresentationPlaylistGroup))]
    [DataContract]
    public class PresentationLibrary : ILibrary<IPresentation, IPlaylist<IPresentation>>
    {
        #region Constants

        private const string Lib_File_Name = "Library.slf";

        #endregion

        #region Private Data Members

        PresentationUnitOfWork _presentationUnitOfWork;

        /// <summary>
        /// The location of library on disk.
        /// </summary>
        private string _location;
        /// <summary>
        /// The collectio of presentations
        /// </summary>
        private ObservableCollection<IPresentation> _items;
        /// <summary>
        /// Gets or sets the collection playlists.
        /// </summary>
        /// <value>
        /// The collection of playlists.
        /// </value>
        private ObservableCollection<IPlaylist<IPresentation>> _playlists { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PresentationLibrary"/> class.
        /// </summary>
        public PresentationLibrary()
        {
            Initialize();
        }

        /// <summary>
        /// Creates a new Library.
        /// </summary>
        public PresentationLibrary(string location, string name = "")
        {
            Initialize();
            _location = location;
            Name = name;
            if (Directory.Exists(_location) == false)
            {
                Directory.CreateDirectory(_location);
            }
            _presentationUnitOfWork = new PresentationUnitOfWork(location);
            
            Load(location, name);            
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>
        /// The unique identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has been changed.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance was changes; otherwise, <c>false</c>.
        /// </value>
        public bool IsDirty { get; private set; }

        /// <summary>
        /// Name of the Library.
        /// </summary>        
        public string Name { get; set; }

        /// <summary>
        /// Physical location of the Library on Hard drive.
        /// </summary>         
        public string Location
        {
            get
            {
                return _location;
            }
            internal set
            {
                _location = value;
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
                return LibraryType.Presentation;
            }
        }

        /// <summary>
        /// Gets the items of this library.
        /// </summary>
        /// <value>
        /// The items of this library.
        /// </value>
        public ObservableCollection<IPresentation> Items
        {
            get
            {
                if (_items == null)
                {
                    _items = new ObservableCollection<IPresentation>();
                    (_items as ObservableCollection<IPresentation>).CollectionChanged += Presentations_CollectionChanged;
                }
                return _items;
            }
        }

        /// <summary>
        /// Gets the collection of playlists.
        /// </summary>
        /// <value>
        /// The collection of playlists for this library.
        /// </value>
        public ObservableCollection<IPlaylist<IPresentation>> Playlists
        {
            get
            {
                if (_playlists == null)
                {
                    _playlists = new ObservableCollection<IPlaylist<IPresentation>>();
                    (_playlists as ObservableCollection<IPlaylist<IPresentation>>).CollectionChanged += Playlists_CollectionChanged;
                }
                return _playlists;
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
        }

        /// <summary>
        /// Handles the CollectionChanged event of the Presentations control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        private void Presentations_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var newPresentations = e.NewItems;
                foreach (Presentation document in newPresentations)
                {
                    document.ParentLibraryLocation = Location;
                }
            }
        }

        /// <summary>
        /// Handles the CollectionChanged event of the Playlists control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        private void Playlists_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// First deletes all the presentation in this library.
        /// Deletes this library instance.
        /// </summary>
        public void Delete()
        {
            Logger.LogEntry();

            foreach (String file in Directory.EnumerateFiles(Location))
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception ex)
                {
                    Logger.LogMsg.Error(String.Format("Presentation \"{0}\" could not be deleted", file), ex);
                }
            }
            try
            {
                Directory.Delete(Location);
            }
            catch (Exception ex)
            {
                Logger.LogMsg.Error(String.Format("Library \"{0}\" could not be deleted", Location), ex);
            }

            Logger.LogExit();
        }

        /// <summary>
        /// First saves all the presentation in this library.
        /// Saves this library instance.
        /// </summary>
        public void Save()
        {
            Logger.LogEntry();

            foreach (Presentation document in Items)
            {
                document.Save();
            }

            if (ApplicationSettings.Instance.ActiveUserAccount.ActiveProfile.GeneralSettings.PresentationLibraries.Contains(this) == false)
            {
                ApplicationSettings.Instance.ActiveUserAccount.ActiveProfile.GeneralSettings.PresentationLibraries.Add(this);
            }

            foreach (Presentation document in Items)
            {
                document.Save();
            }

            Serializer<PresentationLibrary> serializer = new Serializer<PresentationLibrary>();
            string path = System.IO.Path.Combine(ApplicationSettings.Instance.ActiveUserAccount.ActiveProfile.DocumentLibrariesFolderPath, Name);
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
        public void Save(string path)
        {
            Logger.LogEntry();

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
            Logger.LogEntry();

            ILibrary<IPresentation, IPlaylist<IPresentation>> clonedLibrary = new PresentationLibrary();

            try
            {
                using (var stream = new MemoryStream())
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(stream, this);
                    stream.Position = 0;

                    clonedLibrary = (ILibrary<IPresentation, IPlaylist<IPresentation>>)binaryFormatter.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                Logger.LogMsg.Error(ex.Message, ex);
            }

            Logger.LogExit();

            return clonedLibrary;
        }

        public override bool Equals(object obj)
        {
            PresentationLibrary library = obj as PresentationLibrary;
            if (library != null)
            {
                return Id.Equals(library.Id);
            }
            return false;
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Loads the specified location.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public void Load(string location, string name)
        {
            Logger.LogEntry();
            
            //Serializer<PresentationLibrary> serializer = new Serializer<PresentationLibrary>();
            if (Directory.Exists(location) == false)
            {
                throw new FileNotFoundException("Specified media library not found", name);
            }
            Location = location;
            Name = name;
                    
            foreach (string file in Directory.GetFiles(location, string.Concat("*", Constants.Default_Document_Extension)))
            {
                //PresentationDTO presentationDTO = _presentationUnitOfWork.Repository.Get(presentation => presentation.Path.Equals(file)).FirstOrDefault();                
                Items.Add(null);
            }            

            Logger.LogExit();
        }

        #endregion

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
            Logger.LogEntry();

            Id = reader.GetAttribute("Id").ToGuid();
            Name = reader.GetAttribute("Name");
            _location = reader.GetAttribute("Location");

            reader.Read();

            Logger.LogExit();
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> stream to which the object is serialized.</param>
        public void WriteXml(XmlWriter writer)
        {
            Logger.LogEntry();

            writer.WriteAttributeString("Id", Id.ToString());
            writer.WriteAttributeString("Name", Name);
            writer.WriteAttributeString("Location", Location);

            // Write Presentations
            writer.WriteStartElement("Presentations");
            foreach (Presentation presentation in Items)
            {
                //PresentationDTO presentationDTO = Mapper.Map<Presentation, PresentationDTO>(presentation);
                //presentationDTO.WriteXml(writer);
            }
            writer.WriteEndElement();

            // Write Playlists
            writer.WriteStartElement("Playlists");
            foreach (IPlaylist<IPresentation> playlist in Playlists)
            {
                playlist.WriteXml(writer);
            }
            writer.WriteEndElement();

            Logger.LogExit();
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
