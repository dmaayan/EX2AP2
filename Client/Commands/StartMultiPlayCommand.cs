using MVC;
using System;
using System.Net.Sockets;

namespace Client.Commands
{
    /// <summary>
    /// command for start multiplayer game
    /// </summary>
    public class StartMultiPlayCommand : ICommand
    {

        /// <summary>
        /// send and receive messages
        /// </summary>
        private MessageTransmiter messageRec;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="mr">is the messageTransmiter </param>
        public StartMultiPlayCommand(MessageTransmiter mr)
        {
            messageRec = mr;
        }

        /// <summary>
        /// executes the start command
        /// </summary>
        /// <param name="args">arguments of the command</param>
        /// <param name="client">to give the command</param>
        /// <returns> the Status</returns>
        public Status Execute(string[] args, TcpClient client)
        {
            string message = String.Join(" ", args);
            // send and receive a message
            messageRec.SendMessage(message);
            string result = messageRec.GetMassage().Message;
            Console.WriteLine(result);
            return Status.KeepConnection;
        }
    }
}
