
using System.Net.Sockets;
namespace MVC
{
    /// <summary>
    /// finish game Command
    /// </summary>
    public class FinishGameCommand : Command
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="m">model</param>
        /// <param name="ic">view</param>
        public FinishGameCommand(IModel model, IClientHandler clientHandle) :
            base(model, clientHandle) { }

        /// <summary>
        /// executes the command given
        /// </summary>
        /// <param name="args">command received from the client</param>
        /// <param name="client">the client to give the command</param>
        /// <returns>the status of the command</returns>
        public override Status Execute(string[] args, TcpClient client)
        {
            string name = args[0];
            // get its game
            Game game = Model.CloseGame(name, client);
            if (game == null)
            {
                // set the statues, send to client and return
                Stat.SetStatues(Status.Finish, "No active game to close");
                Handler.SendToClient(Stat.ToJson(), client);
                return Status.Disconnect;
            }
            // set the statues, send to client and return
            Stat.SetStatues(Status.Close, "Game is closing");
            Handler.SendToClient(Stat.ToJson(), client);

            // inform the other player that its game have been closed
            Player otherPlayer = game.GetOtherPlayer(client);
            Stat.SetStatues(Status.Finish, "Closed game");
            Handler.SendToClient(Stat.ToJson(), otherPlayer.Client);
            return Status.Disconnect;
        }
    }
}
