using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI.singlePlayerSettings.model
{
    class SingleSettingsModel
    {
        string mazeName = null;
        int mazeRows;
        int mazeCols;

        public SingleSettingsModel ()
        {
            mazeRows = Properties.Settings.Default.MazeRows;
            mazeCols = Properties.Settings.Default.MazeCols;
        }

        public string MazeName
        {
            get { return mazeName; }
            set { mazeName = value; }
        }

        public int MazeRows
        {
            get { return mazeRows; }
            set { mazeRows = value; }
        }

        public int MazeCols
        {
            get { return mazeCols; }
            set { mazeCols = value; }
        }

        void SaveSettings()
        {

        }

    }
}
