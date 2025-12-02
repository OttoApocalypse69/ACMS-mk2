using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PixelArtEditor.Core.Interfaces;
using PixelArtEditor.Core.Models;
using System.Collections.ObjectModel;

namespace PixelArtEditor.UI.ViewModels
{
    /// <summary>
    /// The view model for the layer view, which manages the collection of layers and provides commands for layer operations.
    /// </summary>
    public partial class LayerViewModel : ObservableObject
    {
        private readonly ILayerService _layerService;
        private readonly IToolService _toolService;

        /// <summary>
        /// Gets the collection of layers.
        /// </summary>
        public ObservableCollection<Layer> Layers => _layerService.Layers;

        /// <summary>
        /// Gets the tool service.
        /// </summary>
        public IToolService ToolService => _toolService;

        /// <summary>
        /// Gets or sets the currently active layer.
        /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="LayerViewModel"/> class.
        /// </summary>
        /// <param name="layerService">The service for managing layers.</param>
        /// <param name="toolService">The service for managing tools.</param>
        public LayerViewModel(ILayerService layerService, IToolService toolService)
        {
            _layerService = layerService;
            _toolService = toolService;
        }

        /// <summary>
        /// A command to add a new layer.
        /// </summary>
        [RelayCommand]
        private void AddLayer()
        {
            _layerService.AddLayer($"Layer {Layers.Count + 1}");
        }

        /// <summary>
        /// A command to remove a layer.
        /// </summary>
        /// <param name="layer">The layer to remove.</param>
        [RelayCommand]
        private void RemoveLayer(Layer layer)
        {
            _layerService.RemoveLayer(layer);
        }

        /// <summary>
        /// A command to duplicate a layer.
        /// </summary>
        /// <param name="layer">The layer to duplicate.</param>
        [RelayCommand]
        private void DuplicateLayer(Layer layer)
        {
            _layerService.DuplicateLayer(layer);
        }

        /// <summary>
        /// A command to merge a layer with the one below it.
        /// </summary>
        /// <param name="layer">The layer to merge down.</param>
        [RelayCommand]
        private void MergeDown(Layer layer)
        {
            _layerService.MergeDown(layer);
        }
        
        /// <summary>
        /// A command to toggle the visibility of a layer.
        /// </summary>
        /// <param name="layer">The layer to toggle the visibility of.</param>
        [RelayCommand]
        private void ToggleVisibility(Layer layer)
        {
            if (layer != null)
            {
                layer.IsVisible = !layer.IsVisible;
            }
        }
    }
}
