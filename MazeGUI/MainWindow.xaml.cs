using MazeGUI.multiPlayerSettings.view;
using MazeGUI.settings.view;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MazeGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SettingsWindow sw = null;
        private bool isSettingWinOpen = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isSettingWinOpen)
            {
                sw = new SettingsWindow();
                sw.Closed += ChangeCloseStatus;
                sw.Show();
            }
            Hide();
            isSettingWinOpen = true;
        }

        public void ChangeCloseStatus(object sender, System.EventArgs e) {
            isSettingWinOpen = false;
        }

        private void SinglePlayerButton_Click(object sender, RoutedEventArgs e)
        {
            new SinglePlayerSettingsWindow().Show();
            Hide();
        }

        private void MultiPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            new MultiSettingsWindow().Show();
            Hide();
        }
    }
}
