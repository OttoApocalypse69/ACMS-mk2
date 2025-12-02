using PixelArtEditor.Core.Interfaces;
using PixelArtEditor.Core.Models;
using SkiaSharp;
using System;

namespace PixelArtEditor.Core.Tools
{
    public class RectangleSelectTool : ITool
    {
        public string Name => "Rectangle Select";
        private bool _isSelecting;
        private SKPointI _startPoint;
        private SKPointI _endPoint;
        
        public event Action<SKRectI> SelectionChanged;

        public void OnMouseDown(Layer layer, int x, int y, SKColor color)
        {
            _isSelecting = true;
            _startPoint = new SKPointI(x, y);
            _endPoint = _startPoint;
            NotifySelectionChanged();
        }

        public void OnMouseMove(Layer layer, int x, int y, SKColor color)
        {
            if (_isSelecting)
            {
                _endPoint = new SKPointI(x, y);
                NotifySelectionChanged();
            }
        }

        public void OnMouseUp(Layer layer, int x, int y, SKColor color)
        {
            if (_isSelecting)
            {
                _isSelecting = false;
                _endPoint = new SKPointI(x, y);
                NotifySelectionChanged();
            }
        }

        private void NotifySelectionChanged()
        {
            var rect = GetSelectionRect();
            SelectionChanged?.Invoke(rect);
        }

        public SKRectI GetSelectionRect()
        {
            int left = Math.Min(_startPoint.X, _endPoint.X);
            int top = Math.Min(_startPoint.Y, _endPoint.Y);
            int right = Math.Max(_startPoint.X, _endPoint.X);
            int bottom = Math.Max(_startPoint.Y, _endPoint.Y);
            
            return new SKRectI(left, top, right, bottom);
        }
    }
}
