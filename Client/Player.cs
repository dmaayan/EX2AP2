using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Player
    {
        private TcpClient client;
        private List<Direction> way;
        private bool wait;

        public Player(TcpClient c)
        {
            wait = false;
            client = c;
            way = new List<Direction>();
        }

        public TcpClient Client
        {
            get { return client; }
        }

        public List<Direction> Way
        {
            get { return way; }
        }

        public void WaitForPlayer()
        {
            wait = true;
            while (wait)
            {
                System.Threading.Thread.Sleep(1);
            }
        }

        public void StopWaiting()
        {
            wait = false;
        }


        // לבדוק אם צריך את זה
        public override bool Equals(object obj)
        {
            return client.Equals(obj);
        }
    }
}
