using MazeGUI.etc;
using MazeLib;
using MVC;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI.singlePlayerMaze.model
{
    class SinglePlayerMazeModel : AbstractMazeModel, ISinglePlayerMazeModel
    {
        private int searchAlgoritm;
        private string mazeSolution;

        public SinglePlayerMazeModel(Maze m) : base(m)
        {
            searchAlgoritm = Properties.Settings.Default.SearchAlgorithm;
        }

        public string SolveMaze()
        {
            if (mazeSolution == null)
            {
                Statues stat = ClientSingleton.Client.SendMesseage("solve " + MazeName + " " + searchAlgoritm);
                mazeSolution = MazeSolution.FromJson(stat.Message);
                
            }
            return mazeSolution;
        }
    }
}
