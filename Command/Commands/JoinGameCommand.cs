using MazeLib;
using System.Net.Sockets;

namespace MVC
{
    public class JoinGameCommand : Command
    {

        public JoinGameCommand(IModel model) : base(model) { }

        public override string Execute(string[] args, TcpClient client)
        {
            if (args.Length != 1)
            {
                return "Parameter does not match";
            }
            Maze maze = Model.JoinGame(args[0], client);
            if (maze == null)
            {
                return "Error: Can't find game named: " + args[0];
            }
            return maze.ToJSON();
        }
    }
}