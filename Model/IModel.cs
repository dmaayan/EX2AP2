using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Command
{
    public interface IModel
    {
        Maze GenerateMaze(string name, int rows, int cols);

        Maze GetMaze(string name);
    }
}
