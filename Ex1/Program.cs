using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;

namespace Ex1
{
    class Program
    {
        static void Main(string[] args)
        {
            CompareSolvers();
            Console.ReadLine();
        }

        public static void CompareSolvers()
        {
            DFSMazeGenerator mazeGenerator = new DFSMazeGenerator();
            Maze maze = mazeGenerator.Generate(20, 20);
            Console.WriteLine(maze.ToString());
            MazeAdapter mazeAdapter = new MazeAdapter(maze);
            BFS<Position> bfs = new BFS<Position>();
            DFS<Position> dfs = new DFS<Position>();
            bfs.search(mazeAdapter);
            dfs.search(mazeAdapter);
            Console.WriteLine("BFS evaluated nodes: " + bfs.getNumberOfNodesEvaluated());
            Console.WriteLine("DFS evaluated nodes: " + dfs.getNumberOfNodesEvaluated());

        }
    }
}
