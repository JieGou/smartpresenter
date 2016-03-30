using System.Windows.Media;

namespace SmartPresenter.Common.Extensions
{
    /// <summary>
    /// An Extension class for Brush objects allowing serialization using XamlSerialization.
    /// </summary>
    public static class BrushExtension
    {
        /// <summary>
        /// Saves to string.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <returns></returns>
        public static string SaveToString(this Brush brush)
        {
            return SmartXamlWriter.Save(brush);
        }

        /// <summary>
        /// Loads from string.
        /// </summary>
        /// <param name="brushString">The brush string.</param>
        /// <returns></returns>
        public static Brush LoadFromString(string brushString)
        {
            return (Brush)SmartXamlReader.Load(brushString);
        }
    }
}
