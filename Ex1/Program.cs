using System;
using MazeGeneratorLib;
using MazeLib;
using SearchAlgorithmsLib;

namespace Ex1
{
    class Program
    {
        /// <summary>
        /// main
        /// </summary>
        /// <param name="args">arguments from user</param>
        static void Main(string[] args)
        {
            CompareSolvers();
            Console.ReadLine();
        }

        /// <summary>
        /// CompareSolvers is a static function that compare between BFS and DFS
        /// </summary>
        public static void CompareSolvers()
        {
            // generate maze
            DFSMazeGenerator mazeGenerator = new DFSMazeGenerator();
            Maze maze = mazeGenerator.Generate(100, 100);
            Console.WriteLine(maze.ToString());

            // solve using BFS and DFS
            MazeAdapter mazeAdapter = new MazeAdapter(maze);
            BFS<Position> bfs = new BFS<Position>();
            DFS<Position> dfs = new DFS<Position>();
            bfs.Search(mazeAdapter);
            dfs.Search(mazeAdapter);

            // print the amount of nodes evaluated
            Console.WriteLine("BFS evaluated nodes: " + bfs.GetNumberOfNodesEvaluated());
            Console.WriteLine("DFS evaluated nodes: " + dfs.GetNumberOfNodesEvaluated());

        }
    }
}
