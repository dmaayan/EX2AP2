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
    }
}
