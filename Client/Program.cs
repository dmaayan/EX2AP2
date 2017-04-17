
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
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
        private static TcpClient client;
        private static IPEndPoint ep;

        static void Main(string[] args)
        {
            ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
            client = new TcpClient();
            client.Connect(ep);
            Console.WriteLine("You are connected!");
            using (stream = client.GetStream())
            using (reader = new BinaryReader(stream))
            using (writer = new BinaryWriter(stream))
            {
                string command = "";
                while (!command.Contains("close"))
                {
                    try
                    {
                        // Send data to server
                        Console.Write("Please enter a command: \n");
                        command = Console.ReadLine();
                        writer.Write(command);
                        mr.open();
                        // Get result from server
                        string result = mr.GetMassage();
                        if ((command.Contains("start") || command.Contains("join"))
                             && !result.Contains("Error"))
                        {
                            StartMultiplay();
                        }
                        Console.WriteLine("{0}", result);
                    } catch (Exception e)
                    {
                        Console.WriteLine(e);
                        break;
                    }
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
                    try
                    {
                        string otherPlayerMove = reader.ReadString();
                        if (otherPlayerMove.Contains("close"))
                        {
                            active = false;
                        }
                        writer.Write("Received move");
                        Console.WriteLine(otherPlayerMove);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        break;
                    }
                }
            }, ct);
        }

        private static void StopMultiplay()
        {
            ts.Cancel();
        }

        public class MassageReceiver {
            private string massageReceived;
            private Task receiver;
            private bool isOpen;

            public MassageReceiver()
            {
                massageReceived = null;
                isOpen = false;
            }

            public void open()
            {
                client.Connect(ep);
                isOpen = true;
                receiver = new Task(() => {
                    using (stream = client.GetStream())
                    using (reader = new BinaryReader(stream))
                    {
                        while (isOpen)
                        {
                            if (massageReceived == null)
                            {
                                string current = reader.ReadString();
                                if (current.Contains("\"Direction\": "))
                                {
                                    Console.WriteLine(current);
                                    continue;
                                }
                                massageReceived = current;
                            }
                            else
                            {
                                Thread.Sleep(1);
                            }
                        }
                    }
                });
                receiver.Start();
            }

            public string GetMassage()
            {
                while (massageReceived == null)
                {
                    Thread.Sleep(1);
                }
                string current = massageReceived;
                massageReceived = null;
                return current;
            }

            public void close()
            {
                isOpen = false;
                receiver.Wait();
                client.Close();
            }
        }
    }
}