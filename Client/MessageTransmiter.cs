﻿using MVC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Client
{
    /// <summary>
    /// sends and received messages to and from the server.
    /// </summary>
    public class MessageTransmiter
    {
        /// <summary>
        /// event to notify all about messages received from the server
        /// </summary>
        private event EventHandler<StatuesEventArgs> NotifyAboutMessage;
        /// <summary>
        /// client currently active
        /// </summary>
        private TcpClient client;
        /// <summary>
        /// the end point to the connection
        /// </summary>
        private IPEndPoint ep;
        /// <summary>
        /// the network stream
        /// </summary>
        private NetworkStream stream;
        /// <summary>
        /// read messages from the server
        /// </summary>
        private BinaryReader reader;
        /// <summary>
        /// write messages to the server
        /// </summary>
        private BinaryWriter writer;
        /// <summary>
        /// statues of the send returned from the server
        /// </summary>
        private Statues statues;
        /// <summary>
        /// all the delegates signed for the event
        /// </summary>
        private List<EventHandler<StatuesEventArgs>> delegates;
        /// <summary>
        /// wait for messages from the server in multiplayer connection
        /// </summary>
        private Task receiver;
        /// <summary>
        /// true if there is a connection Open
        /// </summary>
        private bool isOpen;
        /// <summary>
        /// true if there is a game
        /// </summary>
        private bool isMultiActive;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="endPoint">for the connection</param>
        public MessageTransmiter(IPEndPoint endPoint)
        {
            ep = endPoint;
            isOpen = false;
            isMultiActive = false;
            statues = null;
            delegates = new List<EventHandler<StatuesEventArgs>>();
        }

        /// <summary>
        /// add event handler to the event
        /// </summary>
        /// <param name="messageEventHandler">event to add</param>
        public void SignForEvent(EventHandler<StatuesEventArgs> messageEventHandler)
        {
            NotifyAboutMessage += messageEventHandler;
            // record all incoming delegates
            delegates.Add(messageEventHandler);
        }

        /// <summary>
        /// property isMultiActive
        /// </summary>
        public bool IsMultiActive
        {
            get { return isMultiActive; }
            set { isMultiActive = value; }
        }

        /// <summary>
        /// opens a new multiplayer connection to the client
        /// </summary>
        public void Open()
        {
            // sets bools to true and message to null
            isMultiActive = true;
            isOpen = true;

            // create a client and Open the streams
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
                    if (statues == null)
                    {
                        string result;
                        Status status;
                        try
                        {
                            // get a message from the server and parse it to Statues
                            result = reader.ReadString();
                            statues = Statues.FromJson(result);
                            status = statues.Stat;
                            NotifyAboutMessage?.Invoke(this, new StatuesEventArgs(statues));
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Connect to the server have been closed");
                            // an error in the connection
                            statues = new Statues();
                            statues.SetStatues(Status.Close, "Error receiveing message from the server");
                            status = Status.Disconnect;
                        }
                        // switch through the different options received from the server
                        switch (status)
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
                            case Status.Play:
                                {
                                    statues = null;
                                    continue;
                                }
                            case Status.CloseGame:
                                {
                                    // close the connection
                                    writer.Write("exit");
                                    Close();
                                    break;
                                }
                            case Status.Finish:
                                {
                                    // close the connection
                                    writer.Write("exit");
                                    Close();
                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
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
        /// close an Open connection to the server
        /// </summary>
        public void Close()
        {
            // turn off the flags
            isMultiActive = false;
            isOpen = false;
            // dispose of the stream resourecs
            writer.Dispose();
            reader.Dispose();
            stream.Dispose();
            client.Close();
            // unsign all the delegates in the event
            foreach (EventHandler<StatuesEventArgs> e in delegates)
            {
                NotifyAboutMessage -= e;
            }
            delegates.Clear();
            statues = null;
        }

        /// <summary>
        /// if there is a connection with the server, close it and update the server
        /// </summary>
        public void FinishGame(string[] args)
        {
            // if there is a connection
            if (isOpen)
            {
                writer.Write(String.Join(" ", args));

                receiver.Wait();
            }
        }

        /// <summary>
        /// multiplayer message transferring
        /// </summary
        /// <param name="message">if there is a connection, sends message.
        /// Open a connection if not opened</param>
        public void SendMessage(string message)
        {
            // checks for Open connection
            if (!isOpen)
            {
                // Open new connection
                Open();
            }
            // send message
            writer.Write(message);
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
                statues = Statues.FromJson(result);
            }
            // close the client
            newClient.Close();
        }

        /// <summary>
        /// gets a statues from the server using the Open connection
        /// </summary>
        /// <returns>the statues received from the server</returns>
        public Statues getStatues()
        {
            // wait for the receiver to get a message
            while (statues == null)
            {
                if (isOpen)
                {
                    Thread.Sleep(1);
                }
                else
                {
                    return null;
                }
            }
            // save the message at current and reset the messageReceiverd to null
            Statues current = statues;
            statues = null;
            return current;
        }
    }
}
