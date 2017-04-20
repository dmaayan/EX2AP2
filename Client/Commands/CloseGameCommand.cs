using MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class CloseGameCommand : ICommand
    {
        private MessageTransmiter messageRec;

        public CloseGameCommand(MessageTransmiter mr)
        {
            messageRec = mr;
        }

        public Status Execute(string[] args, TcpClient client)
        {
            string message = String.Join(" ", args);
            messageRec.SendMessage(message);
            Statues stat = Statues.FromJson(messageRec.GetMassage());
            Console.WriteLine(stat.Message);
            return stat.Stat;
        }
    }
}
