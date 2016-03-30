
using SmartPresenter.Common.Interfaces;

namespace SmartPresenter.BO.Common.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T1">The type of the 1.</typeparam>
    /// <typeparam name="T2">The type of the 2.</typeparam>
    /// <typeparam name="T3">The type of the 3.</typeparam>
    interface ILibraryFactory<T1, T2>
        where T1 : class, IEntity
        where T2 : IPlaylist<T1>
    {
        #region Methods

        /// <summary>
        /// Creates a library object.
        /// </summary>
        /// <returns></returns>
        ILibrary<T1, T2> CreateLibrary();

        #endregion
    }
}
