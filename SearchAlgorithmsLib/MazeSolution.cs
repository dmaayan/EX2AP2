using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmsLib
{
    public class MazeSolution
    {
        private List<Direction> directions;
        private int nodesEvaluated;
        private string mazeName;

        public MazeSolution(Solution<Position> s, string name)
        {
            directions = new List<Direction>();
            SolutionToDirections(s);
            nodesEvaluated = s.NodesEvaluated;
            mazeName = name;
        }

        private void SolutionToDirections(Solution<Position> solution)
        {
            List<State<Position>> positions = solution.getSolution;
            State<Position> state = positions.First();
            Position pos = state.getState();
            positions.Remove(state);
            while (positions.Count > 0)
            {
                state = state.CameFrom;
                positions.Remove(state);
                Position otherPos = state.getState();
                if (pos.Col > otherPos.Col)
                {
                    directions.Add(Direction.Left);
                }
                else if (pos.Col < otherPos.Col)
                {
                    directions.Add(Direction.Right);
                }
                else if (pos.Row > otherPos.Row)
                {
                    directions.Add(Direction.Down);
                }
                else if (pos.Row < otherPos.Row)
                {
                    directions.Add(Direction.Up);
                }
                else
                {
                    directions.Add(Direction.Unknown);
                }
                pos = otherPos;
            }
        }

        public string ToJson()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("{");
            sb.AppendLine("  \"Name\": \"" + mazeName + "\",");
            string direct = "";
            foreach(Direction d in directions)
            {
                direct += (int)d;
            }
            sb.AppendLine("  \"Solution\": \"" + direct + "\",");
            sb.AppendLine("  \"NodesEvaluated\" :" + nodesEvaluated);
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
