
using System;
using System.Windows;
namespace SmartPresenter.UI.Common
{
    /// <summary>
    /// A class for BookMark inside a Video/Audio which will be used to cut/trim a video.
    /// </summary>
    public class BookMark
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BookMark"/> class.
        /// </summary>
        /// <param name="time">The time.</param>
        public BookMark(TimeSpan time)
        {
            Time = time;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        public TimeSpan Time { get; set; }

        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        public Thickness Margin { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public BookMarkType Type { get; set; }

        #endregion

    }

    /// <summary>
    /// Type of Book Mark.
    /// </summary>
    public enum BookMarkType
    {
        In,
        Out,
    }
}
