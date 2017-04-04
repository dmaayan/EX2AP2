using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Priority_Queue;

namespace SearchAlgorithmsLib
{
    public abstract class PrioritySearcher<T> : ISearcher<T>
    {
        private SimplePriorityQueue<State<T>> openList;
        private int evaluatedNodes;
        public PrioritySearcher()
        {
            openList = new SimplePriorityQueue<State<T>>();
            evaluatedNodes = 0;
        }
        protected State<T> popOpenList()
        {
            evaluatedNodes++;
            return openList.poll();
        }
        // a property of openList 
        public int OpenListSize
        { // it is a read-only property :) 
            get { return openList.Count; }
        }

        // ISearcher’s methods:
        public int getNumberOfNodesEvaluated()
        {
            return evaluatedNodes;
        }
        public abstract Solution search(ISearchable<T> searchable);
    }
}
