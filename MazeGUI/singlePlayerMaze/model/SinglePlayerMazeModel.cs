using MazeGUI.etc;
using MazeLib;
using MVC;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI.singlePlayerMaze.model
{
    /// <summary>
    /// single player model
    /// </summary>
    class SinglePlayerMazeModel : AbstractMazeModel, ISinglePlayerMazeModel
    {
        /// <summary>
        /// the search algorithm to solve the mazes with
        /// </summary>
        private int searchAlgoritm;
        /// <summary>
        /// the maze solution. requested only once from the server
        /// </summary>
        private string mazeSolution;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="m">the maze to play</param>
        public SinglePlayerMazeModel(Maze m) : base(m)
        {
            searchAlgoritm = Properties.Settings.Default.SearchAlgorithm;
        }

        /// <summary>
        /// solve the maze
        /// </summary>
        /// <returns>string representation of the solution</returns>
        public string SolveMaze()
        {
            // if the solution is null
            if (mazeSolution == null)
            {
                // ask for a solution from the server
                Statues stat = ClientSingleton.Client.SendMesseage("solve " +
                                                                    MazeName + " " +
                                                                    searchAlgoritm);
                // if received null from the server
                if (stat == null)
                {
                    return null;
                }
                // return the mazesolution using the solution from the server
                mazeSolution = MazeSolution.FromJson(stat.Message);
            }
            return mazeSolution;
        }
    }
}
