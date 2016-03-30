using SmartPresenter.UI.Common.Enums;
using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace SmartPresenter.UI.Common.Converters
{
    /// <summary>
    /// Converter to convert from SlideViewer/MediaViewer "Mode" to correct orientation of panel.
    /// </summary>
    public class ModeToOrientationConverter : IValueConverter
    {
        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Orientation orientation = Orientation.Horizontal;

            if (value is MediaViewerMode)
            {
                MediaViewerMode mode = (MediaViewerMode)value;
                switch (mode)
                {
                    case MediaViewerMode.Details:
                        orientation = Orientation.Vertical;
                        break;
                    case MediaViewerMode.Thumbnail:
                        orientation = Orientation.Horizontal;
                        break;
                }
            }
            else if (value is SlideViewerMode)
            {
                SlideViewerMode mode = (SlideViewerMode)value;
                switch (mode)
                {
                    case SlideViewerMode.Details:
                        orientation = Orientation.Vertical;
                        break;
                    case SlideViewerMode.Thumbnail:
                        orientation = Orientation.Horizontal;
                        break;
                }
            }

            return orientation;
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
