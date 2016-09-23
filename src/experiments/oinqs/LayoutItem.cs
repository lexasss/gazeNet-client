using System;
using System.Drawing;

namespace GazeNetClient.Experiment.OinQs
{
    [Serializable]
    public class LayoutItem
    {
        public string text { get; private set; }
        public int x { get; private set; }
        public int y { get; private set; }

        public LayoutItem() { }
        public LayoutItem(string aText, Point aLocation)
        {
            text = aText;
            x = aLocation.X;
            y = aLocation.Y;
        }
    }
}
