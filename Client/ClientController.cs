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
            commands.Add("finish", new FinishCommand(mr));
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
            Status stat = command.Execute(args);
            if (stat == Status.Play || stat == Status.Finish || stat == Status.Close)
            {
                return null;
            }
            // get response from the server
            return mr.getStatues();
        }
    }
}
