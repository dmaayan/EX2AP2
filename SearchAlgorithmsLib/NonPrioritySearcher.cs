using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public abstract class NonPrioritySearcher<T> : Searcher<T>
    {
        private Dictionary<State<T>, State<T>> parents;

        public NonPrioritySearcher()
        {
            parents = new Dictionary<State<T>, State<T>>();
        }

        public Dictionary<State<T>, State<T>> Parents
        {
            get { return parents; }
        }

        public override abstract Solution<T> Search(ISearchable<T> searchable);
    }
}
