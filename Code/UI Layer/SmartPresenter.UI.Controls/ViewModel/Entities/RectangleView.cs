using SmartPresenter.BO.Common.Entities;
using SmartPresenter.BO.Common.Enums;
using SmartPresenter.Common.Enums;
using System;
using System.ComponentModel;

namespace SmartPresenter.UI.Controls.ViewModel
{
    /// <summary>
    /// A class for Rectangle Object UI.
    /// </summary>
    [DisplayName("Rectangle")]
    [Serializable]
    public class RectangleView : ShapeView
    {
        #region Private Data Members

        private Rectangle _rectangle;

        #endregion

        #region Constructor

        public RectangleView() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleView"/> class.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        public RectangleView(Shape rectangle)
            : base(rectangle)
        {
            _rectangle = rectangle as Rectangle;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleView"/> class.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        public RectangleView(Rectangle rectangle)
            : base(rectangle)
        {
            _rectangle = rectangle;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the corner radius of object.
        /// </summary>
        /// <value>
        /// The corner radius of object.
        /// </value>
        [DisplayName("Corner Radius")]
        [Category("Appearance")]
        public int CornerRadius
        {
            get
            {
                return _rectangle.CornerRadius;
            }
            set
            {
                if (_rectangle != null)
                {
                    _rectangle.CornerRadius = value;
                    OnPropertyChanged("CornerRadius");
                }
            }
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public override ElementType Type
        {
            get
            {
                return ElementType.Rectangle;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the inner object.
        /// </summary>
        /// <returns></returns>
        protected internal override Shape GetInnerObjectCloned()
        {
            return (Rectangle)_rectangle.Clone();
        }

        /// <summary>
        /// Clones the internal.
        /// </summary>
        /// <returns></returns>
        protected override object CloneInternal()
        {
            ShapeView elementView = new RectangleView(this.GetInnerObjectCloned());

            return elementView;
        }

        #endregion

    }
}
