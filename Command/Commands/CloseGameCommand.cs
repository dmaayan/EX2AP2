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
        public CloseGameCommand(IModel model) : base(model) { }

        public override string Execute(string[] args, TcpClient client)
        {
            string name = args[0];
            Model.CloseGame(name);
            return "Game closed";
        }
    }
}
