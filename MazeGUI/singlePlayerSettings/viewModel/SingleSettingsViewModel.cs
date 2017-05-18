using MazeGUI.singlePlayerSettings.model;
using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI.singlePlayerSettings.viewModel
{
    public class SingleSettingsViewModel : ViewModel
    {
        ISingleSettingsModel model;

        public SingleSettingsViewModel(ISingleSettingsModel Imodel)
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

        public Maze Connect()
        {
            return model.Connect();
        }
    }
}
