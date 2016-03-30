
using System;
namespace SmartPresenter.BO.Media.Encoder
{
    /// <summary>
    /// A class to represent a marker in a media file.
    /// </summary>
    public class Marker
    {
        #region Private Data Members



        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Marker"/> class.
        /// </summary>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        public Marker(TimeSpan startTime, string tag = "")
        {
            Time = startTime;
            Tag = tag;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>
        /// The start time.
        /// </value>
        public TimeSpan Time { get; set; }

        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        /// <value>
        /// The tag.
        /// </value>
        public string Tag { get; set; }

        #endregion

        #region Methods

        #region Private Methods



        #endregion

        #region Public Methods



        #endregion

        #endregion
    }
}
