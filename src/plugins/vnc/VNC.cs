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
        private Config iConfig;
        private Options iOptions;
        private Rectangle iDisplaySize;

        private Process iServer = null;
        private List<Process> iViewers = new List<Process>();

        public string Name { get; } = "vnc";
        public string DisplayName { get; } = "VNC server and client";
        public bool IsExclusive { get; } = false;
        public Dictionary<string, Utils.UIAction> MenuItems { get; } = null;
        public OptionsWidget Options { get { return iOptions; } }

        public bool Enabled
        {
            get { return iConfig.Enabled; }
            set
            {
                iConfig.Enabled = value;
                if (iConfig.Enabled)
                    StartProcesses();
                else
                    ShutdownProcesses();
            }
        }

        public event EventHandler<string> Log = delegate { };
        public event EventHandler<RequestArgs> Req = delegate { };

        public VNC()
        {
            iConfig = Utils.Storage<Config>.load();

            iOptions = new Options();

            if (iConfig.Enabled)
                StartProcesses();
        }

        ~VNC()
        {
            ShutdownProcesses();
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
            iOptions.txbUVNCInstallationFolder.Text = iConfig.UVNCInstallationFolder;

            iOptions.chkServerEnabled.Checked = iConfig.ServerEnabled;

            iOptions.chkViewersEnabled.Checked = iConfig.ViewersEnabled;
            iOptions.lsvViewers.Items.Clear();
            foreach (Viewer viewer in iConfig.Viewers)
                iOptions.lsvViewers.Items.Add(new ListViewItem(new string[] { viewer.IP, viewer.Name }));

            iOptions.chkViewOnly.Checked = iConfig.ViewOnly;
            iOptions.chkAutoScaling.Checked = iConfig.AutoScaling;
            iOptions.chkSharedServer.Checked = iConfig.SharedServer;
            iOptions.chkDisableClipboard.Checked = iConfig.DisableClipboard;
        }

        public void acceptOptions()
        {
            ShutdownProcesses();

            iConfig.UVNCInstallationFolder = iOptions.txbUVNCInstallationFolder.Text;

            iConfig.ServerEnabled = iOptions.chkServerEnabled.Checked;

            iConfig.ViewersEnabled = iOptions.chkViewersEnabled.Checked;
            iConfig.Viewers.Clear();
            foreach (ListViewItem viewer in iOptions.lsvViewers.Items)
                iConfig.Viewers.Add(new Viewer(viewer.SubItems[0].Text, viewer.SubItems[1].Text));

            iConfig.ViewOnly = iOptions.chkViewOnly.Checked;
            iConfig.AutoScaling = iOptions.chkAutoScaling.Checked;
            iConfig.SharedServer = iOptions.chkSharedServer.Checked;
            iConfig.DisableClipboard = iOptions.chkDisableClipboard.Checked;
        }

        public Processor.GazePoint feedOwnPoint(Processor.GazePoint aSample)
        {
            return aSample;
        }

        public bool feedReceivedPoint(string aFrom, ref PointF aPoint)
        {
            return true;
        }

        private void CloseRunningProcesses()
        {
            var serversOrViewers = Process.GetProcesses().Where(
                process => Config.isVNCProcess(process.ProcessName)
            );
            foreach (Process process in serversOrViewers)
            {
                Console.Write($"Closing {process.ProcessName} launched externally...  ");
                if (process.MainWindowHandle == IntPtr.Zero || !process.CloseMainWindow())
                    process.Kill();
                    
                process.WaitForExit(5000);
                Console.WriteLine(process.HasExited ? "done" : "failed");
            }
        }

        private void ShutdownProcesses()
        {
            Console.Write("Closing the clients...  ");
            foreach (Process viewer in iViewers)
                if (!viewer.HasExited && !viewer.CloseMainWindow())
                    viewer.Kill();
            foreach (Process viewer in iViewers)
                viewer.WaitForExit(5000);
            Console.WriteLine(iViewers.All(viewer => viewer.HasExited) ? "done" : "failed");

            iViewers.Clear();

            if (iServer != null && !iServer.HasExited)
            {
                Console.Write("Closing the server...  ");
                iServer.Kill();
                iServer.WaitForExit(5000);
                Console.WriteLine(iServer.HasExited ? "done" : "failed");
            }
            iServer = null;
        }

        private void StartProcesses()
        {
            if (!Config.isVNCInstalledInto(iConfig.UVNCInstallationFolder))
                return;

            CloseRunningProcesses();

            if (iConfig.ServerEnabled)
            {
                Console.Write("Generating passwords...  ");
                Process setPassword = Process.Start(iConfig.SetPasswordFileName, $"{Config.PASSWORD} {Config.PASSWORD}");
                setPassword.WaitForExit(3000);
                Console.WriteLine(setPassword.HasExited ? "done" : "failed");

                Console.Write("Staring the server...  ");
                iServer = Process.Start(iConfig.ServerFileName);
                Console.WriteLine(iServer != null && !iServer.HasExited ? "done" : "failed");
            }

            if (iConfig.ViewersEnabled)
            {
                foreach (Viewer viewer in iConfig.Viewers)
                {
                    Console.Write($"Staring the viewer {viewer.IP}...  ");
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append($" -server {viewer.IP}");
                    sb.Append($" -password {Config.PASSWORD}");
                    sb.Append($" -config {Config.UVNC_CONFIG}");
                    sb.Append(" -delay 3");
                    sb.Append(" -restricted");
                    sb.Append(" -nostatus");
                    sb.Append(" -nohotkeys");
                    sb.Append(" -notoolbar");
                    sb.Append(" -disablesponsor");
                    if (iConfig.ViewOnly)
                        sb.Append(" -viewonly");
                    if (iConfig.AutoScaling)
                        sb.Append(" -autoscaling");
                    if (iConfig.SharedServer)
                        sb.Append(" -shared");
                    if (iConfig.DisableClipboard)
                        sb.Append(" -disableclipboard");

                    Process viewerProcess = Process.Start(iConfig.ViewerFileName, sb.ToString());
                    Console.WriteLine(viewerProcess != null && !viewerProcess.HasExited ? "done" : "failed");
                    iViewers.Add(viewerProcess);
                }
            }
        }
    }
}
