using SmartPresenter.BO.Common.Enums;
using SmartPresenter.Common.Enums;
using SmartPresenter.UI.Controls.ViewModel;

namespace SmartPresenter.UI.RenderEngine.DirectX.Entities
{
    /// <summary>
    /// A Factory class to create Drawables of different types.
    /// </summary>
    class ShapeRendererFactory : IDrawableFactory
    {
        #region IRendererFactory Members


        /// <summary>
        /// Creates the Drawable.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <returns></returns>
        public IDrawable CreateDrawable(ShapeView shape)
        {
            switch(shape.Type)
            {
                case ElementType.Rectangle:
                case ElementType.Square:
                    return new RectangleRenderer(shape as RectangleView);
                case ElementType.Circle:
                    return new CircleRenderer(shape as CircleView);
                case ElementType.Audio:
                    return new AudioRenderer(shape as AudioView);
                case ElementType.Image:
                    return new ImageRenderer(shape as ImageView);
                case ElementType.Text:
                    return new TextRenderer(shape as TextView);
                case ElementType.Video:
                    return new VideoRenderer(shape as VideoView);
                default:
                    return null;
            }
        }

        #endregion
    }
}
