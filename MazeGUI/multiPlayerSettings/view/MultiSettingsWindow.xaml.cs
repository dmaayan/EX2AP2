using MazeGUI.multiPlayerSettings.viewModel;
using MazeGUI.multiPlayerSettings.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MazeGUI.multiPlayerMaze.view;
using System.Collections.ObjectModel;
using MazeLib;

namespace MazeGUI.multiPlayerSettings.view
{
    /// <summary>
    /// Interaction logic for MultiSettingsWindow.xaml
    /// </summary>
    public partial class MultiSettingsWindow : Window
    {
        private MultiSettingsViewModel model;
        public ObservableCollection<string> gamesList = new ObservableCollection<string>();

        public MultiSettingsWindow()
        {
            model = new MultiSettingsViewModel(new MultiSettingsModel());
            DataContext = model;
            InitializeComponent();
            multiSettingsContol.okButton.Click += OKButton_Click;
            multiSettingsContol.cancelButton.Click += CancelButton_Click;
            Cols = Properties.Settings.Default.MazeCols;
            Rows = Properties.Settings.Default.MazeRows;
            ListComboBox.ItemsSource = gamesList;
        }
        public int Cols
        {
            get { return model.Cols; }
            set { model.Cols = value; }
        }
        public int Rows
        {
            get { return model.Rows; }
            set { model.Rows = value; }
        }

        public string MazeName
        {
            get { return model.MazeName; }
            set { model.MazeName = value; }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            Close();
        }

        private void ListComboBox_DropDownOpened(object sender, EventArgs e)
        {
            string[] gamesArray = model.GetListGames();
            if (gamesArray != null)
            {
                gamesList.Clear();
                foreach (string game in gamesArray)
                {
                    gamesList.Add(game);
                }
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (MazeName == null)
            {
                MessageBox.Show("Enter Maze Name");
                return;
            }

            ListGamesTextBlock.Visibility = Visibility.Hidden;
            JoinButton.Visibility = Visibility.Hidden;
            ListComboBox.Visibility = Visibility.Hidden;
            multiSettingsContol.Visibility = Visibility.Hidden;
            waitLabel.Visibility = Visibility.Visible;
            backButton.Visibility = Visibility.Visible;

            Task t = new Task(() =>
            {
                Maze maze = model.StartGame();
                if (maze != null)
                {
                    Dispatcher.Invoke(() =>
                    {
                        new MultiMazesWindow(maze).Show();
                        Close();
                    });
                }
                else
                {
                    Dispatcher.Invoke(() =>
                    {
                        ListGamesTextBlock.Visibility = Visibility.Visible;
                        JoinButton.Visibility = Visibility.Visible;
                        ListComboBox.Visibility = Visibility.Visible;
                        multiSettingsContol.Visibility = Visibility.Visible;
                        waitLabel.Visibility = Visibility.Hidden;
                        backButton.Visibility = Visibility.Hidden;
                    });
                }

            });
            t.Start();
        }

        private void JoinButton_Click(object sender, RoutedEventArgs e)
        {
            Maze maze = model.JoinGame((string)ListComboBox.SelectedItem);

            if (maze != null)
            {
                new MultiMazesWindow(maze).Show();
                Close();
            }
            else
            {
                MessageBox.Show("Game not available, choose another game");
            }
        }
        
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Don't push it !!!! ");

            /*
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            model.BackToMenu();
            win.Show();
            Close();
            */
        }
    }
}