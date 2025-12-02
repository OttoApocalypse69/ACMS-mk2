using System.Collections.ObjectModel;
using PixelArtEditor.Core.Models;

namespace PixelArtEditor.Core.Interfaces
{
    /// <summary>
    /// Defines a service for managing the layers in the pixel art image.
    /// This service provides functionalities for adding, removing, and manipulating layers.
    /// </summary>
    public interface ILayerService
    {
        /// <summary>
        /// Gets the collection of layers in the image.
        /// </summary>
        ObservableCollection<Layer> Layers { get; }

        /// <summary>
        /// Gets or sets the currently active layer.
        /// </summary>
        Layer ActiveLayer { get; set; }

        /// <summary>
        -        /// Adds a new layer with the specified name.
        +        /// Adds a new layer with the specified name and the same dimensions as the canvas.
        /// </summary>
        /// <param name="name">The name of the new layer.</param>
        void AddLayer(string name);

        /// <summary>
        /// Removes the specified layer.
        /// </summary>
        /// <param name="layer">The layer to remove.</param>
        void RemoveLayer(Layer layer);

        /// <summary>
        /// Moves a layer from one position to another in the layer stack.
        /// </summary>
        /// <param name="oldIndex">The original index of the layer.</param>
        /// <param name="newIndex">The new index for the layer.</param>
        void MoveLayer(int oldIndex, int newIndex);

        /// <summary>
        /// Merges the specified layer with the layer below it.
        /// </summary>
        /// <param name="layer">The layer to merge down.</param>
        void MergeDown(Layer layer);

        /// <summary>
        /// Creates a duplicate of the specified layer.
        /// </summary>
        /// <param name="layer">The layer to duplicate.</param>
        void DuplicateLayer(Layer layer);

        /// <summary>
        /// Clears the content of the specified layer.
        /// </summary>
        /// <param name="layer">The layer to clear.</param>
        void ClearLayer(Layer layer);
    }
}
