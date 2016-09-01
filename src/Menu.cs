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
        private ToolStripMenuItem tsmiServerConnect;
        private ToolStripMenuItem tsmiTogglePointerVisibility;
        private ToolStripMenuItem tsmiETUDOptions;
        private ToolStripMenuItem tsmiETUDCalibrate;
        private ToolStripMenuItem tsmiETUDToggleTracking;
        private ToolStripSeparator tssETUDSeparator;
        private ToolStripMenuItem tsmiExit;
        private ContextMenuStrip cmsMenu;

        #endregion

        #region Events

        public event Action OnShowOptions = delegate { };
        public event Action OnServerConnect = delegate { };
        public event Action OnTogglePointerVisibility = delegate { };
        public event Action OnShowETUDOptions = delegate { };
        public event Action OnCalibrateTracker = delegate { };
        public event Action OnToggleTracking = delegate { };
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

            tsmiServerConnect = new ToolStripMenuItem("Connect");
            tsmiServerConnect.Click += new EventHandler((s, e) => OnServerConnect());

            tsmiTogglePointerVisibility = new ToolStripMenuItem("Show pointers");
            tsmiTogglePointerVisibility.Click += new EventHandler((s, e) => OnTogglePointerVisibility());

            tsmiETUDOptions = new ToolStripMenuItem("ETU-Driver");
            tsmiETUDOptions.Click += new EventHandler((s, e) => OnShowETUDOptions());

            tsmiETUDCalibrate = new ToolStripMenuItem("Calibrate");
            tsmiETUDCalibrate.Click += new EventHandler((s, e) => OnCalibrateTracker());

            tsmiETUDToggleTracking = new ToolStripMenuItem("Start");
            tsmiETUDToggleTracking.Click += new EventHandler((s, e) => OnToggleTracking());

            tssETUDSeparator = new ToolStripSeparator();

            tsmiExit = new ToolStripMenuItem("Exit");
            tsmiExit.Click += new EventHandler((s, e) => OnExit());

            cmsMenu = new ContextMenuStrip();

            cmsMenu.Items.Add(tsmiOptions);
            cmsMenu.Items.Add(tsmiServerConnect);
            cmsMenu.Items.Add(tsmiTogglePointerVisibility);
            cmsMenu.Items.Add("-");
            cmsMenu.Items.Add(tsmiETUDOptions);
            cmsMenu.Items.Add(tsmiETUDCalibrate);
            cmsMenu.Items.Add(tsmiETUDToggleTracking);
            cmsMenu.Items.Add(tssETUDSeparator);
            cmsMenu.Items.Add(tsmiExit);
        }

        public void update(State aState)
        {
            tsmiOptions.Enabled = !aState.IsShowingOptions;// && !aState.IsTracking;
            tsmiServerConnect.Text = aState.IsServerConnected ? "Disconnect" : "Connect";
            tsmiTogglePointerVisibility.Text = aState.IsPointerVisible ? "Hide pointers" : "Show pointers";
            tsmiETUDOptions.Enabled = !aState.IsShowingOptions && aState.HasTrackingDevices && !aState.IsTrackingGaze;
            tsmiETUDCalibrate.Enabled = !aState.IsShowingOptions && aState.IsTrackerConnected && !aState.IsTrackingGaze;
            tsmiETUDToggleTracking.Enabled = !aState.IsShowingOptions && aState.IsTrackerConnected && aState.IsTrackerCalibrated;
            tsmiETUDToggleTracking.Text = aState.IsTrackingGaze ? "Stop" : "Start";
            tsmiExit.Enabled = !aState.IsShowingOptions;

            tsmiServerConnect.Visible = !aState.IsEyeTrackingRequired;
            tsmiETUDOptions.Visible = aState.IsEyeTrackingRequired;
            tsmiETUDCalibrate.Visible = aState.IsEyeTrackingRequired;
            tsmiETUDToggleTracking.Visible = aState.IsEyeTrackingRequired;
            tssETUDSeparator.Visible = aState.IsEyeTrackingRequired;
        }

        #endregion
    }
}
