using System;
using System.Globalization;
using System.Windows.Data;

namespace PixelArtEditor.UI.Converters
{
    public class ToolNameToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string toolName)
            {
                return toolName switch
                {
                    "Pencil" => "PencilIcon",
                    "Eraser" => "EraserIcon",
                    "Eyedropper" => "EyedropperIcon",
                    "Fill" => "FillIcon",
                    "Select" => "SelectIcon",
                    _ => null
                };
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
