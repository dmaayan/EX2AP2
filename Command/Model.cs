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
        private Dictionary<string, Maze> singlePlayerMazes;
        private Dictionary<string, Maze> multiPlayerMazes;
        private Dictionary<string, MazeSolution> singlePlayerSolved;
        private Dictionary<string, Game> mazeNameToGame;
        private Dictionary<TcpClient, string> clientToMazeName;

        /// <summary>
        /// constructor
        /// </summary>
        public Model()
        {
            singlePlayerMazes = new Dictionary<string, Maze>();
            multiPlayerMazes = new Dictionary<string, Maze>();
            activeGames = new Dictionary<string, Maze>();
            waitingList = new Dictionary<string, Maze>();
            singlePlayerSolved = new Dictionary<string, MazeSolution>();
            mazeNameToGame = new Dictionary<string, Game>();
            clientToMazeName = new Dictionary<TcpClient, string>();
        }

        /// <summary>
        /// Generate a Maze, if the maze exist close it and create new maze
        /// </summary>
        /// <param name="name">the name of the maze </param>
        /// <param name="rows">the rows of the maze </param>
        /// <param name="cols">the cols of the maze </param>
        /// <returns>the new maze</returns>
        public Maze SingleGameGenerateMaze(string name, int rows, int cols)
        {
            DFSMazeGenerator mazeGenerator = new DFSMazeGenerator();
            Maze maze = mazeGenerator.Generate(rows, cols);
            maze.Name = name;
            // if the maze exist close it
            if (singlePlayerMazes.ContainsKey(name))
            {
                CloseGame(name);
            }
            // create new maze
            singlePlayerMazes.Add(name, maze);
            return maze;
        }

        /// <summary>
        /// Generate a Maze, if the maze exist close it and create new maze
        /// </summary>
        /// <param name="name">the name of the maze </param>
        /// <param name="rows">the rows of the maze </param>
        /// <param name="cols">the cols of the maze </param>
        /// <returns>the new maze</returns>
        private Maze MultiGameGenerateMaze(string name, int rows, int cols)
        {
            DFSMazeGenerator mazeGenerator = new DFSMazeGenerator();
            Maze maze = mazeGenerator.Generate(rows, cols);
            maze.Name = name;
            // if the maze exist close it
            if (multiPlayerMazes.ContainsKey(name))
            {
                return null;
            }
            // create new maze
            multiPlayerMazes.Add(name, maze);
            return maze;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">the name of the maze</param>
        /// <param name="searcher">the algorithem to solve with</param>
        /// <returns>the solution to the maze</returns>
        public MazeSolution SolveMaze(string name, searchAlgo searcher)
        {
            // checks if the maze exist
            if (!singlePlayerMazes.ContainsKey(name))
            {
                return null;
            }
            // checks if the solution exist
            if (singlePlayerSolved.ContainsKey(name))
            {
                return singlePlayerSolved[name];
            }
            // solve the maze and return the solution
            Maze maze = singlePlayerMazes[name];
            MazeAdapter mazeAdapter = new MazeAdapter(maze);
            MazeSolution ms = new MazeSolution(searcher(mazeAdapter), maze.Name);
            singlePlayerSolved.Add(name, ms);
            return ms;
        }
        
        ///// <summary>
        ///// get a maze
        ///// </summary>
        ///// <param name="name">the name of the maze</param>
        ///// <returns>the maze if exist</returns>
        //public Maze GetMaze(string name)
        //{
        //    if (singlePlayerMazes.ContainsKey(name))
        //    {
        //        return singlePlayerMazes[name];
        //    }
        //    return null;
        //}

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
            Maze maze = MultiGameGenerateMaze(name, rows, cols);
            if (maze == null)
            {
                return null;
            }
            string mazeName = maze.Name;
            waitingList.Add(name, maze);
            Player p = new Player(client);
            mazeNameToGame.Add(mazeName, new Game(p));
            mazeNameToGame[mazeName].waitForOtherPlayer();
            clientToMazeName.Add(client, mazeName);
            return maze;
        }

        public Maze JoinGame(string name, TcpClient client)
        {
            Maze maze;
            if (waitingList.ContainsKey(name))
            {
                maze = waitingList[name];
                string mazeName = maze.Name;
                activeGames.Add(name, maze);
                waitingList.Remove(name);
                Player player = new Player(client);
                mazeNameToGame[mazeName].AddPlayer(player);
                clientToMazeName.Add(client, mazeName);
                return maze;
            }
            return null;
        }

        public Maze PlayGame(Direction move, TcpClient client)
        {
            if (clientToMazeName.ContainsKey(client))
            {
                string mazeName = clientToMazeName[client];
                Game game = mazeNameToGame[mazeName];
                Maze maze = multiPlayerMazes[mazeName];
                if (activeGames.ContainsKey(maze.Name))
                {
                    //player.Way.Add(move);
                    return maze;
                }
            }
            return null;
        }

        public void CloseGame(string name)
        {
            if (singlePlayerMazes.ContainsKey(name))
            {
                singlePlayerMazes.Remove(name);
                if (singlePlayerSolved.ContainsKey(name))
                {
                    singlePlayerSolved.Remove(name);
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

        public Player GetPlayerToSendMove(TcpClient tcc)
        {
            Game game = mazeNameToGame[clientToMazeName[tcc]];
            return game.GetOtherPlayer(tcc);
        }
    }
}
