using System;
using System.Collections.Generic;
using MVC;
using Client.Commands;

namespace Client
{
    /// <summary>
    /// use commands design to use the user commands
    /// </summary>
    public class ClientController
    {
        /// <summary>
        /// stores commands
        /// </summary>
        private Dictionary<string, ICommand> commands;
        /// <summary>
        /// send and receive messages
        /// </summary>
        private MessageTransmiter mr;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="messageReceiver">responsible for message transferring to the server</param>
        public ClientController(MessageTransmiter messageReceiver)
        {
            mr = messageReceiver;
            SinglePlayCommand singlePC = new SinglePlayCommand(mr);
            // set the commands dictionary
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

        /// <summary>
        /// execute commands 
        /// </summary>
        /// <param name="commandLine">command received from the user</param>
        /// <returns>the status returned from the command executed</returns>
        public Statues ExecuteCommand(string commandLine)
        {
            // split the commandline
            string[] args = commandLine.Split();

            // execute command
            ICommand command = commands[args[0]];
            command.Execute(args);
            return mr.getStatues();
        }
    }
}
