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
        private IClientHandler ic;

        public SolveMazeCommand(IModel model, IClientHandler clientHandle) : base(model)
        {
            ic = clientHandle;
        }

        public override Status Execute(string[] args, TcpClient client)
        {
            if (args.Length != 2 || !options.Contains(args[1]))
            {
                Stat.SetStatues(Status.Disconnect, "Parameter does not match");
                ic.SendToClient(Stat.ToJson(), client);
                return Status.Disconnect;
            }
            string[] newArgs = args.Take(1).ToArray();
            MazeSolution ms;
            if (args[1].Equals(options[0]))
            {
                ms = Model.SolveMaze(args[0], new BFS<Position>().search);
            }
            else
            {
                ms = Model.SolveMaze(args[0], new DFS<Position>().search);
            }
            if (ms == null)
            {
                Stat.SetStatues(Status.Disconnect, "No solution possible");
                ic.SendToClient(Stat.ToJson(), client);
            }
            else
            {
                Stat.SetStatues(Status.Disconnect, ms.ToJson());
                ic.SendToClient(Stat.ToJson(), client);
            }
            return Status.Disconnect;

        }
    }
}