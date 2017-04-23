using System.Net.Sockets;
using Newtonsoft.Json;

namespace MVC
{
    /// <summary>
    /// List Game Names Command
    /// </summary>
    public class ListGameNamesCommand : Command
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="m">model</param>
        /// <param name="ic">view</param>
        public ListGameNamesCommand(IModel model, IClientHandler clientHandle) : 
            base(model, clientHandle) { }

        /// <summary>
        /// executes the command given
        /// </summary>
        /// <param name="args">command received from the client</param>
        /// <param name="client">the client to give the command</param>
        /// <returns>the status of the command</returns>
        public override Status Execute(string[] args, TcpClient client)
        {
            // set the statues with the list of names, send to client and return
            Stat.SetStatues(Status.Disconnect, JsonConvert.SerializeObject(Model.GetAllNames()));
            Handler.SendToClient(Stat.ToJson(), client);
            return Status.Disconnect;
        }
    }
}