using MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class StatuesEventArgs : EventArgs
    {
        private Statues stat;
        public StatuesEventArgs(Statues s)
        {
            stat = s;
        }

        public Statues Stat
        {
            get { return stat; }
        }
    }
}
