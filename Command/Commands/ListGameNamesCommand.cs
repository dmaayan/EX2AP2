using System.Net.Sockets;
using Newtonsoft.Json;

namespace MVC
{
    public class ListGameNamesCommand : Command
    {

        public ListGameNamesCommand(IModel model) : base(model) { }

        public override string Execute(string[] args, TcpClient client)
        {
            return JsonConvert.SerializeObject(Model.GetAllNames());
        }
    }
}