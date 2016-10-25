using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GazeNetClient.Plugin;

namespace GazeNetClient.Plugins.Scaler
{
    public partial class Scaler : IPlugin
    {
        private Config iConfig;
        private Options iOptions;
        private Rectangle iDisplaySize;
        private IntPtr iWindowToScaleReceivedDataTo = IntPtr.Zero;

        public string Name { get; } = "scaler";
        public string DisplayName { get; } = "Gaze area scaler";
        public bool IsExclusive { get; } = false;
        public Dictionary<string, Utils.UIAction> MenuItems { get; } = null;
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

            iOptions.Window = iConfig.Window;
            iWindowToScaleReceivedDataTo = iOptions.WindowPtr;
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
            iOptions.chkOwn.Checked = iConfig.OwnEnabled;
            iOptions.OwnHorizontalRange.set(iConfig.Own.Left, iConfig.Own.Right);
            iOptions.OwnVerticalRange.set(iConfig.Own.Top, iConfig.Own.Bottom);

            iOptions.chkReceived.Checked = iConfig.ReceivedEnabled;
            iOptions.ReceivedHorizontalRange.set(iConfig.Received.Left, iConfig.Received.Right);
            iOptions.ReceivedVerticalRange.set(iConfig.Received.Top, iConfig.Received.Bottom);

            iOptions.Window = iConfig.Window;

            iOptions.updateVisibility();
        }

        public void acceptOptions()
        {
            iConfig.OwnEnabled = iOptions.chkOwn.Checked;
            iConfig.Own = new Rectangle(iOptions.OwnHorizontalRange.From, iOptions.OwnVerticalRange.From, 
                iOptions.OwnHorizontalRange.Value, iOptions.OwnVerticalRange.Value);
            iConfig.ReceivedEnabled = iOptions.chkReceived.Checked;
            iConfig.Window = iOptions.Window;
            if (string.IsNullOrEmpty(iConfig.Window))
            {
                iConfig.Received = new Rectangle(iOptions.ReceivedHorizontalRange.From, iOptions.ReceivedVerticalRange.From,
                    iOptions.ReceivedHorizontalRange.Value, iOptions.ReceivedVerticalRange.Value);
            }

            iWindowToScaleReceivedDataTo = iOptions.WindowPtr;
        }

        public Processor.GazePoint feedOwnPoint(Processor.GazePoint aSample)
        {
            if (iConfig.OwnEnabled)
            {
                Rectangle rect = iConfig.Own;
                /*
                if (iWindowToScaleReceivedDataTo != IntPtr.Zero)
                {
                    if (!Utils.WinAPI.IsWindow(iWindowToScaleReceivedDataTo))
                        return aSample;

                    Utils.WinAPI.RECT winRect;
                    if (!Utils.WinAPI.GetWindowRect(iWindowToScaleReceivedDataTo, out winRect))
                        return aSample;

                    rect = new Rectangle(winRect.Left, winRect.Top, winRect.Right - winRect.Left, winRect.Bottom - winRect.Top);
                }
                */
                float x = iDisplaySize.Left + rect.Left + rect.Width * (aSample.X / iDisplaySize.Width);
                float y = iDisplaySize.Top + rect.Top + rect.Height * (aSample.Y / iDisplaySize.Height);

                return new Processor.GazePoint(aSample.Timestamp, new PointF((int)x, (int)y));
            }

            return aSample;
        }

        public bool feedReceivedPoint(string aFrom, ref PointF aPoint)
        {
            if (iConfig.ReceivedEnabled)
            {
                Rectangle rect = iConfig.Received;

                if (iWindowToScaleReceivedDataTo != IntPtr.Zero)
                {
                    if (!Utils.WinAPI.IsWindow(iWindowToScaleReceivedDataTo))
                        return false;

                    Utils.WinAPI.RECT winRect;
                    if (!Utils.WinAPI.GetWindowRect(iWindowToScaleReceivedDataTo, out winRect))
                        return false;

                    rect = new Rectangle(winRect.Left, winRect.Top, winRect.Right - winRect.Left, winRect.Bottom - winRect.Top);
                }


                float x = iDisplaySize.Left + rect.Left + rect.Width * (aPoint.X / iDisplaySize.Width);
                float y = iDisplaySize.Top + rect.Top + rect.Height * (aPoint.Y / iDisplaySize.Height);
                aPoint = new PointF((int)x, (int)y);
            }

            return true;
        }
    }
}
