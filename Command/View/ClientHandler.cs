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
                            if (commandLine.Equals("Received move"))
                            {
                                continue;
                            }
                            Console.WriteLine("Got command: {0}", commandLine);
                            string result = controller.ExecuteCommand(commandLine, client);
                            if (result.Equals("Sent Massage to other player"))
                            {
                                continue;
                            }
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
            NetworkStream stream = client.GetStream();
            BinaryReader reader = new BinaryReader(stream);
            BinaryWriter writer = new BinaryWriter(stream);

            writer.Write(s);
            Console.WriteLine("answer sent");
            //****לבדוק אם צריך 
            //string result = reader.ReadString();


        }
    }
}
