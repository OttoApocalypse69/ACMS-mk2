using PixelArtEditor.Core.Interfaces;
using PixelArtEditor.Core.Models;
using SkiaSharp;
using System.Collections.Generic;

namespace PixelArtEditor.Core.Tools
{
    /// <summary>
    /// A tool for filling an area of a layer with a solid color.
    /// This tool uses a flood fill algorithm.
    /// </summary>
    public class FillTool : ITool
    {
        /// <summary>
        /// Gets the name of the tool.
        /// </summary>
        public string Name => "Fill";
        private readonly IHistoryService _historyService;

        /// <summary>
        /// Initializes a new instance of the <see cref="FillTool"/> class.
        /// </summary>
        /// <param name="historyService">The history service to use for undo/redo.</param>
        public FillTool(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        /// <summary>
        /// Handles the mouse down event.
        /// </summary>
        /// <param name="layer">The active layer.</param>
        /// <param name="x">The x-coordinate of the mouse, in pixel space.</param>
        /// <param name="y">The y-coordinate of the mouse, in pixel space.</param>
        /// <param name="color">The currently selected color.</param>
        public void OnMouseDown(Layer layer, int x, int y, SKColor color)
        {
            if (layer == null || layer.IsLocked || !layer.IsVisible) return;
            
            var snapshot = layer.Bitmap.Copy();
            
            FloodFill(layer, x, y, color);
            
            var command = new PixelArtEditor.Core.Commands.DrawCommand(layer, snapshot, layer.Bitmap);
            _historyService.Push(command);
            snapshot.Dispose();
        }

        /// <summary>
        /// Handles the mouse move event.
        /// </summary>
        /// <param name="layer">The active layer.</param>
        /// <param name="x">The x-coordinate of the mouse, in pixel space.</param>
        /// <param name="y">The y-coordinate of the mouse, in pixel space.</param>
        /// <param name="color">The currently selected color.</param>
        public void OnMouseMove(Layer layer, int x, int y, SKColor color) { }

        /// <summary>
        /// Handles the mouse up event.
        /// </summary>
        /// <param name="layer">The active layer.</param>
        /// <param name="x">The x-coordinate of the mouse, in pixel space.</param>
        /// <param name="y">The y-coordinate of the mouse, in pixel space.</param>
        /// <param name="color">The currently selected color.</param>
        public void OnMouseUp(Layer layer, int x, int y, SKColor color) { }

        /// <summary>
        /// Fills an area of the layer with the target color, starting from the specified coordinates.
        /// </summary>
        /// <param name="layer">The layer to fill.</param>
        /// <param name="x">The starting x-coordinate.</param>
        /// <param name="y">The starting y-coordinate.</param>
        /// <param name="targetColor">The color to fill with.</param>
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
