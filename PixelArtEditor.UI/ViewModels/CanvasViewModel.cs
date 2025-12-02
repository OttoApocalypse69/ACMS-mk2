using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using PixelArtEditor.Core.Interfaces;
using PixelArtEditor.Core.Models;
using PixelArtEditor.UI.Messages;
using SkiaSharp;

namespace PixelArtEditor.UI.ViewModels
{
    /// <summary>
    /// The view model for the canvas, which handles rendering and user input.
    /// This class is responsible for managing the canvas's zoom, pan, and active color.
    /// </summary>
    public partial class CanvasViewModel : ObservableObject
    {
        private readonly IRenderer _renderer;
        private readonly IToolService _toolService;
        private readonly ILayerService _layerService;
        private readonly IHistoryService _historyService;
        
        /// <summary>
        /// Occurs when the canvas needs to be redrawn.
        /// </summary>
        public event System.Action InvalidateRequest;

        /// <summary>
        /// Gets or sets the zoom level of the canvas.
        /// </summary>
        [ObservableProperty]
        private float _zoom = 10.0f;

        /// <summary>
        /// Gets or sets the horizontal pan offset of the canvas.
        /// </summary>
        [ObservableProperty]
        private float _panX = 100.0f;

        /// <summary>
        /// Gets or sets the vertical pan offset of the canvas.
        /// </summary>
        [ObservableProperty]
        private float _panY = 100.0f;

        /// <summary>
        /// Gets or sets the currently active drawing color.
        /// </summary>
        [ObservableProperty]
        private SKColor _activeColor = SKColors.Black;

        /// <summary>
        /// Initializes a new instance of the <see cref="CanvasViewModel"/> class.
        /// </summary>
        /// <param name="renderer">The renderer to use for drawing the canvas.</param>
        /// <param name="toolService">The service for managing tools.</param>
        /// <param name="layerService">The service for managing layers.</param>
        /// <param name="historyService">The service for managing undo/redo history.</param>
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
            
            _layerService.Layers.CollectionChanged += (s, e) =>
            {
                if (e.NewItems != null)
                {
                    foreach (Layer layer in e.NewItems)
                    {
                        layer.PropertyChanged += OnLayerPropertyChanged;
                    }
                }
                
                if (e.OldItems != null)
                {
                    foreach (Layer layer in e.OldItems)
                    {
                        layer.PropertyChanged -= OnLayerPropertyChanged;
                    }
                }
                
                InvalidateRequest?.Invoke();
            };
            
            foreach (var layer in _layerService.Layers)
            {
                layer.PropertyChanged += OnLayerPropertyChanged;
            }
            
            if (_layerService.ActiveLayer != null)
            {
                var canvas = _layerService.ActiveLayer.Bitmap;
                canvas.SetPixel(0, 0, SKColors.Red);
                canvas.SetPixel(1, 1, SKColors.Green);
                canvas.SetPixel(2, 2, SKColors.Blue);
                canvas.SetPixel(31, 31, SKColors.White);
                
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
                WeakReferenceMessenger.Default.Send(new ColorChangedMessage(color));
            };
        }

        /// <summary>
        /// Handles the PropertyChanged event for a layer.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnLayerPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            InvalidateRequest?.Invoke();
        }

        /// <summary>
        /// Renders the canvas.
        /// </summary>
        /// <param name="canvas">The SkiaSharp canvas to render on.</param>
        public void Render(SKCanvas canvas)
        {
            _renderer.Render(canvas, _layerService.Layers, Zoom, PanX, PanY);
        }

        /// <summary>
        /// Handles the mouse down event on the canvas.
        /// </summary>
        /// <param name="x">The x-coordinate of the mouse.</param>
        /// <param name="y">The y-coordinate of the mouse.</param>
        public void OnMouseDown(float x, float y)
        {
            int canvasX = (int)((x - PanX) / Zoom);
            int canvasY = (int)((y - PanY) / Zoom);
            
            _toolService.ActiveTool?.OnMouseDown(_layerService.ActiveLayer, canvasX, canvasY, ActiveColor);
        }

        /// <summary>
        /// Handles the mouse move event on the canvas.
        /// </summary>
        /// <param name="x">The x-coordinate of the mouse.</param>
        /// <param name="y">The y-coordinate of the mouse.</param>
        public void OnMouseMove(float x, float y)
        {
            int canvasX = (int)((x - PanX) / Zoom);
            int canvasY = (int)((y - PanY) / Zoom);
            
            _toolService.ActiveTool?.OnMouseMove(_layerService.ActiveLayer, canvasX, canvasY, ActiveColor);
        }

        /// <summary>
        /// Handles the mouse up event on the canvas.
        /// </summary>
        /// <param name="x">The x-coordinate of the mouse.</param>
        /// <param name="y">The y-coordinate of the mouse.</param>
        public void OnMouseUp(float x, float y)
        {
            int canvasX = (int)((x - PanX) / Zoom);
            int canvasY = (int)((y - PanY) / Zoom);
            
            _toolService.ActiveTool?.OnMouseUp(_layerService.ActiveLayer, canvasX, canvasY, ActiveColor);
        }
    }
}
