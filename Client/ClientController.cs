using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC;
using static Client.Program;

namespace Client
{
    public class ClientController
    {
        private Dictionary<string, ICommand> commands;
        private MassageReceiver mr;


        public ClientController()
        {
            mr = new MassageReceiver();
            MultiPlayCommand mpc = new MultiPlayCommand(mr);
            SinglePlayCommand spc = new SinglePlayCommand(mr);
            commands = new Dictionary<string, ICommand>();
            commands.Add("generate", spc);
            commands.Add("solve", spc);
            commands.Add("start", mpc);
            commands.Add("list", spc);
            commands.Add("join", mpc);
            commands.Add("play", mpc);
            commands.Add("close", mpc);
        }

        private void AddAllCommands()
        {
            
        }
    }
}
