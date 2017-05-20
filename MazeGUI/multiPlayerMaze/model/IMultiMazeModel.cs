using Client;
using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI.multiPlayerMaze.model
{
    public interface IMultiMazeModel : IMazeModel
    {
        event EventHandler<StatuesEventArgs> MoveOpponent;

        Direction PlayMove();
        void Close();
    }
}
