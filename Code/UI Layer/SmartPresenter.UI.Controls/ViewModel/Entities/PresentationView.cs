using Microsoft.Practices.Prism.Mvvm;
using SmartPresenter.BO.Common;
using SmartPresenter.BO.Common.Entities;
using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.BO.UndoRedo;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;

namespace SmartPresenter.UI.Controls.ViewModel
{
    /// <summary>
    /// A class for Presentation Object UI.
    /// </summary>
    public class PresentationView : BindableBase, IUndoRedoCapable
    {
        #region Private Data Members

        private bool _isEditable;
        private IPresentation _presentation;
        private ObservableCollection<SlideView> _slides = new ObservableCollection<SlideView>();
        private int _zoomRatio;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PresentationView"/> class.
        /// </summary>
        /// <param name="presentation">The presentation.</param>
        public PresentationView(Presentation presentation)
        {
            _presentation = presentation;
            _presentation.SlidesCollectionChanged += Presentation_SlidesCollectionChanged;
            Initialize();
            UndoRedoManager.Instance.Register(this);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the presentation.
        /// </summary>
        /// <value>
        /// The presentation.
        /// </value>
        public IPresentation Presentation
        {
            get
            {
                return _presentation;
            }
        }

        /// <summary>
        /// Name of the document.
        /// </summary>
        public string Name
        {
            get
            {
                return _presentation.Name;
            }
            set
            {
                OnUndoRedoPropertyChanged("Name", _presentation.Name, value);
                string oldPath = _presentation.Path;
                _presentation.Name = value;
                OnPropertyChanged("Name");
                OnPropertyChanged("Path");
                _presentation.Save();
                File.Delete(oldPath);
            }
        }

        /// <summary>
        /// Size of the document.
        /// </summary>
        public string Size
        {
            get
            {
                FileInfo fileInfo = new FileInfo(Path);

                if (fileInfo.Length < 1024)
                {
                    return string.Format(Thread.CurrentThread.CurrentUICulture, "{0} B", fileInfo.Length);
                }
                else if (fileInfo.Length < (1024 * 1024))
                {
                    return string.Format(Thread.CurrentThread.CurrentUICulture, "{0} KB", fileInfo.Length / 1024);
                }
                else if (fileInfo.Length < (1024 * 1024 * 1024))
                {
                    return string.Format(Thread.CurrentThread.CurrentUICulture, "{0} MB", fileInfo.Length / (1024 * 1024));
                }
                return "N/A";
            }
        }

        /// <summary>
        /// Category for a document like Home, College, Office Presentation.
        /// </summary>
        public string Category
        {
            get
            {
                return _presentation.Category;
            }
            set
            {
                OnUndoRedoPropertyChanged("Category", _presentation.Category, value);
                _presentation.Category = value;
                OnPropertyChanged("Category");
            }
        }

        /// <summary>
        /// Path for a document stored on drive.
        /// </summary>
        public string Path
        {
            get
            {
                return _presentation.Path;
            }
        }

        /// <summary>
        /// If Document is in editable mode.
        /// </summary>
        public bool IsEditable
        {
            get
            {
                return _isEditable;
            }
            set
            {
                _isEditable = value;
                OnPropertyChanged("IsEditable");
            }
        }

        /// <summary>
        /// Gets or sets the parent library location.
        /// </summary>
        /// <value>
        /// The parent library location.
        /// </value>
        public string ParentLibraryLocation
        {
            get
            {
                return _presentation.ParentLibraryLocation;
            }
            set
            {
                _presentation.ParentLibraryLocation = value;
            }
        }

        /// <summary>
        /// Gets the slides.
        /// </summary>
        /// <value>
        /// The slides.
        /// </value>
        public ReadOnlyObservableCollection<SlideView> Slides
        {
            get
            {
                if (_slides == null || _slides.Count == 0)
                {
                    _slides = new ObservableCollection<SlideView>();
                    foreach (ISlide slide in _presentation.Slides)
                    {
                        SlideView slideView = new SlideView(slide);
                        slideView.PropertyChanged += Slide_PropertyChanged;
                        _slides.Add(slideView);
                    }
                }
                return new ReadOnlyObservableCollection<SlideView>(_slides);
            }
        }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        public PlaylistView Parent { get; set; }

        /// <summary>
        /// Gets or sets the width of the presentation.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int Width
        {
            get
            {
                return _presentation.Width;
            }
            set
            {
                OnUndoRedoPropertyChanged("Width", _presentation.Width, value);
                _presentation.Width = value;
                OnPropertyChanged("Width");
            }
        }

        /// <summary>
        /// Gets or sets the height of presentation.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int Height
        {
            get
            {
                return _presentation.Height;
            }
            set
            {
                OnUndoRedoPropertyChanged("Height", _presentation.Height, value);
                _presentation.Height = value;
                OnPropertyChanged("Height");
            }
        }

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
                SetZoomRatioToSlides(value);
            }
        }

