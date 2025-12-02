using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PixelArtEditor.Core.Models
{
    /// <summary>
    /// Defines the available symmetry modes for drawing.
    /// </summary>
    public enum SymmetryMode
    {
        /// <summary>
        /// No symmetry.
        /// </summary>
        None,

        /// <summary>
        /// Horizontal symmetry (mirror on the X-axis).
        /// </summary>
        Horizontal,

        /// <summary>
        /// Vertical symmetry (mirror on the Y-axis).
        /// </summary>
        Vertical,

        /// <summary>
        /// Both horizontal and vertical symmetry.
        /// </summary>
        Both
    }

    /// <summary>
    /// Manages the symmetry settings for drawing tools.
    /// </summary>
    public class SymmetrySettings : INotifyPropertyChanged
    {
        private SymmetryMode _mode = SymmetryMode.None;

        /// <summary>
        /// Gets or sets the current symmetry mode.
        /// </summary>
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

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
