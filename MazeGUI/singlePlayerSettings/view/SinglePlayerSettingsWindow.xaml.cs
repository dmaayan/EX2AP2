using MazeGUI.singlePlayerSettings.model;
using MazeGUI.singlePlayerSettings.viewModel;
using MazeLib;
using System.Windows;

namespace MazeGUI
{

    /// <summary>
    /// The view of single player settings
    /// Interaction logic for SinglePlayerSettingsWindow.xaml
    /// </summary>
    public partial class SinglePlayerSettingsWindow : Window
    {
        /// <summary>
        /// the view model of this view
        /// </summary>
        SingleSettingsViewModel model;

        /// <summary>
        /// Constructor. Initialize component 
        /// </summary>
        public SinglePlayerSettingsWindow()
        {
            model = new SingleSettingsViewModel(new SingleSettingsModel());
            DataContext = model;
            InitializeComponent();
            // add to events
            singelPlayerSettingsControl.okButton.Click += OKButton_Click;
            singelPlayerSettingsControl.cancelButton.Click += CancelButton_Click;
            // Initialize from the defults settings
            Cols = Properties.Settings.Default.MazeCols;
            Rows = Properties.Settings.Default.MazeRows;
        }

        /// <summary>
        /// a property of cols 
        /// </summary>
        public int Cols
        {
            get { return model.Cols; }
            set { model.Cols = value; }
        }

        /// <summary>
        /// a property of rows 
        /// </summary>
        public int Rows
        {
            get { return model.Rows; }
            set { model.Rows = value; }
        }

        /// <summary>
        /// a property of mazeName 
        /// </summary>
        public string MazeName
        {
            get { return model.MazeName; }
            set { model.MazeName = value; }
        }


        /// <summary>
        /// the function called whan ok button was clicked.
        /// start new game.
        /// </summary>
        /// <param name="sender">the button</param>
        /// <param name="e">Routed Event Args</param>
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            // if the name of the maze wasn't insert, pop a message 
            if (MazeName == null)
            {
                MessageBox.Show("Enter Maze Name");
            }
            // get a new maze from the modle and open new single player mazes window
            else
            {
                Maze maze = model.Connect();
                if (maze != null)
                {
                    new SinglePlayerMazeWindow(maze).Show();
                    Close();
                }
            }
        }

        /// <summary>
        /// the function called whan cancel Button was clicked.
        /// </summary>
        /// <param name="sender">the button</param>
        /// <param name="e">Routed Event Args</param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            Close();
        }
    }
}
