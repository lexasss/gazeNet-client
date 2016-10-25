using System.Drawing;

namespace GazeNetClient.Processor
{
    public class GazePoint
    {
        public long Timestamp { get; private set; }
        public float X { get; private set; }
        public float Y { get; private set; }
        public PointF LocationF { get { return new PointF(X, Y); } }
        public Point Location { get { return new Point((int)X, (int)Y); } }

        public GazePoint(long aTimestamp, PointF aPoint) : this(aTimestamp, aPoint.X, aPoint.Y) { }

        public GazePoint(long aTimestamp, float aX, float aY)
        {
            Timestamp = aTimestamp;
            X = aX;
            Y = aY;
        }

        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}", Timestamp, X, Y);
        }
    }
}
