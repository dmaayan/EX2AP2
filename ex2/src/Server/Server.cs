using MVC;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Server
{
    /// <summary>
    /// server class.
    /// </summary>
    public class Server
    {
        /// <summary>
        /// listener for accepting new clients
        /// </summary>
        private TcpListener listener;
        /// <summary>
        /// client handler to handle new clients
        /// </summary>
        private IClientHandler ch;
        /// <summary>
        /// connection end point
        /// </summary>
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