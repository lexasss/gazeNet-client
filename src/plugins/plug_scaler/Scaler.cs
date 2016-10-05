using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GazeNetClient.Plugin;

namespace GazeNetClient.Plugins.Scaler
{
    public partial class Scaler : IPlugin
    {
        private Config iConfig;
        private Options iOptions;
        private Rectangle iDisplaySize;

        public string Name { get; } = "scaler";
        public string DisplayName { get; } = "Gaze area scaler";
        public bool IsExclusive { get; } = false;
        public Dictionary<string, EventHandler> MenuItems { get; } = null;
        public OptionsWidget Options { get { return iOptions; } }

        public bool Enabled
        {
            get { return iConfig.Enabled; }
            set { iConfig.Enabled = value; }
        }

        public event EventHandler<string> Log = delegate { };
        public event EventHandler<RequestArgs> Req = delegate { };

        public Scaler()
        {
            iConfig = Utils.Storage<Config>.load();

            iOptions = new Options();
        }

        ~Scaler()
        {
            Utils.Storage<Config>.save(iConfig);
        }

        public void command(string aCommand, string aValue) { }

        public void start()
        {
            iDisplaySize = Screen.PrimaryScreen.Bounds;
        }

        public void finilize() { }

        public void displayOptions()
        {
            iOptions.cmbAppliesTo.SelectedIndex = (int)iConfig.ScalingTarget;
            iOptions.nudLeft.Value = iConfig.Left;
            iOptions.nudRight.Value = iConfig.Right;
            iOptions.nudTop.Value = iConfig.Top;
            iOptions.nudBottom.Value = iConfig.Bottom;
        }

        public void acceptOptions()
        {
            iConfig.ScalingTarget = (ScalingTarget)iOptions.cmbAppliesTo.SelectedIndex;
            iConfig.Left = (int)iOptions.nudLeft.Value;
            iConfig.Right = (int)iOptions.nudRight.Value;
            iConfig.Top = (int)iOptions.nudTop.Value;
            iConfig.Bottom = (int)iOptions.nudBottom.Value;
        }

        public Processor.GazePoint feedOwnPoint(Processor.GazePoint aSample)
        {
            float x = iDisplaySize.Left + iConfig.Left + iConfig.Width * (aSample.X / iDisplaySize.Width);
            float y = iDisplaySize.Top + iConfig.Top + iConfig.Height * (aSample.Y / iDisplaySize.Height);
            return new Processor.GazePoint(aSample.Timestamp, new PointF(x, y));
        }

        public bool feedReceivedPoint(string aFrom, ref PointF aPoint)
        {
            return true;
        }
    }
}
