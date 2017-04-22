﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC;
using static Client.Program;
using System.Net.Sockets;

namespace Client
{
    /// <summary>
    /// command for single play
    /// </summary>
    public class SinglePlayCommand : ICommand
    {
        /// <summary>
        /// send and receive messages
        /// </summary>
        private MessageTransmiter messageRec;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="mr">is the messageTransmiter </param>
        public SinglePlayCommand(MessageTransmiter mr)
        {
            messageRec = mr;
        }

        /// <summary>
        /// executes a single game command
        /// </summary>
        /// <param name="args">arguments of the command</param>
        /// <param name="client">to give the command</param>
        /// <returns></returns>
        public Status Execute(string[] args, TcpClient client)
        {
            string message = String.Join(" ", args);
            messageRec.SendSingleMessage(message);
            return Status.Disconnect;
        }
    }
}
