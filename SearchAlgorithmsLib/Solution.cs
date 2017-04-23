using System.Collections.Generic;

namespace SearchAlgorithmsLib
{
    /// <summary>
    /// Solution has a list of states and int represent nodesEvaluated
    /// </summary>
    /// <typeparam name="T">is the type of the Solution</typeparam>
    public class Solution<T>
    {
        private List<State<T>> solution;
        private int nodesEvaluated;

        /// <summary>
        /// constuctor
        /// </summary>
        public Solution(List<State<T>> solution, int amountOfNodes)
        {
            this.solution = solution;
            nodesEvaluated = amountOfNodes;
        }

        /// <summary>
        /// a property of solution 
        /// </summary>
        public List<State<T>> GetSolution
        {
            get { return solution; }
        }

        /// <summary>
        /// a property of nodesEvaluated 
        /// </summary>
        public int NodesEvaluated
        {
            get { return nodesEvaluated; }
        }

        /// <summary>
        /// ToJson 
        /// </summary>
        /// <returns> </returns>
        public string ToJson()
        {
            return null;
        }
    }
}
