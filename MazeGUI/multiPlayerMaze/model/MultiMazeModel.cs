using Client;
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
        public event EventHandler<StatuesEventArgs> MoveOpponent;

        public MultiMazeModel(Maze m) : base(m)
        {
            ClientSingleton.Client.SignForMessaging(new EventHandler<StatuesEventArgs>(OnOpponentMove));
        }

        public void OnOpponentMove(object o, StatuesEventArgs e)
        {
            MoveOpponent(this, e);
        }

        public Direction PlayMove()
        {
            return Direction.Down;
        }

        public void Close()
        {

            Statues stat = ClientSingleton.Client.SendMesseage("Close " + MazeName);

        }

    }
}
