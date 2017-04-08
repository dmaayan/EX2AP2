using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class Solution<T>
    {
        private List<State<T>> solution;

        public Solution(List<State<T>> solution)
        {
            this.solution = solution;
        }
    }
}
