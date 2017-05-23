using MazeGUI.etc;
using MazeLib;
using MVC;

namespace MazeGUI.singlePlayerSettings.model
{
    /// <summary>
    /// the single player Settings model inherits ISingleSettingsModel
    /// </summary>
    public class SingleSettingsModel : ISingleSettingsModel
    {
        /// <summary>
        /// the number of the colums in the maze
        /// </summary>
        private int cols;

        /// <summary>
        /// the number of the rows in the maze
        /// </summary>
        private int rows;

        /// <summary>
        /// the name of the maze
        /// </summary>
        private string mazeName;

        /// <summary>
        /// a property of cols
        /// </summary>
        public int Cols {
            get { return cols; }
            set { cols = value; }
        }

        /// <summary>
        /// a property of rows 
        /// </summary>>
        public int Rows {
            get { return rows; }
            set { rows = value; }
        }

        /// <summary>
        /// a property of mazeName 
        /// </summary>
        public string MazeName {
            get { return mazeName; }
            set { mazeName = value; }
        }

        /// <summary>
        /// Connect to the server to generate maze.
        /// </summary>
        /// <returns>a maze</returns>
        public Maze Connect()
        {
            Statues stat = ClientSingleton.Client.SendMesseage("generate " + mazeName +
                                                               " " + Rows + " " + Cols);
            // check if the stat received currctly
            if (stat == null)
            {
                return null;
            }
            return Maze.FromJSON(stat.Message);
        }
    }
}
