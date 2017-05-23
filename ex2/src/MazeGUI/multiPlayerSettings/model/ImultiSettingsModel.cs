using MazeLib;

namespace MazeGUI.multiPlayerSettings.model
{
    /// <summary>
    /// Multi player settings model interface
    /// </summary>
    public interface IMultiSettingsModel
    {
        /// <summary>
        /// a property of cols 
        /// </summary>
        int Cols { get; set; }

        /// <summary>
        /// a property of rows 
        /// </summary>
        int Rows { get; set; }

        /// <summary>
        /// a property of mazeName 
        /// </summary>
        string MazeName { get; set; }

        /// <summary>
        /// function of starting the game 
        /// </summary>
        /// <returns>a maze</returns>
        Maze StartGame();

        /// <summary>
        /// Get a list of games
        /// </summary>
        /// <returns> a list of games that waiting to start </returns>
        string[] GetListGames();

        /// <summary>
        /// apply to join the game 
        /// </summary>
        /// <param name="game">the name of the game to join to</param>
        /// <returns>a maze</returns>
        Maze JoinGame(string game);
    }
}
