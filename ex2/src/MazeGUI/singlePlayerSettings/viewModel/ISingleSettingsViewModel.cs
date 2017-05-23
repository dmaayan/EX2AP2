using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI.singlePlayerSettings.viewModel
{
    public interface ISingleSettingViewModel
    {
        int Cols { get; set; }
        int Rows { get; set; }
        int Name { get; set; }
        Maze Connect();
    }
}
