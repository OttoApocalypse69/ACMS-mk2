using System;

namespace PixelArtEditor.Core.Interfaces
{
    public interface IHistoryService
    {
        bool CanUndo { get; }
        bool CanRedo { get; }
        event Action HistoryChanged;

        void Execute(ICommand command);
        void Push(ICommand command); // For commands that are already executed (like tool strokes)
        void Undo();
        void Redo();
    }
}
