using MazeGUI.singlePlayerMaze.model;
using MazeGUI.etc;
using MazeGUI.singlePlayerMaze.viewModel;
using MazeLib;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MazeGUI
{
    /// <summary>
    /// Interaction logic for SinglePlayerMazeWindow.xaml
    /// </summary>
    public partial class SinglePlayerMazeWindow : NonClosableWindow
    {
        //initializing members
        private SinglePlayerMazeViewModel model;
        private bool isAnimating = false;
        
        public SinglePlayerMazeWindow(Maze m)
        {
            model = new SinglePlayerMazeViewModel(new SinglePlayerMazeModel(m));
            this.DataContext = model;
            InitializeComponent();
            singlePlayerMazeWindow.KeyDown += singlePlayerMazeWindow_KeyDown;
        }

        public string MazeName
        {
            get { return model.MazeName; }
        }

        public int Cols
        {
            get { return model.Cols; }
        }

        public int Rows
        {
            get { return model.Rows; }
        }

        public string MazeString
        {
            get { return model.MazeString; }
        }

        public Position MazeStartPoint
        {
            get { return model.MazeStartPoint; }
        }

        public Position MazeEndPoint
        {
            get { return model.MazeEndPoint; }
        }

        private void singlePlayerMazeWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (isAnimating)
            {
                return;
            }

            if (directions.ContainsKey(e.Key))
            {
                Direction direction = directions[e.Key];
                if (model.IsMoveOk(mazeControl.PlayerPos, direction))
                {
                    mazeControl.PositionPlayer(direction);
                }
            }
            
            if ((mazeControl.PlayerPos.Col == MazeEndPoint.Col) 
                && (mazeControl.PlayerPos.Row == MazeEndPoint.Row))
            {
                FinishGame();
            }
        }

        private void FinishGame()
        {
            MessageBox.Show("You escaped!");
            if (!Restart())
            {
                Dispatcher.Invoke(() =>
                {
                    Close();
                    MainWindow win = (MainWindow)Application.Current.MainWindow;
                    win.Show();
                });
            }
        }

        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            Restart();
        }

        private bool Restart()
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Do you want to restart?",
                                                                "Restart game",
                                                                MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                isAnimating = false;
                mazeControl.Restart();
                return true;
            }
            return false;
        }

        private void MainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            if (isAnimating == false)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Do you want to go back?",
                                                                    "Back to main menu",
                                                                    MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    MainWindow win = (MainWindow)Application.Current.MainWindow;
                    win.Show();
                    Close();
                }
            }
        }

        private void SolveMazeButton_Click(object sender, RoutedEventArgs e)
        {
            if (isAnimating == false)
            {
                string solution = model.SolveMaze();
                if (solution != null)
                {
                    AnimateFromAnotherThread(solution);
                }
            }
        }

        public void AnimateFromAnotherThread(string solution)
        {
            isAnimating = true;
            Position startPoint = mazeControl.PlayerPos;
            Task animation = new Task(() =>
            {

                mazeControl.Restart();
                System.Threading.Thread.Sleep(300);

                for (int i = 0; (i < solution.Length) && (isAnimating == true); i++)
                {
                    mazeControl.PositionPlayer(solution[i]);
                    System.Threading.Thread.Sleep(300);
                }

                if (isAnimating == false)
                {
                    return;
                }
                
                MessageBoxResult messageBoxResult = MessageBox.Show("Continue playing?",
                                                                    "Continue playing",
                                                                    MessageBoxButton.YesNo);
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
            animation.Start();
        }

        private void singlePlayerMazeWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isAnimating = false;
        }

       
    }
}
