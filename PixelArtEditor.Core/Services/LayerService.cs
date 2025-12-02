using System;
using System.Collections.ObjectModel;
using System.Linq;
using PixelArtEditor.Core.Interfaces;
using PixelArtEditor.Core.Models;
using SkiaSharp;

namespace PixelArtEditor.Core.Services
{
    /// <summary>
    /// Manages the layers in the pixel art image.
    /// This class implements the ILayerService interface.
    /// </summary>
    public class LayerService : ILayerService
    {
        private Layer _activeLayer;
        private readonly int _width;
        private readonly int _height;

        /// <summary>
        /// Gets the collection of layers in the image.
        /// </summary>
        public ObservableCollection<Layer> Layers { get; } = new ObservableCollection<Layer>();

        /// <summary>
        /// Gets or sets the currently active layer.
        /// </summary>
        public Layer ActiveLayer
        {
            get => _activeLayer;
            set
            {
                if (_activeLayer != value)
                {
                    _activeLayer = value;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LayerService"/> class.
        /// </summary>
        /// <param name="width">The width of the canvas.</param>
        /// <param name="height">The height of the canvas.</param>
        public LayerService(int width, int height)
        {
            _width = width;
            _height = height;
            AddLayer("Layer 1");
        }

        /// <summary>
        /// Adds a new layer with the specified name.
        /// </summary>
        /// <param name="name">The name of the new layer.</param>
        public void AddLayer(string name)
        {
            var layer = new Layer(_width, _height, name);
            Layers.Insert(0, layer);
            ActiveLayer = layer;
        }

        /// <summary>
        /// Removes the specified layer.
        /// </summary>
        /// <param name="layer">The layer to remove.</param>
        public void RemoveLayer(Layer layer)
        {
            if (Layers.Count > 1 && Layers.Contains(layer))
            {
                int index = Layers.IndexOf(layer);
                Layers.Remove(layer);
                
                if (ActiveLayer == layer)
                {
                    ActiveLayer = Layers.FirstOrDefault();
                }
            }
        }

        /// <summary>
        /// Moves a layer from one position to another in the layer stack.
        /// </summary>
        /// <param name="oldIndex">The original index of the layer.</param>
        /// <param name="newIndex">The new index for the layer.</param>
        public void MoveLayer(int oldIndex, int newIndex)
        {
            if (oldIndex >= 0 && oldIndex < Layers.Count && newIndex >= 0 && newIndex < Layers.Count)
            {
                Layers.Move(oldIndex, newIndex);
            }
        }

        /// <summary>
        /// Creates a duplicate of the specified layer.
        /// </summary>
        /// <param name="layer">The layer to duplicate.</param>
        public void DuplicateLayer(Layer layer)
        {
            if (layer == null) return;

            var newLayer = new Layer(_width, _height, $"{layer.Name} Copy");
            newLayer.Opacity = layer.Opacity;
            newLayer.BlendMode = layer.BlendMode;
            newLayer.IsVisible = layer.IsVisible;
            
            using (var canvas = new SKCanvas(newLayer.Bitmap))
            {
                canvas.DrawBitmap(layer.Bitmap, 0, 0);
            }

            int index = Layers.IndexOf(layer);
            Layers.Insert(index, newLayer);
            ActiveLayer = newLayer;
        }

        /// <summary>
        /// Merges the specified layer with the layer below it.
        /// </summary>
        /// <param name="layer">The layer to merge down.</param>
        public void MergeDown(Layer layer)
        {
            int index = Layers.IndexOf(layer);
            if (index < Layers.Count - 1)
            {
                var layerBelow = Layers[index + 1];
                
                using (var canvas = new SKCanvas(layerBelow.Bitmap))
                using (var paint = new SKPaint())
                {
                    paint.Color = paint.Color.WithAlpha((byte)(layer.Opacity * 255));
                    canvas.DrawBitmap(layer.Bitmap, 0, 0, paint);
                }
                
                RemoveLayer(layer);
                ActiveLayer = layerBelow;
            }
        }

        /// <summary>
        /// Clears the content of the specified layer.
        /// </summary>
        /// <param name="layer">The layer to clear.</param>
        public void ClearLayer(Layer layer)
        {
            layer?.Bitmap.Erase(SKColors.Transparent);
        }
    }
}
