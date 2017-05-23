using MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI
{
    /// <summary>
    /// event args that contain a statues
    /// </summary>
    public class StatuesEventArgs : EventArgs
    {
        /// <summary>
        /// statues of the event triggred
        /// </summary>
        private Statues stat;
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="s">the statues</param>
        public StatuesEventArgs(Statues s)
        {
            stat = s;
        }

        /// <summary>
        /// getter for stat
        /// </summary>
        public Statues Stat
        {
            get { return stat; }
        }
    }
}
