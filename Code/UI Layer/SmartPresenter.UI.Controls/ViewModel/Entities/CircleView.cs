using SmartPresenter.BO.Common.Entities;
using SmartPresenter.BO.Common.Enums;
using SmartPresenter.Common.Enums;
using System;
using System.ComponentModel;

namespace SmartPresenter.UI.Controls.ViewModel
{
    /// <summary>
    /// A class for Circle Object UI.
    /// </summary>
    [DisplayName("Circle")]
    [Serializable]
    public class CircleView : ShapeView
    {
        #region Private Data Members

        private Circle _circle;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CircleView"/> class.
        /// </summary>
        /// <param name="circle">The circle.</param>
        public CircleView(Shape circle)
            : base(circle)
        {
            _circle = (Circle)circle;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CircleView"/> class.
        /// </summary>
        /// <param name="circle">The circle.</param>
        public CircleView(Circle circle)
            : base(circle)
        {
            _circle = circle;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the radius.
        /// </summary>
        /// <value>
        /// The radius.
        /// </value>
        [Category("Layout")]
        public int Radius
        {
            get
            {
                return _circle.Radius;
            }
            set
            {
                _circle.Radius = value;
                OnPropertyChanged("Radius");
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

        #region Methods

        /// <summary>
        /// Gets the inner object.
        /// </summary>
        /// <returns></returns>
        protected internal override Shape GetInnerObjectCloned()
        {
            return (Circle)_circle.Clone();
        }

        /// <summary>
        /// Clones the internal.
        /// </summary>
        /// <returns></returns>
        protected override object CloneInternal()
        {
            ShapeView elementView = new CircleView(this.GetInnerObjectCloned());

            return elementView;
        }

        #endregion
    }
}
