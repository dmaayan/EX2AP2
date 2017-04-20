using System.Net.Sockets;

namespace MVC
{

    public interface IController
    {

        void SetModel(IModel model);

        IClientHandler View
        {
            set;
        }

        Status ExecuteCommand(string commandLine, TcpClient client);

    }
}