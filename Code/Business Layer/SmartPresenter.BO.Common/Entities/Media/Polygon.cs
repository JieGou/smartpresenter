using SmartPresenter.BO.Common.Enums;
using SmartPresenter.Common.Enums;
using SmartPresenter.Common.Extensions;
using System;
using System.Windows;
using System.Windows.Media;
using System.Xml;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// A class representing Polygon object.
    /// </summary>
    [Serializable]
    public class Polygon : Shape
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon"/> class.
        /// </summary>
        public Polygon()
        {
            Initialize();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        /// <value>
        /// The points.
        /// </value>
        public PointCollection Points { get; set; }

        /// <summary>
        /// Gets or sets the fill rule.
        /// </summary>
        /// <value>
        /// The fill rule.
        /// </value>
        public FillRule FillRule { get; set; }

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
        /// Renders an object.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void Render()
        {
            throw new NotImplementedException();
        }        

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            this.Points = new PointCollection();
        }

        #endregion
    }
}
