using System.ComponentModel;

namespace MazeGUI
{
    /// <summary>
    /// an abstract class of ViewMode, inherits INotifyPropertyChanged
    /// </summary>
    public abstract class ViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// the event PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notify whan Property changed
        /// </summary>
        /// <param name="propName">string of the name</param>
        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
