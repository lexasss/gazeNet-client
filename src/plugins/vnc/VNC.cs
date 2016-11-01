using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using GazeNetClient.Plugin;

namespace GazeNetClient.Plugins.VNC
{
    public partial class VNC : IPlugin
    {
        private delegate bool CheckProcessName(Process aProcess);

        private Config iConfig;
        private Options iOptions;
        private Rectangle iDisplaySize;

        private Process iServer = null;

        public string Name { get; } = "vnc";
        public string DisplayName { get; } = "VNC server and client";
        public bool IsExclusive { get; } = false;
        public Dictionary<string, Utils.UIAction> MenuItems { get; } = new Dictionary<string, Utils.UIAction>();
        public OptionsWidget Options { get { return iOptions; } }

        public bool Enabled
        {
            get { return iConfig.Enabled; }
            set
            {
                iConfig.Enabled = value;
                if (iConfig.Enabled)
                {
                    StartServer();
                    if (!iConfig.LaunchViewersOnStart)
                        StartViewers();
                }
                else
                {
                    ShutdownAll();
                }
            }
        }

        public event EventHandler<string> Log = delegate { };
        public event EventHandler<RequestArgs> Req = delegate { };

        public VNC()
        {
            iConfig = Utils.Storage<Config>.load();

            MenuItems.Add("vnc_resize", new Utils.UIAction("VNC: resize viewers", () => { ResizeViewers(); }, Properties.Resources.resize, false));

            iDisplaySize = Screen.PrimaryScreen.Bounds;

            iOptions = new Options();

            if (iConfig.Enabled)
            {
                StartServer();
                if (!iConfig.LaunchViewersOnStart)
                    StartViewers();
            }
        }

        ~VNC()
        {
            ShutdownAll();
            Utils.Storage<Config>.save(iConfig);
        }

        public void command(string aCommand, string aValue) { }

        public void start()
        {
            iDisplaySize = Screen.PrimaryScreen.Bounds;

            if (iConfig.LaunchViewersOnStart)
                StartViewers();
        }

        public void finilize()
        {
            if (iConfig.LaunchViewersOnStart)
                ShutdownViewers();
        }

        public void displayOptions()
        {
            iOptions.txbUVNCInstallationFolder.Text = iConfig.UVNCInstallationFolder;

            iOptions.chkServerEnabled.Checked = iConfig.ServerEnabled;

            iOptions.chkViewersEnabled.Checked = iConfig.ViewersEnabled;
            iOptions.lsvViewers.Items.Clear();
            foreach (Viewer viewer in iConfig.Viewers)
                iOptions.lsvViewers.Items.Add(new ListViewItem(new string[] { viewer.IP, viewer.Name }));

            iOptions.chkViewOnly.Checked = iConfig.ViewOnly;
            iOptions.chkLaunchViewerOnStart.Checked = iConfig.LaunchViewersOnStart;
        }

        public void acceptOptions()
        {
            bool keepServerRunning = iServer != null && iOptions.chkServerEnabled.Checked;
            bool keepViewersRunning = iConfig.ViewersEnabled == iOptions.chkViewersEnabled.Checked &&
                iConfig.ViewOnly == iOptions.chkViewOnly.Checked &&
                iConfig.LaunchViewersOnStart == iOptions.chkLaunchViewerOnStart.Checked &&
                iConfig.Viewers.All(viewer =>
                {
                    foreach (ListViewItem item in iOptions.lsvViewers.Items)
                        if (viewer.IP == item.SubItems[0].Text && viewer.Name == item.SubItems[1].Text)
                            return true;
                    return false;
                });

            if (!keepServerRunning)
                ShutdownServer();
            if (!keepViewersRunning)
                ShutdownViewers();

            iConfig.UVNCInstallationFolder = iOptions.txbUVNCInstallationFolder.Text;

            iConfig.ServerEnabled = iOptions.chkServerEnabled.Checked;

            iConfig.ViewersEnabled = iOptions.chkViewersEnabled.Checked;

            if (!keepViewersRunning)
                iConfig.Viewers.Clear();

            foreach (ListViewItem item in iOptions.lsvViewers.Items)
            {
                bool exists = iConfig.Viewers.Any(viewer => viewer.IP == item.SubItems[0].Text && viewer.Name == item.SubItems[1].Text);
                if (!exists)
                    iConfig.Viewers.Add(new Viewer(item.SubItems[0].Text, item.SubItems[1].Text));
            }

            iConfig.ViewOnly = iOptions.chkViewOnly.Checked;
            iConfig.LaunchViewersOnStart = iOptions.chkLaunchViewerOnStart.Checked;
        }

        public void updateMenuItems(InternalState aInternalState)
        {
            MenuItems["vnc_resize"].Enabled = iConfig.Viewers.Any(viewer => viewer.Process != null );
            foreach (Utils.UIAction action in MenuItems.Values)
                action.Visible = this.Enabled;
        }

        public Processor.GazePoint feedOwnPoint(Processor.GazePoint aSample)
        {
            return aSample;
        }

