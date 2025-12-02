using System;

namespace PixelArtEditor.Core.Interfaces
{
    /// <summary>
    /// Defines a service for managing the command history.
    /// This service allows for executing, undoing, and redoing commands.
    /// </summary>
    public interface IHistoryService
    {
        /// <summary>
        /// Gets a value indicating whether an undo operation can be performed.
        /// </summary>
        bool CanUndo { get; }

        /// <summary>
        /// Gets a value indicating whether a redo operation can be performed.
        /// </summary>
        bool CanRedo { get; }

        /// <summary>
        /// Occurs when the command history has changed.
        /// </summary>
        event Action HistoryChanged;

        /// <summary>
        /// Executes a command and adds it to the history.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        void Execute(ICommand command);

        /// <summary>
        /// Pushes a command that has already been executed onto the history stack.
        /// This is useful for commands like tool strokes that are executed interactively.
        /// </summary>
        /// <param name="command">The command to push.</param>
        void Push(ICommand command);

        /// <summary>
        /// Undoes the last executed command.
        /// </summary>
        void Undo();

        /// <summary>
        /// Redoes the last undone command.
        /// </summary>
        void Redo();
    }
}
