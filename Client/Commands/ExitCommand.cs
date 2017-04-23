using MVC;
using System.Net.Sockets;

namespace Client.Commands
{
    /// <summary>
    /// command for exit client
    /// </summary>
    public class ExitCommand : ICommand
    {
        /// <summary>
        /// send and receive messages
        /// </summary>
        private MessageTransmiter messageRec;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="mr">is the messageTransmiter </param>
        public ExitCommand(MessageTransmiter mr)
        {
            messageRec = mr;
        }

        /// <summary>
        /// executes the exit command
        /// </summary>
        /// <param name="args">arguments of the command</param>
        /// <param name="client">to give the command</param>
        /// <returns></returns>
        public Status Execute(string[] args, TcpClient client)
        {
            // close connection with the server
            messageRec.Exit();
            return Status.Exit;
        }
    }
}
