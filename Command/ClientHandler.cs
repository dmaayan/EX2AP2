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
                    string commandLine = "";
                    //** לשנות את התנאי
                    while (!commandLine.Contains("close"))
                    {
                        try
                        {
                            commandLine = reader.ReadString();
                            Console.WriteLine("Got command: {0}", commandLine);
                            string result = controller.ExecuteCommand(commandLine, client);
                            writer.Write(result);
                        }
                        //**לשנות את האקספשין 
                        catch (IOException e)
                        {
                            Console.WriteLine(e);
                            break;
                        }
                    }

                }
                client.Close();
            }).Start();
        }

        public void SendToClient(string s, TcpClient client)
        {
            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(s);
                Console.WriteLine("answer sent");
                string result = reader.ReadString();
                if (result.Equals("Received move"))
                {

                }
            }
        }
    }
}
