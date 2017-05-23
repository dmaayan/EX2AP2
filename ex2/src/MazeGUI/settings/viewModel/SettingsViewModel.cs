namespace MazeGUI
{
    /// <summary>
    /// The view model of settings. inherits ViewModel
    /// </summary>
    class SettingsViewModel : ViewModel
    {
        /// <summary>
        /// the model of the view model
        /// </summary>
        private ISettingsModel model;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="mod">the modlel</param>
        public SettingsViewModel(ISettingsModel mod)
        {
            model = mod;
        }

        /// <summary>
        /// a property of serverIP, 
        /// whan set, notify.
        /// </summary>
        public string ServerIP
        {
            get { return model.ServerIP; }
            set
            {
                model.ServerIP = value;
                NotifyPropertyChanged("ServerIP");
            }
        }

        /// <summary>
        /// a property of serverPort, 
        /// whan set, notify.
        /// </summary>
        public int ServerPort
        {
            get { return model.ServerPort; }
            set
            {
                model.ServerPort = value;
                NotifyPropertyChanged("ServerPort");
            }
        }

        /// <summary>
        /// a property of mazeRows, 
        /// whan set, notify.
        /// </summary>
        public int MazeRows
        {
            get { return model.MazeRows; }
            set
            {
                model.MazeRows = value;
                NotifyPropertyChanged("MazeRows");
            }
        }

        /// <summary>
        /// a property of mazeCols, 
        /// whan set, notify.
        /// </summary>
        public int MazeCols
        {
            get { return model.MazeCols; }
            set
            {
                model.MazeCols = value;
                NotifyPropertyChanged("MazeCols");
            }
        }

        /// <summary>
        /// a property of searchAlgorithm, 
        /// whan set, notify.
        /// </summary>
        public int SearchAlgorithm
        {
            get { return model.SearchAlgorithm; }
            set
            {
                model.SearchAlgorithm = value;
                NotifyPropertyChanged("SearchAlgorithm");
            }
        }
        
        /// <summary>
        /// save the settings to be defaults
        /// </summary>
        public void SaveSettings()
        {
            model.SaveSettings();
        }

        /// <summary>
        /// reloads the old settings
        /// </summary>
        public void ReloadSettings()
        {
            model.ReloadSettings();
        }
    }
}
