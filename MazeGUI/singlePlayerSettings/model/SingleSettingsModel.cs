using MazeLib;
using MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI.singlePlayerSettings.model
{
    public class SingleSettingsModel : ISingleSettingsModel
    {
        private int cols;
        private int rows;
        private string name;

        public SingleSettingsModel()
        {

        }

        public int Cols {
            get { return cols; }
            set { cols = value; }
        }

        public int Rows {
            get { return rows; }
            set { rows = value; }
        }

        public string Name {
            get { return name; }
            set { name = value; }
        }

        public Maze Connect()
        {
            Statues stat = ClientSingleton.Client.SendMesseage("generate " 
                                                                + Name + " " + Rows + " " + Cols);
            return Maze.FromJSON(stat.Message);
        }
    }
}
