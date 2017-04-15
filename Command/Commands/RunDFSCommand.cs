using MazeLib;
using System.Net.Sockets;
using SearchAlgorithmsLib;
using Ex1;

namespace Command
{
    internal class RunDFSCommand : Command
    {

        public RunDFSCommand(IModel model) : base(model)
        {
        }

        public override string Execute(string[] args, TcpClient client)
        {
            return Model.SolveMaze(args[0], new DFSAlgo<Position>().search).ToString();
        }
    }
}