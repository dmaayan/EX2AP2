using System.Windows.Media;

namespace MazeGUI.etc
{
    public class ColorFactory
    {
        /// <summary>
        /// returns the color of the using the chars
        /// </summary>
        /// <param name="c">is the char</param>
        /// <returns>black or white</returns>
        public static Brush GetColor(char c)
        {
            switch (c)
            {
                case '1':
                    {
                        // a wall color
                        return Brushes.Black;
                    }
                default:
                    {
                        // undefined char color
                        return Brushes.White;
                    }
            }
        }
    }
}