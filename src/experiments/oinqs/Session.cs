using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace GazeNetClient.Experiment.OinQs
{
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
                    Append("Sender").Append("\t").
                    Append("Result").Append("\t").
                    Append("Time").
                    ToString();
            }
        }

        public Log(Plugins.OinQs.LayoutItem[] aItems, string aSender, TrialResult aResult, int aTime)
        {
            iItems = aItems;

            ObjectCounts = aItems.Length;
            TargetPresence = HasTarget();
            Sender = aSender;
            Result = aResult;
            Time = aTime;
        }

        public override string ToString()
        {
            return new StringBuilder().
                Append(iItems.Length).Append("\t").
                Append(TargetPresence ? 1 : 0).Append("\t").
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

        private List<bool> iTargetPresences;
        private Plugins.OinQs.LayoutItem[] iCurrentItems;
        private int iTrialIndex = -1;

        private List<Log> iLogs = new List<Log>();
        private DateTime iTrialStart;

        public Session(Config aConfig)
        {
            iConfig = aConfig;
            AssignTargetPresences();
        }

        public Plugins.OinQs.LayoutItem[] createTrial()
        {
            iTrialIndex++;

            Size size = iConfig.UseWholeScreen ? System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size : iConfig.FieldSize;
            iCurrentItems = RandomLayout.create(size, iConfig.ObjectCount, iTargetPresences[iTrialIndex]);
            return iCurrentItems;
        }

        public void startTrial()
        {
            iTrialStart = DateTime.Now;
        }

        public bool finishTrial(string aSender, TrialResult aResult)
        {
            Log log = new Log(iCurrentItems, aSender, aResult, (int)(DateTime.Now - iTrialStart).TotalMilliseconds);
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

        private void AssignTargetPresences()
        {
            iTargetPresences = new List<bool>(iConfig.TrialCount);
            int trialsWithTargets = iConfig.TrialsWithTarget;

            for (int i = 0; i < iConfig.TrialCount; i++)
            {
                iTargetPresences.Add(i < trialsWithTargets);
            }

            Random rand = new Random();
            for (int i = 0; i < 3 * iConfig.TrialCount; i++)
            {
                int a = rand.Next(iConfig.TrialCount);
                int b = rand.Next(iConfig.TrialCount);
                bool val = iTargetPresences[a];
                iTargetPresences[a] = iTargetPresences[b];
                iTargetPresences[b] = val;
            }
        }
    }
}
