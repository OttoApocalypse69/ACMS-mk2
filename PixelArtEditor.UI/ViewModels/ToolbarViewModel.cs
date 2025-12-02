using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PixelArtEditor.Core.Interfaces;
using PixelArtEditor.Core.Tools;
using System.Collections.Generic;

namespace PixelArtEditor.UI.ViewModels
{
    public partial class ToolbarViewModel : ObservableObject
    {
        private readonly IToolService _toolService;
        private readonly IHistoryService _historyService;

        public IEnumerable<ITool> Tools => _toolService.AvailableTools;

        [ObservableProperty]
        private int _pencilSize = 1;

        public ITool ActiveTool
        {
            get => _toolService.ActiveTool;
            set
            {
                if (_toolService.ActiveTool != value)
                {
                    _toolService.ActiveTool = value;
                    OnPropertyChanged();
                    ApplyPencilSize();
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

            ApplyPencilSize();
        }

        partial void OnPencilSizeChanged(int value)
        {
            ApplyPencilSize();
        }

        private void ApplyPencilSize()
        {
            foreach (var tool in _toolService.AvailableTools)
            {
                if (tool is PencilTool pencil)
                {
                    pencil.Size = _pencilSize < 1 ? 1 : _pencilSize;
                }
            }
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
