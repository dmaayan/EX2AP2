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
        private MassageReceiver massageRec;

        public MultiPlayCommand(MassageReceiver mr)
        {
            massageRec = mr;
        }

         public string Execute(string[] args, TcpClient client)
        {
          
            return null;
        }
    }
}
