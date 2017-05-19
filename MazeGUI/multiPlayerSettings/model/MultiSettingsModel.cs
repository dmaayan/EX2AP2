using MVC;
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

        public void Start()
        {

            Statues stat = ClientSingleton.Client.SendMesseage("start " + mazeName + " " + Rows + " " + Cols);
        }

        public void JoinToGame()
        {
//            Statues stat = ClientSingleton.Client.SendMesseage("start " + mazeName + " " + Rows + " " + Cols);
        }

    }
}
