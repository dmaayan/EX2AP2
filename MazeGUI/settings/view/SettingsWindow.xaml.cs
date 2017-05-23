using MazeGUI.etc;
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

namespace MazeGUI.settings.view
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : NonClosableWindow
    {
        private SettingsViewModel viewModel;

        public SettingsWindow()
        {
            InitializeComponent();
            viewModel = new SettingsViewModel(new ApplicationSettingsModel());
            DataContext = viewModel;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SaveSettings();
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            Close();
        }

        private void BtnCancel_Click(object sender, System.EventArgs e)
        {
            viewModel.ReloadSettings();
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            Close();
        }

        private void SettingsWindow1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //viewModel.ReloadSettings();
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
        }
    }
}
