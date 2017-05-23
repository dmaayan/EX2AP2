using MazeLib;
using System.Collections.Generic;
using System.Linq;
using MazeGeneratorLib;
using SearchAlgorithmsLib;
using System.Net.Sockets;

namespace MVC
{
    /// <summary>
    /// the maze model
    /// </summary>
    public class Model : IModel
    {
        /// <summary>
        /// all the games waiting for a second player
        /// </summary>
        private Dictionary<string, Maze> waitingList;

        /// <summary>
        /// all games currently active
        /// </summary>
        private Dictionary<string, Maze> activeGames;

        /// <summary>
        /// all the single game mazes
        /// </summary>
        private Dictionary<string, Maze> singlePlayerMazes;

        /// <summary>
        /// all the multiplayer games
        /// </summary>
        private Dictionary<string, Maze> multiPlayerMazes;

        /// <summary>
        /// all the solutions to the single game mazes
        /// </summary>
        private Dictionary<string, MazeSolution> singlePlayerSolved;

        /// <summary>
        /// maze name to game dictionary
        /// </summary>
        private Dictionary<string, Game> mazeNameToGame;

        /// <summary>
        /// client to maze dictionary
        /// </summary>
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
                CloseSingleGame(name);
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
            // add the new maze
            multiPlayerMazes.Add(name, maze);
            return maze;
        }

        /// <summary>
        /// solve a maze
        /// </summary>
        /// <param name="name">the name of the maze</param>
        /// <param name="searcher">the algorithem to solve with</param>
        /// <returns>the solution to the maze</returns>
        public MazeSolution SolveMaze(string name, SearchAlgo searcher)
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
            // save the current solution
            singlePlayerSolved.Add(name, ms);
            return ms;
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
            // create a multiplay game
            Maze maze = MultiGameGenerateMaze(name, rows, cols);
            // checks that the maze is ok
            if (maze == null)
            {
                return null;
            }
            // add the maze to the waiting list
            string mazeName = maze.Name;
            waitingList.Add(name, maze);

            // create the game with the player
            Player p = new Player(client);
            mazeNameToGame.Add(mazeName, new Game(p));

            // wait for a player to join the game
            mazeNameToGame[mazeName].waitForOtherPlayer();
            clientToMazeName.Add(client, mazeName);
            return maze;
        }

        /// <summary>
        /// join a game
        /// </summary>
        /// <param name="name"> of the game to join </param>
        /// <param name="client"> that requested to join</param>
        /// <returns>the maze of the game</returns>
        public Maze JoinGame(string name, TcpClient client)
        {
            // check for available maze with that name
            if (waitingList.ContainsKey(name))
            {
                Maze maze = waitingList[name];
                string mazeName = maze.Name;

                // put the game in the active games list and remove from waiting
                activeGames.Add(name, maze);
                waitingList.Remove(name);

                // add the player to the game
                Player player = new Player(client);
                mazeNameToGame[mazeName].AddPlayer(player);
                clientToMazeName.Add(client, mazeName);
                return maze;
            }
            return null;
        }

        /// <summary>
        /// play a move in the game
        /// </summary>
        /// <param name="move"> Direction to play</param>
        /// <param name="client">that moved</param>
        /// <returns>the maze played by this client</returns>
        public Maze PlayGame(Direction move, TcpClient client)
        {
            // checks for the maze
            if (clientToMazeName.ContainsKey(client))
            {
                //get the maze info
                string mazeName = clientToMazeName[client];
                Game game = mazeNameToGame[mazeName];
                Maze maze = multiPlayerMazes[mazeName];

                // checks that the game is active
                if (activeGames.ContainsKey(maze.Name))
                {
                    //player.Way.Add(move);
                    return maze;
                }
            }
            return null;
        }

        /// <summary>
        /// close a single game
        /// </summary>
        /// <param name="name">of the maze to close</param>
        private void CloseSingleGame(string name)
        {
            // remove the maze if found
            if (singlePlayerMazes.ContainsKey(name))
            {
                singlePlayerMazes.Remove(name);
                if (singlePlayerSolved.ContainsKey(name))
                {
                    singlePlayerSolved.Remove(name);
                }
            }
            
        }

        /// <summary>
        /// close a multiplayer game
        /// </summary>
        /// <param name="name">of the maze  to close</param>
        /// <param name="client">that requested to close the game</param>
        /// <returns>the closed game</returns>
        public Game CloseGame(string name, TcpClient client)
        {
            //if the game is on
            if (activeGames.ContainsKey(name) && IsLegalToClose(name, client))
            {
                //remove from active list and multiplayer mazes list
                activeGames.Remove(name);
                multiPlayerMazes.Remove(name);

                // remove the players and clients
                Game game = mazeNameToGame[name];
                mazeNameToGame.Remove(name);
                clientToMazeName.Remove(game.FirstPlayer.Client);
                clientToMazeName.Remove(game.SecondPlayer.Client);
                return game;
            }
            return null;
        }

        /// <summary>
        /// checks if the client had permision to close the game
        /// </summary>
        /// <param name="name">of the maze  to close</param>
        /// <param name="client">that requested to close the game</param>
        /// <returns>true if legal else false</returns>
        private bool IsLegalToClose(string name, TcpClient client)
        {
            if (mazeNameToGame.ContainsKey(name))
            {
                // get the game and other player
                Game game = mazeNameToGame[name];
                Player otherPlayer = game.GetOtherPlayer(client);
                // player is null if the client is not a part of this game
                return otherPlayer != null;
            }
            return false;
        }

        /// <summary>
        /// list all waiting games names
        /// </summary>
        /// <returns>array of waiting games names</returns>
        public string[] GetAllNames()
        {
            return waitingList.Keys.ToArray();
        }
        
        /// <summary>
        /// get the player that plays against the client
        /// </summary>
        /// <param name="client">to get the other player</param>
        /// <returns>the other player</returns>
        public Player GetOtherPlayer(TcpClient client)
        {
            // checks for avtive game
            if (!clientToMazeName.ContainsKey(client))
            {
                return null;
            }
            Game game = mazeNameToGame[clientToMazeName[client]];
            return game.GetOtherPlayer(client);
        }

        /// <summary>
        /// get the game of a player
        /// </summary>
        /// <param name="client">to request its game</param>
        /// <returns>the game of the player</returns>
        public Game GetGameOfPlayer(TcpClient client)
        {
            // checks for active game with this client
            if (!clientToMazeName.ContainsKey(client))
            {
                return null;
            }
            return mazeNameToGame[clientToMazeName[client]];
        }
    }
}
