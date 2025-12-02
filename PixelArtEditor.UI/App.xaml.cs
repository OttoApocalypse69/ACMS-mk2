using Microsoft.Extensions.DependencyInjection;
using PixelArtEditor.UI.ViewModels;
using PixelArtEditor.Core.Services;
using PixelArtEditor.Core.Interfaces;
using System;
using System.Windows;

namespace PixelArtEditor.UI
{
    public partial class App : Application
    {
        public new static App Current => (App)Application.Current;
        public IServiceProvider Services { get; }

        public App()
        {
            Services = ConfigureServices();
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // Services
            services.AddSingleton<IHistoryService, HistoryService>();
            services.AddSingleton<IRenderer, CanvasRenderer>();
            services.AddSingleton<IToolService, ToolService>();
            services.AddSingleton<ILayerService>(provider => new LayerService(32, 32)); // Default size for now

            // ViewModels with explicit dependency resolution
            services.AddSingleton<ColorPaletteViewModel>();
            services.AddSingleton<ToolbarViewModel>(provider => new ToolbarViewModel(
                provider.GetRequiredService<IToolService>(),
                provider.GetRequiredService<IHistoryService>()
            ));
            services.AddSingleton<LayerViewModel>(provider => new LayerViewModel(
                provider.GetRequiredService<ILayerService>(),
                provider.GetRequiredService<IToolService>()
            ));
            services.AddSingleton<CanvasViewModel>(provider => new CanvasViewModel(
                provider.GetRequiredService<IRenderer>(),
                provider.GetRequiredService<IToolService>(),
                provider.GetRequiredService<ILayerService>(),
                provider.GetRequiredService<IHistoryService>()
            ));
            services.AddSingleton<MainViewModel>(provider => new MainViewModel(
                provider.GetRequiredService<CanvasViewModel>(),
                provider.GetRequiredService<ToolbarViewModel>(),
                provider.GetRequiredService<ColorPaletteViewModel>(),
                provider.GetRequiredService<LayerViewModel>()
            ));

            // Views
            services.AddSingleton<MainWindow>(provider => new MainWindow(
                provider.GetRequiredService<MainViewModel>()
            ));

            return services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this.DispatcherUnhandledException += App_DispatcherUnhandledException;

            try 
            {
                var mainWindow = Services.GetRequiredService<MainWindow>();
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException != null ? $"\n\nInner Exception: {ex.InnerException.Message}\n{ex.InnerException.StackTrace}" : "";
                MessageBox.Show($"Startup Error: {ex.Message}\n{ex.StackTrace}{innerException}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
            }
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"Unhandled Error: {e.Exception.Message}\n{e.Exception.StackTrace}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }
    }
}
