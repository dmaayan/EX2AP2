﻿using MVC;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// server
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // start and set the MVC items
            IController c = new Controller();
            IModel model = new Model();
            IClientHandler ic = new ClientHandler(c);
            c.View = ic;
            c.SetModel(model);

            // get connection info from app.config file
            int port = int.Parse(ConfigurationManager.AppSettings["port"].ToString());
            IPAddress ip = IPAddress.Parse(ConfigurationManager.AppSettings["server"].ToString());

            // start the server working
            Server server = new Server(port, ip, ic);
            server.Start();
            server.Stop();
        }
    }
}
