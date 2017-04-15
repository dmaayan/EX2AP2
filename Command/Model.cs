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

namespace Command
{
    public class Model : IModel
    {
        private Dictionary<string, Maze> waitingList;
        private Dictionary<string, Maze> activeGames;
        private Dictionary<string, Maze> mazes;
        private Dictionary<string, Solution<Position>> solved;
        private Dictionary<Player, Maze> playersToMaze;
        private Dictionary<TcpClient, Player> clientToPlayer;

        public Model()
        {
            mazes = new Dictionary<string, Maze>();
            activeGames = new Dictionary<string, Maze>();
            waitingList = new Dictionary<string, Maze>();
            solved = new Dictionary<string, Solution<Position>>();
            playersToMaze = new Dictionary<Player, Maze>();
            clientToPlayer = new Dictionary<TcpClient, Player>();
        }

        public Maze GenerateMaze(string name, int rows, int cols)
        {
            DFSMazeGenerator mazeGenerator = new DFSMazeGenerator();
            Maze maze = mazeGenerator.Generate(rows, cols);
            maze.Name = name;
            if (mazes.ContainsKey(name))
            {
                CloseGame(name);
            }
            mazes.Add(name, maze);
            return maze;
        }

        public Solution<Position> SolveMaze(string name, searchAlgo searcher)
        {
            if (!mazes.ContainsKey(name))
            {
                return null;
            }
            if (solved.ContainsKey(name))
            {
                return solved[name];
            }
            Maze maze = mazes[name];
            MazeAdapter mazeAdapter = new MazeAdapter(maze);
            solved.Add(name, searcher(mazeAdapter));
            return solved[name];
        }

        public Maze GetMaze(string name)
        {
            if (mazes.ContainsKey(name))
            {
                return mazes[name];
            }
            return null;
        }

        public Maze StartGame(string name, int rows,int cols, TcpClient client)
        {
            Maze maze;
            if (mazes.ContainsKey(name))
            {
                maze = mazes[name];
            }
            else
            {
                maze = GenerateMaze(name, rows, cols);
            }
            waitingList.Add(name, maze);
            playersToMaze.Add(new Player(client), maze);
            return maze;
        }

        public Maze JoinGame(string name, TcpClient client)
        {
            Maze maze;
            if (waitingList.ContainsKey(name))
            {
                maze = waitingList[name];
                activeGames.Add(name, maze);
                waitingList.Remove(name);
                playersToMaze.Add(new Player(client), maze);
                return maze;
            }
            return null;
        }

        public Maze PlayGame(string move, TcpClient client)
        {
            if (clientToPlayer.ContainsKey(client))
            {
                clientToPlayer[client].Way.Add(Enum.Parse(Direction, move));
                Maze maze = playersToMaze[player];
            }
            if (activeGames.ContainsKey(name))
            {
                activeGames.Add(name, waitingList[name]);
                waitingList.Remove(name);
                return mazes[name];
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

    }
}
