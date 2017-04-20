using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace MVC
{
    public class Controller : IController
    {

        private Dictionary<string, ICommand> commands;
        private IModel model;
        private IClientHandler view;

        public Controller()
        {
            commands = new Dictionary<string, ICommand>();
        }

        public void SetModel(IModel model)
        {
            this.model = model;
            AddAllCommands();
        }

        public IClientHandler View
        {
            set { view = value; }
        }

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
        
        public Status ExecuteCommand(string commandLine, TcpClient client)
        {
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];
            if (!commands.ContainsKey(commandKey))
            {
                Statues stat = new Statues();
                stat.SetStatues(Status.Disconnect, "Command not found");
                view.SendToClient(stat.ToJson(), client);
                return Status.Disconnect;
            }
            string[] args = arr.Skip(1).ToArray();
            ICommand command = commands[commandKey];
            return command.Execute(args, client);
        }
    }
}
