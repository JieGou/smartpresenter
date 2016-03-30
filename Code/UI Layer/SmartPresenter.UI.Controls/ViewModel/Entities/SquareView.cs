using SmartPresenter.BO.Common.Entities;
using SmartPresenter.BO.Common.Enums;
using SmartPresenter.Common.Enums;
using System;
using System.ComponentModel;

namespace SmartPresenter.UI.Controls.ViewModel
{
    /// <summary>
    /// A class for Square Object UI.
    /// </summary>
    [DisplayName("Square")]
    [Serializable]
    public class SquareView : RectangleView
    {
        #region Private Data Members

        private Square _square;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SquareView"/> class.
        /// </summary>
        /// <param name="shape">The shape.</param>
        public SquareView(Shape shape)
            : base(shape)
        {
            _square = (Square)shape;
        }

        #endregion

        #region Properties

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

        #region Methods

        /// <summary>
        /// Gets the inner object.
        /// </summary>
        /// <returns></returns>
        protected internal override Shape GetInnerObjectCloned()
        {
            return (Square)_square.Clone();
        }

        /// <summary>
        /// Clones the internal.
        /// </summary>
        /// <returns></returns>
        protected override object CloneInternal()
        {
            ShapeView elementView = new SquareView(this.GetInnerObjectCloned());

            return elementView;
        }

        #endregion
    }
}
