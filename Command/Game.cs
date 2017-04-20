using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MVC
{
    public class Game
    {
        Player player;
        Player otherPlayer;
        //bool active = false;

        public Game(Player creator)
        {
            player = creator;
            //PlayGame(creator);
        }

        public Player FirstPlayer
        {
            get { return player; }
        }

        public Player SecondPlayer
        {
            get { return otherPlayer; }
        }

        public void AddPlayer(Player secondPlayer)
        {
            otherPlayer = secondPlayer;
            player.StopWaiting();
            //PlayGame(secondPlayer);
        }

        public void waitForOtherPlayer()
        {
            player.WaitForPlayer();
        }

        public Player GetOtherPlayer(TcpClient tcc)
        {
            if (player.Client == tcc)
            {
                return otherPlayer;
            }
            if (otherPlayer.Client == tcc)
            {
                return player;
            }
            return null;
        }

        //private void PlayGame(Player p)
        //{
        //    active = true;
        //    using (NetworkStream stream = p.Client.GetStream())
        //    using (BinaryReader reader = new BinaryReader(stream))
        //    using (BinaryWriter writer = new BinaryWriter(stream))
        //    {
        //        Task task = new Task(() =>
        //        {
        //            while (active)
        //            {
        //                try
        //                {
        //                    Console.WriteLine(reader.ReadString());
        //                }
        //                catch (IOException e)
        //                {
        //                    Console.WriteLine(e);
        //                    active = false;
        //                    return;
        //                }
        //            }
        //        });
        //        string command = "";
        //        task.Start();
        //        while (!command.Contains("exit"))
        //        {
        //            // Send data to server
        //            Console.Write("Starting multiplayer Game.\n");
        //            Console.Write("Please enter a move: \n");
        //            command = Console.ReadLine();
        //            writer.Write(command);
        //            // Get result from server
        //            string result = reader.ReadString();
        //            Console.WriteLine("{0}", result);
        //        }
        //        active = false;
        //        task.Wait();
        //    }
        //}
    }
}
