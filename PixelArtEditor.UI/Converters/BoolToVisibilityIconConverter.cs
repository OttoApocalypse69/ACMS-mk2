using System;
using System.Globalization;
using System.Windows.Data;

namespace PixelArtEditor.UI.Converters
{
    /// <summary>
    /// Converts a boolean value to a visibility icon.
    /// This is used to display an eye icon for visible layers and an empty circle for hidden layers.
    /// </summary>
    public class BoolToVisibilityIconConverter : IValueConverter
    {
        /// <summary>
        /// Converts a boolean value to a visibility icon.
        /// </summary>
        /// <param name="value">The boolean value to convert.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>An eye icon if the value is true, otherwise an empty circle.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isVisible && isVisible)
            {
                return "👁";
            }
            return "○";
        }

        /// <summary>
        /// Converts a visibility icon back to a boolean value.
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
