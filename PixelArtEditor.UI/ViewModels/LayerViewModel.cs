using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PixelArtEditor.Core.Interfaces;
using PixelArtEditor.Core.Models;
using System.Collections.ObjectModel;

namespace PixelArtEditor.UI.ViewModels
{
    public partial class LayerViewModel : ObservableObject
    {
        private readonly ILayerService _layerService;
        private readonly IToolService _toolService;

        public ObservableCollection<Layer> Layers => _layerService.Layers;
        public IToolService ToolService => _toolService;

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
    }
}
