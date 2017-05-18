using MazeGeneratorLib;
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
        private static int rectSizePX = 10;
        private List<Rectangle> rects;
        private Position player;
        public MazeControl()
        {
            InitializeComponent();
            rects = new List<Rectangle>();
        }

        public void start()
        {
            mazeCanvas.Width = rectSizePX * Rows;
            mazeCanvas.Height = rectSizePX * Cols;
            initializeMazeLabels();
        }

        private void initializeMazeLabels()
        {
            CanvasBorder.BorderThickness = new Thickness(0.2);
            double rectHeight = rectSizePX;
            double rectWidth = rectSizePX;
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
            //player = new Position(MazeStartPoint.Row, MazeStartPoint.Col);
        }

        public void PositionPlayer(Position mazeStartPoint, Position lastPosition)
        {
            Dispatcher.Invoke(() =>
            {
                rects[lastPosition.Row * Cols + lastPosition.Col].Fill =
                    ColorFactory.GetColor(MazeString[lastPosition.Row * Cols + lastPosition.Col]);
                rects[mazeStartPoint.Row * Cols + mazeStartPoint.Col].Fill = Brushes.Aquamarine;
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
    }
}
