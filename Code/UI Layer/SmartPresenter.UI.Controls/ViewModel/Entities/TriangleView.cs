using SmartPresenter.BO.Common.Entities;
using SmartPresenter.BO.Common.Enums;
using SmartPresenter.Common.Enums;
using System.ComponentModel;

namespace SmartPresenter.UI.Controls.ViewModel.Entities
{
    /// <summary>
    /// A class for Triangle Object UI.
    /// </summary>
    [DisplayName("Triangle")]
    public class TriangleView : ShapeView
    {
        #region Private Data Members

        private Triangle _triangle;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TriangleView"/> class.
        /// </summary>
        /// <param name="shape">The shape.</param>
        public TriangleView(Shape shape)
            : base(shape)
        {
            _triangle = (Triangle)shape;
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
                return ElementType.Triangle;
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
            return (Triangle)_triangle.Clone();
        }

        /// <summary>
        /// Clones the internal.
        /// </summary>
        /// <returns></returns>
        protected override object CloneInternal()
        {
            ShapeView elementView = new TriangleView(this.GetInnerObjectCloned());

            return elementView;
        }

        #endregion
    }
}
