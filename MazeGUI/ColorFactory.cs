using System.Windows.Media;

namespace MazeGUI
{
    public class ColorFactory
    {
        public static Brush GetColor(char c)
        {
            switch (c)
            {
                case '0':
                    {
                        // a road color
                        return Brushes.White;
                    }
                    
                case '1':
                    {
                        // a wall color
                        return Brushes.Black;
                    }
                case '*':
                    {
                        // the start point color
                        return Brushes.Red;
                    }
                case '#':
                    {
                        // the exit color
                        return Brushes.Green;
                    }
                default:
                    {
                        // undefined char color
                        return Brushes.Blue;
                    }
            }
        }
    }
}