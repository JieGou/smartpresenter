using System;
using System.Globalization;
using System.Windows.Data;

namespace SmartPresenter.UI.Common
{
    /// <summary>
    /// A converter to convert timespan to value and back.
    /// </summary>
    public class TimeSpanToDoubleConverter : IValueConverter
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
            if (value is TimeSpan)
            {
                TimeSpan timeSpan = TimeSpan.Parse(value.ToString());
                return timeSpan.TotalSeconds;
            }
            else
            {
                double totalseconds = System.Convert.ToDouble(value);
                return TimeSpan.FromSeconds(totalseconds);
            }
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
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is TimeSpan)
            {
                TimeSpan timeSpan = TimeSpan.Parse(value.ToString());
                return timeSpan.TotalSeconds;
            }
            else
            {
                double totalseconds = System.Convert.ToDouble(value);
                return TimeSpan.FromSeconds(totalseconds);
            }
        }
    }
}
