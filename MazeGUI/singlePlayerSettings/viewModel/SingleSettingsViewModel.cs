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

        public SingleSettingsViewModel(ISingleSettingsModel m)
        {
            model = m;
            Cols = Properties.Settings.Default.MazeCols;
            Rows = Properties.Settings.Default.MazeRows;
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

        public string Name
        {
            get { return model.Name; }
            set
            {
                model.Name = value;
                NotifyPropertyChanged("MazeNameTxtBox");
            }
        }

        public Maze Connect()
        {
            return model.Connect();
        }
    }
}
