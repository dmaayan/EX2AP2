using MazeGUI.singlePlayerMaze.model;
using MazeLib;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI.singlePlayerMaze.viewModel
{
    class SinglePlayerMazeViewModel : ViewModel
    {
        private ISinglePlayerMazeModel model;

        public SinglePlayerMazeViewModel(ISinglePlayerMazeModel mod)
        {
            model = mod;
        }

        public string MazeName
        {
            get { return model.Name; }
        }

        public int Cols
        {
            get { return model.Cols; }
        }

        public int Rows
        {
            get { return model.Rows; }
        }

        public string MazeString
        {
            get { return model.MazeString; }
        }

        public Position MazeStartPoint
        {
            get { return model.MazeStartPoint; }
        }

        public Position MazeEndPoint
        {
            get { return model.MazeEndPoint; }
        }

        public bool IsMoveOk(Position mazeStartPoint, Direction direct)
        {
            return model.IsMoveOk(mazeStartPoint, direct);
        }

        public string SolveMaze()
        {
            return model.SolveMaze();
        }
    }
}
