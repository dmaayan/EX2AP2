using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI.singlePlayerMaze.model
{
    public interface ISinglePlayerMazeModel
    {
        string Name { get; }

        int Cols { get; }

        int Rows { get; }

        string MazeString { get; }

        Position MazeStartPoint { get; }

        Position MazeEndPoint { get; }

        bool IsMoveOk(Position mazeStartPoint, Direction direct);
    }
}
