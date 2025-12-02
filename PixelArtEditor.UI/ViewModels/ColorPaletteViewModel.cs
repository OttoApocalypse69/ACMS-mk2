using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using PixelArtEditor.UI.Messages;
using SkiaSharp;
using System.Collections.ObjectModel;

namespace PixelArtEditor.UI.ViewModels
{
    /// <summary>
    /// The view model for the color palette, which manages the available colors and the selected color.
    /// </summary>
    public partial class ColorPaletteViewModel : ObservableObject
    {
        /// <summary>
        /// Gets or sets the currently selected color.
        /// </summary>
        [ObservableProperty]
        private SKColor _selectedColor;
        
        /// <summary>
        /// Gets the collection of available colors in the palette.
        /// </summary>
        public ObservableCollection<SKColor> Colors { get; } = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorPaletteViewModel"/> class.
        /// </summary>
        public ColorPaletteViewModel()
        {
            Colors.Add(SKColors.Black);
            Colors.Add(new SKColor(34, 34, 34));
            Colors.Add(new SKColor(68, 68, 68));
            Colors.Add(new SKColor(102, 102, 102));
            Colors.Add(new SKColor(136, 136, 136));
            Colors.Add(new SKColor(170, 170, 170));
            Colors.Add(new SKColor(204, 204, 204));
            Colors.Add(SKColors.White);
            
            Colors.Add(new SKColor(255, 0, 0));
            Colors.Add(new SKColor(0, 255, 0));
            Colors.Add(new SKColor(0, 0, 255));
            
            Colors.Add(new SKColor(255, 255, 0));
            Colors.Add(new SKColor(0, 255, 255));
            Colors.Add(new SKColor(255, 0, 255));
            
            Colors.Add(new SKColor(139, 0, 0));
            Colors.Add(new SKColor(220, 20, 60));
            Colors.Add(new SKColor(255, 99, 71));
            Colors.Add(new SKColor(255, 127, 80));
            Colors.Add(new SKColor(255, 182, 193));
            Colors.Add(new SKColor(255, 105, 180));
            
            Colors.Add(new SKColor(255, 140, 0));
            Colors.Add(new SKColor(255, 165, 0));
            Colors.Add(new SKColor(255, 215, 0));
            Colors.Add(new SKColor(139, 69, 19));
            Colors.Add(new SKColor(160, 82, 45));
            Colors.Add(new SKColor(210, 105, 30));
            
            Colors.Add(new SKColor(255, 255, 224));
            Colors.Add(new SKColor(189, 183, 107));
            Colors.Add(new SKColor(154, 205, 50));
            Colors.Add(new SKColor(124, 252, 0));
            Colors.Add(new SKColor(0, 255, 127));
            Colors.Add(new SKColor(0, 128, 0));
            Colors.Add(new SKColor(34, 139, 34));
            Colors.Add(new SKColor(144, 238, 144));
            
            Colors.Add(new SKColor(0, 139, 139));
            Colors.Add(new SKColor(72, 209, 204));
            Colors.Add(new SKColor(175, 238, 238));
            Colors.Add(new SKColor(0, 0, 139));
            Colors.Add(new SKColor(0, 0, 205));
            Colors.Add(new SKColor(30, 144, 255));
            Colors.Add(new SKColor(135, 206, 235));
            Colors.Add(new SKColor(173, 216, 230));
            
            Colors.Add(new SKColor(75, 0, 130));
            Colors.Add(new SKColor(138, 43, 226));
            Colors.Add(new SKColor(147, 112, 219));
            Colors.Add(new SKColor(186, 85, 211));
            Colors.Add(new SKColor(218, 112, 214));
            Colors.Add(new SKColor(221, 160, 221));
            Colors.Add(new SKColor(238, 130, 238));
            
            Colors.Add(new SKColor(255, 228, 225));
            Colors.Add(new SKColor(255, 239, 213));
            Colors.Add(new SKColor(255, 250, 205));
            Colors.Add(new SKColor(240, 255, 240));
            Colors.Add(new SKColor(240, 248, 255));
            Colors.Add(new SKColor(230, 230, 250));
            
            SelectedColor = SKColors.Black;
        }

        /// <summary>
        /// This method is called when the SelectedColor property changes.
        /// It sends a ColorChangedMessage to notify other view models of the change.
        /// </summary>
        /// <param name="value">The new selected color.</param>
        partial void OnSelectedColorChanged(SKColor value)
        {
            WeakReferenceMessenger.Default.Send(new ColorChangedMessage(value));
        }
    }
}
