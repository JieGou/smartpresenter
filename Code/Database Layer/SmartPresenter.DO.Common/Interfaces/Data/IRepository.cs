using SmartPresenter.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartPresenter.Data.Common.Interfaces
{
    /// <summary>
    /// A basic repository interface. Any repository should provide these basic methods.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class, IEntity
    {
        #region Properties

        /// <summary>
        /// Gets the count of the items in repository.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        int Count { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Adds the specified entity to repository.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        bool Add(T entity);
        /// <summary>
        /// Deletes the specified entity from repository.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        bool Delete(T entity);
        /// <summary>
        /// Deletes the entity specified by identifier from repository.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        bool Delete(Guid id);
        /// <summary>
        /// Updates the specified old entity with new one.
        /// </summary>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns></returns>
        bool Update(T oldValue, T newValue);
        /// <summary>
        /// Gets all items from repository.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();
        /// <summary>
        /// Gets the entity specified by identifier from repository.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        T Get(Guid id);
        /// <summary>
        /// Gets the entity specified by query string.
        /// </summary>
        /// <param name="queryString">The query string.</param>
        /// <returns></returns>
        IEnumerable<T> Get(Func<T, bool> queryString);
        /// <summary>
        /// Clears this repository.
        /// </summary>
        void Clear();

        #endregion

    }
}
