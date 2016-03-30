using SmartPresenter.BO.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// Class to represent a Factory returning Null Shapes.
    /// </summary>
    public sealed class NullShapeFactory : IShapeFactory
    {
        #region Private Data Members

        private static NullShapeFactory _instance;
        private static volatile Object _lock = new Object();

        #endregion

        #region Constructor

        /// <summary>
        /// Prevents a default instance of the <see cref="NullShapeFactory"/> class from being created.
        /// </summary>
        private NullShapeFactory()
        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static NullShapeFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new NullShapeFactory();
                        }
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region IShapeFactory Members
        /// <summary>
        /// Creates the shape.
        /// </summary>
        /// <returns></returns>
        public Shape CreateElement()
        {
            return NullShape.Instance;
        }

        /// <summary>
        /// Creates an shape from specified path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Shape CreateElement(string path)
        {
            return NullShape.Instance;
        }

        #endregion
    }
}
