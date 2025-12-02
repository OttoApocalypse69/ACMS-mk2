using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using PixelArtEditor.Core.Interfaces;
using PixelArtEditor.Core.Models;
using PixelArtEditor.UI.Messages;
using SkiaSharp;

namespace PixelArtEditor.UI.ViewModels
{
    public partial class CanvasViewModel : ObservableObject
    {
        private readonly IRenderer _renderer;
        private readonly IToolService _toolService;
        private readonly ILayerService _layerService;
        private readonly IHistoryService _historyService;
        
        public event System.Action InvalidateRequest;

        [ObservableProperty]
        private float _zoom = 10.0f; // Start with some zoom

        [ObservableProperty]
        private float _panX = 100.0f;

        [ObservableProperty]
        private float _panY = 100.0f;

        [ObservableProperty]
        private SKColor _activeColor = SKColors.Black;

        public CanvasViewModel(IRenderer renderer, IToolService toolService, ILayerService layerService, IHistoryService historyService)
        {
            _renderer = renderer;
            _toolService = toolService;
            _layerService = layerService;
            _historyService = historyService;
            
            _historyService.HistoryChanged += () =>
            {
                InvalidateRequest?.Invoke();
            };
            
            // Subscribe to layer changes (add/remove)
            _layerService.Layers.CollectionChanged += (s, e) =>
            {
                // When layers are added, subscribe to their PropertyChanged
                if (e.NewItems != null)
                {
                    foreach (Layer layer in e.NewItems)
                    {
                        layer.PropertyChanged += OnLayerPropertyChanged;
                    }
                }
                
                // When layers are removed, unsubscribe
                if (e.OldItems != null)
                {
                    foreach (Layer layer in e.OldItems)
                    {
                        layer.PropertyChanged -= OnLayerPropertyChanged;
                    }
                }
                
                InvalidateRequest?.Invoke();
            };
            
            // Subscribe to existing layers
            foreach (var layer in _layerService.Layers)
            {
                layer.PropertyChanged += OnLayerPropertyChanged;
            }
            
            // Draw something for testing on the initial layer
            if (_layerService.ActiveLayer != null)
            {
                var canvas = _layerService.ActiveLayer.Bitmap;
                canvas.SetPixel(0, 0, SKColors.Red);
                canvas.SetPixel(1, 1, SKColors.Green);
                canvas.SetPixel(2, 2, SKColors.Blue);
                canvas.SetPixel(31, 31, SKColors.White);
                
                // Draw a border
                for(int i=0; i<32; i++) {
                    canvas.SetPixel(i, 0, SKColors.Yellow);
                    canvas.SetPixel(i, 31, SKColors.Yellow);
                    canvas.SetPixel(0, i, SKColors.Yellow);
                    canvas.SetPixel(31, i, SKColors.Yellow);
                }
            }

            WeakReferenceMessenger.Default.Register<ColorChangedMessage>(this, (r, m) => 
            {
                ActiveColor = m.Value;
            });

            _toolService.ColorPicked += (color) =>
            {
                ActiveColor = color;
                // Also notify others if needed, e.g. ColorPalette
                WeakReferenceMessenger.Default.Send(new ColorChangedMessage(color));
            };
        }

        private void OnLayerPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // When any layer property changes (Opacity, IsVisible, BlendMode, etc.), redraw
            InvalidateRequest?.Invoke();
        }

        public void Render(SKCanvas canvas)
        {
            _renderer.Render(canvas, _layerService.Layers, Zoom, PanX, PanY);
        }

        public void OnMouseDown(float x, float y)
        {
            int canvasX = (int)((x - PanX) / Zoom);
            int canvasY = (int)((y - PanY) / Zoom);
            
            _toolService.ActiveTool?.OnMouseDown(_layerService.ActiveLayer, canvasX, canvasY, ActiveColor);
        }

        public void OnMouseMove(float x, float y)
        {
            int canvasX = (int)((x - PanX) / Zoom);
            int canvasY = (int)((y - PanY) / Zoom);
            
            _toolService.ActiveTool?.OnMouseMove(_layerService.ActiveLayer, canvasX, canvasY, ActiveColor);
        }

        public void OnMouseUp(float x, float y)
        {
            int canvasX = (int)((x - PanX) / Zoom);
            int canvasY = (int)((y - PanY) / Zoom);
            
            _toolService.ActiveTool?.OnMouseUp(_layerService.ActiveLayer, canvasX, canvasY, ActiveColor);
        }
    }
}
