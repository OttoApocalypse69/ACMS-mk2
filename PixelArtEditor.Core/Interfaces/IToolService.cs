using PixelArtEditor.Core.Models;
using System.Collections.Generic;

namespace PixelArtEditor.Core.Interfaces
{
    /// <summary>
    /// Defines a service for managing the available tools and the active tool.
    /// </summary>
    public interface IToolService
    {
        /// <summary>
        /// Gets or sets the currently active tool.
        /// </summary>
        ITool ActiveTool { get; set; }

        /// <summary>
        /// Gets the collection of available tools.
        /// </summary>
        IEnumerable<ITool> AvailableTools { get; }

        /// <summary>
        /// Gets the history service, used for undoing and redoing tool actions.
        /// </summary>
        IHistoryService HistoryService { get; }

        /// <summary>
        /// Gets the symmetry settings, which define how tool actions are mirrored.
        /// </summary>
        SymmetrySettings SymmetrySettings { get; }

        /// <summary>
        /// Occurs when a color is picked from the canvas, for example, by the color picker tool.
        /// </summary>
        event System.Action<SkiaSharp.SKColor> ColorPicked;

        /// <summary>
        /// Triggers the ColorPicked event.
        /// </summary>
        /// <param name="color">The color that was picked.</param>
        void TriggerColorPicked(SkiaSharp.SKColor color);
    }
}
