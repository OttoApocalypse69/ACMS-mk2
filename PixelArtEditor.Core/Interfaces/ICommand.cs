namespace PixelArtEditor.Core.Interfaces
{
    public interface ICommand
    {
        string Name { get; }
        void Execute();
        void Undo();
    }
}
