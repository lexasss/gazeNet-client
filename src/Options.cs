using System;
using System.Drawing;
using System.Windows.Forms;

namespace GazeNetClient
{
    public partial class Options : Form
    {
        #region Internal members

        private Pointer.Collection iPointers;
        private Processor.TwoLevelLowPassFilter iFilter;
        private AutoStarter iAutoStarter;
        private WebSocket.Client iWebSocketClient;

        #endregion

        public ImageList.ImageCollection Icons { get { return imlIcons.Images; } }

        #region Public methods

        public Options()
        {
            InitializeComponent();
            Icon = Icon.FromHandle(new Bitmap(Icons["icon"]).GetHicon());
            foreach (Pointer.Style pointerStyle in Enum.GetValues(typeof(Pointer.Style)))
            {
                cmbPointerAppearance.Items.Add(pointerStyle);
            }
        }

        public void load(Pointer.Collection aPointers, Processor.TwoLevelLowPassFilter aFilter, AutoStarter aAutoStarter, WebSocket.Client aWebSocketClient)
        {
            iPointers = aPointers;
            iFilter = aFilter;
            iAutoStarter = aAutoStarter;
            iWebSocketClient = aWebSocketClient;

            txbServerHost.Text = WebSocket.Client.Host;
            nudServerPort.Value = WebSocket.Client.Port;
            lblStatus.Text = iWebSocketClient.Connected ? "online" : "offline";

            iPointers.pushSettings();

            cmbPointerAppearance.SelectedItem = iPointers.Settings.Appearance;
            trbPointerOpacity.Value = (int)Math.Round(iPointers.Settings.Opacity * 10);
            trbPointerSize.Value = iPointers.Settings.Size / 10;
            nudPointerFadingInterval.Value = iPointers.Settings.FadingInterval;
            nudPointerNoDataVisibilityDuration.Value = iPointers.Settings.NoDataVisibilityInterval;

            nudFilterTLow.Value = aFilter.TLow;
            nudFilterTHigh.Value = aFilter.THigh;
            nudFilterWindowSize.Value = aFilter.WindowSize;
            nudFilterFixationThreshold.Value = aFilter.FixationThreshold;

            chkAutoStarterEnabled.Checked = iAutoStarter.Enabled;
        }

        public void save(bool aAccept)
        {
            iPointers.popSettings(!aAccept);
            
            if (aAccept)
            {
                iFilter.TLow = (int)nudFilterTLow.Value;
                iFilter.THigh = (int)nudFilterTHigh.Value;
                iFilter.WindowSize = (int)nudFilterWindowSize.Value;
                iFilter.FixationThreshold = (int)nudFilterFixationThreshold.Value;

                iPointers.Settings.FadingInterval = (long)nudPointerFadingInterval.Value;
                iPointers.Settings.NoDataVisibilityInterval = (long)nudPointerNoDataVisibilityDuration.Value;

                iAutoStarter.Enabled = chkAutoStarterEnabled.Checked;

                if (WebSocket.Client.Host != txbServerHost.Text || WebSocket.Client.Port != (ushort)nudServerPort.Value)
                {
                    WebSocket.Client.Host = txbServerHost.Text;
                    WebSocket.Client.Port = (ushort)nudServerPort.Value;
                    iWebSocketClient.restart();
                }
            }
        }

        #endregion

        #region Event handlers

        private void cmbAppearance_SelectedIndexChanged(object sender, EventArgs e)
        {
            iPointers.Settings.Appearance = (Pointer.Style)cmbPointerAppearance.SelectedItem;
        }

        private void trbOpacity_ValueChanged(object sender, EventArgs e)
        {
            lblOpacity.Text = (10 * trbPointerOpacity.Value).ToString() + "%";
            iPointers.Settings.Opacity = (double)trbPointerOpacity.Value / 10;
        }

        private void trbSize_ValueChanged(object sender, EventArgs e)
        {
            lblSize.Text = (10 * trbPointerSize.Value).ToString() + " px";
            iPointers.Settings.Size = trbPointerSize.Value * 10;
        }

        #endregion
    }
}
