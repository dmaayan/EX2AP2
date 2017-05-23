using MazeGUI.etc;

namespace MazeGUI.singlePlayerMaze.model
{
    /// <summary>
    /// interface for the single player model
    /// </summary>
    public interface ISinglePlayerMazeModel : IMazeModel
    {
        /// <summary>
        /// solve the maze
        /// </summary>
        /// <returns>string representation of the solution</returns>
        string SolveMaze();
    }
}
