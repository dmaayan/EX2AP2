
using System.Net.Sockets;
namespace MVC
{
    /// <summary>
    /// Exit Client Command
    /// </summary>
    public class ExitClientCommand : Command
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="m">model</param>
        /// <param name="ic">view</param>
        public ExitClientCommand(IModel model, IClientHandler clientHandle) :
            base(model, clientHandle) { }

        /// <summary>
        /// executes the command given
        /// </summary>
        /// <param name="args">command received from the client</param>
        /// <param name="client">the client to give the command</param>
        /// <returns>the status of the command</returns>
        public override Status Execute(string[] args, TcpClient client)
        {
            // get the game played by this client if exist
            Game game = Model.GetGameOfPlayer(client);
            if (game == null)
            {
                // set the statues, send to client and return
                Stat.SetStatues(Status.Exit, "No active game to close");
                Handler.SendToClient(Stat.ToJson(), client);
                return Status.Disconnect;
            }
            // set the statues, send to client and return
            Stat.SetStatues(Status.Exit, "Game is closing");
            Handler.SendToClient(Stat.ToJson(), client);

            // inform the other plaayer that its game have been closed
            Player otherPlayer = game.GetOtherPlayer(client);
            Stat.SetStatues(Status.Close, "Closed game");
            Handler.SendToClient(Stat.ToJson(), otherPlayer.Client);
            return Status.Disconnect;
        }
    }
}
