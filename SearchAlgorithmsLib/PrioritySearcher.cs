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

        public PrioritySearcher()
        {
            openList = new SimplePriorityQueue<State<T>>();
        }
        //**   לבדוק מה באמת מחזיר - רפרנס או העתק

        public SimplePriorityQueue<State<T>> OpenList
        {
            get { return openList; }
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
        }

        public void updateStateIfPathBetter(State<T> current)
        {
            foreach (State<T> state in openList)
            {
                if (state.Equals(current))
                {
                    if (current.Cost < state.Cost)
                    {
                        state.CameFrom = current.CameFrom;
                        openList.UpdatePriority(state, (float)current.Cost);
                    }
                }
            }
        }

        public override abstract Solution<T> search(ISearchable<T> searchable);
    }
}
