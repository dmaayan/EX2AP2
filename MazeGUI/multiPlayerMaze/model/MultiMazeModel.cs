using Client;
using MazeGUI.etc;
using MazeLib;
using MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI.multiPlayerMaze.model
{
    public class MultiMazeModel : AbstractMazeModel, IMultiMazeModel
    {
        public event EventHandler<StatuesEventArgs> registerForMessages;

        public MultiMazeModel(Maze m) : base(m)
        {
            ClientSingleton.Client.SignForMessaging(new EventHandler<StatuesEventArgs>(OnOpponentMove));
        }

        public void OnOpponentMove(object o, StatuesEventArgs e)
        {
            registerForMessages(this, e);
        }

        public Direction PlayMove()
        {
            return Direction.Down;
        }

        public void CloseGame()
        {
            Statues stat = ClientSingleton.Client.SendMesseage("close " + MazeName);
        }
        // finish, the player won the game
        public void FinishGame()
        {
            Statues stat = ClientSingleton.Client.SendMesseage("finish " + MazeName);
        }

        public void SendMove(Direction direction)
        {
            Statues stat = ClientSingleton.Client.SendMesseage("play " + direction.ToString());
            return;
        }
    }
}
