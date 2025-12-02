using PixelArtEditor.Core.Interfaces;
using PixelArtEditor.Core.Models;
using PixelArtEditor.Core.Tools;
using System.Collections.Generic;
using System.Linq;

namespace PixelArtEditor.Core.Services
{
    public class ToolService : IToolService
    {
        public ITool ActiveTool { get; set; }
        public IEnumerable<ITool> AvailableTools { get; }
        public IHistoryService HistoryService { get; }
        public SymmetrySettings SymmetrySettings { get; }
        
        public event System.Action<SkiaSharp.SKColor> ColorPicked;

        public ToolService(IHistoryService historyService)
        {
            HistoryService = historyService;
            SymmetrySettings = new SymmetrySettings();
            
            AvailableTools = new List<ITool>
            {
                new PencilTool(historyService, SymmetrySettings),
                new EraserTool(historyService),
                new EyedropperTool(TriggerColorPicked),
                new FillTool(historyService),
                new RectangleSelectTool()
            };
            
            // Default tool
            ActiveTool = AvailableTools.First();
        }

        public void TriggerColorPicked(SkiaSharp.SKColor color)
        {
            ColorPicked?.Invoke(color);
        }
    }
}
