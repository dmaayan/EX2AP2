using Client;
using MazeGUI.etc;
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
        event EventHandler<StatuesEventArgs> registerForMessages;

        Direction PlayMove();
        void CloseGame();
        void FinishGame();
        void SendMove(Direction direction);
    }
}
