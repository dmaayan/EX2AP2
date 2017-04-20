using MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client.Commands
{
    public class StartMultiPlayCommand : ICommand
    {
        private MessageTransmiter messageRec;

        public StartMultiPlayCommand(MessageTransmiter mr)
        {
            messageRec = mr;
        }

        public Status Execute(string[] args, TcpClient client)
        {
            string message = String.Join(" ", args);
            messageRec.SendMessage(message);
            string result = Statues.FromJson(messageRec.GetMassage()).Message;
            Console.WriteLine(result);
            return Status.KeepConnection;
        }
    }
}
