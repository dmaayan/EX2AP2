using MazeGeneratorLib;
using MazeGUI.etc;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MazeGUI.userControls
{
    /// <summary>
    /// Interaction logic for MazeControl.xaml
    /// </summary>
    public partial class MazeControl : UserControl
    {
        private static Dictionary<char, Direction> charDirections =
                                                   new Dictionary<char, Direction>
                                                   {
                                                       { '0', Direction.Left },
                                                       { '1', Direction.Right },
                                                       { '2', Direction.Up },
                                                       { '3', Direction.Down },
                                                   };
        private static int rectSizePX = 10;
        private List<Rectangle> rects;
        private Position playerPos;

        public MazeControl()
        {
            rects = new List<Rectangle>();
            InitializeComponent();
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            canvasBorder.BorderThickness = new Thickness(0.2);
            mazeCanvas.Width = rectSizePX * Rows;
            mazeCanvas.Height = rectSizePX * Cols;
            InitializeMazeLabels();
        }

        private void InitializeMazeLabels()
        {
            double rectHeight = rectSizePX;
            double rectWidth = rectSizePX;
            player.Width = rectWidth;
            player.Height = rectHeight;
            double distanceFromLeft = 0;
            double distanceFromTop = 0;

            for (int i = 0; i < Rows; i++)
            {
                distanceFromLeft = 0;
                for (int j = 0; j < Cols; j++)
                {

                    Rectangle rectangle = new Rectangle();
                    rectangle.Width = rectWidth;
                    rectangle.Height = rectHeight;
                    Canvas.SetLeft(rectangle, distanceFromLeft);
                    Canvas.SetTop(rectangle, distanceFromTop);
                    rectangle.Fill = ColorFactory.GetColor(MazeString[i * Cols + j]);
                    rects.Add(rectangle);
                    mazeCanvas.Children.Add(rectangle);
                    distanceFromLeft = (distanceFromLeft + rectWidth);
                }
                distanceFromTop = (distanceFromTop + rectHeight);
            }
            PositionPlayer(MazeStartPoint.Row, MazeStartPoint.Col);
        }

        public void PositionPlayer(int row, int col)
        {
            Dispatcher.Invoke(() =>
            {
                PlayerPos = new Position(row, col);
                Canvas.SetLeft(player, col * rectSizePX);
                Canvas.SetTop(player, row * rectSizePX);
            });
        }

        public void PositionPlayer(Direction direct)
        {
            Dispatcher.Invoke(() =>
            {
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

        public void PositionPlayer(char v)
        {
            if (charDirections.ContainsKey(v))
            {
                PositionPlayer(charDirections[v]);
            }
        }

        public void Restart()
        {
            Dispatcher.Invoke(() =>
            {
                PositionPlayer(MazeStartPoint.Row, MazeStartPoint.Col);
            });
        }


        //Using a DependencyProperty as the backing store for Maze.This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(MazeControl), new PropertyMetadata(default(int)));

        //Using a DependencyProperty as the backing store for Maze.This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColsProperty =
            DependencyProperty.Register("Cols", typeof(int), typeof(MazeControl), new PropertyMetadata(default(int)));

        // Using a DependencyProperty as the backing store for Maze.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MazeStringProperty =
            DependencyProperty.Register("MazeString", typeof(string), typeof(MazeControl), new PropertyMetadata(default(string)));

        // Using a DependencyProperty as the backing store for Maze.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MazeStartPointProperty =
            DependencyProperty.Register("MazeStartPoint", typeof(Position), typeof(MazeControl), new PropertyMetadata(default(Position)));

        // Using a DependencyProperty as the backing store for Maze.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MazeEndPointProperty =
            DependencyProperty.Register("MazeEndPoint", typeof(Position), typeof(MazeControl), new PropertyMetadata(default(Position)));

        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        public int Cols
        {
            get { return (int)GetValue(ColsProperty); }
            set { SetValue(ColsProperty, value); }
        }
        
        public string MazeString
        {
            get { return (string)GetValue(MazeStringProperty); }
            set { SetValue(MazeStringProperty, value); }
        }

        public Position MazeStartPoint
        {
            get { return (Position)GetValue(MazeStartPointProperty); }
            set { SetValue(MazeStartPointProperty, value); }
        }

        public Position MazeEndPoint
        {
            get { return (Position)GetValue(MazeEndPointProperty); }
            set { SetValue(MazeEndPointProperty, value); }
        }

        public Position PlayerPos
        {
            get { return playerPos; }
            set { playerPos = value; }
        }
    }
}
