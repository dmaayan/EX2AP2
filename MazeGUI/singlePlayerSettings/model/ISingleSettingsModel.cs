using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI.singlePlayerSettings
{
    public interface ISingleSettingsModel
    {
        int Cols { get; set; }

        int Rows { get; set; }

        string Name { get; set; }

        Maze Connect();
    }
}
