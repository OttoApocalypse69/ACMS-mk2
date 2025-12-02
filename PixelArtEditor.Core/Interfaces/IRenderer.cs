using SkiaSharp;
using PixelArtEditor.Core.Models;
using System.Collections.Generic;

namespace PixelArtEditor.Core.Interfaces
{
    /// <summary>
    /// Defines a renderer for drawing the pixel art canvas.
    /// This interface abstracts the rendering logic, allowing for different rendering implementations.
    /// </summary>
    public interface IRenderer
    {
        /// <summary>
        /// Renders the layers of the pixel art image onto a SkiaSharp canvas.
        /// </summary>
        /// <param name="canvas">The SkiaSharp canvas to render on.</param>
        /// <param name="layers">The collection of layers to render.</param>
        /// <param name="zoom">The zoom level to apply to the rendering.</param>
        /// <param name="panX">The horizontal pan offset.</param>
        /// <param name="panY">The vertical pan offset.</param>
        void Render(SKCanvas canvas, IEnumerable<Layer> layers, float zoom, float panX, float panY);
    }
}
