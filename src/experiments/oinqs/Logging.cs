using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazeNetClient.Experiment.OinQs
{
    public enum TrialResult
    {
        Timeout,
        NotFound,
        Found
    }

    public class Log
    {
        public string Sender { get; private set; }
        public TrialResult Result { get; private set; }
        public int Time { get; private set; }

        public Log(string aSender, TrialResult aResult, int aTime)
        {
            Sender = aSender;
            Result = aResult;
            Time = aTime;
        }

        public override string ToString()
        {
            return string.Format("{0}\t{1}\t{2}", Sender, Result, Time);
        }
    }

    public class Logger
    {
        private List<Log> iLogs = new List<Log>();
        private DateTime iTrialStart;
        private int iTrialCount;

        public void initialize(int aTrialCount)
        {
            iTrialCount = aTrialCount;
            iLogs.Clear();
        }

        public void startTrial()
        {
            iTrialStart = DateTime.Now;
        }

        public bool finishTrial(string aSender, TrialResult aResult)
        {
            Log log = new Log(aSender, aResult, (int)(DateTime.Now - iTrialStart).TotalMilliseconds);
            iLogs.Add(log);

            return iLogs.Count == iTrialCount;
        }

        public void save(string aFileName)
        {
            foreach (Log log in iLogs)
                Console.WriteLine(log);
        }
    }
}
