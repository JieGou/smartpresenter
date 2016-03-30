using SmartPresenter.BO.Common.Entities;
using System.ComponentModel;

namespace SmartPresenter.UI.Controls.ViewModel
{
    /// <summary>
    /// A class for Image Object UI.
    /// </summary>
    [DisplayName("Image")]
    public class ImageView : MediaView
    {

        #region Private Data Members

        private Image _image;

        #endregion

        #region Constructor

        public ImageView() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageView"/> class.
        /// </summary>
        /// <param name="mediaItem">The media item.</param>
        public ImageView(Image mediaItem)
            : base(mediaItem)
        {
            _image = mediaItem;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageView"/> class.
        /// </summary>
        /// <param name="mediaItem">The media item.</param>
        public ImageView(Shape mediaItem)
            : base(mediaItem)
        {
            _image = (Image)mediaItem;
        }

        #endregion

        #region Properties



        #endregion

        #region Methods

        /// <summary>
        /// Gets the inner object.
        /// </summary>
        /// <returns></returns>
        protected internal override Shape GetInnerObjectCloned()
        {
            return (Image)_image.Clone();
        }

        /// <summary>
        /// Clones the internal.
        /// </summary>
        /// <returns></returns>
        protected override object CloneInternal()
        {
            ShapeView elementView = new ImageView(this.GetInnerObjectCloned());

            return elementView;
        }

        #endregion
    }
}
