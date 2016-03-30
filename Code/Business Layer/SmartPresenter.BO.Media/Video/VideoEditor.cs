using Microsoft.Expression.Encoder;
using SmartPresenter.Common;
using System;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace SmartPresenter.BO.Media.Video
{
    /// <summary>
    /// A class for editing video.
    /// </summary>
    public class VideoEditor
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoEditor"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public VideoEditor(string fileName)
        {
            FileName = fileName;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the video thumbnail as bitmap image.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static BitmapImage GetVideoThumbnailAsBitmapImage(string fileName)
        {
            if (MediaHelper.IsValidVideoFile(fileName))
            {
                return MediaHelper.ConvertToBitmapImage(GetVideoThumbnail(fileName));
            }
            throw new ArgumentException();
        }

        /// <summary>
        /// Gets the video thumbnail.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>
        public static Bitmap GetVideoThumbnail(string fileName)
        {
            if (MediaHelper.IsValidVideoFile(fileName))
            {
                //MediaItem mediaItem = new MediaItem(fileName);
                //return mediaItem.MainMediaFile.GetThumbnail(TimeSpan.FromSeconds(5), mediaItem.OriginalVideoSize);
                AudioVideoFile avFile = new AudioVideoFile(fileName);
                return avFile.GetThumbnail(TimeSpan.FromSeconds(5), avFile.VideoStreams[0].VideoSize);
            }
            throw new ArgumentException();
        }

        /// <summary>
        /// Clips the video.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        public void ClipVideo(TimeSpan startTime, TimeSpan endTime)
        {

        }

        #endregion

        #region Private Methods



        #endregion
    }
}
