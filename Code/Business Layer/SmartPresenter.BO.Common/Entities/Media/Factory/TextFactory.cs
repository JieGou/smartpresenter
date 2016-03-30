using SmartPresenter.BO.Common.Interfaces;
using System;
using System.Windows.Media;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// A concrete factory class to create Text objects.
    /// </summary>
    public class TextFactory : IShapeFactory
    {
        /// <summary>
        /// Creates a Text.
        /// </summary>
        /// <returns></returns>
        public Shape CreateElement()
        {
            Text text = new Text();
            text.Width = 200;
            text.Height = 60;
            text.Font.Size = 25;
            text.Background = Brushes.White;
            text.Color = Brushes.Black;
            return text;
        }

        /// <summary>
        /// Creates a Text from specified path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Shape CreateElement(string path)
        {
            throw new NotImplementedException();
        }
    }
}
