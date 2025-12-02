using SkiaSharp;
using PixelArtEditor.Core.Interfaces;
using PixelArtEditor.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace PixelArtEditor.Core.Services
{
    public class CanvasRenderer : IRenderer
    {
        public void Render(SKCanvas canvas, IEnumerable<Layer> layers, float zoom, float panX, float panY)
        {
            canvas.Clear(SKColors.DarkGray); // Background

            // Draw Checkerboard pattern for transparency
            DrawCheckerboard(canvas, zoom, panX, panY);

            // Save matrix
            canvas.Save();

            // Apply transformations
            canvas.Translate(panX, panY);
            canvas.Scale(zoom);

            // Draw layers from Bottom to Top
            // Assuming index 0 is Top, we need to reverse
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

            // Restore matrix
            canvas.Restore();
        }

        private void DrawCheckerboard(SKCanvas canvas, float zoom, float panX, float panY)
        {
            // TODO: Implement efficient checkerboard drawing
            // For now, just a solid color is fine as per previous implementation, 
            // but let's make it a slightly lighter gray to distinguish from "outside" canvas
            canvas.Clear(SKColors.Gray);
        }

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
