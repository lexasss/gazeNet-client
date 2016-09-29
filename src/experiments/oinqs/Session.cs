using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace GazeNetClient.Experiment.OinQs
{
    public class Session
    {
        private Config iConfig;

        private List<TrialCondition> iTrialConditions;
        private Plugins.OinQs.LayoutItem[] iCurrentItems;
        private int iTrialIndex = -1;

        private List<Trial> iLogs = new List<Trial>();
        private DateTime iTrialStart;

        public bool Active { get; private set; } = false;

        public Session(Config aConfig)
        {
            iConfig = aConfig;
            iTrialConditions = CreateTrialConditions();
        }

        public Plugins.OinQs.LayoutItem[] createTrial()
        {
            iTrialIndex++;

            TrialCondition condition = iTrialConditions[iTrialIndex];
            iCurrentItems = LayoutGenerator.create(iConfig.ScreenResolution, condition);

            Active = true;
            return iCurrentItems;
        }

        public void startTrial()
        {
            iTrialStart = DateTime.Now;
        }

        public bool finishTrial(string aSender, TrialResult aResult)
        {
            int time = (int)(DateTime.Now - iTrialStart).TotalMilliseconds;
            int orientation = iTrialConditions[iTrialIndex].Orientation;
            Trial log = new Trial(iCurrentItems, orientation, aSender, aResult, time);
            iLogs.Add(log);

            Active = false;
            return iLogs.Count == iConfig.TrialCount;
        }

        public void save(string aFileName)
        {
            using (StreamWriter writer = new StreamWriter(aFileName))
            {
                writer.WriteLine(Trial.Header);
                foreach (Trial log in iLogs)
                    writer.WriteLine(log.ToString());
            }

            iTrialIndex = -1;
        }

        public bool isResultCorrect(TrialResult aResult)
        {
            return iTrialConditions[iTrialIndex].TargetPresence ?
                aResult == TrialResult.Found :
                aResult == TrialResult.NotFound;
        }

        private List<TrialCondition> CreateTrialConditions()
        {
            List<TrialCondition> orderedConditions = new List<TrialCondition>();

            for (int ai = 0; ai < Config.TARGET_PRESENCE.Length; ai++)
            {
                bool targetPresence = Config.TARGET_PRESENCE[ai];
                for (int oi = 0; oi < Config.ORIENTATIONS.Length; oi++)
                {
                    int orientation = Config.ORIENTATIONS[oi];
                    for (int gi = 0; gi < Config.GRIDS.Length; gi++)
                    {
                        Size grid = Config.GRIDS[gi];
                        for (int ri = 0; ri < iConfig.Repetitions; ri++)
                        {
                            orderedConditions.Add(new TrialCondition(targetPresence, orientation, grid));
                        }
                    }
                }
            }

            List<TrialCondition> randomizedConditions = new List<TrialCondition>();

            Random rand = new Random();
            while (orderedConditions.Count > 0)
            {
                TrialCondition condition = orderedConditions[rand.Next(orderedConditions.Count)];
                randomizedConditions.Add(condition);
                orderedConditions.Remove(condition);
            }

            return randomizedConditions;
        }
    }
}
