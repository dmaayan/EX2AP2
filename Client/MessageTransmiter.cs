using MVC;
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
    public class MessageTransmiter
    {
        private TcpClient client;
        private IPEndPoint ep;
        private NetworkStream stream;
        private BinaryReader reader;
        private BinaryWriter writer;
        private string massageReceived;
        private Task receiver;
        private bool isOpen;
        private bool isMultiActive;

        public MessageTransmiter(IPEndPoint endPoint)
        {
            ep = endPoint;
            massageReceived = null;
            isOpen = false;
            isMultiActive = false;
        }

        public bool IsMultiActive
        {
            get { return isMultiActive; }
            set { isMultiActive = value; }
        }

        public void open()
        {
            massageReceived = null;
            isMultiActive = true;
            isOpen = true;

            client = new TcpClient();
            client.Connect(ep);
            stream = client.GetStream();
            reader = new BinaryReader(stream);
            writer = new BinaryWriter(stream);
            receiver = new Task(() =>
            {
                while (isOpen)
                {
                    if (massageReceived == null)
                    {
                        string result;
                        Statues statues;
                        try
                        {
                            result = reader.ReadString();
                            statues = Statues.FromJson(result);
                        } catch (Exception e)
                        {
                            statues = new Statues();
                            statues.SetStatues(Status.Close, "Error");
                            result = "Error";
                            break;
                        }
                        switch (statues.Stat)
                        {
                            case Status.Disconnect:
                                {
                                    isMultiActive = false;
                                    isOpen = false;
                                    break;
                                }
                            case Status.KeepConnection:
                                {
                                    break;
                                }
                            case Status.Close:
                                {
                                    Close();
                                    break;
                                }
                            case Status.Exit:
                                {
                                    break;
                                }
                            case Status.PrintAndContinue:
                                {
                                    Console.WriteLine(statues.Message);
                                    continue;
                                }
                            case Status.PrintAndStop:
                                {
                                    Console.WriteLine(statues.Message);
                                    writer.Write("exit");
                                    string feedback = reader.ReadString();
                                    Close();
                                    break;
                                }
                            case Status.Error:
                                {
                                    Console.WriteLine(statues.Message);
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                        massageReceived = result;
                    }
                    else
                    {
                        Thread.Sleep(1);
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

        public void Close()
        {
            isMultiActive = false;
            isOpen = false;
            writer.Dispose();
            reader.Dispose();
            stream.Dispose();
            client.Close();
        }

        public void Exit()
        {
            if (isOpen)
            {
                isMultiActive = false;
                isOpen = false;
                writer.Write("exit");
                GetMassage();
                receiver.Wait();
                stream.Dispose();
                reader.Dispose();
                writer.Dispose();
                client.Close();
            }
        }

        public void SendMessage(string message)
        {
            if (!isOpen)
            {
                open();
            }
            writer.Write(message);
            Console.WriteLine("message: " + message + " have been sent");
        }

        public void SendSingleMessage(string message)
        {
            TcpClient newClient = new TcpClient();
            
            newClient.Connect(ep);
            using (NetworkStream newStream = newClient.GetStream())
            using (BinaryWriter newWriter = new BinaryWriter(newStream))
            using (BinaryReader newReader = new BinaryReader(newStream))
            {
                newWriter.Write(message);
                string result = newReader.ReadString();
                Statues statues = Statues.FromJson(result);
                Console.WriteLine(statues.Message);
            }
            newClient.Close();
        }
        
    }
}
