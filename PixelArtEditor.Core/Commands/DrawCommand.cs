using PixelArtEditor.Core.Interfaces;
using PixelArtEditor.Core.Models;
using SkiaSharp;

namespace PixelArtEditor.Core.Commands
{
    public class DrawCommand : ICommand
    {
        private readonly Layer _layer;
        private readonly SKBitmap _previousBitmap;
        private readonly SKBitmap _newBitmap;

        public string Name => "Draw";

        public DrawCommand(Layer layer, SKBitmap previousBitmap, SKBitmap newBitmap)
        {
            _layer = layer;
            // Store deep copies
            _previousBitmap = previousBitmap.Copy();
            _newBitmap = newBitmap.Copy();
        }

        public void Execute()
        {
            // Restore new state
            using (var canvas = new SKCanvas(_layer.Bitmap))
            {
                canvas.Clear();
                canvas.DrawBitmap(_newBitmap, 0, 0);
            }
        }

        public void Undo()
        {
            // Restore old state
            using (var canvas = new SKCanvas(_layer.Bitmap))
            {
                canvas.Clear();
                canvas.DrawBitmap(_previousBitmap, 0, 0);
            }
        }
    }
}
