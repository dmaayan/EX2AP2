using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class BFS<T> : PrioritySearcher<T>
    {
        // Searcher's abstract method overriding 
        public override Solution<T> search(ISearchable<T> searchable)
        {
            addToOpenList(searchable.getInitialState()); 
            HashSet<State<T>> closed = new HashSet<State<T>>();

            State<T> goal = searchable.getGoalState();

            while (OpenListSize > 0)
            {
                State<T> state = popOpenList();         // removes the best state
                closed.Add(state);                      // add it to the closed hash

                if (state.Equals(goal))
                {
                    return backTrace(goal);            // back traces through the parents
                }
                // calling the delegated method, returns a list of states with state as a parent 
                List<State<T>> succerssors = searchable.getAllPossibleStates(state);
                foreach (State<T> s in succerssors)
                { 
                    if (!closed.Contains(s) && !OpenList.Contains(s))
                    {
                        //***lock
                        s.CameFrom = state;  
                        addToOpenList(s);
                    }
                    // the cost of the new way is better
                    else if (OpenList.Contains(s))
                    {
                        s.CameFrom = state;
                        updateStateIfPathBetter(s);
                    }
                }
            }
            return null;
        }
        //** לבדוק סינטקס של פונקציות
        private Solution<T> backTrace(State<T> goal)
        {
            // ** לבדוק עניין של זכרון
            List<State<T>> solution = new List<State<T>>();
            State<T> state = goal;
            while (state != null)
            {
                solution.Add(state);
                state = state.CameFrom;
            } 
            return new Solution<T>(solution, getNumberOfNodesEvaluated());
        }


    }
}

