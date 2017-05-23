using System.Net.Sockets;


namespace MVC
{
    /// <summary>
    /// interface view for the MVC
    /// </summary>
    public interface IClientHandler
    {
        /// <summary>
        /// handles a client accepted by the server
        /// </summary>
        /// <param name="client">to handle</param>
        void HandleClient(TcpClient client);

        /// <summary>
        /// sends a message to a specific client
        /// </summary>
        /// <param name="s">message to send</param>
        /// <param name="client">send to client</param>
        void SendToClient(string s, TcpClient client);
    }
}
