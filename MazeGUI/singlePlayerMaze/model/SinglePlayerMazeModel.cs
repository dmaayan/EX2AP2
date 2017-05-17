using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI.singlePlayerMaze.model
{
    class SinglePlayerMazeModel : ISinglePlayerMazeModel
    {
        Maze maze;

        public SinglePlayerMazeModel(Maze m)
        {
            maze = m;
        }

        public string Name {
            get { return maze.Name; }
        }

        public int Cols {
            get { return maze.Cols; }
        }

        public int Rows {
            get { return maze.Rows; }
        }
    }
}
