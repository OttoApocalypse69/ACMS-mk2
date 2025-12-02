using System;
using System.Globalization;
using System.Windows.Data;

namespace PixelArtEditor.UI.Converters
{
    public class BoolToVisibilityIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isVisible && isVisible)
            {
                return "👁"; // Eye icon
            }
            return "○"; // Empty circle or closed eye
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
