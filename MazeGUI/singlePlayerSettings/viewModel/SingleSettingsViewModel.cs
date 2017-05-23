using MazeLib;

namespace MazeGUI.singlePlayerSettings.viewModel
{
    /// <summary>
    /// Single settings view model inherits ViewModel
    /// </summary>
    public class SingleSettingsViewModel : ViewModel
    {
        /// <summary>
        /// the model of the view model
        /// </summary>
        ISingleSettingsModel model;

        /// <summary>
        /// constructor.
        /// </summary>
        /// <param name="Imodel">is the model</param>
        public SingleSettingsViewModel(ISingleSettingsModel Imodel)
        {
            model = Imodel;
        }
        /// <summary>
        /// 
        /// a property of cols, 
        /// whan set, notify.
        /// </summary>
        public int Cols
        {
            get { return model.Cols; }
            set
            {
                model.Cols = value;
                NotifyPropertyChanged("txtCols");
            }
        }

        /// <summary>
        /// a property of rows, 
        /// whan set, notify.
        /// </summary>
        public int Rows
        {
            get { return model.Rows; }
            set
            {
                model.Rows = value;
                NotifyPropertyChanged("txtRows");
            }
        }

        /// <summary>
        /// a property of mazeName, 
        /// whan set, notify.
        /// </summary>
        public string MazeName
        {
            get { return model.MazeName; }
            set
            {
                model.MazeName = value;
                NotifyPropertyChanged("mazeNameTxtBox");
            }
        }

        /// <summary>
        /// Connect to the model to generate maze.
        /// </summary>
        /// <returns>a maze</returns>
        public Maze Connect()
        {
            return model.Connect();
        }
    }
}
