using PixelArtEditor.Core.Interfaces;
using PixelArtEditor.Core.Models;
using SkiaSharp;

namespace PixelArtEditor.Core.Tools
{
    public class RectangleTool : ITool
    {
        public string Name => "Rectangle";

        private bool _isDrawing;
        private int _startX;
        private int _startY;
        private SKBitmap _snapshot = null!;
        private readonly IHistoryService _historyService;
        private readonly SymmetrySettings _symmetrySettings;

        public RectangleTool(IHistoryService historyService, SymmetrySettings symmetrySettings)
        {
            _historyService = historyService;
            _symmetrySettings = symmetrySettings;
        }

        public void OnMouseDown(Layer layer, int x, int y, SKColor color)
        {
            if (layer == null || layer.IsLocked || !layer.IsVisible) return;

            _snapshot = layer.Bitmap.Copy();
            _startX = x;
            _startY = y;
            _isDrawing = true;
        }

        public void OnMouseMove(Layer layer, int x, int y, SKColor color)
        {
            if (!_isDrawing || layer == null || layer.IsLocked || !layer.IsVisible) return;

            using (var canvas = new SKCanvas(layer.Bitmap))
            {
                canvas.Clear();
                canvas.DrawBitmap(_snapshot, 0, 0);
            }

            DrawRectangle(layer, _startX, _startY, x, y, color);
        }

        public void OnMouseUp(Layer layer, int x, int y, SKColor color)
        {
            if (!_isDrawing || layer == null || layer.IsLocked || !layer.IsVisible) return;

            DrawRectangle(layer, _startX, _startY, x, y, color);
            _isDrawing = false;

            var command = new PixelArtEditor.Core.Commands.DrawCommand(layer, _snapshot, layer.Bitmap);
            _historyService.Push(command);
            _snapshot.Dispose();
        }

        private void DrawRectangle(Layer layer, int x0, int y0, int x1, int y1, SKColor color)
        {
            int left = System.Math.Min(x0, x1);
            int right = System.Math.Max(x0, x1);
            int top = System.Math.Min(y0, y1);
            int bottom = System.Math.Max(y0, y1);

            for (int x = left; x <= right; x++)
            {
                SetPixelWithSymmetry(layer, x, top, color);
                SetPixelWithSymmetry(layer, x, bottom, color);
            }

            for (int y = top; y <= bottom; y++)
            {
                SetPixelWithSymmetry(layer, left, y, color);
                SetPixelWithSymmetry(layer, right, y, color);
            }
        }

        private void SetPixelWithSymmetry(Layer layer, int x, int y, SKColor color)
        {
            if (x >= 0 && x < layer.Bitmap.Width && y >= 0 && y < layer.Bitmap.Height)
            {
                layer.Bitmap.SetPixel(x, y, color);

                if (_symmetrySettings == null || _symmetrySettings.Mode == SymmetryMode.None)
                {
                    return;
                }

                int width = layer.Bitmap.Width;
                int height = layer.Bitmap.Height;

                if (_symmetrySettings.Mode == SymmetryMode.Horizontal || _symmetrySettings.Mode == SymmetryMode.Both)
                {
                    int mirrorX = width - 1 - x;
                    if (mirrorX >= 0 && mirrorX < width)
                    {
                        layer.Bitmap.SetPixel(mirrorX, y, color);
                    }
                }

                if (_symmetrySettings.Mode == SymmetryMode.Vertical || _symmetrySettings.Mode == SymmetryMode.Both)
                {
                    int mirrorY = height - 1 - y;
                    if (mirrorY >= 0 && mirrorY < height)
                    {
                        layer.Bitmap.SetPixel(x, mirrorY, color);
                    }
                }

                if (_symmetrySettings.Mode == SymmetryMode.Both)
                {
                    int mirrorX = width - 1 - x;
                    int mirrorY = height - 1 - y;
                    if (mirrorX >= 0 && mirrorX < width && mirrorY >= 0 && mirrorY < height)
                    {
                        layer.Bitmap.SetPixel(mirrorX, mirrorY, color);
                    }
                }
            }
        }
    }
}


