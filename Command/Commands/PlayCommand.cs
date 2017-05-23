using MazeLib;
using System;
using System.Net.Sockets;
using System.Text;

namespace MVC
{
    /// <summary>
    /// Play Command
    /// </summary>
    public class PlayCommand : Command
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="m">model</param>
        /// <param name="ic">view</param>
        public PlayCommand(IModel model, IClientHandler clientHandle) : 
            base(model, clientHandle) { }

        /// <summary>
        /// executes the command given
        /// </summary>
        /// <param name="args">command received from the client</param>
        /// <param name="client">the client to give the command</param>
        /// <returns>the status of the command</returns>
        public override Status Execute(string[] args, TcpClient client)
        {
            Stat.SetStatues(Status.KeepConnection, "Parameter does not match");
            // checks that the argument length is valid
            if (args.Length != 1)
            {
                // send statues to client
                Handler.SendToClient(Stat.ToJson(), client);
                return Status.KeepConnection;
            }
            // change input to match enum Direction
            String move = char.ToUpper(args[0].ToLower()[0]) + args[0].ToLower().Substring(1);
            if (!Enum.IsDefined(typeof(Direction), move))
            {
                // send statues to client
                Handler.SendToClient(Stat.ToJson(), client);
                return Status.KeepConnection;
            }
            // get the direction from the command
            Direction direction = (Direction) Enum.Parse(typeof(Direction), move);
            // get the maze played by this player
            Maze maze = Model.PlayGame(direction, client);
            if (maze == null)
            {
                // set the statues with the message, send to client and return
                Stat.SetStatues(Status.Close, "Game is not on");
                Handler.SendToClient(Stat.ToJson(), client);
                return Status.Close;
            }
            // get other player and check that is not null
            Player otherPlayer = Model.GetOtherPlayer(client);
            if (otherPlayer == null)
            {
                Stat.SetStatues(Status.Close, "No other player to send move to");
                Handler.SendToClient(Stat.ToJson(), client);
                return Status.Close;
            }

            // set the statues with the message, send to client and return
            Stat.SetStatues(Status.Play, direction.ToString());
            Handler.SendToClient(Stat.ToJson(), otherPlayer.Client);
            return Status.KeepConnection;
        }
    }
}