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

        public Player(TcpClient c)
        {
            client = c;
            way = new List<Direction>();
        }

        public List<Direction> Way
        {
            get { return way; }
        }

        public override bool Equals(object obj)
        {
            return client.Equals(obj);
        }
    }
}
