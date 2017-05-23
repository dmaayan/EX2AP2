
namespace MazeGUI
{
    /// <summary>
    /// The model of settings. inherits ISettingsModel
    /// </summary>
    class ApplicationSettingsModel : ISettingsModel
    {
        /// <summary>
        /// a property of serverIP, get and set the defaults settings
        /// </summary>
        public string ServerIP
        {
            get { return Properties.Settings.Default.ServerIP; }
            set { Properties.Settings.Default.ServerIP = value; }
        }

        /// <summary>
        /// a property of serverPort, get and set the defaults settings
        /// </summary>
        public int ServerPort
        {
            get { return Properties.Settings.Default.ServerPort; }
            set { Properties.Settings.Default.ServerPort = value; }
        }

        /// <summary>
        /// a property of mazeRows, get and set the defaults settings
        /// </summary>
        public int MazeRows {
            get { return Properties.Settings.Default.MazeRows; }
            set { Properties.Settings.Default.MazeRows = value; }
        }

        /// <summary>
        /// a property of mazeCols, get and set the defaults settings
        /// </summary>
        public int MazeCols {
            get { return Properties.Settings.Default.MazeCols; }
            set { Properties.Settings.Default.MazeCols = value; }
        }

        /// <summary>
        /// a property of searchAlgorithm, get and set the defaults settings
        /// </summary>
        public int SearchAlgorithm {
            get { return Properties.Settings.Default.SearchAlgorithm; }
            set { Properties.Settings.Default.SearchAlgorithm = value; }
        }

        /// <summary>
        /// save the settings to be defaults
        /// </summary>
        public void SaveSettings()
        {
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// reloads the old settings
        /// </summary>
        public void ReloadSettings()
        {
            Properties.Settings.Default.Reload();
        }
    }
}