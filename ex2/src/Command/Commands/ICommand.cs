using System.Net.Sockets;


namespace MVC
{
    /// <summary>
    /// interface ICommand
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// executes the command given
        /// </summary>
        /// <param name="args">command received from the client</param>
        /// <param name="client">the client to give the command</param>
        /// <returns>the status of the command</returns>
        Status Execute(string[] args, TcpClient client = null);
    }
}
