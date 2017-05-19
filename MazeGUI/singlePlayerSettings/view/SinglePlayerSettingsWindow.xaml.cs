using MazeGUI.singlePlayerSettings.model;
using MazeGUI.singlePlayerSettings.viewModel;
using MazeLib;
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

namespace MazeGUI
{

    /// <summary>
    /// Interaction logic for SinglePlayerSettingsWindow.xaml
    /// </summary>
    public partial class SinglePlayerSettingsWindow : Window
    {
        SingleSettingsViewModel model;

        public SinglePlayerSettingsWindow()
        {
            model = new SingleSettingsViewModel(new SingleSettingsModel());
            DataContext = model;
            InitializeComponent();
            singelPlayerSettingsControl.okButton.Click += OKButton_Click;
            singelPlayerSettingsControl.cancelButton.Click += CancelButton_Click;
            Cols = Properties.Settings.Default.MazeCols;
            Rows = Properties.Settings.Default.MazeRows;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (MazeName == "")
            {
                MessageBox.Show("Enter Maze Name");
            }
            else
            {
                Maze maze = model.Connect();
                new SinglePlayerMazeWindow(maze).Show();
                Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            Close();
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

    }
}
