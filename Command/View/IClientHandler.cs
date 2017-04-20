﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MVC
{
    public interface IClientHandler
    {
        void HandleClient(TcpClient client);

        void SendToClient(string s, TcpClient client);

        //void SendToClient(string s);
    }
}
