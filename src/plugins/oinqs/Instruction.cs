using System;

namespace GazeNetClient.Plugins.OinQs
{
    [Serializable]
    public class Instruction
    {
        public string text { get; set; }
        public int time { get; set; }

        public Instruction() { }
        public Instruction(string aText, int aTime)
        {
            text = aText;
            time = aTime;
        }
    }
}
