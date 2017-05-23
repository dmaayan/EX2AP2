using MazeGUI.etc;
using MazeLib;
using MVC;
using Newtonsoft.Json;

namespace MazeGUI.multiPlayerSettings.model
{
    /// <summary>
    /// the multi player Settings model inherits IMultiSettingsModel
    /// </summary>
    public class MultiSettingsModel : IMultiSettingsModel
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
        public int Cols
        {
            get { return cols; }
            set { cols = value; }
        }

        /// <summary>
        /// a property of rows 
        /// </summary>>
        public int Rows
        {
            get { return rows; }
            set { rows = value; }
        }

        /// <summary>
        /// a property of mazeName 
        /// </summary>
        public string MazeName
        {
            get { return mazeName; }
            set { mazeName = value; }
        }

        /// <summary>
        /// function of starting the game 
        /// </summary>
        /// <returns>a maze</returns>
        public Maze StartGame()
        {
            Statues stat = ClientSingleton.Client.SendMesseage("start " + mazeName + 
                                                               " " + Rows + " " + Cols);
            // check if the stat received currctly
            if (stat == null)
            {
                return null;
            }
            return Maze.FromJSON(stat.Message);

        }

        /// <summary>
        /// Get a list of games
        /// </summary>
        /// <returns> a list of games that waiting to start </returns>
        public string[] GetListGames()
        {
            Statues stat = ClientSingleton.Client.SendMesseage("list");
            // check if the stat received currctly
            if (stat == null)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<string[]>(stat.Message);
        }

        /// <summary>
        /// apply to join the game.
        /// </summary>
        /// <param name="game">the name of the game to join to</param>
        /// <returns>a maze</returns>
        public Maze JoinGame(string game)
        {
            Statues stat = ClientSingleton.Client.SendMesseage("join " + game);
            // check if the stat received currctly
            if (stat == null)
            {
                return null;
            }
            return Maze.FromJSON(stat.Message);
        }
    }
}
