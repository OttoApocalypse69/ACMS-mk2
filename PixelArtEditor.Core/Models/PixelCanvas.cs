using SkiaSharp;

namespace PixelArtEditor.Core.Models
{
    /// <summary>
    /// Represents the pixel data for a single layer.
    /// This class provides methods for getting and setting individual pixel colors.
    /// </summary>
    public class PixelCanvas
    {
        /// <summary>
        /// Gets the width of the canvas.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Gets the height of the canvas.
        /// </summary>
        public int Height { get; }
        private SKBitmap _bitmap;

        /// <summary>
        /// Initializes a new instance of the <see cref="PixelCanvas"/> class.
        /// </summary>
        /// <param name="width">The width of the canvas.</param>
        /// <param name="height">The height of the canvas.</param>
        public PixelCanvas(int width, int height)
        {
            Width = width;
            Height = height;
            _bitmap = new SKBitmap(width, height);
            _bitmap.Erase(SKColors.Transparent);
        }

        /// <summary>
        /// Sets the color of a single pixel on the canvas.
        /// </summary>
        /// <param name="x">The x-coordinate of the pixel.</param>
        /// <param name="y">The y-coordinate of the pixel.</param>
        /// <param name="color">The color to set the pixel to.</param>
        public void SetPixel(int x, int y, SKColor color)
        {
            if (x >= 0 && x < Width && y >= 0 && y < Height)
            {
                _bitmap.SetPixel(x, y, color);
            }
        }

        /// <summary>
        /// Gets the color of a single pixel on the canvas.
        /// </summary>
        /// <param name="x">The x-coordinate of the pixel.</param>
        /// <param name="y">The y-coordinate of the pixel.</param>
        /// <returns>The color of the pixel.</returns>
        public SKColor GetPixel(int x, int y)
        {
             if (x >= 0 && x < Width && y >= 0 && y < Height)
            {
                return _bitmap.GetPixel(x, y);
            }
            return SKColors.Transparent;
        }

        /// <summary>
        /// Gets the underlying SkiaSharp bitmap for the canvas.
        /// </summary>
        public SKBitmap Bitmap => _bitmap;
    }
}
