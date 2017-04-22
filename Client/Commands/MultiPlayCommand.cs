using System;
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
    /// command for multigame play
    /// </summary>
    class MultiPlayCommand : ICommand
    {
        /// <summary>
        /// send and receive messages
        /// </summary>
        private MessageTransmiter messageRec;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="mr">is the messageTransmiter </param>
        public MultiPlayCommand(MessageTransmiter mr)
        {
            messageRec = mr;
        }

        /// <summary>
        /// executes a multiplayer play command
        /// </summary>
        /// <param name="args">arguments of the command</param>
        /// <param name="client">to give the command</param>
        /// <returns></returns>
        public Status Execute(string[] args, TcpClient client)
        {
            // checks if there is a connection with the server
            if (messageRec.IsMultiActive)
            {
                string message = String.Join(" ", args);
                messageRec.SendMessage(message);
                return Status.KeepConnection;
            }
            Console.WriteLine("Game is not on");
            return Status.Disconnect;
        }
    }
}
