using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI.singlePlayerSettings
{
    interface ISingleSettingsMoudel
    {
        int MazeName { get; set; }
        int MazeRows { get; set; }
        int MazeCols { get; set; }
        void SaveSettings();

    }
}
