using PixelArtEditor.Core.Interfaces;
using PixelArtEditor.Core.Models;
using SkiaSharp;
using System;

namespace PixelArtEditor.Core.Tools
{
    public class TextTool : ITool
    {
        public string Name => "Text";
        public string Text { get; set; } = "Hello";
        public string FontFamily { get; set; } = "Arial";
        public float FontSize { get; set; } = 12f;
        public SKColor Color { get; set; } = SKColors.Black;

        private bool _isPlacing;
        private SKPointI _startPoint;
        private SKPointI _endPoint;
        private SKBitmap _snapshot = null!;
        private readonly IHistoryService _historyService;

        public TextTool(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        public void OnMouseDown(Layer layer, int x, int y, SKColor color)
        {
            if (layer == null || layer.IsLocked || !layer.IsVisible) return;

            _snapshot = layer.Bitmap.Copy();
            _isPlacing = true;
            _startPoint = new SKPointI(x, y);
            _endPoint = _startPoint;
        }

        public void OnMouseMove(Layer layer, int x, int y, SKColor color)
        {
            if (_isPlacing)
            {
                _endPoint = new SKPointI(x, y);
            }
        }

        public void OnMouseUp(Layer layer, int x, int y, SKColor color)
        {
            if (_isPlacing && layer != null)
            {
                _isPlacing = false;
                _endPoint = new SKPointI(x, y);
                DrawText(layer);

                if (_snapshot != null)
                {
                    var command = new PixelArtEditor.Core.Commands.DrawCommand(layer, _snapshot, layer.Bitmap.Copy());
                    _historyService.Push(command);
                    _snapshot.Dispose();
                }
            }
        }

        private void DrawText(Layer layer)
        {
            using (var canvas = new SKCanvas(layer.Bitmap))
            using (var paint = new SKPaint())
            {
                paint.TextSize = FontSize;
                paint.Color = Color;
                paint.Typeface = SKTypeface.FromFamilyName(FontFamily);
                paint.IsAntialias = true;

                var rect = new SKRect(Math.Min(_startPoint.X, _endPoint.X), Math.Min(_startPoint.Y, _endPoint.Y), Math.Max(_startPoint.X, _endPoint.X), Math.Max(_startPoint.Y, _endPoint.Y));
                canvas.DrawText(Text, rect.Left, rect.Bottom, paint);
            }
        }
    }
}
