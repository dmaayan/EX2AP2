using MazeLib;

namespace MazeGUI.singlePlayerSettings
{
    public interface ISingleSettingsModel
    {
        /// <summary>
        /// a property of cols
        /// </summary>
        int Cols { get; set; }

        /// <summary>
        /// a property of rows 
        /// </summary>>
        int Rows { get; set; }

        /// <summary>
        /// a property of mazeName 
        /// </summary>
        string MazeName { get; set; }

        /// <summary>
        /// Connect to the server to generate maze.
        /// </summary>
        /// <returns>a maze</returns>
        Maze Connect();
    }
}
