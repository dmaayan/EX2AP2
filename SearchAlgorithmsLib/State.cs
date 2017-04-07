using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class State<T>
    {
        private T state;            // the state represented by a string
        private double cost;        // cost to reach this state (set by a setter)
        private State<T> cameFrom;  // the state we came from to this state (setter)

        public State(T state) // CTOR
        {
            this.state = state;
        }

        public double Cost
        {
            get { return cost; }
            set { cost = value; }
        }

        public State<T> CameFrom
        {
            get { return cameFrom; }
            set { cameFrom = value; }
        }

        public T getState()
        {
            return state;
        }

        // overload Object's Equals method
        public bool Equals(State<T> s) 
        {
            return state.Equals(s.getState());
        }

    
        public static class StatePool
        {
            // Dictionary<TKey, TValue>
            private static Dictionary<T, State<T>> pool = new Dictionary<T, State<T>>();

            public static State<T> getState(T state)
            {
                // if the state doesn't exist, add it to the pool  
                if (!pool.ContainsKey(state))
                {
                    pool.Add(state, new State<T>(state));
                }
                return pool[state];
            }

        }

    }
}
