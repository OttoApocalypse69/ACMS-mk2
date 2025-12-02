using SkiaSharp;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace PixelArtEditor.UI.Converters
{
    /// <summary>
    /// Converts a SkiaSharp SKColor to a Windows Media SolidColorBrush.
    /// This is used to display SkiaSharp colors in the WPF UI.
    /// </summary>
    public class SkColorToBrushConverter : IValueConverter
    {
        /// <summary>
        /// Converts a SKColor to a SolidColorBrush.
        /// </summary>
        /// <param name="value">The SKColor to convert.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A SolidColorBrush representing the SKColor, or Brushes.Transparent if the value is not a SKColor.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SKColor skColor)
            {
                return new SolidColorBrush(Color.FromArgb(skColor.Alpha, skColor.Red, skColor.Green, skColor.Blue));
            }
            return Brushes.Transparent;
        }

        /// <summary>
        /// Converts a SolidColorBrush back to a SKColor.
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
