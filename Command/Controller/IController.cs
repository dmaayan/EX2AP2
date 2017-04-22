using System.Net.Sockets;

namespace MVC
{
    /// <summary>
    /// interface IController
    /// </summary>
    public interface IController
    {
        /// <summary>
        /// sets the model of the controller
        /// </summary>
        /// <param name="model">to set</param>
        /// <param name="clientHandler">view to set</param>
        void SetModelndView(IModel model, IClientHandler clientHandler);

        /// /// <summary>
        /// execute commands 
        /// </summary>
        /// <param name="commandLine">command received from the user</param>
        /// <param name="client">that requested the command</param>
        /// <returns>the status returned from the command executed</returns>
        Status ExecuteCommand(string commandLine, TcpClient client);

    }
}