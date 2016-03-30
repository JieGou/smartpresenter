using Microsoft.Practices.Prism.Mvvm;
using SmartPresenter.BO.Common.Entities;
using SmartPresenter.BO.Common.Enums;
using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.BO.UndoRedo;
using SmartPresenter.Common.Enums;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Media.Effects;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace SmartPresenter.UI.Controls.ViewModel
{
    /// <summary>
    /// A class for Shape Object UI.
    /// </summary>
    [Serializable]
    public abstract class ShapeView : BindableBase, ICloneable, IUndoRedoCapable
    {

        #region Private Data Members

        private Shape _shape;
        private bool _isSelected;
        private int _zoomRatio;

        #endregion

        #region Constructor

        public ShapeView() 
        {
            UndoRedoManager.Instance.Register(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShapeView"/> class.
        /// </summary>
        /// <param name="shape">The shape.</param>
        public ShapeView(Shape shape) : this()
        {
            _shape = shape;
            this.ZoomRatio = 1;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the x cordinate of object.
        /// </summary>
        /// <value>
        /// The x cordinate.
        /// </value>
        [Category("Layout")]
        public int X
        {
            get
            {
                return _shape.X;
            }
            set
            {
                if (_shape != null)
                {
                    OnUndoRedoPropertyChanged("X", _shape.X, value);
                    _shape.X = value;
                    OnPropertyChanged("X");
                }
            }
        }
        /// <summary>
        /// Gets or sets the y cordinate of object.
        /// </summary>
        /// <value>
        /// The y cordinate.
        /// </value>
        [Category("Layout")]
        public int Y
        {
            get
            {
                return _shape.Y;
            }
            set
            {
                if (_shape != null)
                {
                    OnUndoRedoPropertyChanged("Y", _shape.Y, value);
                    _shape.Y = value;
                    OnPropertyChanged("Y");
                }
            }
        }
        /// <summary>
        /// Gets or sets the width of object.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        [Category("Layout")]
        public int Width
        {
            get
            {
                return _shape.Width;
            }
            set
            {
                if (_shape != null)
                {
                    OnUndoRedoPropertyChanged("Width", _shape.Width, value);
                    _shape.Width = value;
                    OnPropertyChanged("Width");
                }
            }
        }
        /// <summary>
        /// Gets or sets the height of object.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        [Category("Layout")]
        public int Height
        {
            get
            {
                return _shape.Height;
            }
            set
            {
                if (_shape != null)
                {
                    OnUndoRedoPropertyChanged("Height", _shape.Height, value);
                    _shape.Height = value;
                    OnPropertyChanged("Height");
                }
            }
        }

        /// <summary>
        /// Gets or sets the border thickness of object.
        /// </summary>
        /// <value>
        /// The border thickness of object.
        /// </value>
        [DisplayName("Stroke Thickness")]
        [Category("Appearance")]
        public double StrokeThickness
        {
            get
            {
                return _shape.StrokeThickness;
            }
            set
            {
                if (_shape != null)
                {
                    OnUndoRedoPropertyChanged("StrokeThickness", _shape.StrokeThickness, value);
                    _shape.StrokeThickness = value;
                    OnPropertyChanged("StrokeThickness");
                }
            }
        }
        /// <summary>
        /// Gets or sets the color of the border of object.
        /// </summary>
        /// <value>
        /// The color of the border of object.
        /// </value>
        [Category("Brush")]
        public Brush Stroke
        {
            get
            {
                return _shape.Stroke;
            }
            set
            {
                if (_shape != null)
                {
                    OnUndoRedoPropertyChanged("Stroke", _shape.Stroke, value);
                    _shape.Stroke = value;
                    OnPropertyChanged("Stroke");
                }
            }
        }
        /// <summary>
        /// Gets or sets the back color of object.
        /// </summary>
        /// <value>
        /// The back color of the object.
        /// </value>
        [Category("Brush")]
        public Brush Background
        {
            get
            {
                return _shape.Background;
            }
            set
            {
                if (_shape != null)
                {
                    OnUndoRedoPropertyChanged("Background", _shape.Background, value);
                    _shape.Background = value;
                    OnPropertyChanged("Background");
                }
            }
        }
        /// <summary>
        /// Gets or sets the shadow of object.
        /// </summary>
        /// <value>
        /// The shadow of object.
        /// </value>
        [ExpandableObject]
        [Category("Appearance")]
        public DropShadowEffect Shadow
        {
            get
            {
                return _shape.Shadow;
            }
            set
            {
                if (_shape != null)
                {
                    OnUndoRedoPropertyChanged("Shadow", _shape.Shadow, value);
                    _shape.Shadow = value;
                    OnPropertyChanged("Shadow");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </value>
        [Category("Common")]
        public bool IsEnabled
        {
            get
            {
                return _shape.IsEnabled;
            }
            set
            {
                if (_shape != null)
                {
                    OnUndoRedoPropertyChanged("IsEnabled", _shape.IsEnabled, value);
                    _shape.IsEnabled = value;
                    OnPropertyChanged("IsEnabled");
                }
            }
        }

        /// <summary>
        /// Gets or sets the opacity.
        /// </summary>
        /// <value>
        /// The opacity.
        /// </value>
        [Category("Appearance")]
        public double Opacity
        {
            get
            {
                return _shape.Opacity;
            }
            set
            {
                if (_shape != null)
                {
                    OnUndoRedoPropertyChanged("Opacity", _shape.Opacity, value);
                    _shape.Opacity = value;
                    OnPropertyChanged("Opacity");
                }
            }
        }

        /// <summary>
        /// Gets or sets if an item is selected on UI.
        /// </summary>
        [Category("Common")]
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                if (_shape != null)
                {
                    _isSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }

        /// <summary>
        /// Transformations applied to object.
        /// </summary>
        [Category("Transform")]
        public Transform Transform
        {
            get
            {
                return _shape.Transform;
            }
            set
            {
                if (_shape != null)
                {
                    OnUndoRedoPropertyChanged("Transform", _shape.Transform, value);
                    _shape.Transform = value;
                    OnPropertyChanged("Transform");
                }
            }
        }

        /// <summary>
        /// Gets the parent slide.
        /// </summary>
        /// <value>
        /// The parent slide.
        /// </value>
        public ISlide ParentSlide
        {
            get
            {
                return _shape.ParentSlide;
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
                if (_shape != null)
                {
                    _zoomRatio = value;
                    OnPropertyChanged("ZoomRatio");
                    OnPropertyChanged("ViewerWidth");
                    OnPropertyChanged("ViewerHeight");
                    OnPropertyChanged("ViewerX");
                    OnPropertyChanged("ViewerY");
                }
            }
        }

        /// <summary>
        /// Gets the viewer viewer x.
        /// </summary>
        /// <value>
        /// The viewer viewer x.
        /// </value>
        public double ViewerX
        {
            get
            {
                return _shape.X * ZoomRatio / 6;
            }
        }

        /// <summary>
        /// Gets the viewer viewer y.
        /// </summary>
        /// <value>
        /// The viewer viewer y.
        /// </value>
        public double ViewerY
        {
            get
            {
                return _shape.Y * ZoomRatio / 6;
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
                return _shape.Width * ZoomRatio / 6;
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
                return _shape.Height * ZoomRatio / 6;
            }
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>        
        public abstract ElementType Type { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return _shape.Id.GetHashCode();
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
            if (obj is ShapeView)
            {
                ShapeView targetObject = obj as ShapeView;
                return _shape.Id.Equals(targetObject._shape.Id);
            }
            return false;
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public virtual object Clone()
        {
            return CloneInternal();
        }

        /// <summary>
        /// internal method for Cloning, will be overridden in derived classes to provide their own cloning behaviour.
        /// </summary>
        /// <returns></returns>
        protected abstract Object CloneInternal();

        /// <summary>
        /// Gets the clone of inner object.
        /// </summary>
        /// <returns></returns>
        protected internal virtual Shape GetInnerObjectCloned()
        {
            return (Shape)_shape.Clone();
        }

        /// <summary>
        /// Gets the inner object.
        /// </summary>
        /// <returns></returns>
        protected internal virtual Shape GetInnerObject()
        {
            return _shape;
        }

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
