using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using SearchAlgorithmsLib;

namespace Ex1
{
    public class MazeAdapter : ISearchable<Position>
    {
        private Maze maze;

        public MazeAdapter(Maze m)
        {
            this.maze = m;
        }

        public State<Position> getInitialState()
        {
            return State<Position>.StatePool.getState(maze.InitialPos);
        }

        public State<Position> getGoalState()
        {
            return State<Position>.StatePool.getState(maze.GoalPos);
        }

        public List<State<Position>> getAllPossibleStates(State<Position> position)
        {
            List<State<Position>> neighbors = new List<State<Position>>();

            int row = position.getState().Row;
            int col = position.getState().Col;

            if ((maze.Rows > row) && (maze[row + 1, col] == CellType.Free))
            {
                neighbors.Add(State<Position>.StatePool.getState(new Position(row + 1, col)));
            }
            if ((row > 0) && (maze[row - 1, col] == CellType.Free))
            {
                neighbors.Add(State<Position>.StatePool.getState(new Position(row - 1, col)));
            }
            if ((col > 0) && (maze[row, col - 1] == CellType.Free))
            {
                neighbors.Add(State<Position>.StatePool.getState(new Position(row, col - 1)));
            }
            if ((maze.Cols > col) && (maze[row, col + 1] == CellType.Free))
            {
                neighbors.Add(State<Position>.StatePool.getState(new Position(row, col + 1)));
            }
            return neighbors;
        }


        

    }
}
