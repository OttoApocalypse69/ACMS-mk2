using PixelArtEditor.UI.ViewModels;
using SkiaSharp;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PixelArtEditor.UI.Views
{
    public partial class ColorPaletteView : UserControl
    {
        private bool _isUpdating = false;

        public ColorPaletteView()
        {
            InitializeComponent();
        }

        private void OnHueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_isUpdating || DataContext == null) return;
            UpdateColorFromHSV();
        }

        private void OnSaturationChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_isUpdating || DataContext == null) return;
            UpdateColorFromHSV();
        }

        private void OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_isUpdating || DataContext == null) return;
            UpdateColorFromHSV();
        }

        private void OnRgbChanged(object sender, TextChangedEventArgs e)
        {
            if (_isUpdating || DataContext == null) return;
            UpdateColorFromRGB();
        }

        private void OnHexChanged(object sender, TextChangedEventArgs e)
        {
            if (_isUpdating || DataContext == null) return;
            UpdateColorFromHex();
        }

        private void OnPaletteColorSelected(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is ColorPaletteViewModel vm && vm.SelectedColor != SKColor.Empty)
            {
                UpdateControlsFromColor(vm.SelectedColor);
            }
        }

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

        private void UpdateControlsFromColor(SKColor color)
        {
            _isUpdating = true;
            UpdateHSVFromColor(color);
            UpdateRGBFromColor(color);
            UpdateHexFromColor(color);
            _isUpdating = false;
        }

        private void UpdateHSVFromColor(SKColor color)
        {
            color.ToHsv(out float h, out float s, out float v);
            HueSlider.Value = h;
            SaturationSlider.Value = s;
            ValueSlider.Value = v;
        }

        private void UpdateRGBFromColor(SKColor color)
        {
            RedBox.Text = color.Red.ToString();
            GreenBox.Text = color.Green.ToString();
            BlueBox.Text = color.Blue.ToString();
        }

        private void UpdateHexFromColor(SKColor color)
        {
            HexBox.Text = $"{color.Red:X2}{color.Green:X2}{color.Blue:X2}";
        }
    }
}
