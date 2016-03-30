using SmartPresenter.BO.Common.Enums;
using SmartPresenter.Common.Enums;
using System;
using System.Xml;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// A null shape object.
    /// </summary>
    [Serializable]
    public sealed class NullShape : Shape
    {
        #region Private Data Members

        private static volatile NullShape _instance;
        private static Object _lock = new Object();

        #endregion

        #region Constructor

        /// <summary>
        /// Prevents a default instance of the <see cref="NullPresentation"/> class from being created.
        /// </summary>
        private NullShape()
        {

        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the single instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static NullShape Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new NullShape();
                        }
                    }
                }
                return _instance;
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
                return ElementType.None;
            }
        }

        /// <summary>
        /// Renders an object.
        /// </summary>
        public override void Render()
        {
        }

        #endregion

        #region Methods

        #endregion
    }
}
