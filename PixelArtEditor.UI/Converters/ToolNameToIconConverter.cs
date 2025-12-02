using System;
using System.Globalization;
using System.Windows.Data;

namespace PixelArtEditor.UI.Converters
{
    /// <summary>
    /// Converts a tool name to a corresponding icon resource key.
    /// This is used to display the correct icon for each tool in the UI.
    /// </summary>
    public class ToolNameToIconConverter : IValueConverter
    {
        /// <summary>
        /// Converts a tool name to an icon resource key.
        /// </summary>
        /// <param name="value">The tool name to convert.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>The resource key for the tool's icon, or null if the tool name is not recognized.</returns>
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

        /// <summary>
        /// Converts an icon resource key back to a tool name.
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
