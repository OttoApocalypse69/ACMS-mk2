using PixelArtEditor.Core.Interfaces;
using PixelArtEditor.Core.Models;
using SkiaSharp;
using System;

namespace PixelArtEditor.Core.Tools
{
    public class EyedropperTool : ITool
    {
        public string Name => "Eyedropper";
        private readonly Action<SKColor> _onColorPicked;

        public EyedropperTool(Action<SKColor> onColorPicked)
        {
            _onColorPicked = onColorPicked;
        }

        public void OnMouseDown(Layer layer, int x, int y, SKColor color)
        {
            PickColor(layer, x, y);
        }

        public void OnMouseMove(Layer layer, int x, int y, SKColor color)
        {
            // Optional: Continuous picking while dragging
            // PickColor(layer, x, y);
        }

        public void OnMouseUp(Layer layer, int x, int y, SKColor color)
        {
        }

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
