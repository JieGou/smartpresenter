using SmartPresenter.BO.Common.Enums;
using SmartPresenter.Common.Enums;
using System;
using System.Xml;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// A class representing Ellipse object.
    /// </summary>
    [Serializable]
    public class Ellipse : Shape
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Ellipse"/> class.
        /// </summary>
        public Ellipse()
            : base()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Ellipse"/> class.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Ellipse(int width, int height)
            : base(width, height)
        {

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

        #region Method

        #region Private Methods

        

        #endregion

        #region Public Methods

        /// <summary>
        /// Renders an object.
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
