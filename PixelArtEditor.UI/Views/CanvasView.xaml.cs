using PixelArtEditor.UI.ViewModels;
using SkiaSharp.Views.Desktop;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PixelArtEditor.UI.Views
{
    /// <summary>
    /// Interaction logic for CanvasView.xaml
    /// This view is responsible for displaying the canvas and handling user input.
    /// </summary>
    public partial class CanvasView : UserControl
    {
        private bool _isPanning;
        private Point _lastMousePosition;

        /// <summary>
        /// Initializes a new instance of the <see cref="CanvasView"/> class.
        /// </summary>
        public CanvasView()
        {
            InitializeComponent();
            this.DataContextChanged += OnDataContextChanged;
        }

        /// <summary>
        /// Handles the DataContextChanged event.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is CanvasViewModel oldVm)
            {
                oldVm.InvalidateRequest -= OnInvalidateRequest;
            }
            if (e.NewValue is CanvasViewModel newVm)
            {
                newVm.InvalidateRequest += OnInvalidateRequest;
            }
        }

        /// <summary>
        /// Handles the InvalidateRequest event from the view model.
        /// </summary>
        private void OnInvalidateRequest()
        {
            SkiaElement.InvalidateVisual();
        }

        /// <summary>
        /// Handles the PaintSurface event of the SkiaElement.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            if (DataContext is CanvasViewModel viewModel)
            {
                viewModel.Render(e.Surface.Canvas);
            }
        }

        /// <summary>
        /// Handles the MouseDown event.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                _isPanning = true;
                _lastMousePosition = e.GetPosition(this);
                CaptureMouse();
            }
            else if (e.LeftButton == MouseButtonState.Pressed && DataContext is CanvasViewModel vm)
            {
                var pos = e.GetPosition(this);
                vm.OnMouseDown((float)pos.X, (float)pos.Y);
                CaptureMouse();
                SkiaElement.InvalidateVisual();
            }
        }

        /// <summary>
        /// Handles the MouseMove event.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!(DataContext is CanvasViewModel vm)) return;

            if (_isPanning)
            {
                var currentPosition = e.GetPosition(this);
                var delta = currentPosition - _lastMousePosition;
                
                vm.PanX += (float)delta.X;
                vm.PanY += (float)delta.Y;
                
                _lastMousePosition = currentPosition;
                SkiaElement.InvalidateVisual();
            }
            else if (e.LeftButton == MouseButtonState.Pressed)
            {
                var pos = e.GetPosition(this);
                vm.OnMouseMove((float)pos.X, (float)pos.Y);
                SkiaElement.InvalidateVisual();
            }
        }

        /// <summary>
        /// Handles the MouseUp event.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (_isPanning)
            {
                _isPanning = false;
                ReleaseMouseCapture();
            }
            else if (DataContext is CanvasViewModel vm)
            {
                var pos = e.GetPosition(this);
                vm.OnMouseUp((float)pos.X, (float)pos.Y);
                ReleaseMouseCapture();
                SkiaElement.InvalidateVisual();
            }
        }

        /// <summary>
        /// Handles the MouseWheel event.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (DataContext is CanvasViewModel vm)
            {
                float zoomFactor = 1.1f;
                if (e.Delta < 0)
                    vm.Zoom /= zoomFactor;
                else
                    vm.Zoom *= zoomFactor;
                
                SkiaElement.InvalidateVisual();
            }
        }
    }
}
