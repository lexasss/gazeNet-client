using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace GazeNetClient.Experiment.OinQs
{
    public class TrialConditions
    {
        public bool TargetPresence { get; private set; }
        public int Orientation { get; private set; }

        public TrialConditions(bool aTargetPresence, int aOrientation)
        {
            TargetPresence = aTargetPresence;
            Orientation = aOrientation;
        }
    }

    public enum TrialResult
    {
        Timeout = -1,
        NotFound = 0,
        Found = 1
    }

    public class Log
    {
        private Plugins.OinQs.LayoutItem[] iItems;

        public int ObjectCounts { get; private set; }
        public bool TargetPresence { get; private set; }
        public int Orientation { get; private set; }
        public string Sender { get; private set; }
        public TrialResult Result { get; private set; }
        public int Time { get; private set; }

        public static string Header
        {
            get
            {
                /*
                StringBuilder sb = new StringBuilder();
                Type type = typeof(Log);
                System.Reflection.PropertyInfo[] pis = type.GetProperties();
                foreach (System.Reflection.PropertyInfo propInfo in pis)
                    if (propInfo.PropertyType.IsPublic && propInfo.PropertyType.)
                        sb.Append(propInfo.Name).Append("\t");
                return sb.ToString();*/
                return new StringBuilder().
                    Append("Objects").Append("\t").
                    Append("Target").Append("\t").
                    Append("Orientation").Append("\t").
                    Append("Sender").Append("\t").
                    Append("Result").Append("\t").
                    Append("Time").
                    ToString();
            }
        }

        public Log(Plugins.OinQs.LayoutItem[] aItems, int aOrientation, string aSender, TrialResult aResult, int aTime)
        {
            iItems = aItems;

            ObjectCounts = aItems.Length;
            TargetPresence = HasTarget();
            Orientation = aOrientation;
            Sender = aSender;
            Result = aResult;
            Time = aTime;
        }

        public override string ToString()
        {
            return new StringBuilder().
                Append(iItems.Length).Append("\t").
                Append(TargetPresence ? 1 : 0).Append("\t").
                Append(Orientation).Append("\t").
                Append(Sender).Append("\t").
                Append((int)Result).Append("\t").
                Append(Time).
                ToString();
        }

        private bool HasTarget()
        {
            bool hasTarget = false;
            foreach (Plugins.OinQs.LayoutItem item in iItems)
            {
                if (item.IsTarget)
                {
                    hasTarget = true;
                    break;
                }
            }
            return hasTarget;
        }
    }

    public class Session
    {
        private Config iConfig;

        private List<TrialConditions> iTrialConditions;
        private Plugins.OinQs.LayoutItem[] iCurrentItems;
        private int iTrialIndex = -1;

        private List<Log> iLogs = new List<Log>();
        private DateTime iTrialStart;

        public Session(Config aConfig)
        {
            iConfig = aConfig;
            AssignStimuliTypes();
        }

        public Plugins.OinQs.LayoutItem[] createTrial()
        {
            iTrialIndex++;

            Size size = iConfig.UseWholeScreen ? System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size : iConfig.FieldSize;
            iCurrentItems = RandomLayout.create(size, iConfig.ObjectCount, iTrialConditions[iTrialIndex]);
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
            Log log = new Log(iCurrentItems, orientation, aSender, aResult, time);
            iLogs.Add(log);

            return iLogs.Count == iConfig.TrialCount;
        }

        public void save(string aFileName)
        {
            using (StreamWriter writer = new StreamWriter(aFileName))
            {
                writer.WriteLine(Log.Header);
                foreach (Log log in iLogs)
                    writer.WriteLine(log.ToString());
            }
        }

        private void AssignStimuliTypes()
        {
            iTrialConditions = new List<TrialConditions>(iConfig.TrialCount);
            int trialsWithTargets = iConfig.TrialsWithTarget;

            for (int i = 0; i < iConfig.TrialCount; i++)
            {
                bool isTargetPresented = i < trialsWithTargets;
                int orientation = (i % 4) * 90;
                iTrialConditions.Add(new TrialConditions(isTargetPresented, orientation));
            }

            Random rand = new Random();
            for (int i = 0; i < 3 * iConfig.TrialCount; i++)
            {
                int a = rand.Next(iConfig.TrialCount);
                int b = rand.Next(iConfig.TrialCount);
                TrialConditions val = iTrialConditions[a];
                iTrialConditions[a] = iTrialConditions[b];
                iTrialConditions[b] = val;
            }
        }
    }
}
