
using System.Collections.Generic;
namespace SmartPresenter.BO.Media.Encoder
{
    /// <summary>
    /// A class to perform batch encoding.
    /// </summary>
    public class EncodingJob
    {
        #region Private Data Members



        #endregion

        #region Constructor



        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the media items.
        /// </summary>
        /// <value>
        /// The media items.
        /// </value>
        public List<MediaItem> MediaItems { get; set; }

        #endregion

        #region Methods

        #region Private Methods



        #endregion

        #region Public Methods

        /// <summary>
        /// Encodes this job.
        /// </summary>
        public void Encode()
        {

        }

        #endregion

        #endregion
    }
}