        public bool feedReceivedPoint(string aFrom, ref PointF aPoint)
        {
            if (!iConfig.ViewersEnabled)
                return true;

            foreach (Viewer viewer in iConfig.Viewers)
            {
                if (viewer.Name == aFrom && viewer.Process != null)
                {
                    IntPtr viewerWindow = viewer.Process.MainWindowHandle;
                    if (!Utils.WinAPI.IsWindow(viewerWindow))
                        return false;

                    Utils.WinAPI.RECT winRect;
                    if (!Utils.WinAPI.GetWindowRect(viewerWindow, out winRect))
                        return false;

                    Utils.WinAPI.RECT clientRect;
                    if (!Utils.WinAPI.GetClientRect(viewerWindow, out clientRect))
                        return false;

                    Size clientSize = new Size(clientRect.Right, clientRect.Bottom);
                    int border = ((winRect.Right - winRect.Left) - clientSize.Width) / 2;
                    Rectangle rect = new Rectangle(winRect.Left + border,
                        winRect.Bottom - border - clientSize.Height,
                        clientSize.Width, clientSize.Height);

                    float x = iDisplaySize.Left + rect.Left + rect.Width * (aPoint.X / iDisplaySize.Width);
                    float y = iDisplaySize.Top + rect.Top + rect.Height * (aPoint.Y / iDisplaySize.Height);
                    aPoint = new PointF((int)x, (int)y);

                    return true;
                }
            }

            return false;
        }

        private void CloseProcessesLaunchedExternally(CheckProcessName aProcessNameMeetsCondition)
        {
            var processes = Process.GetProcesses().Where(
                process => aProcessNameMeetsCondition(process)
            );
            foreach (Process process in processes)
            {
                Console.Write($"Closing {process.ProcessName} launched externally...  ");
                if (process.MainWindowHandle == IntPtr.Zero || !process.CloseMainWindow())
                    process.Kill();

                process.WaitForExit(5000);
                Console.WriteLine(process.HasExited ? "done" : "failed");
            }
        }

        private void ShutdownViewers()
        {
            Console.Write("Closing the viewers...  ");

            foreach (Viewer viewer in iConfig.Viewers)
                if (viewer.Process != null && !viewer.Process.HasExited && !viewer.Process.CloseMainWindow())
                    viewer.Process.Kill();

            foreach (Viewer viewer in iConfig.Viewers)
                viewer.Process?.WaitForExit(5000);

            Console.WriteLine(iConfig.Viewers.All(viewer => viewer.Process?.HasExited ?? true) ? "done" : "failed");

            foreach (Viewer viewer in iConfig.Viewers)
                viewer.Process = null;
        }

        private void ShutdownServer()
        {
            if (iServer != null && !iServer.HasExited)
            {
                Console.Write("Closing the server...  ");
                iServer.Kill();
                iServer.WaitForExit(5000);
                Console.WriteLine(iServer.HasExited ? "done" : "failed");
            }

            iServer = null;
        }

        private void ShutdownAll()
        {
            ShutdownViewers();
            ShutdownServer();
        }

        private void StartServer()
        {
            if (!Config.isVNCInstalledInto(iConfig.UVNCInstallationFolder))
                return;
            if (iServer != null)
                return;

            CloseProcessesLaunchedExternally(process => process.ProcessName == Config.UVNC_SERVER);

            if (iConfig.ServerEnabled && iServer == null)
            {
                Console.Write("Generating passwords...  ");
                Process setPassword = Process.Start(iConfig.SetPasswordFileName, $"{Config.PASSWORD} {Config.PASSWORD}");
                setPassword.WaitForExit(3000);
                Console.WriteLine(setPassword.HasExited ? "done" : "failed");

                Console.Write("Staring the server...  ");
                iServer = Process.Start(iConfig.ServerFileName);
                Console.WriteLine(iServer != null && !iServer.HasExited ? "done" : "failed");
            }
        }

        private void StartViewers()
        {
            if (!Config.isVNCInstalledInto(iConfig.UVNCInstallationFolder))
                return;
            if (iConfig.Viewers.Any(viewer => viewer.Process != null))
                return;

            CloseProcessesLaunchedExternally(process => process.ProcessName == Config.UVNC_VIEWER);

            if (iConfig.ViewersEnabled)
            {
                foreach (Viewer viewer in iConfig.Viewers)
                {
                    if (viewer.Process != null)
                        continue;

                    Console.Write($"Staring the viewer {viewer.IP}...  ");
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append($" -server {viewer.IP}");
                    sb.Append($" -password {Config.PASSWORD}");
                    sb.Append($" -config {Config.UVNC_CONFIG}");
                    sb.Append($" -autoreconnect {Config.UVNC_RECONNECT_DELAY}");
                    //sb.Append($" -reconnect {100}");
                    sb.Append(" -delay 3");
                    sb.Append(" -restricted");
                    sb.Append(" -nostatus");
                    sb.Append(" -nohotkeys");
                    sb.Append(" -notoolbar");
                    sb.Append(" -disablesponsor");
                    sb.Append(" -disableclipboard");
                    sb.Append(" -autoscaling");
                    sb.Append(" -shared");
                    if (iConfig.ViewOnly)
                        sb.Append(" -viewonly");

                    Process viewerProcess = Process.Start(iConfig.ViewerFileName, sb.ToString());
                    Console.WriteLine(viewerProcess != null && !viewerProcess.HasExited ? "done" : "failed");

                    viewer.Process = viewerProcess;
                }
            }
        }

        private void ResizeViewers()
        {
            Console.WriteLine("Resizing...");
            foreach (Viewer viewer in iConfig.Viewers)
            {
                if (viewer.Process == null)
                    continue;

                IntPtr window = viewer.Process.MainWindowHandle;
                if (!Utils.WinAPI.IsWindow(window))
                    continue;

                Utils.WinAPI.SetWindowPos(window, Utils.WinAPI.HWND.TOP, 0, 0, iDisplaySize.Width / 2, iDisplaySize.Height / 2,
                    Utils.WinAPI.SWP.NOMOVE | Utils.WinAPI.SWP.NOZORDER | Utils.WinAPI.SWP.NOOWNERZORDER);

                Console.WriteLine($"  resized {viewer.Name}@{viewer.IP}");
            }
            Console.WriteLine("  ...finished");
        }
    }
}
