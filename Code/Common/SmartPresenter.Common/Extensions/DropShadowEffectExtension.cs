using System.Windows.Media.Effects;

namespace SmartPresenter.Common.Extensions
{
    /// <summary>
    /// An Extension class for DropShadowEffect objects allowing serialization using XamlSerialization.
    /// </summary>
    public static class DropShadowEffectExtension
    {
        /// <summary>
        /// Saves to string.
        /// </summary>
        /// <param name="dropShadowEffect">The drop shadow effect.</param>
        /// <returns></returns>
        public static string SaveToString(this DropShadowEffect dropShadowEffect)
        {
            return SmartXamlWriter.Save(dropShadowEffect);
        }

        /// <summary>
        /// Loads from string.
        /// </summary>
        /// <param name="dropShadowEffectString">The drop shadow effect string.</param>
        /// <returns></returns>
        public static DropShadowEffect LoadFromString(string dropShadowEffectString)
        {
            return (DropShadowEffect)SmartXamlReader.Load(dropShadowEffectString);
        }
    }
}
