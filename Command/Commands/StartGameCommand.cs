using MazeLib;
using System;
using System.Net.Sockets;

namespace MVC
{
    public class StartGameCommand : Command
    {
        public StartGameCommand(IModel model) : base(model) { }

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
                Maze maze = Model.StartGame(name, rows, cols, client);
                if (maze == null)
                {
                    return "Error: Name Already taken";
                }
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