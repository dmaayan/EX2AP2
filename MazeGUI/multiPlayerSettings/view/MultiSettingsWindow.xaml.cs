using MazeGUI.multiPlayerSettings.viewModel;
using MazeGUI.multiPlayerSettings.model;
using System;
using System.Threading.Tasks;
using System.Windows;
using MazeGUI.multiPlayerMaze.view;
using System.Collections.ObjectModel;
using MazeLib;

namespace MazeGUI.multiPlayerSettings.view
{
    /// <summary>
    /// the view, multi player settings Window. 
    /// Interaction logic for MultiSettingsWindow.xaml
    /// </summary>
    public partial class MultiSettingsWindow : Window
    {
        /// <summary>
        /// the view model of this view
        /// </summary>
        private MultiSettingsViewModel model;
        /// <summary>
        /// a Observable Collection list of games 
        /// </summary>
        public ObservableCollection<string> gamesList = new ObservableCollection<string>();

        /// <summary>
        /// Constructor. Initialize component 
        /// </summary>
        public MultiSettingsWindow()
        {
            model = new MultiSettingsViewModel(new MultiSettingsModel());
            DataContext = model;
            InitializeComponent();
            // add to events
            multiSettingsContol.okButton.Click += OKButton_Click;
            multiSettingsContol.cancelButton.Click += CancelButton_Click;
            // Initialize from the defults settings
            Cols = Properties.Settings.Default.MazeCols;
            Rows = Properties.Settings.Default.MazeRows;
            ListComboBox.ItemsSource = gamesList;
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

        /// <summary>
        /// the function called whan List ComboBox was open.
        /// </summary>
        /// <param name="sender">the button</param>
        /// <param name="e">Event Args</param>
        private void ListComboBox_DropDownOpened(object sender, EventArgs e)
        {
            // get a new list of the waiting games
            string[] gamesArray = model.GetListGames();
            if (gamesArray != null)
            {
                // copy the games to the gamesList
                gamesList.Clear();
                foreach (string game in gamesArray)
                {
                    gamesList.Add(game);
                }
            }
        }

        /// <summary>
        /// the function called whan ok button was clicked.
        /// start new game, wait for another player
        /// </summary>
        /// <param name="sender">the button</param>
        /// <param name="e">Routed Event Args</param>
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            // if the name of the maze wasn't insert, pop a message 
            if (MazeName == null)
            {
                MessageBox.Show("Enter Maze Name");
                return;
            }
            // hiddes all the things in the window and show a waiting message
            ListGamesTextBlock.Visibility = Visibility.Hidden;
            JoinButton.Visibility = Visibility.Hidden;
            ListComboBox.Visibility = Visibility.Hidden;
            multiSettingsContol.Visibility = Visibility.Hidden;
            waitLabel.Visibility = Visibility.Visible;

            Task t = new Task(() =>
            {
                // get a new maze from the modle and open new multi player mazes window
                Maze maze = model.StartGame();
                if (maze != null)
                {
                    Dispatcher.Invoke(() =>
                    {
                        new MultiMazesWindow(maze).Show();
                        Close();
                    });
                }
                // didn't get the maze, return the window's buttons
                else
                {
                    Dispatcher.Invoke(() =>
                    {
                        ListGamesTextBlock.Visibility = Visibility.Visible;
                        JoinButton.Visibility = Visibility.Visible;
                        ListComboBox.Visibility = Visibility.Visible;
                        multiSettingsContol.Visibility = Visibility.Visible;
                        waitLabel.Visibility = Visibility.Hidden;
                    });
                }

            });
            t.Start();
        }

        /// <summary>
        /// join to a waiting game, against other player.
        /// the function called whan join button was clicked.
        /// </summary>
        /// <param name="sender">the button</param>
        /// <param name="e">Routed Event Args</param>
        private void JoinButton_Click(object sender, RoutedEventArgs e)
        {
            // get a new maze from the modle and open new multi player mazes window
            Maze maze = model.JoinGame((string)ListComboBox.SelectedItem);
            if (maze != null)
            {
                new MultiMazesWindow(maze).Show();
                Close();
            }
            // if failed, pop a messag
            else
            {
                MessageBox.Show("Game not available, choose another game");
            }
        }
    }
}