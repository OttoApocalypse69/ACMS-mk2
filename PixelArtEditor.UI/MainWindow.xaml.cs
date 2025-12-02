using PixelArtEditor.UI.ViewModels;
using System.Windows;

namespace PixelArtEditor.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}