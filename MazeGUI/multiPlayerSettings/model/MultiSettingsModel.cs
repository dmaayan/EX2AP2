using MazeGUI.etc;
using MazeLib;
using MVC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI.multiPlayerSettings.model
{
    public class MultiSettingsModel : IMultiSettingsModel
    {
        private int cols;
        private int rows;
        private string mazeName;

        public MultiSettingsModel()
        {

        }

        public int Cols
        {
            get { return cols; }
            set { cols = value; }
        }

        public int Rows
        {
            get { return rows; }
            set { rows = value; }
        }

        public string MazeName
        {
            get { return mazeName; }
            set { mazeName = value; }
        }

        public Maze StartGame()
        {
            Statues stat = ClientSingleton.Client.SendMesseage("start " + mazeName + " " + Rows + " " + Cols);
            if (stat == null)
            {
                return null;
            }
            return Maze.FromJSON(stat.Message);

        }

        public string[] GetListGames()
        {
            Statues stat = ClientSingleton.Client.SendMesseage("list");
            if (stat == null)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<string[]>(stat.Message);
        }

        public Maze JoinGame(string game)
        {
            Statues stat = ClientSingleton.Client.SendMesseage("join " + game);
            if (stat == null)
            {
                return null;
            }
            return Maze.FromJSON(stat.Message);
        }

        public void BackToMenu()
        {

        }

    }
}
