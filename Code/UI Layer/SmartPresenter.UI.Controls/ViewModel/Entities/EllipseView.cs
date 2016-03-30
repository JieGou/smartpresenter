using SmartPresenter.BO.Common.Entities;
using SmartPresenter.BO.Common.Enums;
using SmartPresenter.Common.Enums;
using System;
using System.ComponentModel;

namespace SmartPresenter.UI.Controls.ViewModel
{
    /// <summary>
    /// A class for Ellipse Object UI.
    /// </summary>
    [DisplayName("Ellipse")]
    [Serializable]
    public class EllipseView : ShapeView
    {
        #region Private Data Members

        private Ellipse _ellipse;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="EllipseView"/> class.
        /// </summary>
        /// <param name="ellipse">The ellipse.</param>
        public EllipseView(Ellipse ellipse)
            : base(ellipse)
        {
            _ellipse = ellipse;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EllipseView"/> class.
        /// </summary>
        /// <param name="shape">The shape.</param>
        public EllipseView(Shape shape)
            : base(shape)
        {
            _ellipse = shape as Ellipse;
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
                return ElementType.Ellipse;
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
            return (Ellipse)_ellipse.Clone();
        }

        /// <summary>
        /// Clones the internal.
        /// </summary>
        /// <returns></returns>
        protected override object CloneInternal()
        {
            ShapeView elementView = new EllipseView(this.GetInnerObjectCloned());

            return elementView;
        }

        #endregion
    }
}
