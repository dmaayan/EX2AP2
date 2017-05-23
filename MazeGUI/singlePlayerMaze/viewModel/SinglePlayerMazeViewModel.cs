using MazeGUI.singlePlayerMaze.model;
using MazeLib;

namespace MazeGUI.singlePlayerMaze.viewModel
{
    /// <summary>
    /// the single player view model
    /// </summary>
    class SinglePlayerMazeViewModel : ViewModel
    {
        /// <summary>
        /// the model of the view model
        /// </summary>
        private ISinglePlayerMazeModel model;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="mod">model of the view model</param>
        public SinglePlayerMazeViewModel(ISinglePlayerMazeModel mod)
        {
            model = mod;
        }

        /// <summary>
        /// getter for the maze name
        /// </summary>
        public string MazeName
        {
            get { return model.MazeName; }
        }

        /// <summary>
        /// getter for the maze columns
        /// </summary>
        public int Cols
        {
            get { return model.Cols; }
        }

        /// <summary>
        /// getter for the maze rows
        /// </summary>
        public int Rows
        {
            get { return model.Rows; }
        }

        /// <summary>
        /// getter for the maze string representation
        /// </summary>
        public string MazeString
        {
            get { return model.MazeString; }
        }

        /// <summary>
        /// getter for the maze start point
        /// </summary>
        public Position MazeStartPoint
        {
            get { return model.MazeStartPoint; }
        }

        /// <summary>
        /// getter for the maze end point
        /// </summary>
        public Position MazeEndPoint
        {
            get { return model.MazeEndPoint; }
        }

        /// <summary>
        /// checks if a direction is valid on the maze
        /// </summary>
        /// <param name="playerPosition">the position of the player on the maze</param>
        /// <param name="direct">direction to check if valid move</param>
        /// <returns>true if move is valid, false otherwise</returns>
        public bool IsMoveOk(Position mazeStartPoint, Direction direct)
        {
            return model.IsMoveOk(mazeStartPoint, direct);
        }

        /// <summary>
        /// solve the maze
        /// </summary>
        /// <returns>string representation of the solution</returns>
        public string SolveMaze()
        {
            return model.SolveMaze();
        }
    }
}
