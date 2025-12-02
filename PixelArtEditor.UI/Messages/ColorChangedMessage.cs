using CommunityToolkit.Mvvm.Messaging.Messages;
using SkiaSharp;

namespace PixelArtEditor.UI.Messages
{
    public class ColorChangedMessage : ValueChangedMessage<SKColor>
    {
        public ColorChangedMessage(SKColor value) : base(value)
        {
        }
    }
}
