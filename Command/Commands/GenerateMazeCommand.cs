using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using System.Net.Sockets;

namespace MVC
{
    public class GenerateMazeCommand : Command
    {
        public GenerateMazeCommand(IModel model) : base(model) { }

        public override string Execute(string[] args, TcpClient client)
        {
            try
            {
                if (args.Length != 3)
                {
                    return "Parameter does not match";
                }
                string name = args[0];
                int rows = int.Parse(args[1]);
                int cols = int.Parse(args[2]);
                Maze maze = Model.SingleGameGenerateMaze(name, rows, cols);
                return maze.ToJSON();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }
    }
}
