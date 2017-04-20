using MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client.Commands
{
    public class ExitCommand : ICommand
    {
        private MessageTransmiter messageRec;

        public ExitCommand(MessageTransmiter mr)
        {
            messageRec = mr;
        }

        public Status Execute(string[] args, TcpClient client)
        {
            messageRec.Exit();
            return Status.Exit;
        }
    }
}
