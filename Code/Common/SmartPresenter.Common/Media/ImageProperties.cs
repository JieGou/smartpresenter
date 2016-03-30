
using System;
using TagLib.Image;
namespace SmartPresenter.Common.Media
{
    /// <summary>
    /// Class to hold image file properties.
    /// </summary>
    public class ImageProperties
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageProperties"/> class.
        /// </summary>
        public ImageProperties()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageProperties"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public ImageProperties(string fileName)
        {
            FileName = fileName;
            Initialize();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; internal set; }

        /// <summary>
        /// Gets the copyright of video.
        /// </summary>
        /// <value>
        /// The copyright of video.
        /// </value>
        public string Copyright { get; internal set; }

        /// <summary>
        /// Gets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string Comment { get; internal set; }

        /// <summary>
        /// Gets the creator.
        /// </summary>
        /// <value>
        /// The creator.
        /// </value>
        public string Creator { get; internal set; }

        /// <summary>
        /// Gets the date created.
        /// </summary>
        /// <value>
        /// The date created.
        /// </value>
        public DateTime DateCreated { get; internal set; }

        /// <summary>
        /// Gets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int Width { get; internal set; }

        /// <summary>
        /// Gets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int Height { get; internal set; }

        /// <summary>
        /// Gets the quality.
        /// </summary>
        /// <value>
        /// The quality.
        /// </value>
        public int Quality { get; internal set; }

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


            Comment = fileTagInfo.Tag.Comment;
            Copyright = fileTagInfo.Tag.Copyright;
            FileName = FileName;
            Height = fileTagInfo.Properties.PhotoHeight;
            Width = fileTagInfo.Properties.PhotoWidth;
            Quality = fileTagInfo.Properties.PhotoQuality;
            ImageTag imageTag = fileTagInfo.Tag as ImageTag;
            if (imageTag != null)
            {
                Creator = imageTag.Creator;
                DateCreated = imageTag.DateTime == null ? DateTime.MinValue : imageTag.DateTime.Value;
            }            
        }

        #endregion

        #endregion
    }
}
