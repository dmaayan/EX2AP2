using MVC;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// server class.
    /// </summary>
    public class Server
    {
        private TcpListener listener;
        private IClientHandler ch;
        private IPEndPoint ep;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="port">of the connection</param>
        /// <param name="ip">of the connection</param>
        /// <param name="ch">to handle incomming clients</param>
        public Server(int port, IPAddress ip, IClientHandler ch)
        {
            this.ch = ch;
            ep = new IPEndPoint(ip, port);
        }

        /// <summary>
        /// starts the server. runs the listener
        /// </summary>
        public void Start()
        {
            // start listening for incoming clients
            listener = new TcpListener(ep);
            listener.Start();
            Console.WriteLine("Waiting for connections...");
            // the listening task
            Task task = new Task(() =>
            {
                while (true)
                {
                    try
                    {
                        // receive a client and handle it
                        TcpClient client = listener.AcceptTcpClient();
                        Console.WriteLine("Got new connection");
                        ch.HandleClient(client);
                    }
                    catch (SocketException)
                    {
                        break;
                    }
                }
                Console.WriteLine("Server stopped");
            });
            // start the listening task
            task.Start();
            task.Wait();
        }

        /// <summary>
        /// stop the server. stop listening
        /// </summary>
        public void Stop()
        {
            listener.Stop();
        }
    }
}