using System;
using System.Web.Script.Serialization;

namespace GazeNetClient.Plugins.OinQs
{
    public static class LayoutItemText
    {
        public static string Target = "O";
        public static string Distractor = "Q";
    }

    [Serializable]
    public class LayoutItem
    {
        public string text { get; set; }
        public double x { get; set; }
        public double y { get; set; }

        [ScriptIgnore]
        public bool IsTarget { get { return text.StartsWith(LayoutItemText.Target); } }

        public LayoutItem() { }
        public LayoutItem(string aText, double aX, double aY)
        {
            text = aText;
            x = aX;
            y = aY;
        }
    }
}
