using Client;
using MazeGUI.etc;
using MazeGUI.multiPlayerMaze.model;
using MazeGUI.multiPlayerMaze.viewModel;
using MazeGUI.userControls;
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
            model.MoveOpponent += MoveOpponentPlayer;
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            model.Close();
            Close();
        }

        private void MoveOpponentPlayer(object sender, StatuesEventArgs statues)
        {
            Direction direction = (Direction)Enum.Parse(typeof(Direction), statues.Stat.Message);
            OpponentMazeControl.PositionPlayer(direction);
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

        private void FinishGame()
        {
            MessageBox.Show("You escaped!");
            
        }
    }
}
