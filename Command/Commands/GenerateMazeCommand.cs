using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using System.Net.Sockets;
using Newtonsoft.Json;

namespace MVC
{
    public class GenerateMazeCommand : Command
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="m">model</param>
        /// <param name="ic">view</param>
        public GenerateMazeCommand(IModel model, IClientHandler clientHandle) : base(model, clientHandle) { }

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
                    // set the statues, send to client and return
                    Stat.SetStatues(Status.Error, "Parameter does not match");
                    Handler.SendToClient(Stat.ToJson(), client);
                    return Status.Disconnect;
                }
                // get the name and sizes
                string name = args[0];
                int rows = int.Parse(args[1]);
                int cols = int.Parse(args[2]);

                // create a maze
                Maze maze = Model.SingleGameGenerateMaze(name, rows, cols);
                // send it back to the client
                Stat.SetStatues(Status.Disconnect, maze.ToJSON());
                Handler.SendToClient(Stat.ToJson(), client);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Status.Disconnect;
        }
    }
}
