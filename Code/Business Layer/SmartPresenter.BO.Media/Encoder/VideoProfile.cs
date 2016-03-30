
using SmartPresenter.Common.Enums;
using System.Windows;
namespace SmartPresenter.BO.Media.Encoder
{
    /// <summary>
    /// Settings of Video Stream.
    /// </summary>
    public class VideoProfile
    {
        #region Private Data Members



        #endregion

        #region Constructor



        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the size of the video.
        /// </summary>
        /// <value>
        /// The size of the video.
        /// </value>
        public System.Drawing.Size VideoSize { get; set; }

        /// <summary>
        /// Gets or sets the frame rate.
        /// </summary>
        /// <value>
        /// The frame rate.
        /// </value>
        public double FrameRate { get; set; }

        /// <summary>
        /// Gets or sets the aspect ratio.
        /// </summary>
        /// <value>
        /// The aspect ratio.
        /// </value>
        public Size AspectRatio { get; set; }

        /// <summary>
        /// Gets or sets the video format.
        /// </summary>
        /// <value>
        /// The video format.
        /// </value>
        public VideoFormat VideoFormat { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [automatic fit].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [automatic fit]; otherwise, <c>false</c>.
        /// </value>
        public bool AutoFit { get; set; }

        /// <summary>
        /// Gets or sets the crop rect.
        /// </summary>
        /// <value>
        /// The crop rect.
        /// </value>
        public CropRect CropRect { get; set; }

        #endregion

        #region Methods

        #region Private Methods



        #endregion

        #region Public Methods



        #endregion

        #endregion
    }
}
