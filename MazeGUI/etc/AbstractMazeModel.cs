using MazeLib;
using System;

namespace MazeGUI.etc
{
    /// <summary>
    /// abstract class to share code between the maze models
    /// </summary>
    public class AbstractMazeModel
    {
        /// <summary>
        /// maze of the model
        /// </summary>
        private Maze maze;
        /// <summary>
        /// the string representation of the maze
        /// </summary>
        private string mazeString;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="m">the maze of the model</param>
        public AbstractMazeModel(Maze m)
        {
            maze = m;
            // create the maze string from the maze
            mazeString = String.Join("", maze.ToString().Split('\r', '\n'));
        }

        /// <summary>
        /// getter for the maze name
        /// </summary>
        public string MazeName
        {
            get { return maze.Name; }
        }

        /// <summary>
        /// getter for the maze columns
        /// </summary>
        public int Cols
        {
            get { return maze.Cols; }
        }

        /// <summary>
        /// getter for the maze rows
        /// </summary>
        public int Rows
        {
            get { return maze.Rows; }
        }

        /// <summary>
        /// getter for the maze string representation
        /// </summary>
        public string MazeString
        {
            get { return mazeString; }
        }

        /// <summary>
        /// getter for the maze start point
        /// </summary>
        public Position MazeStartPoint
        {
            get { return maze.InitialPos; }
        }

        /// <summary>
        /// getter for the maze end point
        /// </summary>
        public Position MazeEndPoint
        {
            get { return maze.GoalPos; }
        }

        /// <summary>
        /// checks if a direction is valid on the maze
        /// </summary>
        /// <param name="playerPosition">the position of the player on the maze</param>
        /// <param name="direct">direction to check if valid move</param>
        /// <returns>true if move is valid, false otherwise</returns>
        public bool IsMoveOk(Position playerPosition, Direction direct)
        {
            // get the row and col of the player position
            int row = playerPosition.Row;
            int col = playerPosition.Col;
            // check the matching direction if the move is valid and the maze is free there
            switch (direct)
            {
                case Direction.Right:
                    {
                        return ((col + 1) < maze.Cols)
                            && (maze[row, (col + 1)] == CellType.Free);
                    }
                case Direction.Up:
                    {
                        return ((row - 1) >= 0)
                            && (maze[row - 1, col] == CellType.Free);
                    }
                case Direction.Down:
                    {
                        return ((row + 1) < maze.Rows)
                            && (maze[row + 1, col] == CellType.Free);
                    }
                case Direction.Left:
                    {
                        return ((col - 1) >= 0)
                            && (maze[row, (col - 1)] == CellType.Free);
                    }
                default:
                    {
                        return false;
                    }
            }
        }
    }
}
