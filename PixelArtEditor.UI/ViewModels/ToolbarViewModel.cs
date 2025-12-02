using CommunityToolkit.Mvvm.ComponentModel;
using PixelArtEditor.Core.Interfaces;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using PixelArtEditor.Core.Interfaces;
using System.Collections.Generic;

namespace PixelArtEditor.UI.ViewModels
{
    public partial class ToolbarViewModel : ObservableObject
    {
        private readonly IToolService _toolService;
        private readonly IHistoryService _historyService;

        public IEnumerable<ITool> Tools => _toolService.AvailableTools;

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

        [RelayCommand]
        private void SelectTool(ITool tool)
        {
            ActiveTool = tool;
        }

        [RelayCommand(CanExecute = nameof(CanUndo))]
        private void Undo()
        {
            _historyService.Undo();
        }

        [RelayCommand(CanExecute = nameof(CanRedo))]
        private void Redo()
        {
            _historyService.Redo();
        }

        private bool CanUndo() => _historyService.CanUndo;
        private bool CanRedo() => _historyService.CanRedo;
    }
}
