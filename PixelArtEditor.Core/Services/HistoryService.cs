using PixelArtEditor.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace PixelArtEditor.Core.Services
{
    /// <summary>
    /// Manages the command history for undo and redo functionality.
    /// This class implements the IHistoryService interface.
    /// </summary>
    public class HistoryService : IHistoryService
    {
        private readonly Stack<ICommand> _undoStack = new Stack<ICommand>();
        private readonly Stack<ICommand> _redoStack = new Stack<ICommand>();

        /// <summary>
        /// Occurs when the command history has changed.
        /// </summary>
        public event Action HistoryChanged;

        /// <summary>
        /// Gets a value indicating whether an undo operation can be performed.
        /// </summary>
        public bool CanUndo => _undoStack.Count > 0;

        /// <summary>
        /// Gets a value indicating whether a redo operation can be performed.
        /// </summary>
        public bool CanRedo => _redoStack.Count > 0;

        /// <summary>
        /// Executes a command and adds it to the history.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        public void Execute(ICommand command)
        {
            _undoStack.Push(command);
            _redoStack.Clear();
            HistoryChanged?.Invoke();
        }

        /// <summary>
        /// Pushes a command that has already been executed onto the history stack.
        /// This is useful for commands like tool strokes that are executed interactively.
        /// </summary>
        /// <param name="command">The command to push.</param>
        public void Push(ICommand command)
        {
            _undoStack.Push(command);
            _redoStack.Clear();
            HistoryChanged?.Invoke();
        }

        /// <summary>
        /// Undoes the last executed command.
        /// </summary>
        public void Undo()
        {
            if (_undoStack.Count > 0)
            {
                var command = _undoStack.Pop();
                command.Undo();
                _redoStack.Push(command);
                HistoryChanged?.Invoke();
            }
        }

        /// <summary>
        /// Redoes the last undone command.
        /// </summary>
        public void Redo()
        {
            if (_redoStack.Count > 0)
            {
                var command = _redoStack.Pop();
                command.Execute();
                _undoStack.Push(command);
                HistoryChanged?.Invoke();
            }
        }
    }
}
