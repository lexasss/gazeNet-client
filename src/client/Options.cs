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

            Icon = Icon.FromHandle(new Bitmap(Icons["initial"]).GetHicon());
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


            if (iWebSocketClient != null)
            {
                txbServerHost.Text = iWebSocketClient.Host;
                nudServerPort.Value = iWebSocketClient.Port;
                lblStatus.Text = iWebSocketClient.Connected ? "online" : "offline";
                txbUserName.Text = iWebSocketClient.Config.UserName;
                txbTopic.Text = iWebSocketClient.Config.Topics;
                cmbRole.SelectedIndex = (int)iWebSocketClient.Config.Role - 1;
            }

            iPointers.pushSettings();

            cmbPointerAppearance.SelectedItem = iPointers.Settings.Appearance;
            trbPointerOpacity.Value = (int)Math.Round(iPointers.Settings.Opacity * 10);
            try
            {
                trbPointerSize.Value = iPointers.Settings.Size / 10;
            }
            catch (Exception)
            {
                trbPointerSize.Value = trbPointerSize.Maximum;
            }
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

                if (iWebSocketClient != null)
                {
                    iWebSocketClient.Host = txbServerHost.Text;
                    iWebSocketClient.Port = (ushort)nudServerPort.Value;
                    iWebSocketClient.Config.UserName = txbUserName.Text;
                    iWebSocketClient.Config.Topics = txbTopic.Text;
                    iWebSocketClient.Config.Role = (WebSocket.ClientRole)(cmbRole.SelectedIndex + 1);

                    if (iWebSocketClient.NeedsRestart)
                    {
                        iWebSocketClient.restart();
                    }
                }
            }
        }

        #endregion

        #region Event handlers

        private void cmbAppearance_SelectedIndexChanged(object sender, EventArgs e)
        {
            iPointers.Settings.Appearance = (Pointer.Style)cmbPointerAppearance.SelectedItem;
            pcbPointer.Image = Pointer.Collection.StyleImages[iPointers.Settings.Appearance];
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
