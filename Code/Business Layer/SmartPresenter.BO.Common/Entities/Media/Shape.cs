using SmartPresenter.BO.Common.Enums;
using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.Common.Enums;
using SmartPresenter.Common.Extensions;
using SmartPresenter.Common.Interfaces;
using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// An interface for all media objects like shapes, images, video etc
    /// </summary>
    [Serializable]
    public abstract class Shape : IEntity
    {
        #region Private Data Members

        [NonSerialized]
        private Brush _borderBrush;
        [NonSerialized]
        private Brush _background;
        [NonSerialized]
        private DropShadowEffect _shadow;
        private double _opacity;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Shape"/> class.
        /// </summary>
        public Shape()
            : this(0, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Shape"/> class.
        /// </summary>
        public Shape(int width, int height)
        {
            if(width < 0)
            {
                throw new ArgumentOutOfRangeException("Width", "Value can't be negative");
            }
            if (height < 0)
            {
                throw new ArgumentOutOfRangeException("Height", "Value can't be negative");
            }
            this.Width = width;
            this.Height = height;
            Initialize();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>
        /// The unique identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the opacity.
        /// </summary>
        /// <value>
        /// The opacity.
        /// </value>
        public double Opacity
        {
            get
            {
                return _opacity;
            }
            set
            {
                if (value < 0 || value > 1)
                {
                    throw new ArgumentOutOfRangeException("Opacity", "Opacity should be in range from 0 to 1");
                }
                _opacity = value;
            }
        }

        /// <summary>
        /// Gets or sets the border thickness of object.
        /// </summary>
        /// <value>
        /// The border thickness of object.
        /// </value>
        public double StrokeThickness { get; set; }
        /// <summary>
        /// Gets or sets the color of the border of object.
        /// </summary>
        /// <value>
        /// The color of the border of object.
        /// </value>
        public Brush Stroke
        {
            get
            {
                return _borderBrush;
            }
            set
            {
                _borderBrush = value;
            }
        }
        /// <summary>
        /// Gets or sets the back color of object.
        /// </summary>
        /// <value>
        /// The back color of the object.
        /// </value>
        public Brush Background
        {
            get
            {
                return _background;
            }
            set
            {
                _background = value;
            }
        }
        /// <summary>
        /// Gets or sets the shadow of object.
        /// </summary>
        /// <value>
        /// The shadow of object.
        /// </value>
        public DropShadowEffect Shadow
        {
            get
            {
                return _shadow;
            }
            set
            {
                _shadow = value;
            }
        }

        /// <summary>
        /// Gets or sets the x cordinate of object.
        /// </summary>
        /// <value>
        /// The x cordinate.
        /// </value>
        public int X { get; set; }
        /// <summary>
        /// Gets or sets the y cordinate of object.
        /// </summary>
        /// <value>
        /// The y cordinate.
        /// </value>
        public int Y { get; set; }
        /// <summary>
        /// Gets or sets the width of object.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public virtual int Width { get; set; }
        /// <summary>
        /// Gets or sets the height of object.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public virtual int Height { get; set; }

        /// <summary>
        /// Gets or sets if the object is horizontally flipped.
        /// </summary>
        public bool IsFlipHorizontal { get; set; }
        /// <summary>
        /// Gets or sets if the objects is vertically flipped.
        /// </summary>
        public bool IsFlipVertical { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public abstract ElementType Type { get; }

        /// <summary>
        /// Gets or sets the rotation angle.
        /// </summary>
        /// <value>
        /// The rotation angle.
        /// </value>
        //public int RotationAngle { get; set; }

        /// <summary>
        /// Gets or sets the transformations.
        /// </summary>
        /// <value>
        /// The transformations.
        /// </value>
        public Transform Transform { get; set; }

        /// <summary>
        /// Gets or sets the parent slide.
        /// </summary>
        /// <value>
        /// The parent slide.
        /// </value>
        public ISlide ParentSlide { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Renders an object.
        /// </summary>
        public abstract void Render();
       
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public object Clone()
        {
            Shape shape = null;

            using (MemoryStream stream = new MemoryStream())
            {
                XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add(String.Empty, String.Empty);
                xmlSerializer.Serialize(stream, this, namespaces);
                stream.Flush();
                stream.Seek(0, SeekOrigin.Begin);

                shape = (Shape)xmlSerializer.Deserialize(stream);
            }

            shape.Id = Guid.NewGuid();

            return shape;
        }
       
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            Id = Guid.NewGuid();
            Background = new SolidColorBrush(Colors.Transparent);
            Stroke = new SolidColorBrush(Colors.Blue);
            StrokeThickness = 1;
            Shadow = new DropShadowEffect();
            IsEnabled = true;
            Opacity = 1;
            Transform = new TransformGroup();
            ((TransformGroup)Transform).Children.Add(new TranslateTransform(0, 0));
            ((TransformGroup)Transform).Children.Add(new RotateTransform(0));
            ((TransformGroup)Transform).Children.Add(new ScaleTransform(1, 1));
            ((TransformGroup)Transform).Children.Add(new SkewTransform(0, 0));
        }

        #endregion

    }
}
