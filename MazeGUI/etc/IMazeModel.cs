using MazeLib;

namespace MazeGUI.etc
{
    /// <summary>
    /// interface for the maze models
    /// </summary>
    public interface IMazeModel
    {
        /// <summary>
        /// maze name getter
        /// </summary>
        string MazeName { get; }

        /// <summary>
        /// getter for the maze columns
        /// </summary>
        int Cols { get; }

        /// <summary>
        /// getter for the maze rows
        /// </summary>
        int Rows { get; }

        /// <summary>
        /// getter for the maze string representation
        /// </summary>
        string MazeString { get; }

        /// <summary>
        /// getter for the maze start point
        /// </summary>
        Position MazeStartPoint { get; }

        /// <summary>
        /// getter for the maze end point
        /// </summary>
        Position MazeEndPoint { get; }

        /// <summary>
        /// checks if a direction is valid on the maze
        /// </summary>
        /// <param name="playerPosition">the position of the player on the maze</param>
        /// <param name="direct">direction to check if valid move</param>
        /// <returns>true if move is valid, false otherwise</returns>
        bool IsMoveOk(Position playerPosition, Direction direct);
    }
}
