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
    /// <summary>
    /// sends and received messages to and from the server.
    /// </summary>
    public class MessageTransmiter
    {
        private TcpClient client;
        private IPEndPoint ep;
        private NetworkStream stream;
        private BinaryReader reader;
        private BinaryWriter writer;
        private string messageReceived;
        private Task receiver;
        private bool isOpen;
        private bool isMultiActive;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="endPoint">for the connection</param>
        public MessageTransmiter(IPEndPoint endPoint)
        {
            ep = endPoint;
            messageReceived = null;
            isOpen = false;
            isMultiActive = false;
        }

        /// <summary>
        /// property
        /// </summary>
        public bool IsMultiActive
        {
            get { return isMultiActive; }
            set { isMultiActive = value; }
        }

        /// <summary>
        /// opens a new multiplayer connection to the client
        /// </summary>
        public void open()
        {
            // sets bools to true and message to null
            messageReceived = null;
            isMultiActive = true;
            isOpen = true;

            // create a client and open the streams
            client = new TcpClient();
            client.Connect(ep);
            stream = client.GetStream();
            reader = new BinaryReader(stream);
            writer = new BinaryWriter(stream);

            // task to listen for the receiving stream. ready for messages from the server
            receiver = new Task(() =>
            {
                // do as long as the isOpen bool is true
                while (isOpen)
                {
                    // read only if the last message received was already taken
                    if (messageReceived == null)
                    {
                        string result;
                        Statues statues;
                        try
                        {
                            // get a message from the server and parse it to Statues
                            result = reader.ReadString();
                            statues = Statues.FromJson(result);
                        } catch (Exception e)
                        {
                            // an error in the connection
                            statues = new Statues();
                            statues.SetStatues(Status.Close, "Error");
                            result = "Error";
                            break;
                        }
                        // switch through the different options received from the server
                        switch (statues.Stat)
                        {
                            case Status.Disconnect:
                                {
                                    Close();
                                    break;
                                }
                            case Status.Close:
                                {
                                    Close();
                                    break;
                                }
                            case Status.PrintAndContinue:
                                {
                                    Console.WriteLine(statues.Message);
                                    continue;
                                }
                            case Status.PrintAndStop:
                                {
                                    // print the message and close the connection
                                    Console.WriteLine(statues.Message);
                                    writer.Write("exit");
                                    string feedback = reader.ReadString();
                                    Close();
                                    break;
                                }
                            case Status.Error:
                                {
                                    // print the error message
                                    Console.WriteLine(statues.Message);
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                        // put the message from the server into the messageReceived var.
                        messageReceived = result;
                    }
                    // wait for some one to receive the message
                    else
                    {
                        Thread.Sleep(1);
                    }
                }
            });
            // start the thread
            receiver.Start();
        }

        /// <summary>
        /// gets a message from the server using the open connection
        /// </summary>
        /// <returns>the message received from the server</returns>
        public string GetMassage()
        {
            // wait for the receiver to get a message
            while (messageReceived == null)
            {
                Thread.Sleep(1);
            }
            // save the message at current and reset the messageReceiverd to null
            string current = messageReceived;
            messageReceived = null;
            return current;
        }

        /// <summary>
        /// close an open connection to the server
        /// </summary>
        public void Close()
        {
            isMultiActive = false;
            isOpen = false;
            writer.Dispose();
            reader.Dispose();
            stream.Dispose();
            client.Close();
        }

        /// <summary>
        /// if there is a connection with the server, close it and update the server
        /// </summary>
        public void Exit()
        {
            // if there is a connection
            if (isOpen)
            {
                // set bools to false
                isMultiActive = false;
                isOpen = false;
                // send exit message to the server for clean disconnect
                writer.Write("exit");
                GetMassage();
                receiver.Wait();
                // dispose all resources
                stream.Dispose();
                reader.Dispose();
                writer.Dispose();
                client.Close();
            }
        }

        /// <summary>
        /// multiplayer message transferring
        /// </summary
        /// <param name="message">if there is a connection, sends message.
        /// open a connection if not opened</param>
        public void SendMessage(string message)
        {
            // checks for open connection
            if (!isOpen)
            {
                // open new connection
                open();
            }
            // send message
            writer.Write(message);
            Console.WriteLine("message: " + message + " have been sent");
        }

        /// <summary>
        /// sends a single message to the server and wait for response back
        /// </summary>
        /// <param name="message"> to send </param>
        public void SendSingleMessage(string message)
        {
            //  create a client and establish a connection
            TcpClient newClient = new TcpClient();
            newClient.Connect(ep);
            using (NetworkStream newStream = newClient.GetStream())
            using (BinaryWriter newWriter = new BinaryWriter(newStream))
            using (BinaryReader newReader = new BinaryReader(newStream))
            {
                // write and erad a message
                newWriter.Write(message);
                string result = newReader.ReadString();
                Statues statues = Statues.FromJson(result);
                Console.WriteLine(statues.Message);
            }
            // close the client
            newClient.Close();
        }
        
    }
}
