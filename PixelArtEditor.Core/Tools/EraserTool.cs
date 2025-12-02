using PixelArtEditor.Core.Interfaces;
using PixelArtEditor.Core.Models;
using SkiaSharp;

namespace PixelArtEditor.Core.Tools
{
    /// <summary>
    /// A tool for erasing pixels on a layer.
    /// This tool sets the color of the pixels to transparent.
    /// </summary>
    public class EraserTool : ITool
    {
        /// <summary>
        /// Gets the name of the tool.
        /// </summary>
        public string Name => "Eraser";
        private bool _isErasing;
        private SKBitmap _snapshot;
        private readonly IHistoryService _historyService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EraserTool"/> class.
        /// </summary>
        /// <param name="historyService">The history service to use for undo/redo.</param>
        public EraserTool(IHistoryService historyService)
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

            _snapshot = layer.Bitmap.Copy();

            _isErasing = true;
            ErasePixel(layer, x, y);
        }

        /// <summary>
        /// Handles the mouse move event.
        /// </summary>
        /// <param name="layer">The active layer.</param>
        /// <param name="x">The x-coordinate of the mouse, in pixel space.</param>
        /// <param name="y">The y-coordinate of the mouse, in pixel space.</param>
        /// <param name="color">The currently selected color.</param>
        public void OnMouseMove(Layer layer, int x, int y, SKColor color)
        {
            if (_isErasing && layer != null && !layer.IsLocked && layer.IsVisible)
            {
                ErasePixel(layer, x, y);
            }
        }

        /// <summary>
        /// Handles the mouse up event.
        /// </summary>
        /// <param name="layer">The active layer.</param>
        /// <param name="x">The x-coordinate of the mouse, in pixel space.</param>
        /// <param name="y">The y-coordinate of the mouse, in pixel space.</param>
        /// <param name="color">The currently selected color.</param>
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

        /// <summary>
        /// Erases a single pixel on the layer.
        /// </summary>
        /// <param name="layer">The layer to erase on.</param>
        /// <param name="x">The x-coordinate of the pixel.</param>
        /// <param name="y">The y-coordinate of the pixel.</param>
        private void ErasePixel(Layer layer, int x, int y)
        {
            if (x >= 0 && x < layer.Bitmap.Width && y >= 0 && y < layer.Bitmap.Height)
            {
                layer.Bitmap.SetPixel(x, y, SKColors.Transparent);
            }
        }
    }
}
