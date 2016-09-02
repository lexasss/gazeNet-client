using System;
using System.Windows.Forms;

namespace GazeNetClient
{
    public class Menu
    {
        #region Declarations

        public struct State
        {
            public bool IsShowingOptions;
            public bool IsEyeTrackingRequired;
            public bool IsServerConnected;
            public bool IsPointerVisible;
            public bool HasTrackingDevices;
            public bool IsTrackerConnected;
            public bool IsTrackerCalibrated;
            public bool IsTrackingGaze;
        }

        #endregion

        #region Internal members

        private ToolStripMenuItem tsmiOptions;
        private ToolStripMenuItem tsmiToggleServerConnection;
        private ToolStripMenuItem tsmiTogglePointerVisibility;
        private ToolStripMenuItem tsmiETUDOptions;
        private ToolStripMenuItem tsmiETUDCalibrate;
        private ToolStripSeparator tssETUDSeparator;
        private ToolStripMenuItem tsmiExit;
        private ContextMenuStrip cmsMenu;

        #endregion

        #region Events

        public event Action OnShowOptions = delegate { };
        public event Action OnToggleServerConnection = delegate { };
        public event Action OnTogglePointerVisibility = delegate { };
        public event Action OnShowETUDOptions = delegate { };
        public event Action OnCalibrateTracker = delegate { };
        public event Action OnExit = delegate { };

        #endregion

        #region Properties

        public ContextMenuStrip Strip { get { return cmsMenu; } }

        #endregion

        #region Public methods

        public Menu()
        {
            tsmiOptions = new ToolStripMenuItem("Options");
            tsmiOptions.Click += new EventHandler((s, e) => OnShowOptions());

            tsmiToggleServerConnection = new ToolStripMenuItem("Connect");
            tsmiToggleServerConnection.Click += new EventHandler((s, e) => OnToggleServerConnection());

            tsmiTogglePointerVisibility = new ToolStripMenuItem("Show pointers");
            tsmiTogglePointerVisibility.Click += new EventHandler((s, e) => OnTogglePointerVisibility());

            tsmiETUDOptions = new ToolStripMenuItem("ETU-Driver");
            tsmiETUDOptions.Click += new EventHandler((s, e) => OnShowETUDOptions());

            tsmiETUDCalibrate = new ToolStripMenuItem("Calibrate");
            tsmiETUDCalibrate.Click += new EventHandler((s, e) => OnCalibrateTracker());

            tssETUDSeparator = new ToolStripSeparator();

            tsmiExit = new ToolStripMenuItem("Exit");
            tsmiExit.Click += new EventHandler((s, e) => OnExit());

            cmsMenu = new ContextMenuStrip();

            cmsMenu.Items.Add(tsmiOptions);
            cmsMenu.Items.Add(tsmiToggleServerConnection);
            cmsMenu.Items.Add(tsmiTogglePointerVisibility);
            cmsMenu.Items.Add("-");
            cmsMenu.Items.Add(tsmiETUDOptions);
            cmsMenu.Items.Add(tsmiETUDCalibrate);
            cmsMenu.Items.Add(tssETUDSeparator);
            cmsMenu.Items.Add(tsmiExit);
        }

        public void update(State aState, bool aDisableAll)
        {
            tsmiOptions.Enabled = !aDisableAll && !aState.IsShowingOptions;// && !aState.IsTracking;
            tsmiToggleServerConnection.Enabled = !aDisableAll && (!aState.IsEyeTrackingRequired || aState.IsTrackerCalibrated);
            if (aDisableAll)
            {
                tsmiToggleServerConnection.Text = "Connecting...";
            }
            else
            {
                tsmiToggleServerConnection.Text = aState.IsServerConnected ? "Disconnect" : "Connect";
            }
            tsmiTogglePointerVisibility.Enabled = !aDisableAll;
            tsmiTogglePointerVisibility.Text = aState.IsPointerVisible ? "Hide pointers" : "Show pointers";
            tsmiETUDOptions.Enabled = !aDisableAll && !aState.IsShowingOptions && aState.HasTrackingDevices && !aState.IsTrackingGaze;
            tsmiETUDCalibrate.Enabled = !aDisableAll && !aState.IsShowingOptions && aState.IsTrackerConnected && !aState.IsTrackingGaze;
            tsmiExit.Enabled = !aState.IsShowingOptions;

            tsmiETUDOptions.Visible = aState.IsEyeTrackingRequired;
            tsmiETUDCalibrate.Visible = aState.IsEyeTrackingRequired;
            tssETUDSeparator.Visible = aState.IsEyeTrackingRequired;
        }

        #endregion
    }
}
