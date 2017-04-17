
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        private static NetworkStream stream;
        private static BinaryReader reader;
        private static BinaryWriter writer;
        private static CancellationTokenSource ts;


        static void Main(string[] args)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
            TcpClient client = new TcpClient();
            client.Connect(ep);
            Console.WriteLine("You are connected!");
            using (stream = client.GetStream())
            using (reader = new BinaryReader(stream))
            using (writer = new BinaryWriter(stream))
            {
                string command = "";
                while (!command.Contains("close"))
                {
                    // Send data to server
                    Console.Write("Please enter a command: \n");
                    command = Console.ReadLine();
                    writer.Write(command);
                    // Get result from server
                    string result = reader.ReadString();
                    if ((command.Contains("start") || command.Contains("join"))
                         && !result.Contains("Error"))
                    {
                        StartMultiplay();
                    }
                    Console.WriteLine("{0}", result);
                }
            }
            StopMultiplay();
            client.Close();

        }

        private static void StartMultiplay()
        {
            ts = new CancellationTokenSource();
            CancellationToken ct = ts.Token;
            Task multiplay = Task.Factory.StartNew(() =>
            {
                bool active = true;
                while (active)
                {
                    string otherPlayerMove = reader.ReadString();
                    if (otherPlayerMove.Contains("close"))
                    {
                        active = false;
                    }
                    writer.Write("Received move");
                    Console.WriteLine(otherPlayerMove);
                }
            }, ct);
        }

        private static void StopMultiplay()
        {
            ts.Cancel();
        }
    }
}