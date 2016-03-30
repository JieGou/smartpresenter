using SmartPresenter.Common.Enums;
using System.IO;

namespace SmartPresenter.Common.Media
{
    /// <summary>
    /// A class to help with file formats
    /// </summary>
    public static class FileFormatHelper
    {
        #region Public Static Methods

        /// <summary>
        /// Gets the video format.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static VideoFormat GetVideoFormat(string fileName)
        {
            VideoFormat videoFormat = VideoFormat.UNKNOWN;
            string extension = Path.GetExtension(fileName);

            switch (extension)
            {
                case ".avi":
                    videoFormat = VideoFormat.AVI;
                    break;
                case ".divx":
                    videoFormat = VideoFormat.DIVX;
                    break;
                case ".flv":
                    videoFormat = VideoFormat.FLV;
                    break;
                case ".m4v":
                    videoFormat = VideoFormat.M4V;
                    break;
                case ".mkv":
                    videoFormat = VideoFormat.MKV;
                    break;
                case ".mov":
                    videoFormat = VideoFormat.MOV;
                    break;
                case ".mp4":
                    videoFormat = VideoFormat.MP4;
                    break;
                case ".mpeg":
                    videoFormat = VideoFormat.MPEG;
                    break;
                case ".mpg":
                    videoFormat = VideoFormat.MPG;
                    break;
                case ".qt":
                    videoFormat = VideoFormat.QT;
                    break;
                case ".wmv":
                    videoFormat = VideoFormat.WMV;
                    break;
            }

            return videoFormat;
        }

        /// <summary>
        /// Gets the audio format.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static AudioFormat GetAudioFormat(string fileName)
        {
            AudioFormat audioFormat = AudioFormat.UNKNOWN;
            string extension = Path.GetExtension(fileName);

            switch (extension)
            {
                case ".aac":
                    audioFormat = AudioFormat.AAC;
                    break;
                case ".m4a":
                    audioFormat = AudioFormat.M4A;
                    break;
                case ".mp3":
                    audioFormat = AudioFormat.MP3;
                    break;
                case ".wav":
                    audioFormat = AudioFormat.WAV;
                    break;
                case ".wma":
                    audioFormat = AudioFormat.WMA;
                    break;
            }

            return audioFormat;
        }

        /// <summary>
        /// Gets the video format.
        /// </summary>
        /// <param name="videoFormat">The video format.</param>
        /// <returns></returns>
        public static string GetExtensionForVideoFormat(VideoFormat videoFormat)
        {
            string extension = "";

            switch (videoFormat)
            {
                case VideoFormat.AVI:
                    extension = ".avi";
                    break;
                case VideoFormat.DIVX:
                    extension = ".divx";
                    break;
                case VideoFormat.FLV:
                    extension = ".flv";
                    break;
                case VideoFormat.M4V:
                    extension = ".m4v";
                    break;
                case VideoFormat.MKV:
                    extension = ".mkv";
                    break;
                case VideoFormat.MOV:
                    extension = ".mov";
                    break;
                case VideoFormat.MP4:
                    extension = ".mp4";
                    break;
                case VideoFormat.MPEG:
                    extension = ".mpeg";
                    break;
                case VideoFormat.MPG:
                    extension = ".mpg";
                    break;
                case VideoFormat.QT:
                    extension = ".qt";
                    break;
                case VideoFormat.WMV:
                    extension = ".wmv";
                    break;
            }

            return extension;
        }

        /// <summary>
        /// Gets the extension for audio format.
        /// </summary>
        /// <param name="audioFormat">The audio format.</param>
        /// <returns></returns>
        public static string GetExtensionForAudioFormat(AudioFormat audioFormat)
        {
            string extension = "";

            switch (audioFormat)
            {
                case AudioFormat.AAC:
                    extension = ".aac";
                    break;
                case AudioFormat.M4A:
                    extension = ".m4a";
                    break;
                case AudioFormat.MP3:
                    extension = ".mp3";
                    break;
                case AudioFormat.WAV:
                    extension = ".wav";
                    break;
                case AudioFormat.WMA:
                    extension = ".wma";
                    break;
            }

            return extension;
        }

        #endregion
    }
}
