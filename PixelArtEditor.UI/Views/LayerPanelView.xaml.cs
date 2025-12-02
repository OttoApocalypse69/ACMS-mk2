using PixelArtEditor.Core.Models;
using PixelArtEditor.UI.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace PixelArtEditor.UI.Views
{
    /// <summary>
    /// Interaction logic for LayerPanelView.xaml
    /// This view displays the list of layers and provides controls for managing them.
    /// </summary>
    public partial class LayerPanelView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LayerPanelView"/> class.
        /// </summary>
        public LayerPanelView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the SymmetryNone button.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void SymmetryNone_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is LayerViewModel vm && vm.ToolService != null)
            {
                vm.ToolService.SymmetrySettings.Mode = SymmetryMode.None;
            }
        }

        /// <summary>
        /// Handles the Click event of the SymmetryHorizontal button.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void SymmetryHorizontal_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is LayerViewModel vm && vm.ToolService != null)
            {
                vm.ToolService.SymmetrySettings.Mode = SymmetryMode.Horizontal;
            }
        }

        /// <summary>
        /// Handles the Click event of the SymmetryVertical button.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void SymmetryVertical_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is LayerViewModel vm && vm.ToolService != null)
            {
                vm.ToolService.SymmetrySettings.Mode = SymmetryMode.Vertical;
            }
        }

        /// <summary>
        /// Handles the Click event of the SymmetryBoth button.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void SymmetryBoth_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is LayerViewModel vm && vm.ToolService != null)
            {
                vm.ToolService.SymmetrySettings.Mode = SymmetryMode.Both;
            }
        }
    }
}
