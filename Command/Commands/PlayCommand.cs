using MazeLib;
using System;
using System.Net.Sockets;

namespace MVC
{
    public class PlayCommand : Command
    {

        public PlayCommand(IModel model) : base(model) { }

        public override string Execute(string[] args, TcpClient client)
        {
            string move = args[0];
            Direction direction = (Direction) Enum.Parse(typeof(Direction), move);
            Maze maze = Model.PlayGame(direction, client);
            return maze.ToJSON();
        }
    }
}