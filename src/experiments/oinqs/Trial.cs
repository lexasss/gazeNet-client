using System.Text;
using System.Drawing;

namespace GazeNetClient.Experiment.OinQs
{
    public class TrialCondition
    {
        public bool TargetPresence { get; private set; }
        public int Orientation { get; private set; }
        public Size Grid { get; private set; }
        public int ObjectCount { get { return Grid.Width * Grid.Height; } }

        public TrialCondition(bool aTargetPresence, int aOrientation, Size aGrid)
        {
            TargetPresence = aTargetPresence;
            Orientation = aOrientation;
            Grid = aGrid;
        }

        public override string ToString()
        {
            return new StringBuilder().
                Append("TargetPresence: ").Append(TargetPresence).Append(", ").
                Append("Orientation: ").Append(Orientation).Append(", ").
                Append("ObjectCount: ").Append(ObjectCount).
                ToString();
        }
    }

    public enum TrialResult
    {
        Timeout = -1,
        NotFound = 0,
        Found = 1
    }

    public class Trial
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

        public Trial(Plugins.OinQs.LayoutItem[] aItems, int aOrientation, string aSender, TrialResult aResult, int aTime)
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
}
