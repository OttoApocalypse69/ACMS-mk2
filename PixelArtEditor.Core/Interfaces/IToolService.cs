using PixelArtEditor.Core.Interfaces;
using PixelArtEditor.Core.Models;
using System.Collections.Generic;

namespace PixelArtEditor.Core.Interfaces // Changed namespace to match ITool
{
    public interface IToolService
    {
        ITool ActiveTool { get; set; }
        IEnumerable<ITool> AvailableTools { get; }
        IHistoryService HistoryService { get; }
        SymmetrySettings SymmetrySettings { get; }
        
        event System.Action<SkiaSharp.SKColor> ColorPicked;
        void TriggerColorPicked(SkiaSharp.SKColor color);
    }
}
