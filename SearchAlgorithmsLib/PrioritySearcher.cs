using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Priority_Queue;

namespace SearchAlgorithmsLib
{
    public abstract class PrioritySearcher<T> : Searcher<T>
    {
        private SimplePriorityQueue<State<T>> openList;
        private Dictionary<State<T>, KeyValuePair<State<T>, double>> updatedStates;

        public PrioritySearcher()
        {
            openList = new SimplePriorityQueue<State<T>>();
            updatedStates = new Dictionary<State<T>, KeyValuePair<State<T>, double>>();
        }

        public SimplePriorityQueue<State<T>> OpenList
        {
            get { return openList; }
        }

        public Dictionary<State<T>, KeyValuePair<State<T>, double>> UpdatedStates
        {
            get { return updatedStates; }
        }

        protected State<T> popOpenList()
        {
            increaseEvaluatedNodes();
            return openList.Dequeue();
        }
        // a property of openList 
        public int OpenListSize
        {
            get { return openList.Count; }
        }

        public void addToOpenList(State<T> state)
        {
            openList.Enqueue(state, (float)(state.Cost));
            UpdatedStates.Add(state, new KeyValuePair<State<T>, double>(state.CameFrom, state.Cost));
        }

        public void updateStateIfPathBetter(State<T> current)
        {
            State<T> state = openList.Where(elem => current.Equals(elem)).First();

            double costOfState = updatedStates[state].Value;
            if (current.Cost < costOfState)
            {
                updatedStates[state] = new KeyValuePair<State<T>, double>
                                       (updatedStates[current].Key, updatedStates[current].Value);
                openList.UpdatePriority(state, (float)updatedStates[current].Value);
            }

        }

        public override abstract Solution<T> search(ISearchable<T> searchable);
    }
}
