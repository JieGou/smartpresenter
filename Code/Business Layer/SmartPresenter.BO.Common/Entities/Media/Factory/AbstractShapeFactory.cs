using SmartPresenter.BO.Common.Enums;
using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// Abstract base class for creating shape factories.
    /// </summary>
    internal abstract class AbstractShapeFactory
    {
        #region Methods

        /// <summary>
        /// Creates the factory based on the type given.
        /// </summary>
        /// <param name="elementType">Type of the element.</param>
        /// <returns></returns>
        public abstract IShapeFactory CreateFactory(ElementType elementType);
        /// <summary>
        /// Creates the factory based on the name given.
        /// </summary>
        /// <param name="elementType">Type of the element.</param>
        /// <returns></returns>
        public abstract IShapeFactory CreateFactory(string elementType);

        #endregion
    }
}
