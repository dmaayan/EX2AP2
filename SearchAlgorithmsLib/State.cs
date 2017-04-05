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

        // ***  לבדוק אם זה מחזיר רפרנס או העתק
        public State<T> CameFrom
        {
            get { return cameFrom; }
            set { cameFrom = value; }
        }

        public T getState()
        {
            return state;
        }

        public bool Equals(State<T> s) // we overload Object's Equals method
        {
            return state.Equals(s.getState());
        }
    }
}
