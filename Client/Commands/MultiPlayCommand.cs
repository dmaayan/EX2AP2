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
    class MultiPlayCommand : ICommand
    {
        private MessageTransmiter messageRec;

        public MultiPlayCommand(MessageTransmiter mr)
        {
            messageRec = mr;
        }

         public Status Execute(string[] args, TcpClient client)
        {
            if (messageRec.IsMultiActive)
            {
                string message = String.Join(" ", args);
                messageRec.SendMessage(message);
                return Status.KeepConnection;
            }
            Console.WriteLine("Game is not on");
            return Status.Disconnect;
        }
    }
}
