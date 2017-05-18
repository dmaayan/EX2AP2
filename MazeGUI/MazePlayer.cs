using MazeLib;

namespace MazeGUI
{
    public class MazePlayer
    {
        private Position mazeStartPoint;

        public MazePlayer(Position mazeStartPoint)
        {
            this.mazeStartPoint = mazeStartPoint;
        }

        public Position MazePoint
        {
            get { return mazeStartPoint; }
            set { mazeStartPoint = value; }
        }
    }
}