using MazeLib;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace Command
{
    public delegate Solution<Position> searchAlgo(ISearchable<Position> searchable);
    public interface IModel
    {
        Maze GenerateMaze(string name, int rows, int cols);

        Maze GetMaze(string name);

        Maze StartGame(string name, int rows, int cols, TcpClient client);

        Maze JoinGame(string name, TcpClient client);

        Maze PlayGame(string name, TcpClient client);

        void CloseGame(string name);

        Solution<Position> SolveMaze(string name, searchAlgo search);

    }
}
