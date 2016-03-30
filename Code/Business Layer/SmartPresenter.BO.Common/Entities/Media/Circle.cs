using SmartPresenter.BO.Common.Enums;
using SmartPresenter.Common.Enums;
using SmartPresenter.Common.Extensions;
using System;
using System.Xml;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// A class representing a Circle.
    /// </summary>
    [Serializable]
    public class Circle : Shape
    {
        #region Private Data Members

        private int _radiius;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Circle"/> class.
        /// </summary>
        public Circle()
            : base()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Circle"/> class.
        /// </summary>
        /// <param name="radius">The radius.</param>
        public Circle(int radius)
            : base(radius * 2, radius * 2)
        {
            if(radius < 0)
            {
                throw new ArgumentOutOfRangeException("Radius", "Circle radius can't be negative");
            }
            Radius = radius;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the radius of Circle.
        /// </summary>
        /// <value>
        /// The radius of Circle.
        /// </value>
        public int Radius
        {
            get
            {
                return _radiius;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Radius", "Circle radius can't be negative");
                }
                _radiius = value;
                Width = Height = value * 2;
            }
        }

        /// <summary>
        /// Gets or sets the width of Circle.
        /// </summary>
        /// <value>
        /// The width of Circle.
        /// </value>
        public override int Width
        {
            get
            {
                return base.Width;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Radius", "Circle radius can't be negative");
                }
                base.Width = value;
                _radiius = value / 2;
            }
        }

        /// <summary>
        /// Gets or sets the height of Circle.
        /// </summary>
        /// <value>
        /// The height of Circle.
        /// </value>
        public override int Height
        {
            get
            {
                return base.Height;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Radius", "Circle radius can't be negative");
                }
                base.Height = value;
                _radiius = value / 2;
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
                return ElementType.Circle;
            }
        }

        #endregion

        #region Method

        #region Private Methods

        

        #endregion

        #region Public Methods

        /// <summary>
        /// Renders a Circle.
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
