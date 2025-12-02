using PixelArtEditor.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace PixelArtEditor.Core.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly Stack<ICommand> _undoStack = new Stack<ICommand>();
        private readonly Stack<ICommand> _redoStack = new Stack<ICommand>();

        public event Action? HistoryChanged;

        public bool CanUndo => _undoStack.Count > 0;
        public bool CanRedo => _redoStack.Count > 0;

        public void Execute(ICommand command)
        {
            // We assume the command has already been executed by the tool/action *before* being added here,
            // OR we execute it here.
            // Usually for tools, the tool does the work (interactive), then pushes the command.
            // For one-shot actions (like "Delete Layer"), we might execute it here.
            // Let's adopt the pattern: Command is created *after* the action is complete (for tools),
            // or Command.Execute() is called for discrete actions.
            
            // However, to be consistent, let's say:
            // If it's a tool stroke, the change is already applied to the bitmap. We just push the command.
            // If it's a menu action, we might construct it and call Execute.
            
            // To support both, we can have a Push(ICommand) method.
            // But standard Command pattern is Execute(ICommand).
            
            // Let's stick to: The caller is responsible for the *initial* execution if it's interactive.
            // But if we want to support "Redo", the command must know how to Execute.
            
            _undoStack.Push(command);
            _redoStack.Clear();
            HistoryChanged?.Invoke();
        }

        public void Push(ICommand command)
        {
            _undoStack.Push(command);
            _redoStack.Clear();
            HistoryChanged?.Invoke();
        }

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
