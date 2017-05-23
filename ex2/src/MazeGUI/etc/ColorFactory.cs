using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MazeGUI.etc
{
    /// <summary>
    /// Color factory, match to each char the Color or image
    /// </summary>
    public class ColorFactory
    {
        /// <summary>
        /// returns the color or image of the using the chars
        /// </summary>
        /// <param name="c">is the char</param>
        /// <returns>black or white or image </returns>
        public static Brush GetColor(char c)
        {
            switch (c)
            {
                case '1':
                    {
                        // a wall color
                        return Brushes.Black;
                    }

                case '#':
                    {
                        // the exit point Image
                        return new ImageBrush(new BitmapImage
                                             (new Uri(@"Images/key.jpg",
                                              UriKind.Relative)));
                    }
                default:
                    {
                        // all other chars
                        return Brushes.White;
                    }
            }
        }
    }
}