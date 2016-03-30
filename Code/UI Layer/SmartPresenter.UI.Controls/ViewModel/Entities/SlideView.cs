using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using SmartPresenter.BO.Common.Entities;
using SmartPresenter.BO.Common.Enums;
using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.BO.Common.Transitions;
using SmartPresenter.BO.UndoRedo;
using SmartPresenter.Common.Enums;
using SmartPresenter.UI.Controls.Events;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace SmartPresenter.UI.Controls.ViewModel
{
    /// <summary>
    /// View class for a slide object.
    /// </summary>
    public class SlideView : BindableBase, ICloneable, IUndoRedoCapable
    {

        #region Private Data Members

        private ISlide _slide;
        private ObservableCollection<ShapeView> _elements = new ObservableCollection<ShapeView>();
        private int _zoomRatio;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SlideView"/> class.
        /// </summary>
        /// <param name="slide">The slide.</param>
        public SlideView(ISlide slide)
        {
            _slide = slide;
            _slide.ElementsCollectionChanged += Slide_ElementsCollectionChanged;
            this.ZoomRatio = 1;
            UndoRedoManager.Instance.Register(this);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the hot key.
        /// </summary>
        /// <value>The hot key.</value>
        public char? HotKey
        {
            get
            {
                return _slide.HotKey;
            }
            set
            {
                OnUndoRedoPropertyChanged("HotKey", _slide.HotKey, value);
                _slide.HotKey = value;
                OnPropertyChanged("HotKey");
                OnPropertyChanged("HotKeyVisibility");                
            }
        }

        /// <summary>
        /// Gets the is hot key visible.
        /// </summary>
        /// <value>
        /// The is hot key visible.
        /// </value>
        public Visibility HotKeyVisibility
        {
            get
            {
                if (HotKey == null || HotKey == ' ' || string.IsNullOrEmpty(HotKey.ToString()))
                {
                    return Visibility.Collapsed;
                }
                return Visibility.Visible;
            }
        }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>The notes.</value>
        public string Notes
        {
            get
            {
                return _slide.Notes;
            }
            set
            {
                OnUndoRedoPropertyChanged("Notes", _slide.Notes, value);
                _slide.Notes = value;
                OnPropertyChanged("Notes");
            }
        }

        /// <summary>
        /// Gets or sets the slide label.
        /// </summary>
        public string Label
        {
            get
            {
                return _slide.Label;
            }
            set
            {
                OnUndoRedoPropertyChanged("Label", _slide.Label, value);
                _slide.Label = value;
                OnPropertyChanged("Label");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this slide is enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this slide is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsEnabled
        {
            get
            {
                return _slide.IsEnabled;
            }
            set
            {
                OnUndoRedoPropertyChanged("IsEnabled", _slide.IsEnabled, value);
                _slide.IsEnabled = value;
                OnPropertyChanged("IsEnabled");
            }
        }

        /// <summary>
        /// Gets or sets the presentation ID.
        /// </summary>
        /// <value>The presentation ID.</value>
        public IPresentation ParentPresentation
        {
            get
            {
                return _slide.ParentPresentation;
            }
        }

        /// <summary>
        /// Gets the type of the slide.
        /// </summary>
        /// <value>The type of the slide.</value>
        public SlideType Type
        {
            get
            {
                return _slide.Type;
            }
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
                return _slide.Width;
            }
            set
            {
                _slide.Width = value;
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
                return _slide.Height;
            }
            set
            {
                _slide.Height = value;
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
                return _slide.Background;
            }
            set
            {
                _slide.Background = value;
            }
        }

        /// <summary>
        /// Gets or sets the slide number.
        /// </summary>
        /// <value>The slide number.</value>
        public int SlideNumber
        {
            get
            {
                return _slide.SlideNumber;
            }
            set
            {
                _slide.SlideNumber = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [loop to first].
        /// </summary>
        /// <value><c>true</c> if [loop to first]; otherwise, <c>false</c>.</value>
        public bool LoopToFirst
        {
            get
            {
                return _slide.LoopToFirst;
            }
            set
            {
                OnUndoRedoPropertyChanged("LoopToFirst", _slide.LoopToFirst, value);
                _slide.LoopToFirst = value;
                OnPropertyChanged("LoopToFirst");
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
                return _slide.DelayBeforeNextSlide;
            }
            set
            {
                OnUndoRedoPropertyChanged("DelayBeforeNextSlide", _slide.DelayBeforeNextSlide, value);
                _slide.DelayBeforeNextSlide = value;
                OnPropertyChanged("DelayBeforeNextSlide");
                OnPropertyChanged("IsTimerAvailable");
            }
        }

        /// <summary>
        /// Gets the is timer available.
        /// </summary>
        /// <value>
        /// The is timer available.
        /// </value>
        public Visibility IsTimerAvailable
        {
            get
            {
                if (DelayBeforeNextSlide.TotalSeconds > 0)
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the zoom ratio.
        /// </summary>
        /// <value>
        /// The zoom ratio.
        /// </value>
        public int ZoomRatio
        {
            get
            {
                return _zoomRatio;
            }
            set
            {
                _zoomRatio = value;
                OnPropertyChanged("ZoomRatio");
                OnPropertyChanged("ViewerWidth");
                OnPropertyChanged("ViewerHeight");
                SetZoomRatioToElements(value);
            }
        }

        /// <summary>
        /// Gets or sets the elements of slide.
        /// </summary>
        /// <value>
        /// The elements of slide.
        /// </value>
        public ObservableCollection<ShapeView> Elements
        {
            get
            {
                if (_elements == null || _elements.Count == 0)
                {
                    _elements = new ObservableCollection<ShapeView>();

                    _slide.Elements.ToList().ForEach(shape =>
                    {
                        ShapeView elementView = null;

                        if (shape.Type == ElementType.Rectangle) elementView = new RectangleView(shape as Rectangle);
                        if (shape.Type == ElementType.Square) elementView = new SquareView(shape as Square);
                        if (shape.Type == ElementType.Circle) elementView = new CircleView(shape as Circle);
                        if (shape.Type == ElementType.Ellipse) elementView = new EllipseView(shape as Ellipse);
                        if (shape.Type == ElementType.Image) elementView = new ImageView(shape as SmartPresenter.BO.Common.Entities.Image);
                        if (shape.Type == ElementType.Video) elementView = new VideoView(shape as Video);
                        if (shape.Type == ElementType.Audio) elementView = new AudioView(shape as Audio);
                        if (shape.Type == ElementType.Text) elementView = new TextView(shape as Text);

                        elementView.PropertyChanged += Slide_Element_PropertyChanged;

                        _elements.Add(elementView);
                    });
                }
                return _elements;
            }
        }

        /// <summary>
        /// Gets the width of the viewer.
        /// </summary>
        /// <value>
        /// The width of the viewer.
        /// </value>
        public double ViewerWidth
        {
            get
            {
                return _slide.ParentPresentation.Width * ZoomRatio / 6;
            }
        }

        /// <summary>
        /// Gets the height of the viewer.
        /// </summary>
        /// <value>
        /// The height of the viewer.
        /// </value>
        public double ViewerHeight
        {
            get
            {
                return _slide.ParentPresentation.Height * ZoomRatio / 6;
            }
        }

        /// <summary>
        /// Gets or sets the transition data.
        /// </summary>
        /// <value>
        /// The transition data.
        /// </value>
        public Transition Transition
        {
            get
            {
                return _slide.Transition;
            }
            set
            {
                if (_slide.Transition != null)
                {
                    _slide.Transition.PropertyChanged -= Transition_PropertyChanged;
                }
                OnUndoRedoPropertyChanged("Transition", _slide.Transition, value);
                _slide.Transition = value;
                _slide.Transition.PropertyChanged += Transition_PropertyChanged;
                OnPropertyChanged("Transition");
            }
        }

        /// <summary>
        /// Gets the event aggregator.
        /// </summary>
        /// <value>
        /// The event aggregator.
        /// </value>
        private IEventAggregator EventAggregator
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IEventAggregator>();
            }
        }

        #endregion

        #region Commands

        #region Command Handlers

        #endregion

        #endregion

        #region Methods

        #region Private Methods

        /// <summary>
        /// Handles the ElementsCollectionChanged event of the Slide control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        private void Slide_ElementsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Shape shape in e.NewItems)
                {
                    ShapeView elementView = null;

                    if (shape.Type == ElementType.Rectangle) elementView = new RectangleView(shape as Rectangle);
                    if (shape.Type == ElementType.Square) elementView = new SquareView(shape as Square);
                    if (shape.Type == ElementType.Circle) elementView = new CircleView(shape as Circle);
                    if (shape.Type == ElementType.Ellipse) elementView = new EllipseView(shape as Ellipse);
                    if (shape.Type == ElementType.Image) elementView = new ImageView(shape as SmartPresenter.BO.Common.Entities.Image);
                    if (shape.Type == ElementType.Video) elementView = new VideoView(shape as Video);
                    if (shape.Type == ElementType.Audio) elementView = new AudioView(shape as Audio);
                    if (shape.Type == ElementType.Text) elementView = new TextView(shape as Text);

                    elementView.PropertyChanged += Slide_Element_PropertyChanged;

                    _elements.Add(elementView);
                }
            }
            if (e.OldItems != null)
            {
                foreach (Shape shape in e.OldItems)
                {
                    _elements.Last().PropertyChanged -= Slide_Element_PropertyChanged;
                    ShapeView shapeView = _elements.FirstOrDefault(item => item.GetInnerObject().Equals(shape));
                    _elements.Remove(shapeView);
                }
            }
            OnUndoRedoPropertyChanged("Elements", _slide.Elements, e);
            _slide.MarkDirty();
        }

        /// <summary>
        /// Handles the PropertyChanged event of the Slide_Element control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        void Slide_Element_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _slide.MarkDirty();
        }

        /// <summary>
        /// Renders this instance.
        /// </summary>
        private void Render()
        {
        }

        /// <summary>
        /// Sets the zoom ratio to elements.
        /// </summary>
        /// <param name="zoomValue">The zoom value.</param>
        public void SetZoomRatioToElements(int zoomValue)
        {
            foreach (ShapeView shape in this.Elements)
            {
                shape.ZoomRatio = zoomValue;
            }
        }

        private void Transition_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            EventAggregator.GetEvent<TransitionChangedEvent>().Publish(Transition);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds the element.
        /// </summary>
        /// <param name="shapeView">The element view.</param>
        public void AddElement(ShapeView shapeView)
        {
            this._slide.AddElement(shapeView.GetInnerObject());
        }

        /// <summary>
        /// Adds the element.
        /// </summary>
        /// <param name="shape">The shape.</param>
        public void AddElement(Shape shape)
        {
            this._slide.AddElement(shape);
        }

        /// <summary>
        /// Removes the element.
        /// </summary>
        /// <param name="shape">The shape.</param>
        public void RemoveElement(Shape shape)
        {
            this._slide.RemoveElement(shape);
        }

        /// <summary>
        /// Removes the element.
        /// </summary>
        /// <param name="shapeView">The shape view.</param>
        public void RemoveElement(ShapeView shapeView)
        {
            this._slide.RemoveElement(shapeView.GetInnerObject());
        }

        /// <summary>
        /// Moves the element.
        /// </summary>
        /// <param name="oldIndex">The old index.</param>
        /// <param name="newIndex">The new index.</param>
        public void MoveElement(int oldIndex, int newIndex)
        {
            this._slide.MoveElement(oldIndex, newIndex);
        }

        /// <summary>
        /// Gets the clone of inner object.
        /// </summary>
        /// <returns></returns>
        protected internal virtual ISlide GetInnerObjectCloned()
        {
            return (ISlide)_slide.Clone();
        }

        /// <summary>
        /// Gets the inner object.
        /// </summary>
        /// <returns></returns>
        protected internal virtual ISlide GetInnerObject()
        {
            return _slide;
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public object Clone()
        {
            SlideView slideViewv = new SlideView((ISlide)this._slide.Clone());
            return slideViewv;
        }

        #endregion

        #endregion

        #region Events

        public event UndoRedoEventHandler UndoRedoPropertyChanged;
        
        private void OnUndoRedoPropertyChanged(string propertyName, Object oldValue, Object newValue)
        {
            if(this.UndoRedoPropertyChanged != null)
            {
                this.UndoRedoPropertyChanged(this, new UndoRedoPropertyChangedEventArgs(this, propertyName, oldValue, newValue));
            }
        }

        private void OnUndoRedoPropertyChanged(string propertyName, IList collection, NotifyCollectionChangedEventArgs args)
        {
            if (this.UndoRedoPropertyChanged != null)
            {
                this.UndoRedoPropertyChanged(this, new UndoRedoCollectionChangedEventArgs(this, propertyName, collection, args));
            }
        }

        private void OnUndoRedoPropertyChanged(string propertyName, IList collection, NotifyCollectionChangedAction action, IList oldItems, IList newItems)
        {
            if (this.UndoRedoPropertyChanged != null)
            {
                this.UndoRedoPropertyChanged(this, new UndoRedoCollectionChangedEventArgs(this, propertyName, collection, action, oldItems, newItems));
            }
        }

        #endregion
    }
}
