using PixelArtEditor.Core.Interfaces;
using PixelArtEditor.Core.Models;
using SkiaSharp;

namespace PixelArtEditor.Core.Commands
{
    /// <summary>
    /// A command that represents a drawing action on a layer.
    /// This command is used for undoing and redoing drawing operations.
    /// </summary>
    public class DrawCommand : ICommand
    {
        private readonly Layer _layer;
        private readonly SKBitmap _previousBitmap;
        private readonly SKBitmap _newBitmap;

        /// <summary>
        /// Gets the name of the command.
        /// </summary>
        public string Name => "Draw";

        /// <summary>
        /// Initializes a new instance of the <see cref="DrawCommand"/> class.
        /// </summary>
        /// <param name="layer">The layer that was drawn on.</param>
        /// <param name="previousBitmap">The bitmap of the layer before the drawing action.</param>
        /// <param name="newBitmap">The bitmap of the layer after the drawing action.</param>
        public DrawCommand(Layer layer, SKBitmap previousBitmap, SKBitmap newBitmap)
        {
            _layer = layer;
            _previousBitmap = previousBitmap.Copy();
            _newBitmap = newBitmap.Copy();
        }

        /// <summary>
        /// Executes the command, restoring the new state of the layer.
        /// </summary>
        public void Execute()
        {
            using (var canvas = new SKCanvas(_layer.Bitmap))
            {
                canvas.Clear();
                canvas.DrawBitmap(_newBitmap, 0, 0);
            }
        }

        /// <summary>
        /// Undoes the command, restoring the previous state of the layer.
        /// </summary>
        public void Undo()
        {
            using (var canvas = new SKCanvas(_layer.Bitmap))
            {
                canvas.Clear();
                canvas.DrawBitmap(_previousBitmap, 0, 0);
            }
        }
    }
}
