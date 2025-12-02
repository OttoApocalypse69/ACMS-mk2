using SkiaSharp;
using PixelArtEditor.Core.Interfaces;
using PixelArtEditor.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace PixelArtEditor.Core.Services
{
    /// <summary>
    /// Provides the logic for rendering the pixel art canvas.
    /// This class implements the IRenderer interface.
    /// </summary>
    public class CanvasRenderer : IRenderer
    {
        /// <summary>
        /// Renders the layers of the pixel art image onto a SkiaSharp canvas.
        /// </summary>
        /// <param name="canvas">The SkiaSharp canvas to render on.</param>
        /// <param name="layers">The collection of layers to render.</param>
        /// <param name="zoom">The zoom level to apply to the rendering.</param>
        /// <param name="panX">The horizontal pan offset.</param>
        /// <param name="panY">The vertical pan offset.</param>
        public void Render(SKCanvas canvas, IEnumerable<Layer> layers, float zoom, float panX, float panY)
        {
            canvas.Clear(SKColors.DarkGray);

            DrawCheckerboard(canvas, zoom, panX, panY);

            canvas.Save();

            canvas.Translate(panX, panY);
            canvas.Scale(zoom);

            foreach (var layer in layers.Reverse())
            {
                if (!layer.IsVisible || layer.Opacity <= 0) continue;

                using (var paint = new SKPaint
                {
                    FilterQuality = SKFilterQuality.None,
                    IsAntialias = false,
                    Color = SKColors.White.WithAlpha((byte)(layer.Opacity * 255)),
                    BlendMode = GetSkiaBlendMode(layer.BlendMode)
                })
                {
                    canvas.DrawBitmap(layer.Bitmap, 0, 0, paint);
                }
            }

            canvas.Restore();
        }

        /// <summary>
        /// Draws a checkerboard pattern on the canvas to represent transparency.
        /// </summary>
        /// <param name="canvas">The canvas to draw on.</param>
        /// <param name="zoom">The current zoom level.</param>
        /// <param name="panX">The horizontal pan offset.</param>
        /// <param name="panY">The vertical pan offset.</param>
        private void DrawCheckerboard(SKCanvas canvas, float zoom, float panX, float panY)
        {
            canvas.Clear(SKColors.Gray);
        }

        /// <summary>
        /// Converts the application's BlendMode to the corresponding SkiaSharp SKBlendMode.
        /// </summary>
        /// <param name="mode">The BlendMode to convert.</param>
        /// <returns>The corresponding SKBlendMode.</returns>
        private SKBlendMode GetSkiaBlendMode(BlendMode mode)
        {
            return mode switch
            {
                BlendMode.Normal => SKBlendMode.SrcOver,
                BlendMode.Multiply => SKBlendMode.Multiply,
                BlendMode.Screen => SKBlendMode.Screen,
                BlendMode.Overlay => SKBlendMode.Overlay,
                BlendMode.Darken => SKBlendMode.Darken,
                BlendMode.Lighten => SKBlendMode.Lighten,
                BlendMode.Add => SKBlendMode.Plus,
                BlendMode.Difference => SKBlendMode.Difference,
                _ => SKBlendMode.SrcOver
            };
        }
    }
}
