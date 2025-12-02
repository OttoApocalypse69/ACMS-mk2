using System;
using System.Collections.ObjectModel;
using System.Linq;
using PixelArtEditor.Core.Interfaces;
using PixelArtEditor.Core.Models;
using SkiaSharp;

namespace PixelArtEditor.Core.Services
{
    public class LayerService : ILayerService
    {
        private Layer _activeLayer;
        private readonly int _width;
        private readonly int _height;

        public ObservableCollection<Layer> Layers { get; } = new ObservableCollection<Layer>();

        public Layer ActiveLayer
        {
            get => _activeLayer;
            set
            {
                if (_activeLayer != value)
                {
                    _activeLayer = value;
                    // Notify property changed if we were using INotifyPropertyChanged here
                }
            }
        }

        public LayerService(int width, int height)
        {
            _width = width;
            _height = height;
            // Add initial layer
            AddLayer("Layer 1");
        }

        public void AddLayer(string name)
        {
            var layer = new Layer(_width, _height, name);
            Layers.Insert(0, layer); // Add to top
            ActiveLayer = layer;
        }

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

        public void MoveLayer(int oldIndex, int newIndex)
        {
            if (oldIndex >= 0 && oldIndex < Layers.Count && newIndex >= 0 && newIndex < Layers.Count)
            {
                Layers.Move(oldIndex, newIndex);
            }
        }

        public void DuplicateLayer(Layer layer)
        {
            if (layer == null) return;

            var newLayer = new Layer(_width, _height, $"{layer.Name} Copy");
            newLayer.Opacity = layer.Opacity;
            newLayer.BlendMode = layer.BlendMode;
            newLayer.IsVisible = layer.IsVisible;
            
            // Deep copy bitmap
            using (var canvas = new SKCanvas(newLayer.Bitmap))
            {
                canvas.DrawBitmap(layer.Bitmap, 0, 0);
            }

            int index = Layers.IndexOf(layer);
            Layers.Insert(index, newLayer);
            ActiveLayer = newLayer;
        }

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
                    // TODO: Implement blend modes here
                    canvas.DrawBitmap(layer.Bitmap, 0, 0, paint);
                }
                
                RemoveLayer(layer);
                ActiveLayer = layerBelow;
            }
        }

        public void ClearLayer(Layer layer)
        {
            layer?.Bitmap.Erase(SKColors.Transparent);
        }
    }
}
