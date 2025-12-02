using PixelArtEditor.UI.ViewModels;
using SkiaSharp.Views.Desktop;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PixelArtEditor.UI.Views
{
    public partial class CanvasView : UserControl
    {
        private bool _isPanning;
        private Point _lastMousePosition;

        public CanvasView()
        {
            InitializeComponent();
            this.DataContextChanged += OnDataContextChanged;
        }

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

        private void OnInvalidateRequest()
        {
            SkiaElement.InvalidateVisual();
        }

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            if (DataContext is CanvasViewModel viewModel)
            {
                viewModel.Render(e.Surface.Canvas);
            }
        }

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
