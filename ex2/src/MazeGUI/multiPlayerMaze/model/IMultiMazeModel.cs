using MazeGUI.etc;
using MazeLib;
using System;

namespace MazeGUI.multiPlayerMaze.model
{
    /// <summary>
    /// interface for the multiplay maze model
    /// </summary>
    public interface IMultiMazeModel : IMazeModel
    {
        /// <summary>
        /// event to get messages received from the server
        /// </summary>
        event EventHandler<StatuesEventArgs> registerForMessages;

        /// <summary>
        /// close the game
        /// </summary>
        void CloseGame();

        /// <summary>
        /// finished game
        /// </summary>
        void FinishGame();

        /// <summary>
        /// send a move to the other player
        /// </summary>
        /// <param name="direction"></param>
        void SendMove(Direction direction);
    }
}
