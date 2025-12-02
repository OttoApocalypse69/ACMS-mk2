using SkiaSharp;

namespace PixelArtEditor.Core.Models
{
    public class PixelCanvas
    {
        public int Width { get; }
        public int Height { get; }
        private SKBitmap _bitmap;

        public PixelCanvas(int width, int height)
        {
            Width = width;
            Height = height;
            _bitmap = new SKBitmap(width, height);
            _bitmap.Erase(SKColors.Transparent);
        }

        public void SetPixel(int x, int y, SKColor color)
        {
            if (x >= 0 && x < Width && y >= 0 && y < Height)
            {
                _bitmap.SetPixel(x, y, color);
            }
        }

        public SKColor GetPixel(int x, int y)
        {
             if (x >= 0 && x < Width && y >= 0 && y < Height)
            {
                return _bitmap.GetPixel(x, y);
            }
            return SKColors.Transparent;
        }

        public SKBitmap Bitmap => _bitmap;
    }
}
