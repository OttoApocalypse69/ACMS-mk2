using PixelArtEditor.Core.Interfaces;
using PixelArtEditor.Core.Models;
using SkiaSharp;
using System;

namespace PixelArtEditor.Core.Tools
{
    /// <summary>
    /// A tool for drawing pixels on a layer.
    /// This is the primary tool for drawing.
    /// </summary>
    public class PencilTool : ITool
    {
        /// <summary>
        /// Gets the name of the tool.
        /// </summary>
        public string Name => "Pencil";
        private bool _isDrawing;
        private SKBitmap _snapshot;
        private readonly IHistoryService _historyService;
        private readonly SymmetrySettings _symmetrySettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="PencilTool"/> class.
        /// </summary>
        /// <param name="historyService">The history service to use for undo/redo.</param>
        /// <param name="symmetrySettings">The symmetry settings to use for drawing.</param>
        public PencilTool(IHistoryService historyService, SymmetrySettings symmetrySettings = null)
        {
            _historyService = historyService;
            _symmetrySettings = symmetrySettings;
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
            
            _isDrawing = true;
            DrawPixel(layer, x, y, color);
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
            if (_isDrawing && layer != null && !layer.IsLocked && layer.IsVisible)
            {
                DrawPixel(layer, x, y, color);
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
            if (_isDrawing)
            {
                _isDrawing = false;
                
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
        /// Draws a single pixel on the layer.
        /// </summary>
        /// <param name="layer">The layer to draw on.</param>
        /// <param name="x">The x-coordinate of the pixel.</param>
        /// <param name="y">The y-coordinate of the pixel.</param>
        /// <param name="color">The color to draw the pixel with.</param>
        private void DrawPixel(Layer layer, int x, int y, SKColor color)
        {
            if (x >= 0 && x < layer.Bitmap.Width && y >= 0 && y < layer.Bitmap.Height)
            {
                layer.Bitmap.SetPixel(x, y, color);
                
                if (_symmetrySettings != null)
                {
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
}
