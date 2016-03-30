using SmartPresenter.BO.Common.Entities;
using SmartPresenter.BO.Common.Enums;
using SmartPresenter.Common.Enums;
using System.Windows.Media;

namespace SmartPresenter.UI.Controls.ViewModel.Entities
{
    /// <summary>
    /// A class for Polygon Object UI.
    /// </summary>
    public class PolygonView : ShapeView
    {

        #region Private Data Members

        private Polygon _polygon;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PolygonView"/> class.
        /// </summary>
        /// <param name="shape">The shape.</param>
        public PolygonView(Shape shape)
            : base(shape)
        {
            _polygon = shape as Polygon;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        /// <value>
        /// The points.
        /// </value>
        public PointCollection Points
        {
            get
            {
                return _polygon.Points;
            }
            set
            {
                _polygon.Points = value;
                OnPropertyChanged("Points");
            }
        }

        /// <summary>
        /// Gets or sets the fill rule.
        /// </summary>
        /// <value>
        /// The fill rule.
        /// </value>
        public FillRule FillRule
        {
            get
            {
                return _polygon.FillRule;
            }
            set
            {
                _polygon.FillRule = value;
                OnPropertyChanged("FillRule");
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
                return ElementType.Polygon;
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
            return (Polygon)_polygon.Clone();
        }

        /// <summary>
        /// Clones the internal.
        /// </summary>
        /// <returns></returns>
        protected override object CloneInternal()
        {
            ShapeView elementView = new PolygonView(this.GetInnerObjectCloned());

            return elementView;
        }

        #endregion
    }
}
