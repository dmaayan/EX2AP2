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
        private int rows;
        private int cols;
        private string name;
        private Maze maze;
        private List<Rectangle> rects;

        public MazeControl()
        {
            InitializeComponent();
            
        }

        public void start()
        {
            /*DFSMazeGenerator mz = new DFSMazeGenerator();
            Maze m = mz.Generate(30, 30);
            m.Name = "first";
            maze = m;*/
            rects = new List<Rectangle>();
            mazeCanvas.Width = rectSizePX * Rows;
            mazeCanvas.Height = rectSizePX * Cols;
            initializeMazeLabels();
        }

        private void initializeMazeLabels()
        {
            double rectHeight = rectSizePX;
            double rectWidth = rectSizePX;
            double distanceFromLeft = 0;
            double distanceFromTop = 0;
            SolidColorBrush color = Brushes.Red;
            for(int i = 0; i < Cols; i++)
            {
                distanceFromLeft = 0;
                for (int j = 0; j < Rows; j++)
                {
                    Rectangle rectangle = new Rectangle();
                    rectangle.Width = rectWidth;
                    rectangle.Height = rectHeight;
                    Canvas.SetLeft(rectangle, distanceFromLeft);
                    Canvas.SetTop(rectangle, distanceFromTop);
                    rectangle.Fill = color;
                    rectangle.Stroke = Brushes.Black;
                    rects.Add(rectangle);
                    mazeCanvas.Children.Add(rectangle);
                    distanceFromLeft = (distanceFromLeft + rectWidth);
                }
                color = Brushes.Purple;
                distanceFromTop = (distanceFromTop + rectHeight);
            }
        }

        //Using a DependencyProperty as the backing store for Maze.This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColsProperty =
            DependencyProperty.Register("Cols", typeof(int), typeof(MazeControl), new PropertyMetadata(default(int)));

        //Using a DependencyProperty as the backing store for Maze.This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(MazeControl), new PropertyMetadata(default(int)));

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

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        private static void OnItemsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // AutocompleteTextBox source = d as AutocompleteTextBox;
            // Do something...
        }


        //Using a DependencyProperty as the backing store for Maze.This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(MazeControl), new PropertyMetadata(default(string)));

        public Maze Maze
        {
            get { return (Maze)GetValue(MazeProperty); }
            set { SetValue(MazeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Maze.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MazeProperty =
            DependencyProperty.Register("Maze", typeof(Maze), typeof(MazeControl), new PropertyMetadata(default(Maze)));
            
    }
}
