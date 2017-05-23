using MazeGUI.multiPlayerMaze.view;
using MazeGUI.multiPlayerSettings.model;
using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI.multiPlayerSettings.viewModel
{
    public class MultiSettingsViewModel : ViewModel
    {
        private IMultiSettingsModel model;

        public MultiSettingsViewModel(IMultiSettingsModel Imodel)
        {
            model = Imodel;
        }

        public int Cols
        {
            get { return model.Cols; }
            set
            {
                model.Cols = value;
                NotifyPropertyChanged("txtCols");
            }
        }
        public int Rows
        {
            get { return model.Rows; }
            set
            {
                model.Rows = value;
                NotifyPropertyChanged("txtRows");
            }
        }

        public string MazeName
        {
            get { return model.MazeName; }
            set
            {
                model.MazeName = value;
                NotifyPropertyChanged("mazeNameTxtBox");
            }
        }

        public string[] GetListGames()
        {
            return model.GetListGames();
        }

        public Maze JoinGame(string game)
        {
           return model.JoinGame(game);
        }
       
        public Maze StartGame()
        {
            return model.StartGame();
        }

        public void BackToMenu()
        {
            model.BackToMenu();
        }


    }
}
