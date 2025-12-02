using Microsoft.Extensions.DependencyInjection;
using PixelArtEditor.UI.ViewModels;
using PixelArtEditor.Core.Services;
using PixelArtEditor.Core.Interfaces;
using System;
using System.Windows;

namespace PixelArtEditor.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// This is the main application class, responsible for configuring services and starting the application.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Gets the current instance of the application.
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Gets the service provider for the application.
        /// </summary>
        public IServiceProvider Services { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            Services = ConfigureServices();
        }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        /// <returns>The configured service provider.</returns>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IHistoryService, HistoryService>();
            services.AddSingleton<IRenderer, CanvasRenderer>();
            services.AddSingleton<IToolService, ToolService>();
            services.AddSingleton<ILayerService>(provider => new LayerService(32, 32));

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

            services.AddSingleton<MainWindow>(provider => new MainWindow(
                provider.GetRequiredService<MainViewModel>()
            ));

            return services.BuildServiceProvider();
        }

        /// <summary>
        /// Handles the Startup event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
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

        /// <summary>
        /// Handles unhandled exceptions.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"Unhandled Error: {e.Exception.Message}\n{e.Exception.StackTrace}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }
    }
}
