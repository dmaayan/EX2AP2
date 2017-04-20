using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace MVC
{
    public class CloseGameCommand : Command
    {
        private IClientHandler ic;
        public CloseGameCommand(IModel model, IClientHandler clientHandle) : base(model)
        {
            ic = clientHandle;
        }

        public override Status Execute(string[] args, TcpClient client)
        {
            if (args.Length != 1)
            {
                Stat.SetStatues(Status.KeepConnection, "Parameter does not match");
                ic.SendToClient(Stat.ToJson(), client);
                return Status.KeepConnection;
            }
            string name = args[0];
            Game game = Model.CloseGame(name);
            if (game == null)
            {
                Stat.SetStatues(Status.KeepConnection, "No active game with that name: " + name);
                ic.SendToClient(Stat.ToJson(), client);
                return Status.KeepConnection;
            }
            Stat.SetStatues(Status.Close, "Game named: " + name + " have been closed");
            ic.SendToClient(Stat.ToJson(), client);

            TcpClient otherClient = game.GetOtherPlayer(client).Client;
            Stat.SetStatues(Status.PrintAndStop, "Game named: " + name + " have been closed");
            ic.SendToClient(Stat.ToJson(), otherClient);
            return Status.Disconnect;
        }
    }
}
