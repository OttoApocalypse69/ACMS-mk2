namespace PixelArtEditor.Core.Interfaces
{
    /// <summary>
    /// Defines a command that can be executed and undone.
    /// This is used to implement the undo/redo functionality.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Gets the name of the command.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Executes the command.
        /// </summary>
        void Execute();

        /// <summary>
        /// Undoes the command.
        /// </summary>
        void Undo();
    }
}
