using MazeLib;
using System;
using System.Net.Sockets;

namespace MVC
{
    /// <summary>
    /// Start Game Command
    /// </summary>
    public class StartGameCommand : Command
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="m">model</param>
        /// <param name="ic">view</param>
        public StartGameCommand(IModel model, IClientHandler clientHandle) : 
            base(model, clientHandle) { }

        /// <summary>
        /// executes the command given
        /// </summary>
        /// <param name="args">command received from the client</param>
        /// <param name="client">the client to give the command</param>
        /// <returns>the status of the command</returns>
        public override Status Execute(string[] args, TcpClient client)
        {
            try
            {
                // checks that the argument length is valid
                if (args.Length != 3)
                {
                    // set the statues with the message, send to client and return
                    Stat.SetStatues(Status.Disconnect, "Parameter does not match");
                    Handler.SendToClient(Stat.ToJson(), client);
                    return Status.Disconnect;
                }
                // get the name and sizes
                string name = args[0];
                int rows = int.Parse(args[1]);
                int cols = int.Parse(args[2]);

                // create a multiplayer maze
                Maze maze = Model.StartGame(name, rows, cols, client);
                // checks for valid maze
                if (maze == null)
                {
                    // set the statues with the error message, send to client and return
                    Stat.SetStatues(Status.Disconnect, "Error: Name Already taken");
                    Handler.SendToClient(Stat.ToJson(), client);
                    return Status.Disconnect;
                }
                // set the statues with the maze, send to client and return
                Stat.SetStatues(Status.KeepConnection, maze.ToJSON());
                Handler.SendToClient(Stat.ToJson(), client);
                return Status.KeepConnection;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Status.Disconnect;
        }
    }
}