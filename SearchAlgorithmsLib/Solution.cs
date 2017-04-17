using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SearchAlgorithmsLib
{
    public class Solution<T>
    {
        private List<State<T>> solution;
        private int nodesEvaluated;

        public Solution(List<State<T>> solution, int amountOfNodes)
        {
            this.solution = solution;
            nodesEvaluated = amountOfNodes;
        }

        public List<State<T>> getSolution
        {
            get { return this.solution; }
        }

        public int NodesEvaluated
        {
            get { return nodesEvaluated; }
        }

        public string ToJson()
        {
            return null;
        }
    }
}
