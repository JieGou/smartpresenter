using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPresenter.Common.Interfaces
{
    /// <summary>
    /// An interface for all the item which needs to be saved, providing methods for saving.
    /// </summary>
    public interface ILoad
    {
        #region Methods

        /// <summary>
        /// Loads this instance.
        /// </summary>
        void Load();

        /// <summary>
        /// Loads from specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        void Load(string path);

        #endregion
    }

    public interface ILoad<T>
    {
        #region Methods

        /// <summary>
        /// Loads an object.
        /// </summary>
        /// <returns></returns>
        T Load();

        /// <summary>
        /// Loads an object from specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        T Load(string path);

        #endregion
    }
}
