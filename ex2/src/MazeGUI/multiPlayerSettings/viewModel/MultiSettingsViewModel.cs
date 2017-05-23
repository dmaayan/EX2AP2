using MazeGUI.multiPlayerSettings.model;
using MazeLib;

namespace MazeGUI.multiPlayerSettings.viewModel
{
    /// <summary>
    /// Multi settings view model inherits ViewModel
    /// </summary>
    public class MultiSettingsViewModel : ViewModel
    {
        /// <summary>
        /// the model of the view model
        /// </summary>
        private IMultiSettingsModel model;

        /// <summary>
        /// constructor.
        /// </summary>
        /// <param name="Imodel">is the model</param>
        public MultiSettingsViewModel(IMultiSettingsModel Imodel)
        {
            model = Imodel;
        }

        /// <summary>
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
        /// Get a list of games
        /// </summary>
        /// <returns> a list of games that waiting to start </returns>
        public string[] GetListGames()
        {
            return model.GetListGames();
        }

        /// <summary>
        /// apply to join the game.
        /// </summary>
        /// <param name="game">the name of the game to join to</param>
        /// <returns>a maze</returns>
        public Maze JoinGame(string game)
        {
           return model.JoinGame(game);
        }

        /// <summary>
        /// function of starting the game 
        /// </summary>
        /// <returns>a maze</returns>
        public Maze StartGame()
        {
            return model.StartGame();
        }
    }
}
