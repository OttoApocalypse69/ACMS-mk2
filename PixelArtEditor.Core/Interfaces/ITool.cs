using PixelArtEditor.Core.Models;
using SkiaSharp;

namespace PixelArtEditor.Core.Interfaces
{
    /// <summary>
    /// Defines a tool for interacting with the pixel art canvas.
    /// Tools like the pencil, eraser, and color picker implement this interface.
    /// </summary>
    public interface ITool
    {
        /// <summary>
        /// Gets the name of the tool.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Handles the mouse down event.
        /// </summary>
        /// <param name="layer">The active layer.</param>
        /// <param name="x">The x-coordinate of the mouse, in pixel space.</param>
        /// <param name="y">The y-coordinate of the mouse, in pixel space.</param>
        /// <param name="color">The currently selected color.</param>
        void OnMouseDown(Layer layer, int x, int y, SKColor color);

        /// <summary>
        /// Handles the mouse move event.
        /// </summary>
        /// <param name="layer">The active layer.</param>
        /// <param name="x">The x-coordinate of the mouse, in pixel space.</param>
        /// <param name="y">The y-coordinate of the mouse, in pixel space.</param>
        /// <param name="color">The currently selected color.</param>
        void OnMouseMove(Layer layer, int x, int y, SKColor color);

        /// <summary>
        /// Handles the mouse up event.
        /// </summary>
        /// <param name="layer">The active layer.</param>
        /// <param name="x">The x-coordinate of the mouse, in pixel space.</param>
        /// <param name="y">The y-coordinate of the mouse, in pixel space.</param>
        /// <param name="color">The currently selected color.</param>
        void OnMouseUp(Layer layer, int x, int y, SKColor color);
    }
}
