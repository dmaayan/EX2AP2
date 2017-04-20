using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC;
using static Client.Program;
using System.Net.Sockets;
using Client.Commands;

namespace Client
{
    public class ClientController
    {
        private Dictionary<string, ICommand> commands;
        private MessageTransmiter mr;


        public ClientController(MessageTransmiter messageReceiver)
        {
            mr = messageReceiver;
            SinglePlayCommand singlePC = new SinglePlayCommand(mr);
            commands = new Dictionary<string, ICommand>();
            commands.Add("generate", singlePC);
            commands.Add("solve", singlePC);
            commands.Add("list", singlePC);
            commands.Add("start", new StartMultiPlayCommand(mr));
            commands.Add("join", new StartMultiPlayCommand(mr));
            commands.Add("play", new MultiPlayCommand(mr));
            commands.Add("close", new CloseGameCommand(mr));
            commands.Add("exit", new ExitCommand(mr));
        }

        public Status ExecuteCommand(string commandLine)
        {
            string[] args = commandLine.Split();
            if (args.Length == 0 || !commands.ContainsKey(args[0]))
            {
                Console.WriteLine("Command not found");
                return Status.Error;
            }

            ICommand command = commands[args[0]];
            Status status = command.Execute(args);
            return status;
        }
    }
}
