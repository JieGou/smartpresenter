using System;
using TagLib.Image;
namespace SmartPresenter.Common.Media
{
    /// <summary>
    /// Properties of a media file
    /// </summary>
    public class MediaProperties
    {
        #region Constants

        private string Title_String = "Title";
        private string Description_String = "Description";
        private string Copyright_String = "Copyright";

        #endregion

        #region Constructor

        public MediaProperties(string fileName)
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
        /// Gets the audio properties.
        /// </summary>
        /// <value>
        /// The audio properties.
        /// </value>
        public AudioProperties AudioProperties { get; private set; }

        /// <summary>
        /// Gets the video properties.
        /// </summary>
        /// <value>
        /// The video properties.
        /// </value>
        public VideoProperties VideoProperties { get; private set; }

        /// <summary>
        /// Gets the image properties.
        /// </summary>
        /// <value>
        /// The image properties.
        /// </value>
        public ImageProperties ImageProperties { get; private set; }

        #endregion

        #region Methods

        #region Private Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            if (System.IO.File.Exists(FileName))
            {
                Microsoft.Expression.Encoder.MediaItem mediaItem = new Microsoft.Expression.Encoder.MediaItem(FileName);
                TagLib.File fileTagInfo = TagLib.File.Create(FileName);

                VideoProperties = new VideoProperties()
                {
                    FileName = FileName,
                    VideoSize = mediaItem.OriginalVideoSize,
                    FileDuration = mediaItem.PlaybackDuration,
                    OriginalAspectRatio = mediaItem.OriginalAspectRatio,
                    MaxBitrate = mediaItem.MaxBitrate,
                    OriginalFrameRate = mediaItem.OriginalFrameRate,
                    AudioGainLevel = mediaItem.AudioGainLevel,
                    Bitrate = mediaItem.SourceVideoProfile.Bitrate,
                    Codec = mediaItem.SourceVideoProfile.Codec,
                    Title = mediaItem.Metadata[Title_String],
                    Description = mediaItem.Metadata[Description_String],
                    Copyright = mediaItem.Metadata[Copyright_String],
                };

                AudioProperties = new AudioProperties()
                {
                    BitsPerSample = mediaItem.SourceAudioProfile.BitsPerSample,
                    Channels = mediaItem.SourceAudioProfile.Channels,
                    Duration = mediaItem.MainMediaFile.AudioStreams[0].Duration,
                    SampleSize = mediaItem.MainMediaFile.AudioStreams[0].SampleSize,
                    StreamName = mediaItem.MainMediaFile.AudioStreams[0].StreamName,
                    Bitrate = mediaItem.SourceAudioProfile.Bitrate,
                    Codec = mediaItem.SourceAudioProfile.Codec,
                    Album = fileTagInfo.Tag.Album,
                };

                ImageProperties = new ImageProperties()
                {
                    Comment = fileTagInfo.Tag.Comment,
                    Copyright = fileTagInfo.Tag.Copyright,
                    FileName = FileName,
                    Height = fileTagInfo.Properties.PhotoHeight,
                    Width = fileTagInfo.Properties.PhotoWidth,
                    Quality = fileTagInfo.Properties.PhotoQuality,
                };
                ImageTag imageTag = fileTagInfo.Tag as ImageTag;
                if (imageTag != null)
                {
                    ImageProperties.Creator = imageTag.Creator;
                    ImageProperties.DateCreated = imageTag.DateTime == null ? DateTime.MinValue : imageTag.DateTime.Value;
                }
            }
        }

        #endregion

        #endregion

    }
}
