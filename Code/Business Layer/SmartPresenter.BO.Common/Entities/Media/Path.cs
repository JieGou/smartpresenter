using SmartPresenter.BO.Common.Enums;
using SmartPresenter.Common.Enums;
using System;
using System.Windows.Media;
using System.Xml;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// A class representing a Path object, A Path is made if points.
    /// </summary>
    public class Path : Shape
    {
        #region Properties

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public Geometry Data { get; set; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public override ElementType Type
        {
            get { return ElementType.Path; }
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

        #endregion

    }
}
