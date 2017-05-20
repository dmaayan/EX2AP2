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

namespace MazeGUI.multiPlayerSettings.view
{
    /// <summary>
    /// Interaction logic for MultiSettingsWindow.xaml
    /// </summary>
    public partial class MultiSettingsWindow : Window
    {
        private MultiSettingsViewModel model;

        public MultiSettingsWindow()
        {
            model = new MultiSettingsViewModel(new MultiSettingsModel());
            DataContext = model;
            InitializeComponent();
            multiSettingsContol.okButton.Click += OKButton_Click;
            multiSettingsContol.cancelButton.Click += CancelButton_Click;
            Cols = Properties.Settings.Default.MazeCols;
            Rows = Properties.Settings.Default.MazeRows;
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

    private void OKButton_Click(object sender, RoutedEventArgs e)
    {
        if (MazeName == null)
        {
            MessageBox.Show("Entr Maze Name");
        }
        else
        {
                ListGamesTextBlock.Visibility = Visibility.Hidden;
                JoinButton.Visibility = Visibility.Hidden;
                ListComboBox.Visibility = Visibility.Hidden;
                multiSettingsContol.Visibility = Visibility.Hidden;
                waitLable.Visibility = Visibility.Visible;
                //Maze maze = model.Connect();
                //new MultiMazesWindow().Show();
           
        }
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        MainWindow win = (MainWindow)Application.Current.MainWindow;
        win.Show();
        Close();
    }

        private void JoinButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
