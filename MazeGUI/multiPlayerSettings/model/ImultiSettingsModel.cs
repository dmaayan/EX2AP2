using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI.multiPlayerSettings.model
{
    public interface IMultiSettingsModel
    {
        int Cols { get; set; }

        int Rows { get; set; }

        string MazeName { get; set; }

        Maze StartGame();

        string[] GetListGames();

        Maze JoinGame(string game);
    }
}
