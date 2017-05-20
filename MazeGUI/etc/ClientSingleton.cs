using Client;
using MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI.etc
{
    public class ClientSingleton
    {

        private static ClientSingleton client;
        private int port;
        private string ip;
        ClientController clientController;
        MessageTransmiter mr;

        private ClientSingleton()
        {
            port = Properties.Settings.Default.ServerPort;
            ip = Properties.Settings.Default.ServerIP;
            IPAddress ipAdress = IPAddress.Parse(ip);
            IPEndPoint ep = new IPEndPoint(ipAdress, port);
            // create the message transfering class and the controller
            mr = new MessageTransmiter(ep);
            clientController = new ClientController(mr);
        }

        public void SignForMessaging(EventHandler<StatuesEventArgs> onOpponentMove)
        {
            mr.NotifyAboutMessage += onOpponentMove;
        }

        public static ClientSingleton Client
        {
            get {
                if (client == null)
                {
                    client = new ClientSingleton();
                }
                return client;
            }
        }

        public Statues SendMesseage(string message)
        {
            Statues status;
            // repeat until status from the command is exit

            try
            {
                // apply command with the controller
                status = clientController.ExecuteCommand(message);
            }
            catch (Exception e)
            {
                // connection error
                Console.WriteLine(e);
                return null;
            }
            return status;
        }
    }
}
