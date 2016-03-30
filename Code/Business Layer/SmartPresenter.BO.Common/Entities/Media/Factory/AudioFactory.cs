using SmartPresenter.BO.Common.Interfaces;
using System.Windows.Media;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// A concrete factory class to create Audio objects.
    /// </summary>
    public class AudioFactory : IShapeFactory
    {
        /// <summary>
        /// Creates an Audio.
        /// </summary>
        /// <returns></returns>
        public Shape CreateElement()
        {
            Audio audio = new Audio();
            audio.Background = Brushes.Green;
            audio.Width = audio.Height = 100;
            return audio;
        }

        /// <summary>
        /// Creates an Audio from specified path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Shape CreateElement(string path)
        {
            Audio audio = new Audio(path);
            audio.Background = Brushes.Green;
            audio.Width = audio.Height = 100;
            return audio;
        }
    }
}
