using System.Collections.Generic;
using System.Net.Sockets;
using System.Linq;
using SearchAlgorithmsLib;
using MazeLib;

namespace MVC
{
    public class SolveMazeCommand : Command
    {
        private string[] options = {"0", "1"};
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="m">model</param>
        /// <param name="ic">view</param>
        public SolveMazeCommand(IModel model, IClientHandler clientHandle) : base(model, clientHandle) { }

        /// <summary>
        /// executes the command given
        /// </summary>
        /// <param name="args">command received from the client</param>
        /// <param name="client">the client to give the command</param>
        /// <returns>the status of the command</returns>
        public override Status Execute(string[] args, TcpClient client)
        {
            // checks that the argument length and content are valid
            if (args.Length != 2 || !options.Contains(args[1]))
            {
                // set the statues with the message, send to client and return
                Stat.SetStatues(Status.Disconnect, "Parameter does not match");
                Handler.SendToClient(Stat.ToJson(), client);
                return Status.Disconnect;
            }
            MazeSolution ms;
            // apply the right algorithm according to the command
            if (args[1].Equals(options[0]))
            {
                ms = Model.SolveMaze(args[0], new BFS<Position>().Search);
            }
            else
            {
                ms = Model.SolveMaze(args[0], new DFS<Position>().Search);
            }
            // checks for valid return info
            if (ms == null)
            {
                Stat.SetStatues(Status.Disconnect, "No solution possible");
                Handler.SendToClient(Stat.ToJson(), client);
            }
            else
            {
                // set the statues with the maze solution and send to client
                Stat.SetStatues(Status.Disconnect, ms.ToJson());
                Handler.SendToClient(Stat.ToJson(), client);
            }
            return Status.Disconnect;

        }
    }
}