        /// <summary>
        /// Type of Presentation.
        /// </summary>
        public virtual PresentationType Type
        {
            get
            {
                return PresentationType.Presentation;
            }
        }

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// Updates the business object.
        /// </summary>
        public void UpdateBusinessObject()
        {
            _presentation.Save();
        }

        /// <summary>
        /// Adds the new slide.
        /// </summary>
        public void AddNewSlide()
        {
            _presentation.AddNewSlide();
            UpdateBusinessObject();
        }

        /// <summary>
        /// Adds the element.
        /// </summary>
        /// <param name="slideView">The element view.</param>
        public void AddSlide(SlideView slideView)
        {
            this._presentation.AddSlide(slideView.GetInnerObject());
        }

        /// <summary>
        /// Adds the element.
        /// </summary>
        /// <param name="slide">The slide.</param>
        public void AddSlide(SmartPresenter.BO.Common.Entities.Slide slide)
        {
            this._presentation.AddSlide(slide);
        }

        /// <summary>
        /// Removes the element.
        /// </summary>
        /// <param name="slide">The slide.</param>
        public void RemoveSlide(SmartPresenter.BO.Common.Entities.Slide slide)
        {
            this._presentation.RemoveSlide(slide);
        }

        /// <summary>
        /// Removes the element.
        /// </summary>
        /// <param name="slideView">The slide view.</param>
        public void RemoveSlide(SlideView slideView)
        {
            this._presentation.RemoveSlide(slideView.GetInnerObject());
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            this.ZoomRatio = 1;
        }

        /// <summary>
        /// Handles the PropertyChanged event of the Slide control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void Slide_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _presentation.MarkDirty();
        }

        /// <summary>
        /// Handles the SlidesCollectionChanged event of the Presentation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        private void Presentation_SlidesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (ISlide slide in e.NewItems)
                {
                    SlideView slideView = new SlideView(slide);

                    slideView.PropertyChanged += Slide_PropertyChanged;

                    _slides.Add(slideView);
                }
            }
            if (e.OldItems != null)
            {
                foreach (ISlide slide in e.OldItems)
                {
                    _slides.Last().PropertyChanged -= Slide_PropertyChanged;
                    SlideView slideView = _slides.FirstOrDefault(item => item.GetInnerObject().Equals(slide));
                    _slides.Remove(slideView);
                }
            }
            OnUndoRedoPropertyChanged("Slides", _presentation.Slides, e);
            _presentation.MarkDirty();
        }

        /// <summary>
        /// Sets the zoom ratio to slides.
        /// </summary>
        /// <param name="zoomValue">The zoom value.</param>
        public void SetZoomRatioToSlides(int zoomValue)
        {
            foreach (SlideView slide in this.Slides)
            {
                slide.ZoomRatio = zoomValue;
            }
        }

        #endregion

        #endregion

        #region Events

        public event UndoRedoEventHandler UndoRedoPropertyChanged;

        private void OnUndoRedoPropertyChanged(string propertyName, Object oldValue, Object newValue)
        {
            if (this.UndoRedoPropertyChanged != null)
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
