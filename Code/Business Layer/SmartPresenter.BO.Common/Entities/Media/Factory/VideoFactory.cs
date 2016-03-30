using SmartPresenter.BO.Common.Interfaces;
using System.Windows.Media;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// A concrete factory class to create Video objects.
    /// </summary>
    public class VideoFactory : IShapeFactory
    {
        /// <summary>
        /// Creates a Video.
        /// </summary>
        /// <returns></returns>
        public Shape CreateElement()
        {
            Video video = new Video();
            video.Background = Brushes.Green;
            video.Width = video.Height = 100;
            return video;
        }

        /// <summary>
        /// Creates a Video from specified path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Shape CreateElement(string path)
        {
            Video video = new Video(path);
            video.Background = Brushes.Green;
            video.Width = video.Height = 100;
            return video;
        }
    }
}
