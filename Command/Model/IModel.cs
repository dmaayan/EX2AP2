using MazeLib;
using SearchAlgorithmsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace MVC
{
    // delegate for search algorithms
    public delegate Solution<Position> SearchAlgo(ISearchable<Position> searchable);

    public interface IModel
    {
        Maze SingleGameGenerateMaze(string name, int rows, int cols);

        Maze StartGame(string name, int rows, int cols, TcpClient client);

        Maze JoinGame(string name, TcpClient client);

        Maze PlayGame(Direction move, TcpClient client);

        Game CloseGame(string name);

        MazeSolution SolveMaze(string name, SearchAlgo search);

        string[] GetAllNames();

        Player GetOtherPlayer(TcpClient client);

        Game GetGameOfPlayer(TcpClient client);

    }
}
