using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PixelArtEditor.Core.Models
{
    public enum SymmetryMode
    {
        None,
        Horizontal,  // Mirror on X-axis
        Vertical,    // Mirror on Y-axis
        Both         // Mirror on both axes
    }

    public class SymmetrySettings : INotifyPropertyChanged
    {
        private SymmetryMode _mode = SymmetryMode.None;

        public SymmetryMode Mode
        {
            get => _mode;
            set
            {
                if (_mode != value)
                {
                    _mode = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
