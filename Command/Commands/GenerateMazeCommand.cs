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
        private IClientHandler ic;

        public GenerateMazeCommand(IModel model, IClientHandler clientHandle) : base(model)
        {
            ic = clientHandle;
        }

        public override Status Execute(string[] args, TcpClient client)
        {
            try
            {
                if (args.Length != 3)
                {
                    Stat.SetStatues(Status.Error, "Parameter does not match");
                    ic.SendToClient(Stat.ToJson(), client);
                    return Status.Disconnect;
                }
                string name = args[0];
                int rows = int.Parse(args[1]);
                int cols = int.Parse(args[2]);
                Maze maze = Model.SingleGameGenerateMaze(name, rows, cols);
                Stat.SetStatues(Status.Disconnect, maze.ToJSON());
                ic.SendToClient(Stat.ToJson(), client);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Status.Disconnect;
        }
    }
}
