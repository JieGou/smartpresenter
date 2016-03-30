using SmartPresenter.BO.Common.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Xml;
using System.Xml.Schema;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// A Null Presentation class to support null object pattern.
    /// </summary>
    public sealed class NullPresentation : IPresentation
    {
        #region Private Data Members

        private static NullPresentation _instance;
        private static volatile Object _lock = new Object();

        #endregion

        #region Constructor

        /// <summary>
        /// Prevents a default instance of the <see cref="NullPresentation"/> class from being created.
        /// </summary>
        private NullPresentation()
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
        public static NullPresentation Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new NullPresentation();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>
        /// The unique identifier.
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
        ///   <c>true</c> if this instance was changes; otherwise, <c>false</c>.
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
        /// Gets or sets the name of presentation item.
        /// </summary>
        /// <value>
        /// The name of presentation item.
        /// </value>
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
        /// Gets the path of presentation item.
        /// </summary>
        /// <value>
        /// The path of presentation item.
        /// </value>
        public string Path
        {
            get { return string.Empty; }
        }

        /// <summary>
        /// Type of Presentation.
        /// </summary>
        public PresentationType Type
        {
            get { return PresentationType.None; }
        }

        /// <summary>
        /// Gets or sets the slides of presentation.
        /// </summary>
        /// <value>
        /// The slides.
        /// </value>
        public ObservableCollection<ISlide> Slides
        {
            get
            {
                return new ObservableCollection<ISlide>();
            }
        }

        /// <summary>
        /// Gets or sets the width of presentation.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int Width
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the height of presentation.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int Height
        {
            get;
            set;
        }

        public string Category
        {
            get;
            set;
        }

        public string ParentLibraryLocation { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Empty Save.
        /// </summary>
        public void Save()
        {
        }

        /// <summary>
        /// Empty Save.
        /// </summary>
        public void Save(string path)
        {
        }

        /// <summary>
        /// Empty Clone.
        /// </summary>
        public object Clone()
        {
            return _instance;
        }

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
        /// Marks this instance as dirty.
        /// </summary>
        public void MarkDirty()
        {
            IsDirty = true;
        }

        /// <summary>
        /// Adds the new slide.
        /// </summary>
        public void AddNewSlide()
        {
        }

        /// <summary>
        /// Adds a new shape to slide.
        /// </summary>
        /// <param name="slide"></param>
        public void AddSlide(ISlide slide)
        {
        }

        /// <summary>
        /// Removes the element.
        /// </summary>
        /// <param name="slide">The slide.</param>
        public void RemoveSlide(ISlide slide)
        {
        }

        /// <summary>
        /// Moves the element.
        /// </summary>
        /// <param name="oldIndex">The old index.</param>
        /// <param name="newIndex">The new index.</param>
        public void MoveSlide(int oldIndex, int newIndex)
        {
        }

        #endregion

        #region Events

        public event NotifyCollectionChangedEventHandler SlidesCollectionChanged;

        #endregion

    }
}
