using MazeLib;
using System.Net.Sockets;
using SearchAlgorithmsLib;
using Ex1;

namespace Command
{
    internal class RunBFSCommand : Command
    {

        public RunBFSCommand(IModel model) : base(model)
        {
        }

        public override string Execute(string[] args, TcpClient client)
        {
            return Model.SolveMaze(args[0], new BFS<Position>().search).ToString();
        }
    }
}