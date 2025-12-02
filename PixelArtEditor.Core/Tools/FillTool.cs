using PixelArtEditor.Core.Interfaces;
using PixelArtEditor.Core.Models;
using SkiaSharp;
using System.Collections.Generic;

namespace PixelArtEditor.Core.Tools
{
    public class FillTool : ITool
    {
        public string Name => "Fill";
        private readonly IHistoryService _historyService;

        public FillTool(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        public void OnMouseDown(Layer layer, int x, int y, SKColor color)
        {
            if (layer == null || layer.IsLocked || !layer.IsVisible) return;
            
            var snapshot = layer.Bitmap.Copy();
            
            FloodFill(layer, x, y, color);
            
            // Create command immediately
            var command = new PixelArtEditor.Core.Commands.DrawCommand(layer, snapshot, layer.Bitmap);
            _historyService.Push(command);
            snapshot.Dispose();
        }

        public void OnMouseMove(Layer layer, int x, int y, SKColor color) { }
        public void OnMouseUp(Layer layer, int x, int y, SKColor color) { }

        private void FloodFill(Layer layer, int x, int y, SKColor targetColor)
        {
            if (x < 0 || x >= layer.Bitmap.Width || y < 0 || y >= layer.Bitmap.Height) return;

            var startColor = layer.Bitmap.GetPixel(x, y);
            if (startColor == targetColor) return;

            var stack = new Stack<SKPointI>();
            stack.Push(new SKPointI(x, y));

            while (stack.Count > 0)
            {
                var p = stack.Pop();
                if (p.X < 0 || p.X >= layer.Bitmap.Width || p.Y < 0 || p.Y >= layer.Bitmap.Height) continue;

                var currentColor = layer.Bitmap.GetPixel(p.X, p.Y);
                if (currentColor == startColor)
                {
                    layer.Bitmap.SetPixel(p.X, p.Y, targetColor);

                    stack.Push(new SKPointI(p.X + 1, p.Y));
                    stack.Push(new SKPointI(p.X - 1, p.Y));
                    stack.Push(new SKPointI(p.X, p.Y + 1));
                    stack.Push(new SKPointI(p.X, p.Y - 1));
                }
            }
        }
    }
}
