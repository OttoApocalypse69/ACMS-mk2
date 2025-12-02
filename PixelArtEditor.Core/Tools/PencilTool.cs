using PixelArtEditor.Core.Interfaces;
using PixelArtEditor.Core.Models;
using SkiaSharp;
using System;

namespace PixelArtEditor.Core.Tools
{
    public class PencilTool : ITool
    {
        public string Name => "Pencil";
        private bool _isDrawing;
        private SKBitmap _snapshot;
        private readonly IHistoryService _historyService;
        private readonly SymmetrySettings _symmetrySettings;

        public PencilTool(IHistoryService historyService, SymmetrySettings symmetrySettings = null)
        {
            _historyService = historyService;
            _symmetrySettings = symmetrySettings;
        }

        public void OnMouseDown(Layer layer, int x, int y, SKColor color)
        {
            if (layer == null || layer.IsLocked || !layer.IsVisible) return;
            
            // Capture snapshot before drawing
            _snapshot = layer.Bitmap.Copy();
            
            _isDrawing = true;
            DrawPixel(layer, x, y, color);
        }

        public void OnMouseMove(Layer layer, int x, int y, SKColor color)
        {
            if (_isDrawing && layer != null && !layer.IsLocked && layer.IsVisible)
            {
                DrawPixel(layer, x, y, color);
            }
        }

        public void OnMouseUp(Layer layer, int x, int y, SKColor color)
        {
            if (_isDrawing)
            {
                _isDrawing = false;
                
                // Create command with snapshot and current state
                if (_snapshot != null)
                {
                    var command = new PixelArtEditor.Core.Commands.DrawCommand(layer, _snapshot, layer.Bitmap);
                    _historyService.Push(command);
                    _snapshot.Dispose();
                    _snapshot = null;
                }
            }
        }

        private void DrawPixel(Layer layer, int x, int y, SKColor color)
        {
            if (x >= 0 && x < layer.Bitmap.Width && y >= 0 && y < layer.Bitmap.Height)
            {
                layer.Bitmap.SetPixel(x, y, color);
                
                // Apply symmetry
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
