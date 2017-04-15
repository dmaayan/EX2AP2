using System.Net.Sockets;

namespace MVC
{
    public interface IController
    {

        IModel Model
        {
            set;
        }

        IClientHandler View
        {
            set;
        }

        string ExecuteCommand(string commandLine, TcpClient client);

    }
}