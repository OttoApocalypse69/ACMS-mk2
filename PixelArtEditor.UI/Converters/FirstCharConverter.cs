using System;
using System.Globalization;
using System.Windows.Data;

namespace PixelArtEditor.UI.Converters
{
    /// <summary>
    /// Converts a string to its first character.
    /// This is used to display the first letter of a tool's name as its icon.
    /// </summary>
    public class FirstCharConverter : IValueConverter
    {
        /// <summary>
        /// Converts a string to its first character.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>The first character of the string, or "?" if the string is null or empty.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string s && !string.IsNullOrEmpty(s))
            {
                return s.Substring(0, 1);
            }
            return "?";
        }

        /// <summary>
        /// Converts a character back to a string.
        /// This method is not implemented.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>This method always throws a NotImplementedException.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
