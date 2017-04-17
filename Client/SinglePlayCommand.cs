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
        private MassageReceiver massageRec;

        public SinglePlayCommand(MassageReceiver mr)
        {
            massageRec = mr;
        }

        public string Execute(string[] args, TcpClient client)
        {
            massageRec.open();
            massageRec.GetMassage();
            massageRec.close();

            return null;
        }
    }
}
