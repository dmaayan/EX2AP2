using MazeGUI.singlePlayerMaze.model;
using MazeGUI.etc;
using MazeGUI.singlePlayerMaze.viewModel;
using MazeLib;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;

namespace MazeGUI
{
    /// <summary>
    /// Interaction logic for SinglePlayerMazeWindow.xaml
    /// </summary>
    public partial class SinglePlayerMazeWindow : NonClosableWindow
    {
        /// <summary>
        /// the model of the view
        /// </summary>
        private SinglePlayerMazeViewModel model;

        /// <summary>
        /// is currently animating the solution
        /// </summary>
        private bool isAnimating = false;

        /// <summary>
        /// movement dictionary for use by the mazes
        /// </summary>
        protected static Dictionary<Key, Direction> directions = new Dictionary<Key, Direction>
        {
            { Key.Up, Direction.Up },
            { Key.Down, Direction.Down},
            { Key.Left, Direction.Left},
            { Key.Right, Direction.Right}
        };

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="m">maze to play</param>
        public SinglePlayerMazeWindow(Maze m)
        {
            model = new SinglePlayerMazeViewModel(new SinglePlayerMazeModel(m));
            this.DataContext = model;
            InitializeComponent();
            singlePlayerMazeWindow.KeyDown += singlePlayerMazeWindow_KeyDown;
        }

        /// <summary>
        /// getter for the maze name
        /// </summary>
        public string MazeName
        {
            get { return model.MazeName; }
        }

        /// <summary>
        /// getter for the maze columns
        /// </summary>
        public int Cols
        {
            get { return model.Cols; }
        }

        /// <summary>
        /// getter for the maze rows
        /// </summary>
        public int Rows
        {
            get { return model.Rows; }
        }

        /// <summary>
        /// getter for the maze string representation
        /// </summary>
        public string MazeString
        {
            get { return model.MazeString; }
        }

        /// <summary>
        /// getter for the maze start point
        /// </summary>
        public Position MazeStartPoint
        {
            get { return model.MazeStartPoint; }
        }

        /// <summary>
        /// getter for the maze end point
        /// </summary>
        public Position MazeEndPoint
        {
            get { return model.MazeEndPoint; }
        }

        /// <summary>
        /// event fired upon key down
        /// </summary>
        /// <param name="sender">this window</param>
        /// <param name="e">which key have been pressed</param>
        private void singlePlayerMazeWindow_KeyDown(object sender, KeyEventArgs e)
        {
            // if animating, then dont do any thing
            if (isAnimating)
            {
                return;
            }

            // checks that the key is a direction one
            if (directions.ContainsKey(e.Key))
            {
                // get the direction
                Direction direction = directions[e.Key];
                // check that the player can move in that direction
                if (model.IsMoveOk(mazeControl.PlayerPos, direction))
                {
                    // change the player position
                    mazeControl.PositionPlayer(direction);
                }
            }
            // check if the player have reached the finish line
            if ((mazeControl.PlayerPos.Col == MazeEndPoint.Col) 
                && (mazeControl.PlayerPos.Row == MazeEndPoint.Row))
            {
                FinishGame();
            }
        }

        /// <summary>
        /// finish the game
        /// </summary>
        private void FinishGame()
        {
            MessageBox.Show("You escaped!");
            // check if the player want to restart the game
            if (!Restart())
            {
                // close this window and go back to the main window
                Dispatcher.Invoke(() =>
                {
                    Close();
                    MainWindow win = (MainWindow)Application.Current.MainWindow;
                    win.Show();
                });
            }
        }

        /// <summary>
        /// event fired upon clicking on the restart button
        /// </summary>
        /// <param name="sender">the restart button</param>
        /// <param name="e">event args</param>
        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            Restart();
        }

        /// <summary>
        /// restart the game if the player confirms
        /// </summary>
        /// <returns>true if the player clicked yes, flase otherwise</returns>
        private bool Restart()
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Do you want to restart?",
                                                                "Restart game",
                                                                MessageBoxButton.YesNo);
            // if the player clicked yes
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                isAnimating = false;
                mazeControl.Restart();
                return true;
            }
            return false;
        }

        /// <summary>
        /// event fired upon clicking on the main menu button
        /// </summary>
        /// <param name="sender">the mainMenu button</param>
        /// <param name="e">event args</param>
        private void MainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            // if animating, dont do any thing
            if (isAnimating == false)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Do you want to go back?",
                                                                    "Back to main menu",
                                                                    MessageBoxButton.YesNo);
                // if the player clicked yes, close this window and go back to the main window
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    MainWindow win = (MainWindow)Application.Current.MainWindow;
                    win.Show();
                    Close();
                }
            }
        }

        /// <summary>
        /// event fired upon clicking on the solve button
        /// </summary>
        /// <param name="sender">the solve button</param>
        /// <param name="e">event args</param>
        private void SolveMazeButton_Click(object sender, RoutedEventArgs e)
        {
            // if animating, dont do any thing
            if (isAnimating == false)
            {
                // get the solution from the model
                string solution = model.SolveMaze();
                if (solution != null)
                {
                    // animate the solution
                    AnimateFromAnotherThread(solution);
                }
            }
        }

        /// <summary>
        /// create an animation of the player moving
        /// </summary>
        /// <param name="solution">for this maze</param>
        public void AnimateFromAnotherThread(string solution)
        {
            isAnimating = true;
            // get the current position of the player
            Position startPoint = mazeControl.PlayerPos;
            Task animation = new Task(() =>
            {
                // restart the position of the player
                mazeControl.Restart();
                System.Threading.Thread.Sleep(300);
                // for each step in the solution, move the player and sleep
                for (int i = 0; (i < solution.Length) && (isAnimating == true); i++)
                {
                    mazeControl.PositionPlayer(solution[i]);
                    System.Threading.Thread.Sleep(300);
                }
                // may happen only if the player clicked restart before animation finished
                if (isAnimating == false)
                {
                    return;
                }
                
                // ask if the player wants to continue from where it stopped
                MessageBoxResult messageBoxResult = MessageBox.Show("Continue playing?",
                                                                    "Continue playing",
                                                                    MessageBoxButton.YesNo);
                // if yes, change player position to its starting location
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    mazeControl.PositionPlayer(startPoint.Row, startPoint.Col);
                }
                else
                {
                    FinishGame();
                }
                isAnimating = false;

            });
            // start the animation
            animation.Start();
        }

        /// <summary>
        /// upon closing, change isAnimating to false so that the animation would stop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void singlePlayerMazeWindow_Closing(object sender,
                                                    System.ComponentModel.CancelEventArgs e)
        {
            isAnimating = false;
        }
    }
}
