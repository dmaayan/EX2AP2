using MVC;
using System;
using System.Configuration;
using System.Net;

namespace Client
{
    /// <summary>
    /// client
    /// </summary>
    class Program
    {
        /// <summary>
        /// runs a client and interact with user commands and server connection
        /// </summary>
        /// <param name="args">arguments from the user</param>
        static void Main(string[] args)
        {
            // get the connection info from the app.config file
            int port = int.Parse(ConfigurationManager.AppSettings["port"].ToString());
            string ip = ConfigurationManager.AppSettings["server"].ToString();
            IPAddress ipAdress = IPAddress.Parse(ip);
            IPEndPoint ep = new IPEndPoint(ipAdress, port);
            // create the message transfering class and the controller
            MessageTransmiter mr = new MessageTransmiter(ep);
            ClientController clientController = new ClientController(mr);
            Status status;
            // repeat until status from the command is exit
            do
            {
                try
                {
                    // get command from user
                    Console.Write("Please enter a command: \n");
                    string command = Console.ReadLine();
                    // apply command with the controller
                    status = clientController.ExecuteCommand(command);
                }
                catch (Exception e)
                {
                    // connection error
                    Console.WriteLine(e);
                    break;
                }
            } while (status != Status.Exit);
        }
    }
}