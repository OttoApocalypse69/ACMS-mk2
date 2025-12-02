using PixelArtEditor.Core.Interfaces;
using PixelArtEditor.Core.Models;
using SkiaSharp;

namespace PixelArtEditor.Core.Tools
{
    public class BlurTool : ITool
    {
        public string Name => "Blur";
        public int Strength { get; set; } = 3;
        private bool _isApplying;
        private SKBitmap _snapshot = null!;
        private readonly IHistoryService _historyService;

        public BlurTool(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        public void OnMouseDown(Layer layer, int x, int y, SKColor color)
        {
            if (layer == null || layer.IsLocked || !layer.IsVisible) return;

            _snapshot = layer.Bitmap.Copy();
            _isApplying = true;
            ApplyBlur(layer, x, y);
        }

        public void OnMouseMove(Layer layer, int x, int y, SKColor color)
        {
            if (_isApplying && layer != null && !layer.IsLocked && layer.IsVisible)
            {
                ApplyBlur(layer, x, y);
            }
        }

        public void OnMouseUp(Layer layer, int x, int y, SKColor color)
        {
            if (_isApplying)
            {
                _isApplying = false;

                if (_snapshot != null)
                {
                    var command = new PixelArtEditor.Core.Commands.DrawCommand(layer, _snapshot, layer.Bitmap.Copy());
                    _historyService.Push(command);
                    _snapshot.Dispose();
                }
            }
        }

        private void ApplyBlur(Layer layer, int centerX, int centerY)
        {
            int radius = Strength / 2;
            for (int y = centerY - radius; y <= centerY + radius; y++)
            {
                for (int x = centerX - radius; x <= centerX + radius; x++)
                {
                    if (x >= 0 && x < layer.Bitmap.Width && y >= 0 && y < layer.Bitmap.Height)
                    {
                        layer.Bitmap.SetPixel(x, y, GetBlurredColor(layer.Bitmap, x, y, radius));
                    }
                }
            }
        }

        private SKColor GetBlurredColor(SKBitmap bitmap, int px, int py, int radius)
        {
            int r = 0, g = 0, b = 0, a = 0;
            int count = 0;

            for (int y = py - radius; y <= py + radius; y++)
            {
                for (int x = px - radius; x <= px + radius; x++)
                {
                    if (x >= 0 && x < bitmap.Width && y >= 0 && y < bitmap.Height)
                    {
                        SKColor color = bitmap.GetPixel(x, y);
                        r += color.Red;
                        g += color.Green;
                        b += color.Blue;
                        a += color.Alpha;
                        count++;
                    }
                }
            }

            return new SKColor((byte)(r / count), (byte)(g / count), (byte)(b / count), (byte)(a / count));
        }
    }
}
