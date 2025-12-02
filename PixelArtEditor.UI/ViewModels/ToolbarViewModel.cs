using CommunityToolkit.Mvvm.ComponentModel;
using PixelArtEditor.Core.Interfaces;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;

namespace PixelArtEditor.UI.ViewModels
{
    /// <summary>
    /// The view model for the toolbar, which manages the available tools and the active tool.
    /// It also provides commands for undo and redo.
    /// </summary>
    public partial class ToolbarViewModel : ObservableObject
    {
        private readonly IToolService _toolService;
        private readonly IHistoryService _historyService;

        /// <summary>
        /// Gets the collection of available tools.
        /// </summary>
        public IEnumerable<ITool> Tools => _toolService.AvailableTools;

        /// <summary>
        /// Gets or sets the currently active tool.
        /// </summary>
        public ITool ActiveTool
        {
            get => _toolService.ActiveTool;
            set
            {
                if (_toolService.ActiveTool != value)
                {
                    _toolService.ActiveTool = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ToolbarViewModel"/> class.
        /// </summary>
        /// <param name="toolService">The service for managing tools.</param>
        /// <param name="historyService">The service for managing undo/redo history.</param>
        public ToolbarViewModel(IToolService toolService, IHistoryService historyService)
        {
            _toolService = toolService;
            _historyService = historyService;
            
            _historyService.HistoryChanged += () =>
            {
                UndoCommand.NotifyCanExecuteChanged();
                RedoCommand.NotifyCanExecuteChanged();
            };
        }

        /// <summary>
        /// A command to select a tool.
        /// </summary>
        /// <param name="tool">The tool to select.</param>
        [RelayCommand]
        private void SelectTool(ITool tool)
        {
            ActiveTool = tool;
        }

        /// <summary>
        /// A command to undo the last action.
        /// </summary>
        [RelayCommand(CanExecute = nameof(CanUndo))]
        private void Undo()
        {
            _historyService.Undo();
        }

        /// <summary>
        /// A command to redo the last undone action.
        /// </summary>
        [RelayCommand(CanExecute = nameof(CanRedo))]
        private void Redo()
        {
            _historyService.Redo();
        }

        /// <summary>
        /// Determines whether the undo command can be executed.
        /// </summary>
        /// <returns>True if the undo command can be executed, otherwise false.</returns>
        private bool CanUndo() => _historyService.CanUndo;

        /// <summary>
        /// Determines whether the redo command can be executed.
        /// </summary>
        /// <returns>True if the redo command can be executed, otherwise false.</returns>
        private bool CanRedo() => _historyService.CanRedo;
    }
}
