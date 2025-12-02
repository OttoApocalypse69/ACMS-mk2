using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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

        // File menu commands (placeholders for now, can be wired to real file I/O later)
        [RelayCommand]
        private void NewDocument()
        {
            // TODO: Implement proper "New" behavior (clear layers / reset state)
        }

        [RelayCommand]
        private void OpenDocument()
        {
            // TODO: Implement "Open" behavior
        }

        [RelayCommand]
        private void SaveDocument()
        {
            // TODO: Implement "Save" behavior
        }

        [RelayCommand]
        private void ImportImage()
        {
            // TODO: Implement "Import" behavior
        }

        [RelayCommand]
        private void ExitApplication()
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
