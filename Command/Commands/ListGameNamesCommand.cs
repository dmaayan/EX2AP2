using System.Net.Sockets;

namespace Command
{
    internal class ListGameNamesCommand : Command
    {

        public ListGameNamesCommand(IModel model) : base(model)
        {
        }

        public override string Execute(string[] args, TcpClient client)
        {
            
            return "";
        }
    }
}