using PixelArtEditor.Core.Interfaces;
using PixelArtEditor.Core.Models;
using SkiaSharp;

namespace PixelArtEditor.Core.Tools
{
    public class EraserTool : ITool
    {
        public string Name => "Eraser";
        private bool _isErasing;
        private SKBitmap _snapshot;
        private readonly IHistoryService _historyService;

        public EraserTool(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        public void OnMouseDown(Layer layer, int x, int y, SKColor color)
        {
            if (layer == null || layer.IsLocked || !layer.IsVisible) return;

            // Capture snapshot
            _snapshot = layer.Bitmap.Copy();

            _isErasing = true;
            ErasePixel(layer, x, y);
        }

        public void OnMouseMove(Layer layer, int x, int y, SKColor color)
        {
            if (_isErasing && layer != null && !layer.IsLocked && layer.IsVisible)
            {
                ErasePixel(layer, x, y);
            }
        }

        public void OnMouseUp(Layer layer, int x, int y, SKColor color)
        {
            if (_isErasing)
            {
                _isErasing = false;
                
                if (_snapshot != null)
                {
                    var command = new PixelArtEditor.Core.Commands.DrawCommand(layer, _snapshot, layer.Bitmap);
                    _historyService.Push(command);
                    _snapshot.Dispose();
                    _snapshot = null;
                }
            }
        }

        private void ErasePixel(Layer layer, int x, int y)
        {
            if (x >= 0 && x < layer.Bitmap.Width && y >= 0 && y < layer.Bitmap.Height)
            {
                layer.Bitmap.SetPixel(x, y, SKColors.Transparent);
            }
        }
    }
}
