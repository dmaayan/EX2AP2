using MazeGUI.etc;
using MazeLib;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI.singlePlayerMaze.model
{
    /// <summary>
    /// interface for the single player model
    /// </summary>
    public interface ISinglePlayerMazeModel : IMazeModel
    {
        /// <summary>
        /// solve the maze
        /// </summary>
        /// <returns>string representation of the solution</returns>
        string SolveMaze();
    }
}
