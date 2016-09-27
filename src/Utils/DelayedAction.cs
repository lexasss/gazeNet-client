using System;
using System.Windows.Forms;

namespace GazeNetClient.Utils
{
    public class DelayedAction
    {
        private Timer iTimer = new Timer();
        private Action iAction;

        public static DelayedAction Execute(int aDelay, Action aAction)
        {
            return new DelayedAction(aDelay, aAction);
        }

        public DelayedAction(int aDelay, Action aAction)
        {
            iAction = aAction;

            iTimer.Interval = aDelay;
            iTimer.Tick += Timer_Tick;
            iTimer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
 	        iTimer.Stop();
            iAction();
        }
    }
}
