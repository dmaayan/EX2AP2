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
            commands.Add("generate", new GenerateMazeCommand(model));
            commands.Add("solve", new SolveMazeCommand(model));
            commands.Add("start", new StartGameCommand(model));
            commands.Add("list", new ListGameNamesCommand(model));
            commands.Add("join", new JoinGameCommand(model));
            commands.Add("play", new PlayCommand(model));
            commands.Add("close", new CloseGameCommand(model));
        }

        public IModel Model
        {
            set { model = value; }
        }

        public IClientHandler View
        {
            set { view = value; }
        }
        
        public string ExecuteCommand(string commandLine, TcpClient client)
        {
            string[] arr = commandLine.Split(' ');
            string commandKey = arr[0];
            if (!commands.ContainsKey(commandKey))
            {
                return "Command not found";
            }
            string[] args = arr.Skip(1).ToArray();
            ICommand command = commands[commandKey];
            string result = command.Execute(args, client);
            if (result == null)
            {
                return "Incorrect Command";
            }
            return result;
        }
    }
}
