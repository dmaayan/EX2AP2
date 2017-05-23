using MazeGUI.etc;
using MazeLib;
using MVC;
using System;

namespace MazeGUI.multiPlayerMaze.model
{
    /// <summary>
    /// model for the multiplay window
    /// </summary>
    public class MultiMazeModel : AbstractMazeModel, IMultiMazeModel
    {
        /// <summary>
        /// event to get messages received from the server
        /// </summary>
        public event EventHandler<StatuesEventArgs> registerForMessages;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="m">maze to play</param>
        public MultiMazeModel(Maze m) : base(m)
        {
            // register for messages from the server
            ClientSingleton.Client.SignForMessaging(new EventHandler<StatuesEventArgs>(OnOpponentMove));
        }

        /// <summary>
        /// method to do when message is received
        /// </summary>
        /// <param name="o">object triggered the event</param>
        /// <param name="e">parameters sent</param>
        public void OnOpponentMove(object o, StatuesEventArgs e)
        {
            registerForMessages?.Invoke(this, e);
        }

        /// <summary>
        /// close the game
        /// </summary>
        public void CloseGame()
        {
            Statues stat = ClientSingleton.Client.SendMesseage("close " + MazeName);
        }

        /// <summary>
        /// finished game
        /// </summary>
        public void FinishGame()
        {
            Statues stat = ClientSingleton.Client.SendMesseage("finish " + MazeName);
        }

        /// <summary>
        /// send a move to the other player
        /// </summary>
        /// <param name="direction"></param>
        public void SendMove(Direction direction)
        {
            Statues stat = ClientSingleton.Client.SendMesseage("play " + direction.ToString());
        }
    }
}
