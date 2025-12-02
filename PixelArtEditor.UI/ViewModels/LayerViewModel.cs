using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PixelArtEditor.Core.Interfaces;
using PixelArtEditor.Core.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Microsoft.Win32;
using SkiaSharp;

namespace PixelArtEditor.UI.ViewModels
{
    public partial class LayerViewModel : ObservableObject
    {
        private readonly ILayerService _layerService;
        private readonly IToolService _toolService;

        public ObservableCollection<Layer> Layers => _layerService.Layers;
        public IToolService ToolService => _toolService;

        public int CanvasWidth => _layerService.Width;
        public int CanvasHeight => _layerService.Height;

        [ObservableProperty]
        private int _newCanvasWidth;

        [ObservableProperty]
        private int _newCanvasHeight;

        public Layer ActiveLayer
        {
            get => _layerService.ActiveLayer;
            set
            {
                if (_layerService.ActiveLayer != value)
                {
                    _layerService.ActiveLayer = value;
                    OnPropertyChanged();
                }
            }
        }

        public LayerViewModel(ILayerService layerService, IToolService toolService)
        {
            _layerService = layerService;
            _toolService = toolService;

            _newCanvasWidth = _layerService.Width;
            _newCanvasHeight = _layerService.Height;
        }

        [RelayCommand]
        private void AddLayer()
        {
            _layerService.AddLayer($"Layer {Layers.Count + 1}");
        }

        [RelayCommand]
        private void RemoveLayer(Layer layer)
        {
            _layerService.RemoveLayer(layer);
        }

        [RelayCommand]
        private void DuplicateLayer(Layer layer)
        {
            _layerService.DuplicateLayer(layer);
        }

        [RelayCommand]
        private void MergeDown(Layer layer)
        {
            _layerService.MergeDown(layer);
        }
        
        [RelayCommand]
        private void ToggleVisibility(Layer layer)
        {
            if (layer != null)
            {
                layer.IsVisible = !layer.IsVisible;
            }
        }

        [RelayCommand]
        private void ResizeCanvas()
        {
            if (NewCanvasWidth <= 0 || NewCanvasHeight <= 0)
            {
                return;
            }

            _layerService.Resize(NewCanvasWidth, NewCanvasHeight);
            OnPropertyChanged(nameof(CanvasWidth));
            OnPropertyChanged(nameof(CanvasHeight));
        }

        [RelayCommand]
        private void ResizeCanvas16()
        {
            NewCanvasWidth = 16;
            NewCanvasHeight = 16;
            ResizeCanvas();
        }

        [RelayCommand]
        private void ResizeCanvas32()
        {
            NewCanvasWidth = 32;
            NewCanvasHeight = 32;
            ResizeCanvas();
        }

        [RelayCommand]
        private void ResizeCanvas64()
        {
            NewCanvasWidth = 64;
            NewCanvasHeight = 64;
            ResizeCanvas();
        }

        [RelayCommand]
        private void ImportImage()
        {
            var openDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp",
                CheckFileExists = true,
                Multiselect = false
            };

            if (openDialog.ShowDialog() != true)
            {
                return;
            }

            using var inputStream = File.OpenRead(openDialog.FileName);
            using var imported = SKBitmap.Decode(inputStream);
            if (imported == null)
            {
                return;
            }

            // Resize canvas if imported image is larger
            int targetWidth = System.Math.Max(_layerService.Width, imported.Width);
            int targetHeight = System.Math.Max(_layerService.Height, imported.Height);

            if (targetWidth != _layerService.Width || targetHeight != _layerService.Height)
            {
                _layerService.Resize(targetWidth, targetHeight);
                OnPropertyChanged(nameof(CanvasWidth));
                OnPropertyChanged(nameof(CanvasHeight));
                NewCanvasWidth = targetWidth;
                NewCanvasHeight = targetHeight;
            }

            var layer = _layerService.ActiveLayer;
            if (layer == null) return;

            using var canvas = new SKCanvas(layer.Bitmap);
            canvas.DrawBitmap(imported, 0, 0);
        }

        [RelayCommand]
        private void ExportCanvas()
        {
            var width = _layerService.Width;
            var height = _layerService.Height;

            if (width <= 0 || height <= 0)
            {
                return;
            }

            var saveDialog = new SaveFileDialog
            {
                Filter = "PNG Image|*.png",
                DefaultExt = "png",
                AddExtension = true,
                FileName = "pixel_art.png"
            };

            var result = saveDialog.ShowDialog();
            if (result != true || string.IsNullOrWhiteSpace(saveDialog.FileName))
            {
                return;
            }

            var info = new SKImageInfo(width, height, SKColorType.Rgba8888, SKAlphaType.Premul);
            using var surface = SKSurface.Create(info);
            var canvas = surface.Canvas;
            canvas.Clear(SKColors.Transparent);

            foreach (var layer in _layerService.Layers.Reverse())
            {
                if (!layer.IsVisible || layer.Opacity <= 0) continue;

                using var paint = new SKPaint
                {
                    FilterQuality = SKFilterQuality.None,
                    IsAntialias = false,
                    Color = SKColors.White.WithAlpha((byte)(layer.Opacity * 255)),
                    BlendMode = GetSkiaBlendMode(layer.BlendMode)
                };

                canvas.DrawBitmap(layer.Bitmap, 0, 0, paint);
            }

            using var image = surface.Snapshot();
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            using var stream = File.Open(saveDialog.FileName, FileMode.Create, FileAccess.Write);
            data.SaveTo(stream);
        }

        private static SKBlendMode GetSkiaBlendMode(BlendMode mode)
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
