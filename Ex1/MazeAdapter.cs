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
            State<Position> state = State<Position>.StatePool.getState(maze.InitialPos);
            state.Cost = 0;
            return state;
        }

        public State<Position> getGoalState()
        {
            State<Position> state = State<Position>.StatePool.getState(maze.GoalPos);
            state.Cost = 1;
            return state;
        }

        public List<State<Position>> getAllPossibleStates(State<Position> position)
        {
            List<State<Position>> neighbors = new List<State<Position>>();

            int row = position.getState().Row;
            int col = position.getState().Col;
            State<Position> state;

            if ((maze.Rows > row + 1) && (maze[row + 1, col] == CellType.Free))
            {
                state = State<Position>.StatePool.getState(new Position(row + 1, col));
                state.Cost = 1;
                neighbors.Add(state);
            }
            if ((row > 1) && (maze[row - 1, col] == CellType.Free))
            {
                state = State<Position>.StatePool.getState(new Position(row - 1, col));
                state.Cost = 1;
                neighbors.Add(state);
            }
            if ((col > 1) && (maze[row, col - 1] == CellType.Free))
            {
                state = State<Position>.StatePool.getState(new Position(row, col - 1));
                state.Cost = 1;
                neighbors.Add(state);
            }
            if ((maze.Cols > col + 1) && (maze[row, col + 1] == CellType.Free))
            {
                state = State<Position>.StatePool.getState(new Position(row, col + 1));
                state.Cost = 1;
                neighbors.Add(state);
            }
            return neighbors;
        }


        

    }
}
