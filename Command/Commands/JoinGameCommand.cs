using MazeLib;
using System.Net.Sockets;

namespace MVC
{
    public class JoinGameCommand : Command
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="m">model</param>
        /// <param name="ic">view</param>
        public JoinGameCommand(IModel model, IClientHandler clientHandle) : base(model, clientHandle) { }

        /// <summary>
        /// executes the command given
        /// </summary>
        /// <param name="args">command received from the client</param>
        /// <param name="client">the client to give the command</param>
        /// <returns>the status of the command</returns>
        public override Status Execute(string[] args, TcpClient client)
        {
            // checks that the argument length is valid
            if (args.Length != 1)
            {
                // set the statues, send to client and return
                Stat.SetStatues(Status.Disconnect, "Parameter does not match");
                Handler.SendToClient(Stat.ToJson(), client);
                return Status.Disconnect;
            }
            // get the maze
            Maze maze = Model.JoinGame(args[0], client);
            if (maze == null)
            {
                // set the statues with the error message, send to client and return
                Stat.SetStatues(Status.Disconnect, "Error: Can't find game named: " + args[0]);
                Handler.SendToClient(Stat.ToJson(), client);
                return Status.Disconnect;
            }
            // set the statues with the maze, send to client and return
            Stat.SetStatues(Status.KeepConnection, maze.ToJSON());
            Handler.SendToClient(Stat.ToJson(), client);
            return Status.KeepConnection;
        }
    }
}