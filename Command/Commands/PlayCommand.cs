using MazeLib;
using System;
using System.Net.Sockets;
using System.Text;

namespace MVC
{
    public class PlayCommand : Command
    {
        private IClientHandler ic;

        public PlayCommand(IModel model, IClientHandler clientHandler) : base(model)
        {
            ic = clientHandler;
        }

        public override string Execute(string[] args, TcpClient client)
        {
            if (args.Length != 1)
            {
                return "Parameter does not match";
            }
            String move = args[0].ToLower();
            move = char.ToUpper(move[0]) + move.Substring(1);
            Direction direction = (Direction) Enum.Parse(typeof(Direction), move);
            Maze maze = Model.PlayGame(direction, client);
            Player otherPlayer = Model.GetPlayerToSendMove(client);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("{");
            sb.AppendLine("  \"Name\": \"" + maze.Name + "\"");
            sb.AppendLine("  \"Direction\": \"" + move + "\"");
            sb.AppendLine("}");
            ic.SendToClient(sb.ToString(), otherPlayer.Client);
            return "Sent Massage to other player";
        }
    }
}