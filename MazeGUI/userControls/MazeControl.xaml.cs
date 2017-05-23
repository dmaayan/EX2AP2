using MazeGUI.etc;
using MazeLib;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace MazeGUI.userControls
{
    /// <summary>
    /// Interaction logic for MazeControl.xaml
    /// </summary>
    public partial class MazeControl : UserControl
    {
        /// <summary>
        /// dictionary of char and directions
        /// </summary>
        private static Dictionary<char, Direction> charDirections =
                                                   new Dictionary<char, Direction>
                                                   {
                                                       { '0', Direction.Left },
                                                       { '1', Direction.Right },
                                                       { '2', Direction.Up },
                                                       { '3', Direction.Down },
                                                   };
        /// <summary>
        /// rect size in pixels
        /// </summary>
        private static int rectSizePX = 10;
        /// <summary>
        /// the player current position
        /// </summary>
        private Position playerPos;

        /// <summary>
        /// constructor
        /// </summary>
        public MazeControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// event fi
        /// </summary>
        /// <param name="sender">viewbox</param>
        /// <param name="e">event args</param>
        private void MazeControl_Loaded(object sender, RoutedEventArgs e)
        {
            canvasBorder.BorderThickness = new Thickness(0.2);
            // set the canvas size
            mazeCanvas.Width = rectSizePX * Rows;
            mazeCanvas.Height = rectSizePX * Cols;
            InitializeMazeLabels();
        }

        /// <summary>
        /// initialize the canvas with the maze
        /// </summary>
        private void InitializeMazeLabels()
        {
            // rectangles size
            double rectHeight = rectSizePX;
            double rectWidth = rectSizePX;
            // player size
            player.Width = rectWidth;
            player.Height = rectHeight;
            // distance from the top left corner of the canvas
            double distanceFromLeft = 0;
            double distanceFromTop = 0;
            char sign;
            // fill all the rows
            for (int i = 0; i < Rows; i++)
            {
                // reset the distance from the left for each row
                distanceFromLeft = 0;
                for (int j = 0; j < Cols; j++)
                {
                    // create new rectangle
                    Rectangle rectangle = new Rectangle();
                    rectangle.Width = rectWidth;
                    rectangle.Height = rectHeight;
                    // set place on the canvas
                    Canvas.SetLeft(rectangle, distanceFromLeft);
                    Canvas.SetTop(rectangle, distanceFromTop);
                    sign = MazeString[i * Cols + j];
                    // set the color according to the char
                    rectangle.Fill = ColorFactory.GetColor(sign);
                    // add the rectangle to the canvas's children
                    mazeCanvas.Children.Add(rectangle);
                    distanceFromLeft = (distanceFromLeft + rectWidth);
    
                }
                // add the distance from the top for each row
                distanceFromTop = (distanceFromTop + rectHeight);
            }
            // position the player on the starting position
            PositionPlayer(MazeStartPoint.Row, MazeStartPoint.Col);
        }

        /// <summary>
        /// position the player
        /// </summary>
        /// <param name="row">the row to position</param>
        /// <param name="col">the col to position</param>
        public void PositionPlayer(int row, int col)
        {
            Dispatcher.Invoke(() =>
            {
                // keep track on the current location
                PlayerPos = new Position(row, col);
                // change the rectangle position on the canvas
                Canvas.SetLeft(player, col * rectSizePX);
                Canvas.SetTop(player, row * rectSizePX);
            });
        }

        /// <summary>
        /// position the player
        /// </summary>
        /// <param name="direct">the direction to move the player to</param>
        public void PositionPlayer(Direction direct)
        {
            Dispatcher.Invoke(() =>
            {
                // for each direction, get the proper movment and use the first position player
                switch (direct)
                {
                    case Direction.Down:
                        {
                            PositionPlayer(PlayerPos.Row + 1, PlayerPos.Col);
                            break;
                        }
                    case Direction.Up:
                        {
                            PositionPlayer(PlayerPos.Row - 1, PlayerPos.Col);
                            break;
                        }
                    case Direction.Left:
                        {
                            PositionPlayer(PlayerPos.Row, PlayerPos.Col - 1);
                            break;
                        }
                    case Direction.Right:
                        {
                            PositionPlayer(PlayerPos.Row, PlayerPos.Col + 1);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            });
        }

        /// <summary>
        /// position the player
        /// </summary>
        /// <param name="v"></param>
        public void PositionPlayer(char v)
        {
            // convert the char to direcetion
            if (charDirections.ContainsKey(v))
            {
                // use the position player method with direction
                PositionPlayer(charDirections[v]);
            }
        }

        /// <summary>
        /// restart the player position
        /// </summary>
        public void Restart()
        {
            Dispatcher.Invoke(() =>
            {
                PositionPlayer(MazeStartPoint.Row, MazeStartPoint.Col);
            });
        }


        /// <summary>
        /// Using a DependencyProperty as the backing store for Maze.
        /// </summary>
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(MazeControl),
                                        new PropertyMetadata(default(int)));

        /// <summary>
        /// Using a DependencyProperty as the backing store for Maze.
        /// </summary>
        public static readonly DependencyProperty ColsProperty =
            DependencyProperty.Register("Cols", typeof(int), typeof(MazeControl),
                                        new PropertyMetadata(default(int)));

        /// <summary>
        /// Using a DependencyProperty as the backing store for Maze.
        /// </summary>
        public static readonly DependencyProperty MazeStringProperty =
            DependencyProperty.Register("MazeString", typeof(string), typeof(MazeControl),
                                        new PropertyMetadata(default(string)));

        /// <summary>
        /// Using a DependencyProperty as the backing store for Maze.
        /// </summary>
        public static readonly DependencyProperty MazeStartPointProperty =
            DependencyProperty.Register("MazeStartPoint", typeof(Position), typeof(MazeControl),
                                        new PropertyMetadata(default(Position)));

        /// <summary>
        /// Using a DependencyProperty as the backing store for Maze.
        /// </summary>
        public static readonly DependencyProperty MazeEndPointProperty =
            DependencyProperty.Register("MazeEndPoint", typeof(Position), typeof(MazeControl),
                                        new PropertyMetadata(default(Position)));

        /// <summary>
        /// rows property
        /// </summary>
        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        /// <summary>
        /// cols property
        /// </summary>
        public int Cols
        {
            get { return (int)GetValue(ColsProperty); }
            set { SetValue(ColsProperty, value); }
        }

        /// <summary>
        /// maze string property
        /// </summary>
        public string MazeString
        {
            get { return (string)GetValue(MazeStringProperty); }
            set { SetValue(MazeStringProperty, value); }
        }

        /// <summary>
        /// maze start point property
        /// </summary>
        public Position MazeStartPoint
        {
            get { return (Position)GetValue(MazeStartPointProperty); }
            set { SetValue(MazeStartPointProperty, value); }
        }

        /// <summary>
        /// maze end point property
        /// </summary>
        public Position MazeEndPoint
        {
            get { return (Position)GetValue(MazeEndPointProperty); }
            set { SetValue(MazeEndPointProperty, value); }
        }

        /// <summary>
        /// player position property
        /// </summary>
        public Position PlayerPos
        {
            get { return playerPos; }
            set { playerPos = value; }
        }
    }
}
