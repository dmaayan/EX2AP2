using Client;
using MazeGUI.etc;
using MazeGUI.multiPlayerMaze.model;
using MazeGUI.multiPlayerMaze.viewModel;
using MazeGUI.userControls;
using MazeLib;
using MVC;
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

namespace MazeGUI.multiPlayerMaze.view
{
    /// <summary>
    /// Interaction logic for MultiMazesWindow.xaml
    /// </summary>
    public partial class MultiMazesWindow : NonClosableWindow
    {
        MultiMazeViewModel model;

        public MultiMazesWindow(Maze maze)
        {
            model = new MultiMazeViewModel(new MultiMazeModel(maze));
            this.DataContext = model;
            model.MoveOpponent += OpponentPlay;
            InitializeComponent();
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

        private void NonClosableWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (directions.ContainsKey(e.Key))
            {
                Direction direction = directions[e.Key];
                if (model.IsMoveOk(PlayerMazeControl.PlayerPos, direction))
                {
                    PlayerMazeControl.PositionPlayer(direction);
                    model.SendMove(direction);
                }
            }

            if ((PlayerMazeControl.PlayerPos.Col == MazeEndPoint.Col)
                && (PlayerMazeControl.PlayerPos.Row == MazeEndPoint.Row))
            {
                FinishGame();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            model.CloseGame();
            CloseGame();
        }

        private void OpponentPlay(object sender, StatuesEventArgs statues)
        {
            // Opponent made a move
            if (statues.Stat.Stat == Status.Play)
            {
                Direction direction = (Direction)Enum.Parse(typeof(Direction), statues.Stat.Message);
                OpponentMazeControl.PositionPlayer(direction);
            }
            // Opponent end the game 
            else if (statues.Stat.Stat == Status.CloseGame)
            {
                MessageBox.Show("The other player end the game");
                Dispatcher.Invoke(() =>
                {
                    CloseGame();
                }); 
            }
            // Opponent won the game
            else if (statues.Stat.Stat == Status.Finish)
            {
                MessageBox.Show("You lost!");
                Dispatcher.Invoke(() =>
                {
                    CloseGame();
                });
            }
        }
          
        // player won the game
        private void FinishGame()
        {
            MessageBox.Show("You escaped!");
            model.FinishGame();
            CloseGame();
        }

        private void CloseGame()
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            Close();
        }

        private void NonClosableWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            model.MoveOpponent -= OpponentPlay;
        }
        /*
private void CloseGame()
{
   model.CloseGame();
   Close();
}
*/
        /*
        private void OpponentMove( )
        {
            Direction direction = (Direction)Enum.Parse(typeof(Direction), statues.Stat.Message);
            OpponentMazeControl.PositionPlayer(direction);
        }
        */
    }
}
