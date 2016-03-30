
using Microsoft.Expression.Encoder.Profiles;
using System;
using System.Drawing;
namespace SmartPresenter.Common.Media
{
    /// <summary>
    /// A class to hold properties of a video file like Duration, Length, Size, Framerate etc.
    /// </summary>
    public class VideoProperties
    {
        #region Constants

        private string Title_String = "Title";
        private string Description_String = "Description";
        private string Copyright_String = "Copyright";

        #endregion

        #region Private Data Members



        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoProperties"/> class.
        /// </summary>
        public VideoProperties()
        {
        }

        public VideoProperties(string fileName)
        {
            FileName = fileName;
            Initialize();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; internal set; }

        /// <summary>
        /// Gets the title of video.
        /// </summary>
        /// <value>
        /// The title of video.
        /// </value>
        public string Title { get; internal set; }

        /// <summary>
        /// Gets the description of video.
        /// </summary>
        /// <value>
        /// The description of video.
        /// </value>
        public string Description { get; internal set; }

        /// <summary>
        /// Gets the copyright of video.
        /// </summary>
        /// <value>
        /// The copyright of video.
        /// </value>
        public string Copyright { get; internal set; }

        /// <summary>
        /// Gets or sets the display size of the video.
        /// </summary>
        /// <value>
        /// The display size of the video.
        /// </value>
        public Size VideoSize { get; internal set; }

        /// <summary>
        /// Gets or sets the duration of the file.
        /// </summary>
        /// <value>
        /// The duration of the file.
        /// </value>
        public TimeSpan FileDuration { get; internal set; }

        /// <summary>
        /// Gets the original aspect ratio.
        /// </summary>
        /// <value>
        /// The original aspect ratio.
        /// </value>
        public System.Windows.Size OriginalAspectRatio { get; internal set; }

        /// <summary>
        /// Gets the maximum bitrate.
        /// </summary>
        /// <value>
        /// The maximum bitrate.
        /// </value>
        public int MaxBitrate { get; internal set; }

        /// <summary>
        /// Gets the original frame rate.
        /// </summary>
        /// <value>
        /// The original frame rate.
        /// </value>
        public double OriginalFrameRate { get; internal set; }

        /// <summary>
        /// Gets the audio gain level.
        /// </summary>
        /// <value>
        /// The audio gain level.
        /// </value>
        public double AudioGainLevel { get; internal set; }

        /// <summary>
        /// Gets the bitrate.
        /// </summary>
        /// <value>
        /// The bitrate.
        /// </value>
        public Bitrate Bitrate { get; internal set; }

        /// <summary>
        /// Gets the codec.
        /// </summary>
        /// <value>
        /// The codec.
        /// </value>
        public VideoCodec Codec { get; internal set; }

        #endregion

        #region Methods

        #region Private Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            Microsoft.Expression.Encoder.MediaItem mediaItem = new Microsoft.Expression.Encoder.MediaItem(FileName);
            TagLib.File fileTagInfo = TagLib.File.Create(FileName);

            VideoSize = mediaItem.OriginalVideoSize;
            FileDuration = mediaItem.PlaybackDuration;
            OriginalAspectRatio = mediaItem.OriginalAspectRatio;
            MaxBitrate = mediaItem.MaxBitrate;
            OriginalFrameRate = mediaItem.OriginalFrameRate;
            AudioGainLevel = mediaItem.AudioGainLevel;
            Bitrate = mediaItem.SourceVideoProfile.Bitrate;
            Codec = mediaItem.SourceVideoProfile.Codec;
            Title = mediaItem.Metadata[Title_String];
            Description = mediaItem.Metadata[Description_String];
            Copyright = mediaItem.Metadata[Copyright_String];

        }

        #endregion

        #region Public Methods



        #endregion



        #endregion
    }
}
