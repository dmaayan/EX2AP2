using Ex1;
using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;

namespace Command
{
    public class Model : IModel
    {
        Dictionary<string, Maze> mazes;

        public Maze GenerateMaze(string name, int rows, int cols)
        {
            if (!mazes.ContainsKey(name))
            {
                DFSMazeGenerator mazeGenerator = new DFSMazeGenerator();
                Maze maze = mazeGenerator.Generate(rows, cols);
                maze.Name = name;
                mazes.Add(name, maze);
                return maze;
            }
            return null;
        }

        public Maze GetMaze(string name)
        {
            if (mazes.ContainsKey(name))
            {
                return mazes[name];
            }
            return null;
        }
    }
}
