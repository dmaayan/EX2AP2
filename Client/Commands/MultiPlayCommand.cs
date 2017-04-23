using System;
using MVC;
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
        /// <returns>the Status</returns>
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
