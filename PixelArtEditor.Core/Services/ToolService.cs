using PixelArtEditor.Core.Interfaces;
using PixelArtEditor.Core.Models;
using PixelArtEditor.Core.Tools;
using System.Collections.Generic;
using System.Linq;

namespace PixelArtEditor.Core.Services
{
    /// <summary>
    /// Manages the available tools and the active tool.
    /// This class implements the IToolService interface.
    /// </summary>
    public class ToolService : IToolService
    {
        /// <summary>
        /// Gets or sets the currently active tool.
        /// </summary>
        public ITool ActiveTool { get; set; }

        /// <summary>
        /// Gets the collection of available tools.
        /// </summary>
        public IEnumerable<ITool> AvailableTools { get; }

        /// <summary>
        /// Gets the history service, used for undoing and redoing tool actions.
        /// </summary>
        public IHistoryService HistoryService { get; }

        /// <summary>
        /// Gets the symmetry settings, which define how tool actions are mirrored.
        /// </summary>
        public SymmetrySettings SymmetrySettings { get; }
        
        /// <summary>
        /// Occurs when a color is picked from the canvas, for example, by the color picker tool.
        /// </summary>
        public event System.Action<SkiaSharp.SKColor> ColorPicked;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToolService"/> class.
        /// </summary>
        /// <param name="historyService">The history service to use for undo/redo.</param>
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
            
            ActiveTool = AvailableTools.First();
        }

        /// <summary>
        /// Triggers the ColorPicked event.
        /// </summary>
        /// <param name="color">The color that was picked.</param>
        public void TriggerColorPicked(SkiaSharp.SKColor color)
        {
            ColorPicked?.Invoke(color);
        }
    }
}
