using SmartPresenter.BO.Common.Enums;
using SmartPresenter.Common.Enums;
using System;
using System.Xml;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// Class representing a live feed video coming from a camera or some input source.
    /// </summary>
    [Serializable]
    public class LiveVideo : Rectangle
    {
        #region Properties

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public override ElementType Type
        {
            get { return ElementType.LiveVideo; }
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
