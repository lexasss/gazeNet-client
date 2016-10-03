using System.Collections.Generic;
using System.Text;
using System.IO;
using GazeNetClient.Processor;

namespace GazeNetClient.Plugins.OinQs
{
    internal class Trial
    {
        public static string Header
        {
            get
            {
                return new StringBuilder().
                    Append("Timestamp").Append("\t").
                    Append("X").Append("\t").
                    Append("Y").Append("\t").
                    ToString();
            }
        }

        public TrialConfig Config { get; private set; }
        public List<GazePoint> Samples { get; private set; } = new List<GazePoint>();

        public Trial(TrialConfig aConfig)
        {
            Config = aConfig;
        }

        public void write(StreamWriter aWriter)
        {
            aWriter.WriteLine();
            aWriter.WriteLine(Config);
            foreach (GazePoint sample in Samples)
                aWriter.WriteLine(sample);
        }
    }

    internal class GazeLogger
    {
        private List<Trial> iTrials = new List<Trial>();
        private Trial iCurrentTrial = null;
        private int iTrialIndex = -1;

        public Plugin.ContainerConfig ContainerConfig { get; set; } = new Plugin.ContainerConfig();
        public bool HasData { get { return iTrials.Count > 0; } }

        public void startTrial(TrialConfig aConfig)
        {
            iCurrentTrial = new Trial(aConfig);
            iTrials.Add(iCurrentTrial);
            iTrialIndex++;
        }

        public void endTrial()
        {
            iCurrentTrial = null;
        }

        public void feed(GazePoint aSample)
        {
            iCurrentTrial?.Samples.Add(aSample);
        }

        public void save(string aFileName)
        {
            using (StreamWriter writer = new StreamWriter(aFileName))
            {
                writer.WriteLine(Trial.Header);
                foreach (Trial trial in iTrials)
                    trial.write(writer);
            }

            iTrials.Clear();
            iTrialIndex = -1;
            iCurrentTrial = null;
        }
    }
}
