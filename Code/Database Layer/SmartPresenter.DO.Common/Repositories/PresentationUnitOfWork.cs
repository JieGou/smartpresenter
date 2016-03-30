using SmartPresenter.Common.Logger;
using SmartPresenter.Data.Common.Interfaces;
using SmartPresenter.Data.Entities;
using System;
using System.Collections.Generic;

namespace SmartPresenter.Data.Common.Repositories
{
    /// <summary>
    /// A unit of work done on any presentation.
    /// </summary>
    public class PresentationUnitOfWork : IUnitOfWork<PresentationDTO>
    {
        #region Private Data Members

        private List<PresentationDTO> _safeCopy;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PresentationUnitOfWork"/> class.
        /// </summary>
        /// <param name="location">The location.</param>
        public PresentationUnitOfWork(string location)
        {
            _safeCopy = new List<PresentationDTO>();
            Repository = new PresentationRepository(location);

            UpdateSafeCopy();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the repository.
        /// </summary>
        /// <value>
        /// The repository.
        /// </value>
        public IRepository<PresentationDTO> Repository { get; set; }

        #endregion

        #region Methods

        #region Private Methods

        /// <summary>
        /// Updates the safe copy.
        /// </summary>
        private void UpdateSafeCopy()
        {
            _safeCopy.Clear();
            foreach (PresentationDTO presentation in Repository.GetAll())
            {
                _safeCopy.Add((PresentationDTO)presentation.Clone());
            }
        }

        #endregion

        #region IUnitOfWork Members

        /// <summary>
        /// Commits the changes made to this instance.
        /// </summary>
        /// <returns>
        /// true if commit is successfull else false
        /// </returns>
        public bool Commit()
        {
            bool hasCommitSuceeded = true;

            foreach (PresentationDTO presentation in Repository.Get(p => p.IsDirty))
            {
                try
                {
                    //presentation.Save();
                }
                catch (Exception ex)
                {
                    hasCommitSuceeded = false;
                    Logger.LogMsg.Error(string.Format("Saving {0} Failed.", presentation.Name));
                    Logger.LogMsg.Error(ex.Message, ex);
                }
            }
            if (hasCommitSuceeded == true)
            {
                UpdateSafeCopy();
            }

            return hasCommitSuceeded;
        }

        /// <summary>
        /// Rollbacks the changes made to this instance.
        /// </summary>
        /// <returns>
        /// true if rollback is successfull else false
        /// </returns>
        public bool Rollback()
        {
            bool hasRollbackSuceeded = true;

            Repository.Clear();
            foreach (PresentationDTO presentation in _safeCopy)
            {
                try
                {
                    if (Repository.Add(presentation) == false)
                    {
                        hasRollbackSuceeded = false;
                    }
                }
                catch (Exception ex)
                {
                    hasRollbackSuceeded = false;
                    Logger.LogMsg.Error(string.Format("Saving {0} Failed.", presentation.Name));
                    Logger.LogMsg.Error(ex.Message, ex);
                }
            }

            return hasRollbackSuceeded;
        }

        /// <summary>
        /// Safe commit, If commit fails then automatically rollback.
        /// </summary>
        /// <returns></returns>
        public bool SafeCommit()
        {
            if (Commit() == false)
            {
                return Rollback();
            }
            return true;
        }

        #endregion

        #endregion
    }
}
