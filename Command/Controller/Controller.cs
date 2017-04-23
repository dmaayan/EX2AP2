using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace MVC
{
    /// <summary>
    /// controller for the server's MVC
    /// </summary>
    public class Controller : IController
    {
        /// <summary>
        /// commnds to execute
        /// </summary>
        private Dictionary<string, ICommand> commands;
        /// <summary>
        /// model
        /// </summary>
        private IModel model;
        /// <summary>
        /// view
        /// </summary>
        private IClientHandler view;

        /// <summary>
        /// constructor
        /// </summary>
        public Controller()
        {
            commands = new Dictionary<string, ICommand>();
        }

        /// <summary>
        /// sets the model of the controller
        /// </summary>
        /// <param name="model">to set</param>
        /// <param name="clientHandler">view to set</param>
        public void SetModelndView(IModel model, IClientHandler clientHandler)
        {
            this.model = model;
            view = clientHandler;
            AddAllCommands();
        }

        /// <summary>
        /// add all the commands
        /// </summary>
        private void AddAllCommands()
        {
            commands.Add("generate", new GenerateMazeCommand(model, view));
            commands.Add("solve", new SolveMazeCommand(model, view));
            commands.Add("start", new StartGameCommand(model, view));
            commands.Add("list", new ListGameNamesCommand(model, view));
            commands.Add("join", new JoinGameCommand(model, view));
            commands.Add("play", new PlayCommand(model, view));
            commands.Add("close", new CloseGameCommand(model, view));
            commands.Add("exit", new ExitClientCommand(model, view));
        }

        /// <summary>
        /// execute commands 
        /// </summary>
        /// <param name="commandLine">command received from the user</param>
        /// <param name="client">that requested the command</param>
        /// <returns>the status returned from the command executed</returns>
        public Status ExecuteCommand(string commandLine, TcpClient client)
        {
            // split command
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];
            // check that the command is valid
            if (!commands.ContainsKey(commandKey))
            {
                Statues stat = new Statues();
                stat.SetStatues(Status.Disconnect, "Command not found");
                view.SendToClient(stat.ToJson(), client);
                return Status.Disconnect;
            }
            // get command and execute it
            string[] args = arr.Skip(1).ToArray();
            ICommand command = commands[commandKey];
            return command.Execute(args, client);
        }
    }
}
