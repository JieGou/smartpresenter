using SmartPresenter.BO.Common.Enums;
using SmartPresenter.Common.Enums;
using SmartPresenter.Common.Extensions;
using System;
using System.Xml;
namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// A class to represent Line object.
    /// </summary>
    public class Line : Shape
    {
        #region Properties

        /// <summary>
        /// Gets or sets the x1 cordinate.
        /// </summary>
        /// <value>
        /// The x1 cordinate.
        /// </value>
        public double X1 { get; set; }

        /// <summary>
        /// Gets or sets the x2 cordinate.
        /// </summary>
        /// <value>
        /// The x2 cordinate.
        /// </value>
        public double X2 { get; set; }

        /// <summary>
        /// Gets or sets the y1 cordinate.
        /// </summary>
        /// <value>
        /// The y1 cordinate.
        /// </value>
        public double Y1 { get; set; }

        /// <summary>
        /// Gets or sets the y2 cordinate.
        /// </summary>
        /// <value>
        /// The y2 cordinate.
        /// </value>
        public double Y2 { get; set; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public override ElementType Type
        {
            get { return ElementType.Line; }
        }

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// Renders a Line.
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
