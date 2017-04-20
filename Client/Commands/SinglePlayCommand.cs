using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC;
using static Client.Program;
using System.Net.Sockets;

namespace Client
{
    class SinglePlayCommand : ICommand
    {
        private MessageTransmiter messageRec;

        public SinglePlayCommand(MessageTransmiter mr)
        {
            messageRec = mr;
        }

        public Status Execute(string[] args, TcpClient client)
        {
            string message = String.Join(" ", args);
            messageRec.SendSingleMessage(message);
            return Status.Disconnect;
        }
    }
}
