using System.Collections.Generic;
using MazeLib;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// MazeAdapter, make adaptation from maze to ISearchable 
    /// </summary>
    public class MazeAdapter : ISearchable<Position>
    {
        private Maze maze;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="m">is the maze to save</param>
        public MazeAdapter(Maze m)
        {
            maze = m;
        }

        /// <summary>
        /// get the Initial State, apdate the cost to zero.
        /// </summary>
        /// <returns>the state to start from</returns>
        public State<Position> GetInitialState()
        {
            State<Position> state = State<Position>.StatePool.getState(maze.InitialPos);
            state.Cost = 0;
            state.CameFrom = null;
            return state;
        }

        /// <summary>
        /// get the goal state, apdate the cost to one.
        /// </summary>
        /// <returns>the state that beeing search</returns>
        public State<Position> GetGoalState()
        {
            State<Position> state = State<Position>.StatePool.getState(maze.GoalPos);
            state.Cost = 1;
            return state;
        }

        /// <summary>
        /// check all the direccion and found the possible states
        /// </summary>
        /// <param name="s">the state to look from</param>
        /// <returns>list of Possible States from s </returns>
        public List<State<Position>> GetAllPossibleStates(State<Position> position)
        {
            List<State<Position>> neighbors = new List<State<Position>>();

            int row = position.getState().Row;
            int col = position.getState().Col;
            State<Position> state;

            if ((maze.Rows > row + 1) && (maze[row + 1, col] == CellType.Free))
            {
                state = State<Position>.StatePool.getState(new Position(row + 1, col));
                state.Cost = position.Cost + 1;
                neighbors.Add(state);
            }
            if ((row > 0) && (maze[row - 1, col] == CellType.Free))
            {
                state = State<Position>.StatePool.getState(new Position(row - 1, col));
                state.Cost = position.Cost + 1;
                neighbors.Add(state);
            }
            if ((col > 0) && (maze[row, col - 1] == CellType.Free))
            {
                state = State<Position>.StatePool.getState(new Position(row, col - 1));
                state.Cost = position.Cost + 1;
                neighbors.Add(state);
            }
            if ((maze.Cols > col + 1) && (maze[row, col + 1] == CellType.Free))
            {
                state = State<Position>.StatePool.getState(new Position(row, col + 1));
                state.Cost = position.Cost + 1;
                neighbors.Add(state);
            }
            return neighbors;
        }


    }
}
