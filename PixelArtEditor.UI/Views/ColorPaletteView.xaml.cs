using PixelArtEditor.UI.ViewModels;
using SkiaSharp;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PixelArtEditor.UI.Views
{
    /// <summary>
    /// Interaction logic for ColorPaletteView.xaml
    /// This view allows the user to select a color from a palette or by using sliders and text boxes.
    /// </summary>
    public partial class ColorPaletteView : UserControl
    {
        private bool _isUpdating = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorPaletteView"/> class.
        /// </summary>
        public ColorPaletteView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the ValueChanged event of the HueSlider.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnHueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_isUpdating || DataContext == null) return;
            UpdateColorFromHSV();
        }

        /// <summary>
        /// Handles the ValueChanged event of the SaturationSlider.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnSaturationChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_isUpdating || DataContext == null) return;
            UpdateColorFromHSV();
        }

        /// <summary>
        /// Handles the ValueChanged event of the ValueSlider.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_isUpdating || DataContext == null) return;
            UpdateColorFromHSV();
        }

        /// <summary>
        /// Handles the TextChanged event of the RGB text boxes.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnRgbChanged(object sender, TextChangedEventArgs e)
        {
            if (_isUpdating || DataContext == null) return;
            UpdateColorFromRGB();
        }

        /// <summary>
        /// Handles the TextChanged event of the Hex text box.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnHexChanged(object sender, TextChangedEventArgs e)
        {
            if (_isUpdating || DataContext == null) return;
            UpdateColorFromHex();
        }

        /// <summary>
        /// Handles the SelectionChanged event of the color palette.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnPaletteColorSelected(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is ColorPaletteViewModel vm && vm.SelectedColor != SKColor.Empty)
            {
                UpdateControlsFromColor(vm.SelectedColor);
            }
        }

        /// <summary>
        /// Updates the selected color from the HSV sliders.
        /// </summary>
        private void UpdateColorFromHSV()
        {
            float h = (float)HueSlider.Value;
            float s = (float)SaturationSlider.Value / 100f;
            float v = (float)ValueSlider.Value / 100f;

            var color = SKColor.FromHsv(h, s * 100, v * 100);
            
            _isUpdating = true;
            if (DataContext is ColorPaletteViewModel vm)
            {
                vm.SelectedColor = color;
            }
            UpdateRGBFromColor(color);
            UpdateHexFromColor(color);
            _isUpdating = false;
        }

        /// <summary>
        /// Updates the selected color from the RGB text boxes.
        /// </summary>
        private void UpdateColorFromRGB()
        {
            if (!byte.TryParse(RedBox.Text, out byte r)) r = 0;
            if (!byte.TryParse(GreenBox.Text, out byte g)) g = 0;
            if (!byte.TryParse(BlueBox.Text, out byte b)) b = 0;

            var color = new SKColor(r, g, b);
            
            _isUpdating = true;
            if (DataContext is ColorPaletteViewModel vm)
            {
                vm.SelectedColor = color;
            }
            UpdateHSVFromColor(color);
            UpdateHexFromColor(color);
            _isUpdating = false;
        }

        /// <summary>
        /// Updates the selected color from the Hex text box.
        /// </summary>
        private void UpdateColorFromHex()
        {
            string hex = HexBox.Text.Trim();
            if (hex.Length == 6 && uint.TryParse(hex, System.Globalization.NumberStyles.HexNumber, null, out uint value))
            {
                byte r = (byte)((value >> 16) & 0xFF);
                byte g = (byte)((value >> 8) & 0xFF);
                byte b = (byte)(value & 0xFF);
                var color = new SKColor(r, g, b);
                
                _isUpdating = true;
                if (DataContext is ColorPaletteViewModel vm)
                {
                    vm.SelectedColor = color;
                }
                UpdateHSVFromColor(color);
                UpdateRGBFromColor(color);
                _isUpdating = false;
            }
        }

        /// <summary>
        /// Updates all the color controls from the selected color.
        /// </summary>
        /// <param name="color">The color to update the controls with.</param>
        private void UpdateControlsFromColor(SKColor color)
        {
            _isUpdating = true;
            UpdateHSVFromColor(color);
            UpdateRGBFromColor(color);
            UpdateHexFromColor(color);
            _isUpdating = false;
        }

        /// <summary>
        /// Updates the HSV sliders from the selected color.
        /// </summary>
        /// <param name="color">The color to update the sliders with.</param>
        private void UpdateHSVFromColor(SKColor color)
        {
            color.ToHsv(out float h, out float s, out float v);
            HueSlider.Value = h;
            SaturationSlider.Value = s;
            ValueSlider.Value = v;
        }

        /// <summary>
        /// Updates the RGB text boxes from the selected color.
        /// </summary>
        /// <param name="color">The color to update the text boxes with.</param>
        private void UpdateRGBFromColor(SKColor color)
        {
            RedBox.Text = color.Red.ToString();
            GreenBox.Text = color.Green.ToString();
            BlueBox.Text = color.Blue.ToString();
        }

        /// <summary>
        /// Updates the Hex text box from the selected color.
        /// </summary>
        /// <param name="color">The color to update the text box with.</param>
        private void UpdateHexFromColor(SKColor color)
        {
            HexBox.Text = $"{color.Red:X2}{color.Green:X2}{color.Blue:X2}";
        }
    }
}
