using MVC;
using System.Net.Sockets;

namespace Client.Commands
{
    /// <summary>
    /// command for exit client
    /// </summary>
    public class FinishCommand : ICommand
    {
        /// <summary>
        /// send and receive messages
        /// </summary>
        private MessageTransmiter messageRec;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="mr">is the messageTransmiter </param>
        public FinishCommand(MessageTransmiter mr)
        {
            messageRec = mr;
        }

        /// <summary>
        /// executes the finish command
        /// </summary>
        /// <param name="args">arguments of the command</param>
        /// <param name="client">to give the command</param>
        /// <returns>Status.Finish</returns>
        public Status Execute(string[] args, TcpClient client)
        {
            // close connection with the server
            messageRec.FinishGame();
            return Status.Finish;
        }
    }
}
