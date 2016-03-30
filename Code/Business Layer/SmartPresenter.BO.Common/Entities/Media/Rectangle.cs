using SmartPresenter.BO.Common.Enums;
using SmartPresenter.Common.Enums;
using SmartPresenter.Common.Extensions;
using System;
using System.Windows.Media.Effects;
using System.Xml;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// A clas to represent Rectangle.
    /// </summary>
    [Serializable]
    public class Rectangle : Shape
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> class.
        /// </summary>
        public Rectangle()
            : this(0, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rectangle"/> class.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Rectangle(int width, int height) : base(width, height)
        {
            Initialize();
            Width = width;
            Height = height;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the corner radius of object.
        /// </summary>
        /// <value>
        /// The corner radius of object.
        /// </value>
        public int CornerRadius { get; set; }

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

        #region Private Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            this.Shadow = new DropShadowEffect();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Renders an object.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void Render()
        {
            throw new NotImplementedException();
        }        

        #endregion

        #endregion

    }
}
