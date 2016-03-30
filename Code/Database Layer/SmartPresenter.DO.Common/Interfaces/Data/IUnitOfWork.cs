
using SmartPresenter.Common.Interfaces;

namespace SmartPresenter.Data.Common.Interfaces
{
    /// <summary>
    /// represents a generic unit of work. Can be used as any business transaction.
    /// </summary>
    interface IUnitOfWork<T> where T : class, IEntity
    {
        #region Properties

        /// <summary>
        /// Gets or sets the repository.
        /// </summary>
        /// <value>
        /// The repository.
        /// </value>
        IRepository<T> Repository { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Commits the changes made to this instance.
        /// </summary>
        /// <returns>true if commit is successfull else false</returns>
        bool Commit();
        /// <summary>
        /// Rollbacks the changes made to this instance.
        /// </summary>
        /// <returns>true if rollback is successfull else false</returns>
        bool Rollback();

        /// <summary>
        /// Safe commit, If commit fails then automatically rollback.
        /// </summary>
        /// <returns></returns>
        bool SafeCommit();

        #endregion
    }
}
