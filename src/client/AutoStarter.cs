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

            iDelayedAction = new Utils.DelayedAction(1000, () => Next(true));
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
                iDelayedAction = new Utils.DelayedAction(500, iGazeNetClient.showETUDOptions);
            }
            else
            {
                if (iGazeNetClient.State == GazeNetClient.TrackingState.Connected && aCanRunCalibration)
                {
                    iDelayedAction = new Utils.DelayedAction(500, () =>
                    {
                        iGazeNetClient.calibrate();
                        iDelayedAction = new Utils.DelayedAction(1000, () => Next(false));
                    });
                }
                else if (iGazeNetClient.State == GazeNetClient.TrackingState.Calibrated)
                {
                    iDelayedAction = new Utils.DelayedAction(500, iGazeNetClient.toggleConnection);
                }
            }
        }
    }
}
