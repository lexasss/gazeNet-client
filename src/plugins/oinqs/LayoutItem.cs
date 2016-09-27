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
        public int x { get; set; }
        public int y { get; set; }

        [ScriptIgnore]
        public bool IsTarget { get { return text == LayoutItemText.Target; } }

        public LayoutItem() { }
        public LayoutItem(string aText, int aX, int aY)
        {
            text = aText;
            x = aX;
            y = aY;
        }
    }
}
