using MazeLib;
using System.Net.Sockets;

namespace MVC
{
    public class JoinGameCommand : Command
    {

        public JoinGameCommand(IModel model) : base(model) { }

        public override string Execute(string[] args, TcpClient client)
        {
            return Model.JoinGame(args[0], client).ToJSON();
        }
    }
}