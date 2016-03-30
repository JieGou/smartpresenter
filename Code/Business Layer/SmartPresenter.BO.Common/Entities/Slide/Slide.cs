using SmartPresenter.BO.Common.Enums;
using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.BO.Common.Transitions;
using SmartPresenter.Common.Extensions;
using SmartPresenter.Common.Logger;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// A concrete slide class.
    /// </summary>
    public class Slide : ISlide
    {
        #region Private Data Members

        /// <summary>
        /// The Elements collection for this slide.
        /// </summary>
        private ObservableCollection<Shape> _elements = new ObservableCollection<Shape>();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Slide"/> class.
        /// </summary>
        public Slide()
        {
            Initialize();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has been changed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance was changes; otherwise, <c>false</c>.
        /// </value>
        public bool IsDirty { get; private set; }

        /// <summary>
        /// Gets or sets the hot key.
        /// </summary>
        /// <value>The hot key.</value>
        public char? HotKey { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>The notes.</value>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>The label.</value>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this slide is enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this slide is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the presentation ID.
        /// </summary>
        /// <value>The presentation ID.</value>
        public IPresentation ParentPresentation { get; set; }

        /// <summary>
        /// Gets the type of the slide.
        /// </summary>
        /// <value>The type of the slide.</value>
        public SlideType Type
        {
            get
            {
                return SlideType.Slide;
            }
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the color of the background of slide.
        /// </summary>
        /// <value>
        /// The color of the background of slide.
        /// </value>
        public Brush Background { get; set; }

        /// <summary>
        /// Gets or sets the slide number.
        /// </summary>
        /// <value>The slide number.</value>
        public int SlideNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [loop to first].
        /// </summary>
        /// <value><c>true</c> if [loop to first]; otherwise, <c>false</c>.</value>
        public bool LoopToFirst { get; set; }

        /// <summary>
        /// Gets or sets the delay before next slide.
        /// </summary>
        /// <value>
        /// The delay before next slide.
        /// </value>
        public TimeSpan DelayBeforeNextSlide { get; set; }

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
                return _elements;
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
                return Elements[index];
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
                return Elements.FirstOrDefault(shape => shape.Id.Equals(id));
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

        #region Private Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            Id = Guid.NewGuid();
            Notes = string.Empty;
            Label = string.Empty;
            HotKey = null;
            IsEnabled = true;
            Background = Brushes.CornflowerBlue;
            Transition = TransitionFactory.CreateTransition(TransitionTypes.None);

            _elements.CollectionChanged += Elements_CollectionChanged;
        }

        /// <summary>
        /// Handles the CollectionChanged event of the Elements control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Collections.Specialized.NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void Elements_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            IsDirty = true;
            OnElementsCollectionChanged(sender, e);
            if (e.NewItems != null)
            {
                foreach (Shape shape in e.NewItems)
                {
                    shape.ParentSlide = this;
                }
            }
        }        

        #endregion

        #region ISlide Methods

        

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public object Clone()
        {
            ISlide clonedSlide = new Slide();

            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
                    XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                    namespaces.Add(String.Empty, String.Empty);
                    xmlSerializer.Serialize(stream, this, namespaces);
                    stream.Flush();
                    stream.Seek(0, SeekOrigin.Begin);

                    clonedSlide = (ISlide)xmlSerializer.Deserialize(stream);
                }

                ((Slide)clonedSlide).Id = Guid.NewGuid();
                return clonedSlide;
            }
            catch (Exception ex)
            {
                Logger.LogMsg.Error(ex.Message, ex);
                return null;
            }            
        }

        /// <summary>
        /// Adds a new shape to slide.
        /// </summary>
        /// <param name="shape"></param>
        public void AddElement(Shape shape)
        {
            this._elements.Add(shape);
        }

        /// <summary>
        /// Removes the element.
        /// </summary>
        /// <param name="shape">The shape.</param>
        public void RemoveElement(Shape shape)
        {
            this._elements.Remove(shape);
        }

        /// <summary>
        /// Moves the element.
        /// </summary>
        /// <param name="oldIndex">The old index.</param>
        /// <param name="newIndex">The new index.</param>
        public void MoveElement(int oldIndex, int newIndex)
        {
            var item = this._elements[oldIndex];
            this._elements.RemoveAt(oldIndex);
            if (this._elements.Count > newIndex)
            {
                this._elements.Insert(newIndex, item);
            }
        }

        /// <summary>
        /// Renders this slide.
        /// </summary>
        public void Render()
        {
        }

        /// <summary>
        /// Marks this instance as dirty.
        /// </summary>
        public void MarkDirty()
        {
            IsDirty = true;
            if (ParentPresentation != null)
            {
                ParentPresentation.MarkDirty();
            }
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            ISlide slideObj = obj as ISlide;
            bool isEquals = Id.Equals(slideObj.Id);

            return isEquals;
        }

        #endregion

        #endregion

        #region Events

        /// <summary>
        /// Occurs when elements collection changes.
        /// </summary>
        public event NotifyCollectionChangedEventHandler ElementsCollectionChanged;

        /// <summary>
        /// Called when elements collection changes.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        private void OnElementsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (ElementsCollectionChanged != null)
            {
                ElementsCollectionChanged(sender, e);
            }
        }

        #endregion

    }
}
