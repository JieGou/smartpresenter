
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
namespace SmartPresenter.Common
{
    /// <summary>
    /// Helper class to assist in basic media operatins.
    /// </summary>
    public static class MediaHelper
    {
        #region Constants

        public static string[] ValidImageExtensios = { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };
        public static string[] ValidVideoExtensios = { ".mpg", ".mov", ".wmv", ".mp4", ".avi", ".mpeg", ".m4v", ".divx", ".flv", ".mkv", ".qt" };
        public static string[] ValidAudioExtensios = { ".mp3", ".wma", ".aac", ".wav", ".m4a" };

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Determines whether the file by specified path is valid image file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">File path is invalid</exception>
        public static bool IsValidImageFile(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("File path is invalid");
            string extension = Path.GetExtension(path);
            return ValidImageExtensios.Contains(extension);
        }

        /// <summary>
        /// Determines whether the file by specified path is valid video file
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">File path is invalid</exception>
        public static bool IsValidVideoFile(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("File path is invalid");
            string extension = Path.GetExtension(path);
            return ValidVideoExtensios.Contains(extension);
        }

        /// <summary>
        /// Determines whether the file by specified path is valid audio file
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">File path is invalid</exception>
        public static bool IsValidAudioFile(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("File path is invalid");
            string extension = Path.GetExtension(path);
            return ValidAudioExtensios.Contains(extension);
        }

        /// <summary>
        /// Froms the bitmap.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <returns></returns>
        public static BitmapImage ConvertToBitmapImage(Bitmap bitmap)
        {
            BitmapImage bitmapImage = new BitmapImage();

            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Jpeg);
                memory.Position = 0;
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            }

            return bitmapImage;
        }

        /// <summary>
        /// Saves the bitmap to file.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <param name="thumbnailPath">The thumbnail path.</param>
        /// <returns></returns>
        public static bool SaveBitmapToFile(Bitmap bitmap, string thumbnailPath)
        {
            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (FileStream fileStream = new FileStream(thumbnailPath, FileMode.Create, FileAccess.ReadWrite))
                    {
                        bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        byte[] bytes = memoryStream.ToArray();
                        fileStream.Write(bytes, 0, bytes.Length);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
