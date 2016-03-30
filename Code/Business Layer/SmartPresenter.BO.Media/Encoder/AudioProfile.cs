
using SmartPresenter.Common.Enums;

namespace SmartPresenter.BO.Media.Encoder
{
    /// <summary>
    /// Settings of Audio Stream.
    /// </summary>
    public class AudioProfile
    {
        #region Private Data Members



        #endregion

        #region Constructor



        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the channels.
        /// </summary>
        /// <value>
        /// The channels.
        /// </value>
        public int Channels { get; set; }

        /// <summary>
        /// Gets or sets the bits per sample.
        /// </summary>
        /// <value>
        /// The bits per sample.
        /// </value>
        public int BitsPerSample { get; set; }

        /// <summary>
        /// Gets or sets the bits rate.
        /// </summary>
        /// <value>
        /// The bits rate.
        /// </value>
        public int BitRate { get; set; }

        /// <summary>
        /// Gets or sets the audio format.
        /// </summary>
        /// <value>
        /// The audio format.
        /// </value>
        public AudioFormat AudioFormat { get; set; }

        /// <summary>
        /// Gets or sets the audio gain level.
        /// </summary>
        /// <value>
        /// The audio gain level.
        /// </value>
        public double AudioGainLevel { get; set; }

        #endregion

        #region Methods

        #region Private Methods



        #endregion

        #region Public Methods



        #endregion

        #endregion
    }
}
