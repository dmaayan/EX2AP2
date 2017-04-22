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
        /// <summary>
        /// the model
        /// </summary>
        private IModel model;
        /// <summary>
        /// the view
        /// </summary>
        private IClientHandler handler;
        /// <summary>
        /// statues of the command
        /// </summary>
        private Statues statues;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="m">model</param>
        /// <param name="ic">view</param>
        protected Command(IModel m, IClientHandler ic)
        {
            model = m;
            handler = ic;
            statues = new Statues();
        }

        /// <summary>
        /// model get property
        /// </summary>
        protected IModel Model
        {
            get { return model; }
        }

        /// <summary>
        /// client handler get property
        /// </summary>
        protected IClientHandler Handler
        {
            get { return handler; }
        }

        /// <summary>
        /// statues get and set property
        /// </summary>
        protected Statues Stat
        {
            get { return statues; }
            set { statues = value; }
        }

        /// <summary>
        /// executes the command given
        /// </summary>
        /// <param name="args">command received from the client</param>
        /// <param name="client">the client to give the command</param>
        /// <returns>the status of the command</returns>
        public abstract Status Execute(string[] args, TcpClient client = null);
    }
}
