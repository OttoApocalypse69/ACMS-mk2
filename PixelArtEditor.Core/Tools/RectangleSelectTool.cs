using PixelArtEditor.Core.Interfaces;
using PixelArtEditor.Core.Models;
using SkiaSharp;
using System;

namespace PixelArtEditor.Core.Tools
{
    /// <summary>
    /// A tool for selecting a rectangular area on the canvas.
    /// </summary>
    public class RectangleSelectTool : ITool
    {
        /// <summary>
        /// Gets the name of the tool.
        /// </summary>
        public string Name => "Rectangle Select";
        private bool _isSelecting;
        private SKPointI _startPoint;
        private SKPointI _endPoint;
        
        /// <summary>
        /// Occurs when the selection rectangle has changed.
        /// </summary>
        public event Action<SKRectI> SelectionChanged;

        /// <summary>
        /// Handles the mouse down event.
        /// </summary>
        /// <param name="layer">The active layer.</param>
        /// <param name="x">The x-coordinate of the mouse, in pixel space.</param>
        /// <param name="y">The y-coordinate of the mouse, in pixel space.</param>
        /// <param name="color">The currently selected color.</param>
        public void OnMouseDown(Layer layer, int x, int y, SKColor color)
        {
            _isSelecting = true;
            _startPoint = new SKPointI(x, y);
            _endPoint = _startPoint;
            NotifySelectionChanged();
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
            if (_isSelecting)
            {
                _endPoint = new SKPointI(x, y);
                NotifySelectionChanged();
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
            if (_isSelecting)
            {
                _isSelecting = false;
                _endPoint = new SKPointI(x, y);
                NotifySelectionChanged();
            }
        }

        /// <summary>
        /// Notifies subscribers that the selection has changed.
        /// </summary>
        private void NotifySelectionChanged()
        {
            var rect = GetSelectionRect();
            SelectionChanged?.Invoke(rect);
        }

        /// <summary>
        /// Gets the current selection rectangle.
        /// </summary>
        /// <returns>The selection rectangle.</returns>
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
