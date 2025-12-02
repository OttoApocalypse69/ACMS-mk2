using PixelArtEditor.Core.Models;
using PixelArtEditor.UI.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace PixelArtEditor.UI.Views
{
    public partial class LayerPanelView : UserControl
    {
        public LayerPanelView()
        {
            InitializeComponent();
        }

        private void SymmetryNone_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is LayerViewModel vm && vm.ToolService != null)
            {
                vm.ToolService.SymmetrySettings.Mode = SymmetryMode.None;
            }
        }

        private void SymmetryHorizontal_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is LayerViewModel vm && vm.ToolService != null)
            {
                vm.ToolService.SymmetrySettings.Mode = SymmetryMode.Horizontal;
            }
        }

        private void SymmetryVertical_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is LayerViewModel vm && vm.ToolService != null)
            {
                vm.ToolService.SymmetrySettings.Mode = SymmetryMode.Vertical;
            }
        }

        private void SymmetryBoth_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is LayerViewModel vm && vm.ToolService != null)
            {
                vm.ToolService.SymmetrySettings.Mode = SymmetryMode.Both;
            }
        }
    }
}

