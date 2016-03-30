using SmartPresenter.BO.Common.Enums;
using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.BO.Common.Transitions;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Media;
using System.Xml;
using System.Xml.Schema;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// A null slide for safe implementation.
    /// </summary>
    public sealed class NullSlide : ISlide
    {
        #region Private Data Members

        private static NullSlide _instance;
        private static volatile Object _lock = new Object();

        #endregion

        #region Constructor

        /// <summary>
        /// Prevents a default instance of the <see cref="NullPresentation"/> class from being created.
        /// </summary>
        private NullSlide()
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
        public static NullSlide Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new NullSlide();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Gets or sets the hot key.
        /// </summary>
        /// <value>
        /// The hot key.
        /// </value>
        public char? HotKey
        {
            get
            {
                return ' ';
            }
            set
            {
            }
        }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        public string Notes
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
        /// Gets or sets a value indicating whether this slide is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this slide is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsEnabled
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        /// <summary>
        /// Gets or sets the parent presentation.
        /// </summary>
        /// <value>
        /// The parent presentation.
        /// </value>
        public IPresentation ParentPresentation
        {
            get
            {
                return NullPresentation.Instance;
            }
            set
            {
            }
        }

        /// <summary>
        /// Gets the type of the slide.
        /// </summary>
        /// <value>
        /// The type of the slide.
        /// </value>
        public SlideType Type
        {
            get { return SlideType.None; }
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int Width
        {
            get
            {
                return 0;
            }
            set
            {
            }
        }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int Height
        {
            get
            {
                return 0;
            }
            set
            {
            }
        }

        /// <summary>
        /// Gets or sets the color of the background of slide.
        /// </summary>
        /// <value>
        /// The color of the background of slide.
        /// </value>
        public Brush Background
        {
            get
            {
                return Brushes.Transparent;
            }
            set
            {
            }
        }

        /// <summary>
        /// Gets or sets the slide number.
        /// </summary>
        /// <value>
        /// The slide number.
        /// </value>
        public int SlideNumber
        {
            get { return 0; }
            set { }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [loop to first].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [loop to first]; otherwise, <c>false</c>.
        /// </value>
        public bool LoopToFirst
        {
            get
            {
                return false;
            }
            set
            {
            }
        }

        /// <summary>
        /// Gets or sets the delay before next slide.
        /// </summary>
        /// <value>
        /// The delay before next slide.
        /// </value>
        public TimeSpan DelayBeforeNextSlide
        {
            get
            {
                return TimeSpan.FromSeconds(0);
            }
            set
            {
            }
        }

        /// <summary>
        /// Gets or sets the elements of slide.
        /// </summary>
        /// <value>
        /// The elements of slide.
        /// </value>
        public ObservableCollection<Shape> Elements
        {
            get
            {
                return new ObservableCollection<Shape>();
            }
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
        /// Gets or set the slide label.
        /// </summary>
        public string Label
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
        /// Gets the <see cref="Shape"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="Shape"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public Shape this[int index]
        {
            get
            {
                return NullShape.Instance;
            }
        }

        /// <summary>
        /// Gets the <see cref="Shape"/> with the specified identifier.
        /// </summary>
        /// <value>
        /// The <see cref="Shape"/>.
        /// </value>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Shape this[Guid id]
        {
            get
            {
                return NullShape.Instance;
            }
        }

        /// <summary>
        /// Gets or sets the transition data.
        /// </summary>
        /// <value>
        /// The transition data.
        /// </value>
        public Transition Transition { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Renders this slide.
        /// </summary>
        public void Render()
        {
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
            return NullSlide.Instance;
        }

        /// <summary>
        /// Marks this instance as dirty.
        /// </summary>
        public void MarkDirty()
        {
            IsDirty = true;
        }

        /// <summary>
        /// Adds a new shape to slide.
        /// </summary>
        /// <param name="shape"></param>
        public void AddElement(Shape shape)
        {
        }

        /// <summary>
        /// Removes the element.
        /// </summary>
        /// <param name="shape">The shape.</param>
        public void RemoveElement(Shape shape)
        {
        }

        /// <summary>
        /// Moves the element.
        /// </summary>
        /// <param name="oldIndex">The old index.</param>
        /// <param name="newIndex">The new index.</param>
        public void MoveElement(int oldIndex, int newIndex)
        {
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when [elements collection changed].
        /// </summary>
        public event NotifyCollectionChangedEventHandler ElementsCollectionChanged;

        #endregion

    }
}
