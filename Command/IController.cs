using System.Net.Sockets;

namespace Command
{
    public interface IController
    {

        IModel Model
        {
            set;
        }

        IView View
        {
            set;
        }

        string ExecuteCommand(string commandLine, TcpClient client);

    }
}