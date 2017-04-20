using System.Net.Sockets;
using Newtonsoft.Json;

namespace MVC
{
    public class ListGameNamesCommand : Command
    {
        private IClientHandler ic;

        public ListGameNamesCommand(IModel model, IClientHandler clientHandle) : base(model)
        {
            ic = clientHandle;
        }

        public override Status Execute(string[] args, TcpClient client)
        {
            Stat.SetStatues(Status.Disconnect, JsonConvert.SerializeObject(Model.GetAllNames()));
            ic.SendToClient(Stat.ToJson(), client);
            return Status.Disconnect;
        }
    }
}