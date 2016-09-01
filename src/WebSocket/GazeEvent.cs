using System.Drawing;

namespace GazeNetClient.WebSocket
{
    public class GazeEvent
    {
        public float x { get; set; }
        public float y { get; set; }
        public PointF Location { get { return new PointF(x, y); } }

        public GazeEvent() { }
        public GazeEvent(PointF aPoint)
        {
            x = aPoint.X;
            y = aPoint.Y;
        }

    public GazeEvent(float aX, float aY)
        {
            x = aX;
            y = aY;
        }
    }
    public class GazeEventReceived
    {
        public string from { get; set; }
        public GazeEvent payload { get; set; }

        public GazeEventReceived() { }
        public GazeEventReceived(string aFrom, float aX, float aY)
        {
            from = aFrom;
            payload = new GazeEvent(aX, aY);
        }
    }
    public class GazeEventSent
    {
        public string topic { get; set; }
        public GazeEvent payload { get; set; }

        public GazeEventSent() { }
        public GazeEventSent(string aTopic, float aX, float aY)
        {
            topic = aTopic;
            payload = new GazeEvent(aX, aY);
        }
        public GazeEventSent(string aTopic, GazeEvent aPayload)
        {
            topic = aTopic;
            payload = aPayload;
        }
    }
}
