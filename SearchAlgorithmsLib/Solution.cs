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

        public string ToJson()
        {
            return null;
        }
    }
}
