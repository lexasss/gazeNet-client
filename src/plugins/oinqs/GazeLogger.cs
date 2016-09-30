using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using GazeNetClient.Processor;

namespace GazeNetClient.Plugins.OinQs
{
    internal class Trial
    {
        public class Config
        {
            public string Name { get; private set; }

            public Config(string aName)
            {
                Name = aName;
            }

            public override string ToString()
            {
                return new StringBuilder().
                    Append(Name).
                    ToString();
            }
        }

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

        private Config iConfig;

        public List<GazePoint> Samples { get; private set; } = new List<GazePoint>();

        public Trial(Config aConfig)
        {
            iConfig = aConfig;
        }

        public void write(StreamWriter aWriter)
        {
            aWriter.WriteLine(iConfig);
            foreach (GazePoint sample in Samples)
                aWriter.WriteLine(sample);
        }
    }

    internal class GazeLogger
    {
        private List<Trial> iTrials = new List<Trial>();
        private Trial iCurrentTrial = null;
        private int iTrialIndex = -1;

        public bool HasData { get { return iTrials.Count > 0; } }

        public void startTrial(Trial.Config aConfig)
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
