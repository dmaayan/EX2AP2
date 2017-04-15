using System.Collections.Generic;
using System.Net.Sockets;
using System.Linq;

namespace MVC
{
    public class SolveMazeCommand : Command
    {
        private Dictionary<string, ICommand> commands;

        public SolveMazeCommand(IModel model) : base(model)
        {
            commands = new Dictionary<string, ICommand>();
            commands.Add("0", new RunBFSCommand(model));
            commands.Add("1", new RunDFSCommand(model));
        }

        public override string Execute(string[] args, TcpClient client)
        {
            string[] newArgs = args.Take(1).Skip(1).ToArray();
            if (commands.ContainsKey(args[1]))
            {
                ICommand command = commands[args[1]];
                return command.Execute(newArgs, client);
            }
            return null;
        }
    }
}