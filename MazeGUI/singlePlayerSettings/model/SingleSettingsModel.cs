using MazeGUI.etc;
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
        private string mazeName;

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

        public string MazeName {
            get { return mazeName; }
            set { mazeName = value; }
        }

        public Maze Connect()
        {
            // TODO sinchornize solves
            Statues stat = ClientSingleton.Client.SendMesseage("generate " 
                                                                + mazeName + " " + Rows + " " + Cols);
            return Maze.FromJSON(stat.Message);
        }
    }
}
