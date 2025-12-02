using CommunityToolkit.Mvvm.ComponentModel;

namespace PixelArtEditor.UI.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private CanvasViewModel _canvasViewModel;

        [ObservableProperty]
        private ToolbarViewModel _toolbarViewModel;

        [ObservableProperty]
        private ColorPaletteViewModel _colorPaletteViewModel;

        [ObservableProperty]
        private LayerViewModel _layerViewModel;

        public MainViewModel(CanvasViewModel canvasViewModel, ToolbarViewModel toolbarViewModel, ColorPaletteViewModel colorPaletteViewModel, LayerViewModel layerViewModel)
        {
            CanvasViewModel = canvasViewModel;
            ToolbarViewModel = toolbarViewModel;
            ColorPaletteViewModel = colorPaletteViewModel;
            LayerViewModel = layerViewModel;
        }
    }
}
