using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class DFSAlgo<T> : Searcher<T>
    {
        private HashSet<State<T>> visited;
        private Dictionary<State<T>, State<T>> closed;

        public override Solution<T> search(ISearchable<T> searchable)
        {
            visited = new HashSet<State<T>>();
            closed = new Dictionary<State<T>, State<T>>();
            State<T> v = searchable.getInitialState();
            State<T> goal = searchable.getGoalState();
            Stack<State<T>> S = new Stack<State<T>>();
            S.Push(v);
            closed.Add(v, null);
            while (S.Count > 0)
            {
                v = S.Pop();
                if (goal.Equals(v))
                {
                    return Backtrace(goal);
                }
                if (!visited.Contains(v))
                {
                    visited.Add(v);
                    foreach (State<T> w in searchable.getAllPossibleStates(v))
                    {
                        S.Push(w);
                        closed.Add(w, v);
                    }
                }
            }
            return null;
        }

        private Solution Backtrace(State<T> goal)
        {
            List<State<T>> solution = new List<State<T>>();
            State<T> v = goal;
            while (v != null)
            {
                solution.Add(v);
                v = closed[v];
            }
            return new Solution(solution);
        }
    }
}
