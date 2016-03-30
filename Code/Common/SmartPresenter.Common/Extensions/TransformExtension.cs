using System.Windows.Media;

namespace SmartPresenter.Common.Extensions
{
    /// <summary>
    /// An Extension class for Transform objects allowing serialization using XamlSerialization.
    /// </summary>
    public static class TransformExtension
    {
        /// <summary>
        /// Saves to string.
        /// </summary>
        /// <param name="transform">The transform.</param>
        /// <returns></returns>
        public static string SaveToString(this Transform transform)
        {
            return SmartXamlWriter.Save(transform);
        }

        /// <summary>
        /// Loads from string.
        /// </summary>
        /// <param name="transformString">The transform string.</param>
        /// <returns></returns>
        public static Transform LoadFromString(string transformString)
        {
            return (Transform)SmartXamlReader.Load(transformString);
        }
    }
}
