using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.Common.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Schema;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// A class to represent Null state of a Playlist.
    /// </summary>
    public class NullPlaylist : IPlaylist<IEntity>
    {
        #region Private Data Members

        private static NullPlaylist _instance;
        private static volatile Object _lock = new Object();

        #endregion

        #region Constructor

        /// <summary>
        /// Prevents a default instance of the <see cref="NullPresentation"/> class from being created.
        /// </summary>
        private NullPlaylist()
        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the single instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static NullPlaylist Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new NullPlaylist();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Name of playlist.
        /// </summary>
        public string Name
        {
            get
            {
                return string.Empty;
            }
            set
            {
            }
        }

        /// <summary>
        /// Type of Playlist.
        /// </summary>
        public PlaylistType Type
        {
            get { return PlaylistType.None; }
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id
        {
            get
            {
                return Guid.Empty;
            }
            private set
            {
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has been changed.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance was changes; otherwise, <c>false</c>.
        /// </value>
        public bool IsDirty
        {
            get
            {
                return false;
            }
            private set
            {
            }
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public ObservableCollection<IEntity> Items
        {
            get { return new ObservableCollection<IEntity>(); }
        }

        #endregion

        #region IPlaylist Member Methods

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
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> stream to which the object is serialized.</param>
        public void WriteXml(XmlWriter writer)
        {
        }

        /// <summary>
        /// Saves this instance to default location.
        /// </summary>
        public void Save()
        {
        }

        /// <summary>
        /// Saves this instance to a specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        public void SaveTo(string path)
        {
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public object Clone()
        {
            return NullPlaylist.Instance;
        }

        /// <summary>
        /// Marks this instance as dirty.
        /// </summary>
        public void MarkDirty()
        {
            IsDirty = true;
        }

        #endregion

    }
}
