using System.Windows;
using System.Windows.Media;

/// <summary>
/// Visual Tree helper class.
/// </summary>
namespace SmartPresenter.UI.Common
{
    public static class VisualTreeAssitant
    {
        /// <summary>
        /// Finds the element in visual tree.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parentElement">The parent element.</param>
        /// <returns></returns>
        public static T FindElementInVisualTree<T>(DependencyObject parentElement) where T : DependencyObject
        {
            var count = VisualTreeHelper.GetChildrenCount(parentElement);
            if (count == 0)
                return null;

            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(parentElement, i);

                if (child != null && child is T)
                {
                    return (T)child;
                }
                else
                {
                    var result = FindElementInVisualTree<T>(child);
                    if (result != null)
                        return result;

                }
            }
            return null;
        }
    }
}
