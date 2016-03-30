using SmartPresenter.Common;
using SmartPresenter.Common.Logger;
using SmartPresenter.Data.Common.Interfaces;
using SmartPresenter.Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SmartPresenter.Data.Common.Repositories
{
    /// <summary>
    /// A Concrete Factory class to create Presentations.
    /// </summary>
    public class PresentationRepository : IRepository<PresentationDTO>
    {
        #region Private Data Members

        /// <summary>
        /// The list of presentations in store.
        /// </summary>
        private List<PresentationDTO> _presentations = new List<PresentationDTO>();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PresentationRepository"/> class.
        /// </summary>
        /// <param name="library">The library.</param>
        public PresentationRepository(string location)
        {
            Initialize(location);
        }

        #endregion

        #region Methods

        #region Private Methods

        /// <summary>
        /// Initializes the Repository with contents of specified location.
        /// </summary>
        /// <param name="location">The location.</param>
        private void Initialize(string location)
        {
            if (string.IsNullOrEmpty(location) == false && Directory.Exists(location))
            {
                foreach (string filePath in Directory.EnumerateFiles(location, string.Concat("*", Constants.Default_Document_Extension)))
                {
                    try
                    {
                        _presentations.Add(PresentationDataManager.LoadPresentation(filePath));
                    }
                    catch (Exception ex)
                    {
                        Logger.LogMsg.Error(ex.Message, ex);
                    }
                }
            }
        }

        #endregion

        #region IRepository<Presentation> Members

        /// <summary>
        /// Gets the count of items contained in the repository.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count
        {
            get
            {
                return _presentations.Count;
            }
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public bool Add(PresentationDTO entity)
        {
            try
            {
                if (entity != null && _presentations.Contains(entity) == false)
                {
                    //entity.MarkDirty();
                    _presentations.Add(entity);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.LogMsg.Error(ex.Message, ex);
                return false;
            }
            return false;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public bool Delete(PresentationDTO entity)
        {
            try
            {
                if (entity != null && _presentations.Contains(entity) == true)
                {
                    _presentations.Remove(entity);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.LogMsg.Error(ex.Message, ex);
                return false;
            }
            return false;
        }

        /// <summary>
        /// Deletes the object specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public bool Delete(Guid id)
        {
            try
            {
                PresentationDTO presentation = _presentations.FirstOrDefault(p => p.Id.Equals(id));
                if (presentation != null)
                {
                    _presentations.Remove(presentation);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.LogMsg.Error(ex.Message, ex);
                return false;
            }
            return false;
        }

        /// <summary>
        /// Updates the old value with new value.
        /// </summary>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool Update(PresentationDTO oldValue, PresentationDTO newValue)
        {
            if (newValue != null && _presentations.Contains(oldValue))
            {
                //newValue.MarkDirty();
                _presentations[_presentations.IndexOf(oldValue)] = newValue;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets all presentations from store.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PresentationDTO> GetAll()
        {
            try
            {
                return _presentations.AsQueryable<PresentationDTO>();
            }
            catch (Exception ex)
            {
                Logger.LogMsg.Error(ex.Message, ex);
                return new List<PresentationDTO>().AsQueryable();
            }
        }

        /// <summary>
        /// Gets the presentation specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public PresentationDTO Get(Guid id)
        {
            try
            {
                return _presentations.FirstOrDefault(presentation => presentation.Id.Equals(id)) ?? null;
            }
            catch (Exception ex)
            {
                Logger.LogMsg.Error(ex.Message, ex);
                return null;
            }
        }

        /// <summary>
        /// Finds the presentation specified query string.
        /// </summary>
        /// <param name="queryString">The query string.</param>
        /// <returns></returns>
        public IEnumerable<PresentationDTO> Get(Func<PresentationDTO, bool> queryString)
        {
            try
            {
                return _presentations.Where(queryString).AsQueryable();
            }
            catch (Exception ex)
            {
                Logger.LogMsg.Error(ex.Message, ex);
                return null;
            }
        }

        /// <summary>
        /// Clears this reporitory.
        /// </summary>
        public void Clear()
        {
            _presentations.Clear();
        }

        #endregion

        #endregion
    }
}
