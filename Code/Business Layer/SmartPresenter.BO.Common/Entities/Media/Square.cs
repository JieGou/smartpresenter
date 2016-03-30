using SmartPresenter.BO.Common.Enums;
using SmartPresenter.Common.Enums;
using System;
using System.Xml;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// A class to represent a Square object.
    /// </summary>
    [Serializable]
    public class Square : Rectangle
    {

        #region Private Data Members

        private int _side;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Square"/> class.
        /// </summary>
        public Square()
            : base()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Square"/> class.
        /// </summary>
        /// <param name="length">The length.</param>
        public Square(int length)
            : base(length, length)
        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the width of Square.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public override int Width
        {
            get
            {
                return _side;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Side", "Side of square can't be negative");
                }
                _side = value;
            }
        }

        /// <summary>
        /// Gets or sets the height of Square.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public override int Height
        {
            get
            {
                return _side;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Side", "Side of square can't be negative");
                }
                _side = value;
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
                return ElementType.Square;
            }
        }

        #endregion        

    }
}
