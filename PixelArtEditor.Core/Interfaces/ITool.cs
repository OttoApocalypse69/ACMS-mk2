using PixelArtEditor.Core.Models;
using SkiaSharp;

namespace PixelArtEditor.Core.Interfaces
{
    public interface ITool
    {
        string Name { get; }
        void OnMouseDown(Layer layer, int x, int y, SKColor color);
        void OnMouseMove(Layer layer, int x, int y, SKColor color);
        void OnMouseUp(Layer layer, int x, int y, SKColor color);
    }
}
