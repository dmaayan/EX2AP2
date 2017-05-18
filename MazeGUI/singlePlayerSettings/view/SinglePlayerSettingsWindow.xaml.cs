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
            
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Maze m = model.Connect();
            new SinglePlayerMazeWindow(m).Show();
            Close();
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

        public string Name
        {
            get { return model.Name; }
            set { model.Name = value; }
        }
    }
}
