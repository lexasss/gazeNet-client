using System;

namespace GazeNetClient.Experiment.OinQs
{
    [Serializable]
    public class Config
    {
        public int ObjectCount { get; set; } = 10;
        public int TrialCount { get; set; } = 1;
    }
}
