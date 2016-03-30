using System;

namespace SmartPresenter.Common.Extensions
{
    /// <summary>
    /// Extension methods for string class.
    /// </summary>
    public static class StringExtensions
    {
        #region Static Extension Methods

        /// <summary>
        /// Converts To the int.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static int ToInt(this string str)
        {
            try
            {
                return Convert.ToInt32(str);
            }
            catch (Exception ex)
            {
                Logger.Logger.LogMsg.Error(ex.Message, ex);
                return -1;
            }
        }

        /// <summary>
        /// Converts To the short int.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static short ToShortInt(this string str)
        {
            try
            {
                return Convert.ToInt16(str);
            }
            catch (Exception ex)
            {
                Logger.Logger.LogMsg.Error(ex.Message, ex);
                return -1;
            }
        }

        /// <summary>
        /// Converts To the long int.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static long ToLongInt(this string str)
        {
            try
            {
                return Convert.ToInt64(str);
            }
            catch (Exception ex)
            {
                Logger.Logger.LogMsg.Error(ex.Message, ex);
                return -1;
            }
        }

        /// <summary>
        /// Converts To the unique identifier.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static Guid ToGuid(this string str)
        {
            try
            {
                return Guid.Parse(str);
            }
            catch (Exception ex)
            {
                Logger.Logger.LogMsg.Error(ex.Message, ex);
                return Guid.Empty;
            }
        }

        /// <summary>
        /// Converts To the float.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static float ToFloat(this string str)
        {
            try
            {
                return Convert.ToSingle(str);
            }
            catch (Exception ex)
            {
                Logger.Logger.LogMsg.Error(ex.Message, ex);
                return 0.0f;
            }
        }

        /// <summary>
        /// Converts To the double.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static double ToDouble(this string str)
        {
            try
            {
                return Convert.ToDouble(str);
            }
            catch (Exception ex)
            {
                Logger.Logger.LogMsg.Error(ex.Message, ex);
                return 0.0;
            }
        }

        /// <summary>
        /// Converts To the boolean.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static bool ToBool(this string str)
        {
            try
            {
                return Convert.ToBoolean(str);
            }
            catch (Exception ex)
            {
                Logger.Logger.LogMsg.Error(ex.Message, ex);
                if (str.Equals("True") || str.Equals("true") || str.Equals("T") || str.Equals("t") || str.Equals("1"))
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// To the character.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static char ToChar(this string str)
        {
            try
            {
                return Convert.ToChar(str);
            }
            catch (Exception ex)
            {
                Logger.Logger.LogMsg.Error(ex.Message, ex);
                return ' ';
            }
        }

        /// <summary>
        /// To the time span.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static TimeSpan ToTimeSpan(this string str)
        {
            try
            {
                return TimeSpan.Parse(str);
            }
            catch (Exception ex)
            {
                Logger.Logger.LogMsg.Error(ex.Message, ex);
                return TimeSpan.FromSeconds(0);
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToSafeString(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return string.Empty;
            }
            return str;
        }

        #endregion
    }
}
