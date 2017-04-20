using MazeLib;
using System.Net.Sockets;

namespace MVC
{
    public class JoinGameCommand : Command
    {
        private IClientHandler ic;

        public JoinGameCommand(IModel model, IClientHandler clientHandle) : base(model)
        {
            ic = clientHandle;
        }

        public override Status Execute(string[] args, TcpClient client)
        {
            if (args.Length != 1)
            {
                Stat.SetStatues(Status.Disconnect, "Parameter does not match");
                ic.SendToClient(Stat.ToJson(), client);
                return Status.Disconnect;
            }
            Maze maze = Model.JoinGame(args[0], client);
            if (maze == null)
            {
                Stat.SetStatues(Status.Disconnect, "Error: Can't find game named: " + args[0]);
                ic.SendToClient(Stat.ToJson(), client);
                return Status.Disconnect;
            }
            Stat.SetStatues(Status.KeepConnection, maze.ToJSON());
            ic.SendToClient(Stat.ToJson(), client);
            return Status.KeepConnection;
        }
    }
}