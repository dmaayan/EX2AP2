
namespace MazeGUI
{
    /// <summary>
    /// ISettingsModel is an interface of the modles 
    /// </summary>
    public interface ISettingsModel
    {
        /// <summary>
        /// a property of serverIP 
        /// </summary>
        string ServerIP { get; set; }

        /// <summary>
        /// a property of serverPort
        /// </summary>
        int ServerPort { get; set; }

        /// <summary>
        /// a property of mazeRows 
        /// </summary>
        int MazeRows { get; set; }

        /// <summary>
        /// a property of mazeCols 
        /// </summary>
        int MazeCols { get; set; }

        /// <summary>
        /// a property of searchAlgorithm 
        /// </summary>
        int SearchAlgorithm { get; set; }

        /// <summary>
        /// save the settings to be defaults
        /// </summary>
        void SaveSettings();

        /// <summary>
        /// Reload the settings
        /// </summary>
        void ReloadSettings();
    }
}
