using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MVC
{
    public abstract class Command : ICommand
    {
        private IModel model;
        private Statues statues;

        protected Command(IModel m)
        {
            model = m;
            statues = new Statues();
        }

        protected IModel Model
        {
            get { return model; }
        }

        protected Statues Stat
        {
            get { return statues; }
            set { statues = value; }
        }

        public abstract Status Execute(string[] args, TcpClient client = null);
    }
}
