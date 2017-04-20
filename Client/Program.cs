
using MVC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Client
{
    class Program
    {
        private static IPEndPoint ep;

        static void Main(string[] args)
        {
            ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
            MessageTransmiter mr = new MessageTransmiter(ep);
            ClientController clientController = new ClientController(mr);
            Status status;
            do
            {
                try
                {
                    // Send data to server
                    Console.Write("Please enter a command: \n");
                    string command = Console.ReadLine();
                    status = clientController.ExecuteCommand(command);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    break;
                }
            } while (status != Status.Exit);
        }
    }
}