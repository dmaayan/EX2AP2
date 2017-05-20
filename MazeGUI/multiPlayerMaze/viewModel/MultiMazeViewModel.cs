using Client;
using MazeGUI.multiPlayerMaze.model;
using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI.multiPlayerMaze.viewModel
{
    public class MultiMazeViewModel : ViewModel
    {
        public event EventHandler<StatuesEventArgs> MoveOpponent;

        private IMultiMazeModel model;

        public MultiMazeViewModel(IMultiMazeModel m)
        {
            model = m;
            model.MoveOpponent += OnOpponentMove;
        }

        public void Close()
        {
            model.Close();
        }

        public void OnOpponentMove(object o, StatuesEventArgs statues)
        {
            MoveOpponent(o, statues);
        }

        public string MazeName
        {
            get { return model.MazeName; }
        }

        public int Cols
        {
            get { return model.Cols; }
        }

        public int Rows
        {
            get { return model.Rows; }
        }

        public string MazeString
        {
            get { return model.MazeString; }
        }

        public Position MazeStartPoint
        {
            get { return model.MazeStartPoint; }
        }

        public Position MazeEndPoint
        {
            get { return model.MazeEndPoint; }
        }

        public bool IsMoveOk(Position playerPos, Direction direction)
        {
            return model.IsMoveOk(playerPos, direction);
        }

        public void SendMove(Direction direction)
        {
            model.SendMove(direction);
        }
    }
}
