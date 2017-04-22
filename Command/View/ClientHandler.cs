using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MVC;

namespace MVC
{
    public class ClientHandler : IClientHandler
    {
        /// <summary>
        /// controller to interact with
        /// </summary>
        IController controller;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="c">controller</param>
        public ClientHandler(IController c)
        {
            controller = c;
        }

        /// <summary>
        /// handles a client accepted by the server
        /// </summary>
        /// <param name="client">to handle</param>
        public void HandleClient(TcpClient client)
        {
            // create new task to read and write from a client
            new Task(() =>
            {
                // open the stream, reader and writer
                using (NetworkStream stream = client.GetStream())
                using (BinaryReader reader = new BinaryReader(stream))
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    Status result = Status.KeepConnection;
                    // do until status is KeepConnection
                    do
                    {
                        try
                        {
                            // get command from client
                            string commandLine = reader.ReadString();
                            Console.WriteLine("Got command: {0}", commandLine);
                            // execute the command using the controller
                            result = controller.ExecuteCommand(commandLine, client);
                        }
                        catch (Exception e)
                        {
                            break;
                        }
                    } while (result == Status.KeepConnection);
                }
                // close the client
                client.Close();
            }).Start();
        }

        /// <summary>
        /// sends a message to a specific client
        /// </summary>
        /// <param name="s">message to send</param>
        /// <param name="client">send to client</param>
        public void SendToClient(string s, TcpClient client)
        {
            // open stream and reader
            NetworkStream stream = client.GetStream();
            BinaryWriter writer = new BinaryWriter(stream);
            // send to client
            writer.Write(s);
        }
    }
}
