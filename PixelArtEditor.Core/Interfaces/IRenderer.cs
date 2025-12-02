using SkiaSharp;
using PixelArtEditor.Core.Models;
using System.Collections.Generic;

namespace PixelArtEditor.Core.Interfaces
{
    public interface IRenderer
    {
        void Render(SKCanvas canvas, IEnumerable<Layer> layers, float zoom, float panX, float panY);
    }
}
