using MazeGUI.multiPlayerSettings.view;
using MazeGUI.settings.view;
using System.Windows;

namespace MazeGUI
{
    /// <summary>
    /// The Main Window of the game
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// the settings window of the game
        /// </summary>
        private SettingsWindow sw = null;

        /// <summary>
        /// constructor. Initialize the Component
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// open the settings window.
        /// the function called whan Settings Button was clicked.
        /// </summary>
        /// <param name="sender">the button</param>
        /// <param name="e">Routed Event Args</param>
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            sw = new SettingsWindow();
            sw.Show();
            Hide();
        }

        /// <summary>
        /// open Single Player Settings Window.
        /// the function called whan Single Player Button was clicked.
        /// </summary>
        /// <param name="sender">the button</param>
        /// <param name="e">Routed Event Args</param>
        private void SinglePlayerButton_Click(object sender, RoutedEventArgs e)
        {
            new SinglePlayerSettingsWindow().Show();
            Hide();
        }

        /// <summary>
        /// open Multi Player Settings Window.
        /// the function called whan multi player button was clicked.
        /// </summary>
        /// <param name="sender">the button</param>
        /// <param name="e">Routed Event Args</param>
        private void MultiPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            new MultiSettingsWindow().Show();
            Hide();
        }
    }
}
