using CommunityToolkit.Mvvm.Messaging.Messages;
using SkiaSharp;

namespace PixelArtEditor.UI.Messages
{
    /// <summary>
    /// A message that is sent when the selected color changes.
    /// This message is used to communicate color changes between different view models.
    /// </summary>
    public class ColorChangedMessage : ValueChangedMessage<SKColor>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorChangedMessage"/> class.
        /// </summary>
        /// <param name="value">The new color.</param>
        public ColorChangedMessage(SKColor value) : base(value)
        {
        }
    }
}
