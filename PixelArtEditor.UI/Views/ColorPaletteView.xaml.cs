using PixelArtEditor.UI.ViewModels;
using SkiaSharp;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace PixelArtEditor.UI.Views
{
    public partial class ColorPaletteView : UserControl
    {
        private bool _isUpdating = false;
        private WriteableBitmap _svBitmap;
        private const int SvSize = 140;
        private float _h = 0f;
        private float _s = 1f;
        private float _v = 1f;

        public ColorPaletteView()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            CreateOrUpdateSvBitmap();
            if (DataContext is ColorPaletteViewModel vm)
            {
                if (vm.SelectedColor != SKColor.Empty)
                {
                    UpdateFromColor(vm.SelectedColor);
                }
                else
                {
                    UpdateFromColor(SKColors.White);
                }
            }
        }

        private void OnHueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_isUpdating || DataContext == null) return;
            _h = (float)HueSlider.Value;
            CreateOrUpdateSvBitmap();
            UpdateViewModelColor();
        }

        private void OnSvMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext == null) return;
            CaptureMouse();
            UpdateSvFromMouse(e.GetPosition(SvImage));
        }

        private void OnSvMouseMove(object sender, MouseEventArgs e)
        {
            if (!IsMouseCaptured || DataContext == null) return;
            UpdateSvFromMouse(e.GetPosition(SvImage));
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
                UpdateFromColor(vm.SelectedColor);
            }
        }

        private void UpdateSvFromMouse(Point p)
        {
            double width = SvImage.ActualWidth;
            double height = SvImage.ActualHeight;
            if (width <= 0 || height <= 0) return;

            _s = (float)Math.Clamp(p.X / width, 0, 1);
            _v = (float)Math.Clamp(1.0 - (p.Y / height), 0, 1);

            UpdateMarkerPosition();
            UpdateViewModelColor();
        }

        private void UpdateViewModelColor()
        {
            var color = SKColor.FromHsv(_h, _s * 100f, _v * 100f);

            _isUpdating = true;
            if (DataContext is ColorPaletteViewModel vm)
            {
                vm.SelectedColor = color;
            }
            UpdateHexFromColor(color);
            _isUpdating = false;
        }

        private void CreateOrUpdateSvBitmap()
        {
            int width = SvSize;
            int height = SvSize;

            if (_svBitmap == null || _svBitmap.PixelWidth != width || _svBitmap.PixelHeight != height)
            {
                _svBitmap = new WriteableBitmap(width, height, 96, 96, System.Windows.Media.PixelFormats.Bgra32, null);
                SvImage.Source = _svBitmap;
            }

            int stride = width * 4;
            byte[] pixels = new byte[height * stride];

            for (int y = 0; y < height; y++)
            {
                float v = 1f - (float)y / (height - 1);
                for (int x = 0; x < width; x++)
                {
                    float s = (float)x / (width - 1);
                    var c = SKColor.FromHsv(_h, s * 100f, v * 100f);
                    int index = y * stride + x * 4;
                    pixels[index + 0] = c.Blue;
                    pixels[index + 1] = c.Green;
                    pixels[index + 2] = c.Red;
                    pixels[index + 3] = c.Alpha;
                }
            }
            _svBitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, stride, 0);

            UpdateMarkerPosition();
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
                UpdateFromColor(color);
                _isUpdating = false;
            }
        }

        private void UpdateFromColor(SKColor color)
        {
            color.ToHsv(out float h, out float s, out float v);
            _h = h;
            _s = s / 100f;
            _v = v / 100f;

            HueSlider.Value = _h;
            CreateOrUpdateSvBitmap();
            UpdateMarkerPosition();
            UpdateHexFromColor(color);
        }

        private void UpdateHexFromColor(SKColor color)
        {
            HexBox.Text = $"{color.Red:X2}{color.Green:X2}{color.Blue:X2}";
        }

        private void UpdateMarkerPosition()
        {
            if (SvMarkerTransform == null || SvImage == null) return;

            double width = SvImage.ActualWidth;
            double height = SvImage.ActualHeight;
            if (width <= 0 || height <= 0) return;

            double x = _s * (width - 1);
            double y = (1.0 - _v) * (height - 1);

            SvMarkerTransform.X = x - SvMarker.Width / 2;
            SvMarkerTransform.Y = y - SvMarker.Height / 2;
        }
    }
}
