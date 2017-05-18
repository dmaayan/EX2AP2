using MazeGeneratorLib;
using MazeGUI.singlePlayerMaze.model;
using MazeGUI.singlePlayerMaze.viewModel;
using MazeGUI.singlePlayerSettings.model;
using MazeGUI.singlePlayerSettings.viewModel;
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

namespace MazeGUI
{
    /// <summary>
    /// Interaction logic for SinglePlayerMazeWindow.xaml
    /// </summary>
    public partial class SinglePlayerMazeWindow : Window
    {
        private SinglePlayerMazeViewModel model;
        MazePlayer mazePlayer;

        public SinglePlayerMazeWindow(Maze m)
        {
            model = new SinglePlayerMazeViewModel(new SinglePlayerMazeModel(m));
            this.DataContext = model;
            InitializeComponent();
            mazeControl.start();
            mazePlayer = new MazePlayer(MazeStartPoint);
            mazeControl.PositionPlayer(mazePlayer.MazePoint, mazePlayer.MazePoint);
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
            int row = mazePlayer.MazePoint.Row;
            int col = mazePlayer.MazePoint.Col;
            Position lastPosition = mazePlayer.MazePoint;
            if (e.Key == Key.Down)
            {
                if (model.IsMoveOk(mazePlayer.MazePoint, Direction.Down))
                {
                    mazePlayer.MazePoint = new Position(row + 1, col);
                    mazeControl.PositionPlayer(mazePlayer.MazePoint, lastPosition);
                }
            }
            else if (e.Key == Key.Up)
            {
                if (model.IsMoveOk(mazePlayer.MazePoint, Direction.Up))
                {
                    mazePlayer.MazePoint = new Position(row - 1, col);
                    mazeControl.PositionPlayer(mazePlayer.MazePoint, lastPosition);
                }
            }
            else if (e.Key == Key.Left)
            {
                if (model.IsMoveOk(mazePlayer.MazePoint, Direction.Left))
                {
                    mazePlayer.MazePoint = new Position(row, col - 1);
                    mazeControl.PositionPlayer(mazePlayer.MazePoint, lastPosition);
                }
            }
            else if (e.Key == Key.Right)
            {
                if (model.IsMoveOk(mazePlayer.MazePoint, Direction.Right))
                {
                    mazePlayer.MazePoint = new Position(row, col + 1);
                    mazeControl.PositionPlayer(mazePlayer.MazePoint, lastPosition);
                }
            }
            if ((mazePlayer.MazePoint.Col == MazeEndPoint.Col) 
                && (mazePlayer.MazePoint.Row == MazeEndPoint.Row))
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
