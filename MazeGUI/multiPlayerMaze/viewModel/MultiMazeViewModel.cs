using MazeGUI.multiPlayerMaze.model;
using MazeLib;
using System;

namespace MazeGUI.multiPlayerMaze.viewModel
{
    /// <summary>
    /// view model for the multiplay maze
    /// </summary>
    public class MultiMazeViewModel : ViewModel
    {
        /// <summary>
        /// event to get messages received from the server
        /// </summary>
        public event EventHandler<StatuesEventArgs> MoveOpponent;
        /// <summary>
        /// the model
        /// </summary>
        private IMultiMazeModel model;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="m">model of the viewmodel</param>
        public MultiMazeViewModel(IMultiMazeModel m)
        {
            model = m;
            model.registerForMessages += OnOpponentMove;
        }

        /// <summary>
        /// method to do when message is received
        /// </summary>
        /// <param name="o">object triggered the event</param>
        /// <param name="e">parameters sent</param>
        public void OnOpponentMove(object o, StatuesEventArgs statues)
        {
            MoveOpponent?.Invoke(o, statues);
        }

        /// <summary>
        /// close the game
        /// </summary>
        public void CloseGame()
        {
            model.CloseGame();
        }

        /// <summary>
        /// finished game
        /// </summary>
        public void FinishGame()
        {
            model.FinishGame();
        }

        /// <summary>
        /// maze name getter
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
        public bool IsMoveOk(Position playerPos, Direction direction)
        {
            return model.IsMoveOk(playerPos, direction);
        }

        /// <summary>
        /// send a move to the other player
        /// </summary>
        /// <param name="direction"></param>
        public void SendMove(Direction direction)
        {
            model.SendMove(direction);
        }
    }
}
