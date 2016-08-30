using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazeNetClient.WebSocket
{
    public class GazeEvent
    {
        public int x { get; set; }
        public int y { get; set; }
        public GazeEvent() { }
        public GazeEvent(int aX, int aY)
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
        public GazeEventReceived(string aFrom, int aX, int aY)
        {
            from = aFrom;
            payload = new GazeEvent(aX, aY);
        }
    }
    public class GazeEventSent
    {
        public string source { get; set; }
        public GazeEvent payload { get; set; }

        public GazeEventSent(string aSource, int aX, int aY)
        {
            source = aSource;
            payload = new GazeEvent(aX, aY);
        }
        public GazeEventSent(string aSource, GazeEvent aPayload)
        {
            source = aSource;
            payload = aPayload;
        }
    }
}
