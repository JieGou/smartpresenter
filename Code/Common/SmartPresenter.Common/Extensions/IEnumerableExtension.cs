using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartPresenter.Common.Extensions
{
    /// <summary>
    /// A class to extend IEnumerable interface to support list like methods.
    /// </summary>
    public static class IEnumerableExtension
    {
        #region Static Extension Methods

        ///// <summary>
        ///// Adds the item to specified collection.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="collection">The collection.</param>
        ///// <param name="item">The item.</param>
        //public static void Add<T>(this IEnumerable<T> collection, T item)
        //{
        //    try
        //    {
        //        collection.ToList().Add(item);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Logger.LogMsg.Error(ex.Message, ex);
        //    }
        //}

        ///// <summary>
        ///// Adds the range.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="sourceCollection">The source collection.</param>
        ///// <param name="collection">The collection.</param>
        //public static void AddRange<T>(this IEnumerable<T> sourceCollection, IEnumerable<T> collection)
        //{
        //    try
        //    {
        //        sourceCollection.ToList().AddRange(collection);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Logger.LogMsg.Error(ex.Message, ex);
        //    }
        //}

        ///// <summary>
        ///// Removes the specified source collection.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="sourceCollection">The source collection.</param>
        ///// <param name="item">The item.</param>
        ///// <returns></returns>
        //public static bool Remove<T>(this IEnumerable<T> sourceCollection, T item)
        //{
        //    try
        //    {
        //        return sourceCollection.ToList().Remove(item);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Logger.LogMsg.Error(ex.Message, ex);
        //    }
        //    return false;
        //}

        ///// <summary>
        ///// Removes all.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="sourceCollection">The source collection.</param>
        ///// <param name="match">The match.</param>
        ///// <returns></returns>
        //public static int RemoveAll<T>(this IEnumerable<T> sourceCollection, Predicate<T> match)
        //{
        //    try
        //    {
        //        return sourceCollection.ToList().RemoveAll(match);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Logger.LogMsg.Error(ex.Message, ex);
        //    }
        //    return 0;
        //}

        ///// <summary>
        ///// Removes at.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="sourceCollection">The source collection.</param>
        ///// <param name="index">The index.</param>
        //public static void RemoveAt<T>(this IEnumerable<T> sourceCollection, int index)
        //{
        //    try
        //    {
        //        sourceCollection.ToList().RemoveAt(index);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Logger.LogMsg.Error(ex.Message, ex);
        //    }
        //}

        ///// <summary>
        ///// Removes the range.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="sourceCollection">The source collection.</param>
        ///// <param name="index">The index.</param>
        ///// <param name="count">The count.</param>
        //public static void RemoveRange<T>(this IEnumerable<T> sourceCollection, int index, int count)
        //{
        //    try
        //    {
        //        sourceCollection.ToList().RemoveRange(index, count);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Logger.LogMsg.Error(ex.Message, ex);
        //    }
        //}


        ///// <summary>
        ///// Clears the specified source collection.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="sourceCollection">The source collection.</param>
        //public static void Clear<T>(this IEnumerable<T> sourceCollection)
        //{
        //    try
        //    {
        //        sourceCollection.ToList().Clear();
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Logger.LogMsg.Error(ex.Message, ex);
        //    }
        //}

        /// <summary>
        /// Fors the each.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sourceCollection">The source collection.</param>
        /// <param name="action">The action.</param>
        public static void ForEach<T>(this IEnumerable<T> sourceCollection, Action<T> action)
        {
            sourceCollection.ToList().ForEach(action);
        }

        #endregion
    }
}
