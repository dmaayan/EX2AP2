using MazeGeneratorLib;
using MazeGUI.singlePlayerMaze.model;
using MazeGUI.singlePlayerMaze.viewModel;
using MazeGUI.singlePlayerSettings.model;
using MazeGUI.singlePlayerSettings.viewModel;
using MazeLib;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MazeGUI
{
    /// <summary>
    /// Interaction logic for SinglePlayerMazeWindow.xaml
    /// </summary>
    public partial class SinglePlayerMazeWindow : Window
    {
        // cancel the control box of the window
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        //initializing members
        private SinglePlayerMazeViewModel model;
        private bool isAnimating = false;
        private Dictionary<Key, Direction> directions;
        private Dictionary<char, Direction> charDirections;

        public SinglePlayerMazeWindow(Maze m)
        {
            model = new SinglePlayerMazeViewModel(new SinglePlayerMazeModel(m));
            this.DataContext = model;
            InitializeComponent();
            mazeControl.start();
            singlePlayerMazeWindow.KeyDown += singlePlayerMazeWindow_KeyDown;

            directions = new Dictionary<Key, Direction>();
            directions.Add(Key.Up, Direction.Up);
            directions.Add(Key.Down, Direction.Down);
            directions.Add(Key.Left, Direction.Left);
            directions.Add(Key.Right, Direction.Right);

            charDirections = new Dictionary<char, Direction>();
            charDirections.Add('0', Direction.Left);
            charDirections.Add('1', Direction.Right);
            charDirections.Add('2', Direction.Up);
            charDirections.Add('3', Direction.Down);
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
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            Close();
        }

        private void SolveMazeButton_Click(object sender, RoutedEventArgs e)
        {
            string solution = model.SolveMaze();
            AnimateFromAnotherThread(solution);
        }

        public void AnimateFromAnotherThread(string solution)
        {
            isAnimating = true;
            Position startPoint = mazeControl.PlayerPos;
            Task animation = new Task(() =>
            {

                mazeControl.Restart();
                System.Threading.Thread.Sleep(300);
                int i = 0;
                while ((i < solution.Length) && (isAnimating == true))
                {
                    if (charDirections.ContainsKey(solution[i]))
                    {
                        mazeControl.PositionPlayer(charDirections[solution[i]]);
                    }
                    i++;
                    System.Threading.Thread.Sleep(300);
                }

                if (isAnimating == false)
                {
                    return;
                }
                isAnimating = false;
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
                
            });
            animation.Start();
        }

        private void singlePlayerMazeWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isAnimating = false;
        }

        private void singlePlayerMazeWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }
    }
}
