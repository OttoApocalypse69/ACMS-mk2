using PixelArtEditor.Core.Interfaces;
using PixelArtEditor.Core.Models;
using SkiaSharp;
using System;

namespace PixelArtEditor.Core.Tools
{
    /// <summary>
    /// A tool for picking a color from a layer.
    /// This tool is used to select a color from the canvas.
    /// </summary>
    public class EyedropperTool : ITool
    {
        /// <summary>
        /// Gets the name of the tool.
        /// </summary>
        public string Name => "Eyedropper";
        private readonly Action<SKColor> _onColorPicked;

        /// <summary>
        /// Initializes a new instance of the <see cref="EyedropperTool"/> class.
        /// </summary>
        /// <param name="onColorPicked">The action to execute when a color is picked.</param>
        public EyedropperTool(Action<SKColor> onColorPicked)
        {
            _onColorPicked = onColorPicked;
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
            PickColor(layer, x, y);
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
        }

        /// <summary>
        /// Picks the color of a single pixel on the layer.
        /// </summary>
        /// <param name="layer">The layer to pick the color from.</param>
        /// <param name="x">The x-coordinate of the pixel.</param>
        /// <param name="y">The y-coordinate of the pixel.</param>
        private void PickColor(Layer layer, int x, int y)
        {
            if (layer != null && x >= 0 && x < layer.Bitmap.Width && y >= 0 && y < layer.Bitmap.Height)
            {
                var pickedColor = layer.Bitmap.GetPixel(x, y);
                _onColorPicked?.Invoke(pickedColor);
            }
        }
    }
}
