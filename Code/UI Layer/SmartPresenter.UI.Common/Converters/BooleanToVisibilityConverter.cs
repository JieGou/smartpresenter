using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SmartPresenter.UI.Common
{
    /// <summary>
    /// Converter class to convert from boolean to visibility and back.
    /// </summary>
    public class BooleanToVisibilityConverter : IValueConverter
    {
        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance will return collapse if false is input.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is collapsing; otherwise, <c>false</c>.
        /// </value>
        public bool IsCollapsing { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether behaviour of converter will be inverted.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is inverted; otherwise, <c>false</c>.
        /// </value>
        public bool IsInverted { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Converts a boolean value to Visibility.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;

            if (boolValue == true)
            {
                return IsInverted ? (IsCollapsing ? Visibility.Collapsed : Visibility.Hidden) : Visibility.Visible;
            }
            else
            {
                return IsInverted ? Visibility.Visible : (IsCollapsing ? Visibility.Collapsed : Visibility.Hidden);
            }
        }

        /// <summary>
        /// Converts a visibility value to boolean.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;
            bool result;

            if (visibility == Visibility.Collapsed || visibility == Visibility.Hidden)
            {
                result = false;
            }
            else
            {
                result = true;
            }

            return IsInverted ? !result : result;
        }

        #endregion

    }
}
