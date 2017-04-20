using MazeLib;
using System;
using System.Net.Sockets;

namespace MVC
{
    public class StartGameCommand : Command
    {
        private IClientHandler ic;

        public StartGameCommand(IModel model, IClientHandler clientHandle) : base(model)
        {
            ic = clientHandle;
        }

        public override Status Execute(string[] args, TcpClient client)
        {
            try
            {
                if (args.Length != 3)
                {
                    Stat.SetStatues(Status.Disconnect, "Parameter does not match");
                    ic.SendToClient(Stat.ToJson(), client);
                    return Status.Disconnect;
                }
                string name = args[0];
                int rows = int.Parse(args[1]);
                int cols = int.Parse(args[2]);
                Maze maze = Model.StartGame(name, rows, cols, client);
                if (maze == null)
                {
                    Stat.SetStatues(Status.Disconnect, "Error: Name Already taken");
                    ic.SendToClient(Stat.ToJson(), client);
                    return Status.Disconnect;
                }
                Stat.SetStatues(Status.KeepConnection, maze.ToJSON());
                ic.SendToClient(Stat.ToJson(), client);
                return Status.KeepConnection;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Status.Disconnect;
        }
    }
}