using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class DFSAlgo<T> : NonPrioritySearcher<T>
    {
        private HashSet<State<T>> visited;

        public override Solution<T> search(ISearchable<T> searchable)
        {
            visited = new HashSet<State<T>>();
            State<T> state = searchable.getInitialState();
            State<T> goal = searchable.getGoalState();
            Stack<State<T>> stack = new Stack<State<T>>();

            stack.Push(state);
            state.CameFrom = null;
            Parents.Add(state, null);

            while (stack.Count > 0)
            {
                increaseEvaluatedNodes();
                state = stack.Pop();
                if (goal.Equals(state))
                {
                    return Backtrace(goal);
                }
                if (!visited.Contains(state))
                {
                    visited.Add(state);
                    foreach (State<T> neighbour in searchable.getAllPossibleStates(state).Where
                                                   (elem => !visited.Contains(elem)))
                    {
                        stack.Push(neighbour);
                        neighbour.CameFrom = state;
                        Parents.Add(neighbour, state);
                    }
                }
            }
            return null;
        }

        private Solution<T> Backtrace(State<T> goal)
        {
            List<State<T>> solution = new List<State<T>>();
            State<T> state = goal;
            while (state != null)
            {
                solution.Add(state);
                state = Parents[state];
            }
            return new Solution<T>(solution);
        }
    }
}
