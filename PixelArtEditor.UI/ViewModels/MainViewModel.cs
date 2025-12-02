using CommunityToolkit.Mvvm.ComponentModel;

namespace PixelArtEditor.UI.ViewModels
{
    /// <summary>
    /// The main view model for the application, which holds references to all the other view models.
    /// </summary>
    public partial class MainViewModel : ObservableObject
    {
        /// <summary>
        /// Gets or sets the view model for the canvas.
        /// </summary>
        [ObservableProperty]
        private CanvasViewModel _canvasViewModel;

        /// <summary>
        /// Gets or sets the view model for the toolbar.
        /// </summary>
        [ObservableProperty]
        private ToolbarViewModel _toolbarViewModel;

        /// <summary>
        /// Gets or sets the view model for the color palette.
        /// </summary>
        [ObservableProperty]
        private ColorPaletteViewModel _colorPaletteViewModel;

        /// <summary>
        /// Gets or sets the view model for the layer view.
        /// </summary>
        [ObservableProperty]
        private LayerViewModel _layerViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="canvasViewModel">The view model for the canvas.</param>
        /// <param name="toolbarViewModel">The view model for the toolbar.</param>
        /// <param name="colorPaletteViewModel">The view model for the color palette.</param>
        /// <param name="layerViewModel">The view model for the layer view.</param>
        public MainViewModel(CanvasViewModel canvasViewModel, ToolbarViewModel toolbarViewModel, ColorPaletteViewModel colorPaletteViewModel, LayerViewModel layerViewModel)
        {
            CanvasViewModel = canvasViewModel;
            ToolbarViewModel = toolbarViewModel;
            ColorPaletteViewModel = colorPaletteViewModel;
            LayerViewModel = layerViewModel;
        }
    }
}
