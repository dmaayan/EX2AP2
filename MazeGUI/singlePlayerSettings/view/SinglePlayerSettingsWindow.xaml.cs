using MazeGUI.singlePlayerSettings.viewModel;
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
        private ISingleSettingsViewModel viewModel;

        public SinglePlayerSettingsWindow()
        {
            viewModel = new SingleSettingsViewModel();
            InitializeComponent();
            settingsControl.okButton.Click += OKButton_Click;
            settingsControl.cancelButton.Click += CancelButton_Click;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (settingsControl.mazeNameTxtBox.Text == "")
            {
                MessageBox.Show("Enter Maze Name");
            }
            else
            {
                new SinglePlayerMazeWindow().Show();
                Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            Close();
        }

        private void SinglePlayerWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
        }

    }
}
