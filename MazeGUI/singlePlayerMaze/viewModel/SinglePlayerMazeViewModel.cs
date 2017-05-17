using MazeGUI.singlePlayerMaze.model;
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

        public string Name
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
    }
}
