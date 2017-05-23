using System;
using MVC;
using System.Net.Sockets;

namespace MazeGUI.Commands
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
        /// <returns>the Status</returns>
        public Status Execute(string[] args, TcpClient client)
        {
            string message = String.Join(" ", args);
            messageRec.SendSingleMessage(message);
            return Status.Disconnect;
        }
    }
}
