using SkiaSharp;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PixelArtEditor.Core.Models
{
    public enum BlendMode
    {
        Normal,
        Multiply,
        Screen,
        Overlay,
        Darken,
        Lighten,
        Add,
        Difference
    }

    public class Layer : INotifyPropertyChanged
    {
        private string _name = null!;
        private bool _isVisible = true;
        private double _opacity = 1.0;
        private BlendMode _blendMode = BlendMode.Normal;
        private SKBitmap _bitmap = null!;
        private bool _isLocked;

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public bool IsVisible
        {
            get => _isVisible;
            set { _isVisible = value; OnPropertyChanged(); }
        }

        public double Opacity
        {
            get => _opacity;
            set { _opacity = value; OnPropertyChanged(); }
        }

        public BlendMode BlendMode
        {
            get => _blendMode;
            set { _blendMode = value; OnPropertyChanged(); }
        }

        public bool IsLocked
        {
            get => _isLocked;
            set { _isLocked = value; OnPropertyChanged(); }
        }

        public SKBitmap Bitmap
        {
            get => _bitmap;
            set { _bitmap = value; OnPropertyChanged(); }
        }

        public Layer(int width, int height, string name)
        {
            Name = name;
            Bitmap = new SKBitmap(width, height, SKColorType.Rgba8888, SKAlphaType.Premul);
            // Initialize with transparent pixels
            Bitmap.Erase(SKColors.Transparent);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
