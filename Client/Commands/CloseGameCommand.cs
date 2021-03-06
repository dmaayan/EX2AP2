﻿using MVC;
using System;
using System.Net.Sockets;

namespace Client
{
    /// <summary>
    /// command for close game
    /// </summary>
    public class CloseGameCommand : ICommand
    {
        /// <summary>
        /// send and receive messages
        /// </summary>
        private MessageTransmiter messageRec;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="mr">is the messageTransmiter </param>
        public CloseGameCommand(MessageTransmiter mr)
        {
            messageRec = mr;
        }

        /// <summary>
        /// executes the close command
        /// </summary>
        /// <param name="args">arguments of the command</param>
        /// <param name="client">to give the command</param>
        /// <returns></returns>
        public Status Execute(string[] args, TcpClient client)
        {
            // if the connection is active
            if (messageRec.IsMultiActive)
            {
                string message = String.Join(" ", args);
                // send and receive a message
                messageRec.SendMessage(message);
                return Status.Close;
            }
            Console.WriteLine("Can't close a game without playing");
            return Status.Disconnect;
        }
    }
}
