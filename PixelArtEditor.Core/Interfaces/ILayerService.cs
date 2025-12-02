using System.Collections.ObjectModel;
using PixelArtEditor.Core.Models;

namespace PixelArtEditor.Core.Interfaces
{
    public interface ILayerService
    {
        int Width { get; }
        int Height { get; }

        ObservableCollection<Layer> Layers { get; }
        Layer ActiveLayer { get; set; }
        
        void AddLayer(string name);
        void RemoveLayer(Layer layer);
        void MoveLayer(int oldIndex, int newIndex);
        void MergeDown(Layer layer);
        void DuplicateLayer(Layer layer);
        void ClearLayer(Layer layer);
        void Resize(int width, int height);
    }
}
