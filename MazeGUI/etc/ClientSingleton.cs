using Client;
using MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MazeGUI.etc
{
    /// <summary>
    /// client singleton. manages client
    /// </summary>
    public class ClientSingleton
    {
        /// <summary>
        /// the singleton
        /// </summary>
        private static ClientSingleton client;
        /// <summary>
        /// port to the server
        /// </summary>
        private int port;
        /// <summary>
        /// ip of the server
        /// </summary>
        private string ip;
        /// <summary>
        /// client controller
        /// </summary>
        ClientController clientController;
        /// <summary>
        /// message transmiter. sends messages to the server and receive
        /// </summary>
        MessageTransmiter messageTransmiter;

        /// <summary>
        /// constructor
        /// </summary>
        private ClientSingleton()
        {
            // get the port and ip from the settings
            port = Properties.Settings.Default.ServerPort;
            ip = Properties.Settings.Default.ServerIP;
            IPAddress ipAdress = IPAddress.Parse(ip);
            IPEndPoint ep = new IPEndPoint(ipAdress, port);
            // create the message transfering class and the controller
            messageTransmiter = new MessageTransmiter(ep);
            clientController = new ClientController(messageTransmiter);
        }

        /// <summary>
        /// sign for the event of the message transmiter
        /// </summary>
        /// <param name="messageEventHandler"></param>
        public void SignForMessaging(EventHandler<StatuesEventArgs> messageEventHandler)
        {
            messageTransmiter.SignForEvent(messageEventHandler);
        }

        /// <summary>
        /// the get instance method of the client signleton class
        /// </summary>
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

        /// <summary>
        /// sends a message using the client
        /// </summary>
        /// <param name="message">to send to the server</param>
        /// <returns></returns>
        public Statues SendMesseage(string message)
        {
            Statues status;
            try
            {
                // apply command with the controller
                status = clientController.ExecuteCommand(message);
            }
            catch (Exception e)
            {
                // connection error
                MessageBox.Show("failed to connect to the server");
                Console.WriteLine(e);
                return null;
            }
            return status;
        }
    }
}
