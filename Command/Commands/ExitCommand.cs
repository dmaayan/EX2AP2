
using System.Net.Sockets;

namespace MVC
{
    /// <summary>
    /// exit command
    /// </summary>
    public class ExitCommand : Command
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="m">model</param>
        /// <param name="ic">view</param>
        public ExitCommand(IModel model, IClientHandler clientHandle) :
            base(model, clientHandle) { }

        /// <summary>
        /// executes the command given
        /// </summary>
        /// <param name="args">command received from the client</param>
        /// <param name="client">the client to give the command</param>
        /// <returns>disconnect</returns>
        public override Status Execute(string[] args, TcpClient client)
        {
            return Status.Disconnect;
        }
    }
}
