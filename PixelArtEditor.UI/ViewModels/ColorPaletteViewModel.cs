using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using PixelArtEditor.UI.Messages;
using SkiaSharp;
using System.Collections.ObjectModel;

namespace PixelArtEditor.UI.ViewModels
{
    public partial class ColorPaletteViewModel : ObservableObject
    {
        [ObservableProperty]
        private SKColor _selectedColor;
        
        public ObservableCollection<SKColor> Colors { get; } = new();

        public ColorPaletteViewModel()
        {
            // Grayscale
            Colors.Add(SKColors.Black);
            Colors.Add(new SKColor(34, 34, 34));    // Dark Gray
            Colors.Add(new SKColor(68, 68, 68));    // Gray
            Colors.Add(new SKColor(102, 102, 102)); // Medium Gray
            Colors.Add(new SKColor(136, 136, 136)); // Light Gray
            Colors.Add(new SKColor(170, 170, 170)); // Lighter Gray
            Colors.Add(new SKColor(204, 204, 204)); // Very Light Gray
            Colors.Add(SKColors.White);
            
            // Primary Colors
            Colors.Add(new SKColor(255, 0, 0));     // Red
            Colors.Add(new SKColor(0, 255, 0));     // Green
            Colors.Add(new SKColor(0, 0, 255));     // Blue
            
            // Secondary Colors
            Colors.Add(new SKColor(255, 255, 0));   // Yellow
            Colors.Add(new SKColor(0, 255, 255));   // Cyan
            Colors.Add(new SKColor(255, 0, 255));   // Magenta
            
            // Reds & Pinks
            Colors.Add(new SKColor(139, 0, 0));     // Dark Red
            Colors.Add(new SKColor(220, 20, 60));   // Crimson
            Colors.Add(new SKColor(255, 99, 71));   // Tomato
            Colors.Add(new SKColor(255, 127, 80));  // Coral
            Colors.Add(new SKColor(255, 182, 193)); // Light Pink
            Colors.Add(new SKColor(255, 105, 180)); // Hot Pink
            
            // Oranges & Browns
            Colors.Add(new SKColor(255, 140, 0));   // Dark Orange
            Colors.Add(new SKColor(255, 165, 0));   // Orange
            Colors.Add(new SKColor(255, 215, 0));   // Gold
            Colors.Add(new SKColor(139, 69, 19));   // Saddle Brown
            Colors.Add(new SKColor(160, 82, 45));   // Sienna
            Colors.Add(new SKColor(210, 105, 30));  // Chocolate
            
            // Yellows & Greens
            Colors.Add(new SKColor(255, 255, 224)); // Light Yellow
            Colors.Add(new SKColor(189, 183, 107)); // Dark Khaki
            Colors.Add(new SKColor(154, 205, 50));  // Yellow Green
            Colors.Add(new SKColor(124, 252, 0));   // Lawn Green
            Colors.Add(new SKColor(0, 255, 127));   // Spring Green
            Colors.Add(new SKColor(0, 128, 0));     // Dark Green
            Colors.Add(new SKColor(34, 139, 34));   // Forest Green
            Colors.Add(new SKColor(144, 238, 144)); // Light Green
            
            // Cyans & Blues
            Colors.Add(new SKColor(0, 139, 139));   // Dark Cyan
            Colors.Add(new SKColor(72, 209, 204));  // Medium Turquoise
            Colors.Add(new SKColor(175, 238, 238)); // Pale Turquoise
            Colors.Add(new SKColor(0, 0, 139));     // Dark Blue
            Colors.Add(new SKColor(0, 0, 205));     // Medium Blue
            Colors.Add(new SKColor(30, 144, 255));  // Dodger Blue
            Colors.Add(new SKColor(135, 206, 235)); // Sky Blue
            Colors.Add(new SKColor(173, 216, 230)); // Light Blue
            
            // Purples & Magentas
            Colors.Add(new SKColor(75, 0, 130));    // Indigo
            Colors.Add(new SKColor(138, 43, 226));  // Blue Violet
            Colors.Add(new SKColor(147, 112, 219)); // Medium Purple
            Colors.Add(new SKColor(186, 85, 211));  // Medium Orchid
            Colors.Add(new SKColor(218, 112, 214)); // Orchid
            Colors.Add(new SKColor(221, 160, 221)); // Plum
            Colors.Add(new SKColor(238, 130, 238)); // Violet
            
            // Pastels
            Colors.Add(new SKColor(255, 228, 225)); // Misty Rose
            Colors.Add(new SKColor(255, 239, 213)); // Papaya Whip
            Colors.Add(new SKColor(255, 250, 205)); // Lemon Chiffon
            Colors.Add(new SKColor(240, 255, 240)); // Honeydew
            Colors.Add(new SKColor(240, 248, 255)); // Alice Blue
            Colors.Add(new SKColor(230, 230, 250)); // Lavender
            
            SelectedColor = SKColors.Black;
        }

        partial void OnSelectedColorChanged(SKColor value)
        {
            WeakReferenceMessenger.Default.Send(new ColorChangedMessage(value));
        }
    }
}
