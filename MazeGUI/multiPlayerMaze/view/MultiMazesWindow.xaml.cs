using MazeGUI.etc;
using MazeGUI.multiPlayerMaze.model;
using MazeGUI.multiPlayerMaze.viewModel;
using MazeLib;
using MVC;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace MazeGUI.multiPlayerMaze.view
{
    /// <summary>
    /// Interaction logic for MultiMazesWindow.xaml
    /// </summary>
    public partial class MultiMazesWindow : NonClosableWindow
    {
        /// <summary>
        /// the view model of this view
        /// </summary>
        MultiMazeViewModel model;

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
        /// <param name="maze">to open on this window</param>
        public MultiMazesWindow(Maze maze)
        {
            model = new MultiMazeViewModel(new MultiMazeModel(maze));
            this.DataContext = model;
            model.MoveOpponent += OpponentPlay;
            InitializeComponent();
        }

        /// <summary>
        /// maze name getter
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
        /// event on key down
        /// </summary>
        /// <param name="sender">to click the key</param>
        /// <param name="e">what key have been pressed</param>
        private void NonClosableWindow_KeyDown(object sender, KeyEventArgs e)
        {
            // check that the key is one of the directions
            if (directions.ContainsKey(e.Key))
            {
                // get the direction
                Direction direction = directions[e.Key];
                // check that the direction is valid for the maze
                if (model.IsMoveOk(PlayerMazeControl.PlayerPos, direction))
                {
                    // change the position of the player
                    PlayerMazeControl.PositionPlayer(direction);
                    // update the other player
                    model.SendMove(direction);
                }
            }

            // check if the player reached the end point of the maze
            if ((PlayerMazeControl.PlayerPos.Col == MazeEndPoint.Col)
                && (PlayerMazeControl.PlayerPos.Row == MazeEndPoint.Row))
            {
                FinishGame();
            }
        }

        /// <summary>
        /// click event on the back button
        /// </summary>
        /// <param name="sender">the button</param>
        /// <param name="e">Routed event args</param>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // let the server know about closing the game
            model.CloseGame();
            // open the main window and close this
            CloseGame();
        }

        /// <summary>
        /// when a message have been received from the opponent
        /// </summary>
        /// <param name="sender">is the messageTransmiter</param>
        /// <param name="statues">is the server message</param>
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
          
        /// <summary>
        /// the player have won the game
        /// </summary>
        private void FinishGame()
        {
            MessageBox.Show("You escaped!");
            model.FinishGame();
            CloseGame();
        }

        /// <summary>
        /// the player closed the game
        /// </summary>
        private void CloseGame()
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            Close();
        }

        /// <summary>
        /// event of closing the window
        /// </summary>
        /// <param name="sender">this window</param>
        /// <param name="e">event args</param>
        private void NonClosableWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            model.MoveOpponent -= OpponentPlay;
        }
    }
}
