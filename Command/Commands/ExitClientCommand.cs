using MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MVC
{
    public class ExitClientCommand : Command
    {
        private IClientHandler ic;

        public ExitClientCommand(IModel model, IClientHandler clientHandle) : base(model)
        {
            ic = clientHandle;
        }

        public override Status Execute(string[] args, TcpClient client)
        {
            Game game = Model.GetGameOfPlayer(client);
            if (game == null)
            {
                Stat.SetStatues(Status.Exit, "No active game to close");
                ic.SendToClient(Stat.ToJson(), client);
                return Status.Disconnect;
            }
            Stat.SetStatues(Status.Exit, "Game is closing");
            ic.SendToClient(Stat.ToJson(), client);

            Player otherPlayer = game.GetOtherPlayer(client);
            Stat.SetStatues(Status.Close, "Closed game");
            ic.SendToClient(Stat.ToJson(), otherPlayer.Client);
            return Status.Disconnect;
        }
    }
}
