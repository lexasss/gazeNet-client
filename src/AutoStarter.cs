using System;

namespace GazeNetClient
{
    [Serializable]
    public class AutoStarter
    {
        private Utils.DelayedAction iDelayedAction;
        private GazeNetClient iGazeNetClient;
        private bool iFinished = true;

        public bool Enabled { get; set; }

        public AutoStarter()
        {
            Enabled = false;
        }

        public void run(GazeNetClient aGazeNetClient)
        {
            if (!iFinished)
                return;

            iGazeNetClient = aGazeNetClient;
            iFinished = false;

            iDelayedAction = new Utils.DelayedAction(() => Next(true), 1000);
        }

        private void Next(bool aCanRunCalibration)
        {
            if (iGazeNetClient.State == GazeNetClient.TrackingState.NotAvailable ||
                iGazeNetClient.State == GazeNetClient.TrackingState.Tracking)
            {
                return;
            }
            else if (iGazeNetClient.State == GazeNetClient.TrackingState.Disconnected)
            {
                iDelayedAction = new Utils.DelayedAction(iGazeNetClient.showETUDOptions, 500);
            }
            else
            {
                if (iGazeNetClient.State == GazeNetClient.TrackingState.Connected && aCanRunCalibration)
                {
                    iDelayedAction = new Utils.DelayedAction(() =>
                    {
                        iGazeNetClient.calibrate();
                        iDelayedAction = new Utils.DelayedAction(() => Next(false), 1000);
                    }, 500);
                }
                else if (iGazeNetClient.State == GazeNetClient.TrackingState.Calibrated)
                {
                    iDelayedAction = new Utils.DelayedAction(iGazeNetClient.toggleTracking, 500);
                }
            }
        }
    }
}
