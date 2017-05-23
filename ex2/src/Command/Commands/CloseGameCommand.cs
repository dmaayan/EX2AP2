
using System.Net.Sockets;

namespace MVC
{
    /// <summary>
    /// close game command
    /// </summary>
    public class CloseGameCommand : Command
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="m">model</param>
        /// <param name="ic">view</param>
        public CloseGameCommand(IModel model, IClientHandler clientHandle) :
            base(model, clientHandle) { }

        /// <summary>
        /// executes the command given
        /// </summary>
        /// <param name="args">command received from the client</param>
        /// <param name="client">the client to give the command</param>
        /// <returns>the status of the command</returns>
        public override Status Execute(string[] args, TcpClient client)
        {
            // get the name of the maze to close
            string name = args[0];
            // get its game
            Game game = Model.CloseGame(name, client);
            if (game == null)
            {
                // set the statues, send to client and return
                Stat.SetStatues(Status.KeepConnection, "Can't close game named: " + name);
                Handler.SendToClient(Stat.ToJson(), client);
                return Status.KeepConnection;
            }

            // set the statues and send to client
            Stat.SetStatues(Status.Close, "Game named: " + name + " have been closed");
            Handler.SendToClient(Stat.ToJson(), client);

            // get the other player
            TcpClient otherClient = game.GetOtherPlayer(client).Client;
            // set the statues to the other player and return
            Stat.SetStatues(Status.CloseGame, "Game named: " + name + " have been closed");
            Handler.SendToClient(Stat.ToJson(), otherClient);
            return Status.Disconnect;
        }
    }
}
