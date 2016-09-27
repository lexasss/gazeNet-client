using System;
using System.Drawing;

namespace GazeNetClient.Experiment.OinQs
{
    [Serializable]
    public class Config
    {
        public int ObjectCount { get; set; } = 10;
        public int TrialCount { get; set; } = 10;
        public int OsPerQs { get; set; } = 50;      // %
        public bool UseWholeScreen { get; set; } = true;
        public Size FieldSize { get; set; } = new Size(1366, 768);

        public int TrialsWithTarget { get { return (int)Math.Round((double)OsPerQs * TrialCount / 100); } }
    }
}
