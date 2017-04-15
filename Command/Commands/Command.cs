using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Command
{
    public abstract class Command : ICommand
    {
        private IModel model;

        protected Command(IModel m)
        {
            model = m;
        }

        protected IModel Model
        {
            get { return model; }
        }

        public abstract string Execute(string[] args, TcpClient client = null);
    }
}
