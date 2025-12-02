using PixelArtEditor.UI.ViewModels;
using System.Windows;

namespace PixelArtEditor.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// This is the main window of the application.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        /// <param name="viewModel">The main view model for the application.</param>
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
