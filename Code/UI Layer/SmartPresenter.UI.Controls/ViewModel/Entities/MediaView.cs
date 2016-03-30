using SmartPresenter.BO.Common.Entities;
using SmartPresenter.BO.Common.Enums;
using SmartPresenter.Common.Enums;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Media;

namespace SmartPresenter.UI.Controls.ViewModel
{
    /// <summary>
    /// A base class for Video/Image Object UI.
    /// </summary>
    public abstract class MediaView : RectangleView
    {

        #region Private Data Members

        private Image _mediaItem;

        #endregion

        #region Constructor

        public MediaView() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaView"/> class.
        /// </summary>
        /// <param name="mediaItem">The media item.</param>
        public MediaView(Image mediaItem)
            : base(mediaItem)
        {
            _mediaItem = mediaItem;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaView"/> class.
        /// </summary>
        /// <param name="mediaItem">The media item.</param>
        public MediaView(Shape mediaItem)
            : base(mediaItem)
        {
            _mediaItem = mediaItem as Image;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaView"/> class.
        /// </summary>
        /// <param name="mediaItem">The media item.</param>
        public MediaView(Rectangle mediaItem)
            : base(mediaItem)
        {
            _mediaItem = mediaItem as Image;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public override ElementType Type
        {
            get
            {
                return _mediaItem.Type;
            }
        }

        /// <summary>
        /// Path to the media file.
        /// </summary>
        [Category("File")]
        public string Path
        {
            get
            {
                return _mediaItem.Path;
            }
        }

        /// <summary>
        /// Name of the media file.
        /// </summary>
        [DisplayName("Full File Name")]
        [Category("File")]
        public string FileName
        {
            get
            {
                return System.IO.Path.GetFileName(_mediaItem.Path);
            }
        }

        /// <summary>
        /// Name of the file without extension.
        /// </summary>
        [DisplayName("File Name")]
        [Category("File")]
        public string Name
        {
            get
            {
                return System.IO.Path.GetFileNameWithoutExtension(_mediaItem.Path);
            }
        }

        /// <summary>
        /// Gets the extension of file.
        /// </summary>
        [DisplayName("File Extension")]
        [Category("File")]
        public string Extension
        {
            get
            {
                return System.IO.Path.GetExtension(_mediaItem.Path);
            }
        }

        /// <summary>
        /// Size of the media.
        /// </summary>
        [DisplayName("File Size")]
        [Category("File")]
        public string Size
        {
            get
            {
                FileInfo fileInfo = new FileInfo(Path);

                if (fileInfo.Length < 1024)
                {
                    return string.Format(Thread.CurrentThread.CurrentUICulture, "{0} B", fileInfo.Length);
                }
                else if (fileInfo.Length < (1024 * 1024))
                {
                    return string.Format(Thread.CurrentThread.CurrentUICulture, "{0} KB", fileInfo.Length / 1024);
                }
                else if (fileInfo.Length < (1024 * 1024 * 1024))
                {
                    return string.Format(Thread.CurrentThread.CurrentUICulture, "{0} MB", fileInfo.Length / (1024 * 1024));
                }

                return "N/A";
            }
        }

        /// <summary>
        /// Gets or sets the stretch.
        /// </summary>
        /// <value>
        /// The stretch.
        /// </value>
        [Category("Appearance")]
        public Stretch Stretch
        {
            get
            {
                return _mediaItem.Stretch;
            }
            set
            {
                if (_mediaItem != null)
                {
                    _mediaItem.Stretch = value;
                    OnPropertyChanged("Stretch");
                }
            }
        }

        #endregion

        #region Methods

        #region Private Methods

        #endregion

        #region Public Methods

        #endregion

        #endregion

    }
}
