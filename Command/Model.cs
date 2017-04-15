using Ex1;
using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using SearchAlgorithmsLib;
using System.Net.Sockets;
using Client;

namespace MVC
{
    public class Model : IModel
    {
        private Dictionary<string, Maze> waitingList;
        private Dictionary<string, Maze> activeGames;
        private Dictionary<string, Maze> mazes;
        private Dictionary<string, Solution<Position>> solved;
        private Dictionary<Player, Maze> playersToMaze;
        private Dictionary<TcpClient, Player> clientToPlayer;

        /// <summary>
        /// constructor
        /// </summary>
        public Model()
        {
            mazes = new Dictionary<string, Maze>();
            activeGames = new Dictionary<string, Maze>();
            waitingList = new Dictionary<string, Maze>();
            solved = new Dictionary<string, Solution<Position>>();
            playersToMaze = new Dictionary<Player, Maze>();
            clientToPlayer = new Dictionary<TcpClient, Player>();
        }

        /// <summary>
        /// Generate a Maze, if the maze exist close it and create new maze
        /// </summary>
        /// <param name="name">the name of the maze </param>
        /// <param name="rows">the rows of the maze </param>
        /// <param name="cols">the cols of the maze </param>
        /// <returns>the new maze</returns>
        public Maze GenerateMaze(string name, int rows, int cols)
        {
            DFSMazeGenerator mazeGenerator = new DFSMazeGenerator();
            Maze maze = mazeGenerator.Generate(rows, cols);
            maze.Name = name;
            // if the maze exist close it
            if (mazes.ContainsKey(name))
            {
                CloseGame(name);
            }
            // create new maze
            mazes.Add(name, maze);
            return maze;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">the name of the maze</param>
        /// <param name="searcher">the algorithem to solve with</param>
        /// <returns>the solution to the maze</returns>
        public Solution<Position> SolveMaze(string name, searchAlgo searcher)
        {
            // checks if the maze exist
            if (!mazes.ContainsKey(name))
            {
                return null;
            }
            // checks if the solution exist
            if (solved.ContainsKey(name))
            {
                return solved[name];
            }
            // solve the maze and return the solution
            Maze maze = mazes[name];
            MazeAdapter mazeAdapter = new MazeAdapter(maze);
            Solution<Position> solution = searcher(mazeAdapter);
            solved.Add(name, solution);
            return solution;
        }
        
        /// <summary>
        /// get a maze
        /// </summary>
        /// <param name="name">the name of the maze</param>
        /// <returns>the maze if exist</returns>
        public Maze GetMaze(string name)
        {
            if (mazes.ContainsKey(name))
            {
                return mazes[name];
            }
            return null;
        }

        /// <summary>
        /// start a game
        /// </summary>
        /// <param name="name">of the maze </param>
        /// <param name="rows">of the maze </param>
        /// <param name="cols">of the maze</param>
        /// <param name="client">that start this game</param>
        /// <returns>the maze</returns>
        public Maze StartGame(string name, int rows, int cols, TcpClient client)
        {
            Maze maze;
            // לבדוק אם (מי) בכלל צריך את זה
            if (mazes.ContainsKey(name))
            {
                maze = mazes[name];
            }
            else
            {
                maze = GenerateMaze(name, rows, cols);
            }
            waitingList.Add(name, maze);
            Player p = new Player(client);
            playersToMaze.Add(p, maze);
            p.WaitForPlayer();
            return maze;
        }

        public Maze JoinGame(string name, TcpClient client)
        {
            Maze maze;
            if (waitingList.ContainsKey(name))
            {
                maze = waitingList[name];
                Player p = playersToMaze.Where(elem => 
                    { return elem.Value.Equals(maze); }).First().Key;
                activeGames.Add(name, maze);
                waitingList.Remove(name);
                playersToMaze.Add(new Player(client), maze);
                p.StopWaiting();
                return maze;
            }
            return null;
        }

        public Maze PlayGame(Direction move, TcpClient client)
        {
            if (clientToPlayer.ContainsKey(client))
            {
                Player player = clientToPlayer[client];
                Maze maze = playersToMaze[player];
                if (activeGames.ContainsKey(maze.Name))
                {
                    player.Way.Add(move);
                    return maze;
                }
            }
            return null;
        }

        public void CloseGame(string name)
        {
            if (mazes.ContainsKey(name))
            {
                mazes.Remove(name);
                if (solved.ContainsKey(name))
                {
                    solved.Remove(name);
                }
                if (waitingList.ContainsKey(name))
                {
                    waitingList.Remove(name);
                }
                if (activeGames.ContainsKey(name))
                {
                    activeGames.Remove(name);
                }
            }
            
        }

        public string[] GetAllNames()
        {
            return waitingList.Keys.ToArray();
        }
    }
}
