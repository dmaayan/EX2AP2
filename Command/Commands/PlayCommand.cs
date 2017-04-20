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

        public override Status Execute(string[] args, TcpClient client)
        {
            Stat.SetStatues(Status.KeepConnection, "Parameter does not match");
            if (args.Length != 1)
            {
                ic.SendToClient(Stat.ToJson(), client);
                return Status.KeepConnection;
            }

            String move = char.ToUpper(args[0].ToLower()[0]) + args[0].ToLower().Substring(1);
            if (!Enum.IsDefined(typeof(Direction), move))
            {
                ic.SendToClient(Stat.ToJson(), client);
                return Status.KeepConnection;
            }

            Direction direction = (Direction) Enum.Parse(typeof(Direction), move);
            Maze maze = Model.PlayGame(direction, client);
            if (maze == null)
            {
                Stat.SetStatues(Status.Close, "Game is not on");
                ic.SendToClient(Stat.ToJson(), client);
                return Status.Close;
            }
            Player otherPlayer = Model.GetOtherPlayer(client);
            if (otherPlayer == null)
            {
                Stat.SetStatues(Status.Close, "No other player to send move to");
                ic.SendToClient(Stat.ToJson(), client);
                return Status.Close;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("{");
            sb.AppendLine("  \"Name\": \"" + maze.Name + "\"");
            sb.AppendLine("  \"Direction\": \"" + move + "\"");
            sb.AppendLine("}");

            Stat.SetStatues(Status.PrintAndContinue, sb.ToString());
            ic.SendToClient(Stat.ToJson(), otherPlayer.Client);
            return Status.KeepConnection;
        }
    }
}