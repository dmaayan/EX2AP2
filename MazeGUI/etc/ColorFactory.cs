using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MazeGUI.etc
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
                        // the exit point Image
                        return new ImageBrush(new BitmapImage(new Uri(@"../../Images/key.jpg", UriKind.Relative)));
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