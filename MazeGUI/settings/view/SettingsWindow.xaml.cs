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
    /// The view of the settings
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : NonClosableWindow
    {
        /// <summary>
        /// the view model of settings 
        /// </summary>
        private SettingsViewModel viewModel;

        /// <summary>
        /// constructor, initialize the component
        /// </summary>
        public SettingsWindow()
        {
            InitializeComponent();
            viewModel = new SettingsViewModel(new ApplicationSettingsModel());
            DataContext = viewModel;
        }

        /// <summary>
        /// save the settings to be defults, and go back to the menu
        /// the function called whan ok Button was clicked.
        /// </summary>
        /// <param name="sender">the button</param>
        /// <param name="e">Routed Event Args</param>
        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SaveSettings();
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            Close();
        }

        /// <summary>
        /// Reload the old settings and go back to the menu
        /// the function called whan ok Button was clicked.
        /// </summary>
        /// <param name="sender">the button</param>
        /// <param name="e">Routed Event Args</param>
        private void BtnCancel_Click(object sender, System.EventArgs e)
        {
            viewModel.ReloadSettings();
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            Close();
        }

        /// <summary>
        /// the function called whan the window close.
        /// </summary>
        /// <param name="sender">the window</param>
        /// <param name="e">Cancel event args</param>
        private void SettingsWindow1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
        }
    }
}
