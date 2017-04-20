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
        IController controller;

        public ClientHandler(IController c)
        {
            controller = c;
        }

        public void HandleClient(TcpClient client)
        {
            new Task(() =>
            {
                using (NetworkStream stream = client.GetStream())
                using (BinaryReader reader = new BinaryReader(stream))
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    Status result = Status.KeepConnection;
                    do
                    {
                        try
                        {
                            string commandLine = reader.ReadString();
                            Console.WriteLine("Got command: {0}", commandLine);
                            result = controller.ExecuteCommand(commandLine, client);
                        }
                        catch (Exception e)
                        {
                            break;
                        }
                    } while (result == Status.KeepConnection);
                }
                client.Close();
            }).Start();
        }

        public void SendToClient(string s, TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(s);
        }
    }
}
