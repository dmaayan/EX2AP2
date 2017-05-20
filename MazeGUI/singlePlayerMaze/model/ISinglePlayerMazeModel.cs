using MazeLib;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI.singlePlayerMaze.model
{
    public interface ISinglePlayerMazeModel : IMazeModel
    {
        string SolveMaze();
    }
}
