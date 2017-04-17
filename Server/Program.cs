using MVC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            IController c = new Controller();
            IModel model = new Model();
            IClientHandler ic = new ClientHandler(c);
            c.View = ic;
            c.SetModel(model);
            Server server = new Server(int.Parse(args[0]), ic);
            server.Start();
            server.Stop();
        }
    }
}
