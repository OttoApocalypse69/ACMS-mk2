using SkiaSharp;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PixelArtEditor.Core.Models
{
    /// <summary>
    /// Defines the blending mode used to combine layers.
    /// </summary>
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

    /// <summary>
    /// Represents a single layer in the pixel art image.
    /// A layer has its own bitmap, visibility, opacity, and blend mode.
    /// </summary>
    public class Layer : INotifyPropertyChanged
    {
        private string _name;
        private bool _isVisible = true;
        private double _opacity = 1.0;
        private BlendMode _blendMode = BlendMode.Normal;
        private SKBitmap _bitmap;
        private bool _isLocked;

        /// <summary>
        /// Gets or sets the name of the layer.
        /// </summary>
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the layer is visible.
        /// </summary>
        public bool IsVisible
        {
            get => _isVisible;
            set { _isVisible = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets the opacity of the layer, from 0.0 to 1.0.
        /// </summary>
        public double Opacity
        {
            get => _opacity;
            set { _opacity = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets the blend mode of the layer.
        /// </summary>
        public BlendMode BlendMode
        {
            get => _blendMode;
            set { _blendMode = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the layer is locked for editing.
        /// </summary>
        public bool IsLocked
        {
            get => _isLocked;
            set { _isLocked = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets the SkiaSharp bitmap that holds the pixel data for this layer.
        /// </summary>
        public SKBitmap Bitmap
        {
            get => _bitmap;
            set { _bitmap = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Layer"/> class.
        /// </summary>
        /// <param name="width">The width of the layer.</param>
        /// <param name="height">The height of the layer.</param>
        /// <param name="name">The name of the layer.</param>
        public Layer(int width, int height, string name)
        {
            Name = name;
            Bitmap = new SKBitmap(width, height, SKColorType.Rgba8888, SKAlphaType.Premul);
            Bitmap.Erase(SKColors.Transparent);
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
