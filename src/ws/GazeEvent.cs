using System.Drawing;
using System.Web.Script.Serialization;

namespace GazeNetClient.WebSocket
{
    public class GazeEvent
    {
        public float x { get; set; }
        public float y { get; set; }

        [ScriptIgnore]
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

    public class GazeEventSent : MessageSent<GazeEvent>
    {
        public new MessageType type { get; set; } = MessageType.GazeEvent;
        public GazeEventSent() : base() { }
        public GazeEventSent(string aTopic, GazeEvent aPayload) : base(aTopic, aPayload) { }
    }

    public class GazeEventReceived : MessageReceived<GazeEvent>
    {
        public GazeEventReceived() { }
        public GazeEventReceived(string aFrom, GazeEvent aPayload) : base(aFrom, aPayload) { }
    }
}
