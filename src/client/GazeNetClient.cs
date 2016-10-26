using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using ETUDriver;
using GazeNetClient.Plugin;

// Comment #1
//
// A very weird behaviour of ETU-Driver was observed:
// when ETU-Driver tracking starts/stops from the WebSocket,
// even if its execution is directed to the UI sync context,
// then some functionality inside ETU-Driver becomes asynchronous.
// For example, when Mouse is used for data emultions and 
// ETUDriver.stopTracking() is called this way, then stop() function 
// in Mouse.dll stops the timer, deletes some objects and does other stuff, 
// but timer callback execution is still running in parallel and cause run-time
// errors as a result of de-referencing null pointers.
// 
// To overcome the problem, an additional direct ETU-Driver tracking toggling 
// was executed when using the context menu. The problem remains for the cases 
// when WebSocket disconnects itself: disconnection triggers ETUDriver tracking
// toggling, and at this point, as was described, the ETUDriver device module
// may throw an exception.
//
//
// Comment #2
//
// AppDomain.DoCallback does not help to solve the Commet #1 issue

namespace GazeNetClient
{
    public class GazeNetClient : IDisposable
    {
        public enum TrackingState
        {
            NotAvailable,
            Disconnected,
            Connected,
            Calibrating,
            Calibrated,
            Tracking
        }

        #region Internal members

        private CoETUDriver iETUDriver = null;
        private AppDomain iETUDriverAppDomain = null;
        private Processor.GazeParser iGazeParser = null;
        private Pointer.Collection iPointers = null;
        private Pointer.Pointer iOwnPointer = null;
        private UIActions iUIActions = null;
        private Menu iMenu = null;
        private Toolbar iToolbar = null;
        private NotifyIcon iTrayIcon = null;
        private Options iOptions = null;

        private WebSocket.Client iWebSocketClient = null;
        private SynchronizationContext iUIContext;

        private bool iExitAfterTrackingStopped = false;
        private bool iDisposed = false;

        private Plugin.Plugins iPlugins;

        #endregion

        #region Properties

        public AutoStarter AutoStarter { get; private set; }
        public TrackingState State
        {
            get
            {
                GazeNetClient.TrackingState result = TrackingState.NotAvailable;
                if (iETUDriver.Active != 0)
                    result = GazeNetClient.TrackingState.Tracking;
                else if (iETUDriver.Calibrated != 0)
                    result = GazeNetClient.TrackingState.Calibrated;
                else switch (iETUDriver.Ready)
                    {
                        case 1: result = GazeNetClient.TrackingState.Disconnected; break;
                        case 2: result = GazeNetClient.TrackingState.Connected; break;
                        case 3: result = GazeNetClient.TrackingState.Calibrating; break;
                    }
                return result;
            }
        }

        #endregion

        #region Public methods

        public GazeNetClient()
        {
            AutoStarter = Utils.Storage<AutoStarter>.load();

            iPointers = new Pointer.Collection();

            iPlugins = Plugin.Plugins.load();
            foreach (IPlugin plugin in iPlugins.Items)
            {
                plugin.Log += Plugin_Log;
                plugin.Req += Plugin_Req;
            }

            iETUDriverAppDomain = AppDomain.CurrentDomain;

            iETUDriver = new CoETUDriver();
            iETUDriver.OptionsFile = Utils.Storage.Folder + "etudriver.ini";
            iETUDriver.OnRecordingStart += ETUDriver_OnRecordingStart;
            iETUDriver.OnRecordingStop += ETUDriver_OnRecordingStop;
            iETUDriver.OnCalibrated += ETUDriver_OnCalibrated;
            iETUDriver.OnDataEvent += ETUDriver_OnDataEvent;

            iOwnPointer = new Pointer.Pointer(4, false);
            iOwnPointer.Appearance = Pointer.Style.Simple;
            iOwnPointer.Opacity = 0.5;

            iGazeParser = new Processor.GazeParser();
            iGazeParser.OnNewGazePoint += GazeParser_OnNewGazePoint;

            iWebSocketClient = Utils.Storage<WebSocket.Client>.load();
            iWebSocketClient.OnConnected += WebSocketClient_OnConnected;
            iWebSocketClient.OnClosed += WebSocketClient_OnClosed;
            iWebSocketClient.OnSample += WebSocketClient_OnSample;
            iWebSocketClient.OnCommand += WebSocketClient_OnCommand;

            iOptions = new Options();

            iUIActions = UIActions.create(this);

            iToolbar = new Toolbar(this, iUIActions);
            iToolbar.addPlugins(iPlugins.Items);
            iToolbar.Icon = Icon.FromHandle(new Bitmap(iOptions.Icons["initial"]).GetHicon());
            iToolbar.Show();

            iMenu = new Menu(iUIActions);
            iMenu.addPlugins(iPlugins.Items);

            iTrayIcon = new NotifyIcon();
            iTrayIcon.Icon = iToolbar.Icon;
            iTrayIcon.ContextMenuStrip = iMenu.Strip;
            iTrayIcon.Text = "GazeNet";
            //iTrayIcon.Visible = true;

            Utils.GlobalShortcut.add(new Utils.Shortcut("Pointer", new Action(togglePointersVisibility), Keys.F1, Keys.Control, Keys.Shift));
            Utils.GlobalShortcut.add(new Utils.Shortcut("OwnPointer", new Action(toggleOwnPointerVisibility), Keys.F12, true));
            Utils.GlobalShortcut.add(new Utils.Shortcut("Tracking", new Action(Shortcut_TrackingNext), Keys.Oemtilde, Keys.Control));
            Utils.GlobalShortcut.init();

            UpdateMenu(false);

            iUIContext = WindowsFormsSynchronizationContext.Current;
            if (iUIContext == null)
            {
                throw new Exception("Internal error: no UI ocntext");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void showOptions()
        {
            UpdateMenu(true);

            iOptions.ShowInTaskbar = iTrayIcon.Visible;
            iOptions.load(iPointers, iGazeParser.Filter, AutoStarter, iWebSocketClient);

            bool acceptChanges = iOptions.ShowDialog() == DialogResult.OK;
            iOptions.save(acceptChanges);

            UpdateMenu(false);
        }

        public void showPluginOptions()
        {
            UpdateMenu(true);
            iPlugins.showOptions(iTrayIcon.Visible);
            UpdateMenu(false);
        }

        public void toggleConnection()
        {
            if (iWebSocketClient == null)
                return;

            // see Comment #1
            if ((iWebSocketClient.Config.Role & WebSocket.ClientRole.Source) > 0)
            {
                if (iETUDriver.Active == 0)
                    StartTracking();
                else
                    StopTracking();
            }
            // end see */

            if (!iWebSocketClient.Connected)
                iWebSocketClient.start();
            else
                iWebSocketClient.stop();
        }

        public void togglePointersVisibility()
        {
            iPointers.Visible = !iPointers.Visible;
            UpdateMenu(false);
        }

        public void toggleOwnPointerVisibility()
        {
            if (!iOwnPointer.Visible)
                iOwnPointer.show();
            else
                iOwnPointer.hide();
            UpdateMenu(false);
        }

        public void showETUDOptions()
        {
            UpdateMenu(true);
            iETUDriver.showRecordingOptions();
            UpdateMenu(false);
        }

        public void calibrate()
        {
            UpdateMenu(true);
            iETUDriver.calibrate();
            UpdateMenu(false);
        }

        public void showToolbar()
        {
            iTrayIcon.Visible = false;
            iToolbar.Show();
        }

        public void hideToolbarToTray()
        {
            iToolbar.Hide();
            iTrayIcon.Visible = true;
        }

        public void exit()
        {
            if (iETUDriver?.Active != 0)
            {
                iExitAfterTrackingStopped = true;
                iETUDriver.stopTracking();
            }
            else
            {
                Exit();
            }
        }

        #endregion

        #region Internal methods

        protected virtual void Dispose(bool aDisposing)
        {
            if (iDisposed)
                return;

            if (aDisposing)
            {
                // Free any other managed objects here.
                iGazeParser.Dispose();
                iOwnPointer.Dispose();
                iPointers.Dispose();

                if (iWebSocketClient != null)
                {
                    Utils.Storage<WebSocket.Client>.save(iWebSocketClient);
                    iWebSocketClient.Dispose();
                }

                Utils.Storage<AutoStarter>.save(AutoStarter);
                Utils.GlobalShortcut.close();
            }

            // Free any unmanaged objects here.
            iETUDriver = null;
            
            iDisposed = true;
        }

        private void StartTracking()
        {
            //Pointer.Collection.VisilityFollowsDataAvailability = true;
            if (State != TrackingState.Tracking)
            {
                //iETUDriverAppDomain.DoCallBack(new CrossAppDomainDelegate(() => { // see Comment #2
                iGazeParser.start();
                iETUDriver.startTracking();
                //}));
            }
        }

        private void StopTracking()
        {
            if (State == TrackingState.Tracking)
            {
                //iETUDriverAppDomain.DoCallBack( new CrossAppDomainDelegate( () => { // see Comment #2
                iETUDriver.stopTracking();
                iGazeParser.stop();
                //}));
            }
            //Pointer.Collection.VisilityFollowsDataAvailability = false;
        }

        private void UpdateMenu(bool aIsShowingDialog, bool aConnecting = false)
        {
            InternalState internalState = new InternalState();
            if (!aConnecting)
            {
                internalState.IsShowingOptions = aIsShowingDialog;
                internalState.IsEyeTrackingRequired = iWebSocketClient?.Config.Role.HasFlag(WebSocket.ClientRole.Source) == true;
                internalState.IsServerConnected = iWebSocketClient?.Connected == true;
                internalState.ArePointersVisible = iPointers.Visible;
                internalState.IsOwnPointerVisible = iOwnPointer.Visible;
                internalState.HasTrackingDevices = iETUDriver?.DeviceCount > 0;
                internalState.IsTrackerConnected = iETUDriver?.Ready != 0;
                internalState.IsTrackerCalibrated = iETUDriver?.Calibrated != 0;
                internalState.IsTrackingGaze = iETUDriver?.Active != 0;
            }

            iUIActions.update(internalState, aConnecting);
            iPlugins.enableMenuItems(!internalState.IsServerConnected && !aConnecting);
            iMenu.update();
            iToolbar.update();

            if (iWebSocketClient != null)
            {
                string iconName = !iWebSocketClient.Connected ? "initial" : "connected";
                if (iWebSocketClient.Connected && iWebSocketClient.Config.Role == WebSocket.ClientRole.Observer)
                {
                    iconName += "-as-observer";
                }
                iToolbar.Icon = Icon.FromHandle(new Bitmap(iOptions.Icons[iconName]).GetHicon());
                iTrayIcon.Icon = iToolbar.Icon;
            }
        }

        private void Exit()
        {
            iToolbar.Close();
            iTrayIcon.Visible = false;
            Application.Exit();
        }

        #endregion

        #region ETUD event handlers

        private void ETUDriver_OnCalibrated()
        {
            UpdateMenu(false);
        }

        private void ETUDriver_OnRecordingStart()
        {
            UpdateMenu(false);
            iPlugins.start();
        }

        private void ETUDriver_OnRecordingStop()
        {
            UpdateMenu(false);

            iPlugins.finilize();

            if (iExitAfterTrackingStopped)
            {
                Exit();
            }
        }

        private void ETUDriver_OnDataEvent(EiETUDGazeEvent aEventID, ref int aData, ref int aResult)
        {
            if (aEventID == EiETUDGazeEvent.geSample)
            {
                SiETUDSample smp = iETUDriver.LastSample;
                PointF gazePoint = new PointF(smp.X[0], smp.Y[0]);
                iGazeParser.feed(smp.Time, gazePoint);
            }
        }

        #endregion

        #region Other event handlers

        private void Menu_Exit()
        {
            exit();
        }

        private void WebSocketClient_OnConnected(object sender, EventArgs e)
        {
            iUIContext.Send(new SendOrPostCallback((target) => {
                if ((iWebSocketClient.Config.Role & WebSocket.ClientRole.Source) > 0)
                {
                    StartTracking();
                }
                //else  // see Comment #1
                {
                    UpdateMenu(false);
                }
            }), null);
        }

        private void WebSocketClient_OnClosed(object sender, EventArgs e)
        {
            bool connectionFailure = iUIContext != SynchronizationContext.Current;
            iUIContext.Send(new SendOrPostCallback((target) => {
                if ((iWebSocketClient.Config.Role & WebSocket.ClientRole.Source) > 0)
                {
                    StopTracking();
                }
                //else  // see Comment #1
                {
                    UpdateMenu(false);
                }

                if (connectionFailure)
                {
                    if (iTrayIcon.Visible)
                    {
                        iTrayIcon.ShowBalloonTip(4000, iTrayIcon.Text, "Disconnected from the server", ToolTipIcon.Warning);
                    }
                    if (iWebSocketClient.Config.AutoRestartOnDisconnection)
                    {
                        Utils.DelayedAction.Execute(3000, new Action(() => toggleConnection()));
                    }
                }
            }), null);
        }

        private void WebSocketClient_OnSample(object aSender, WebSocket.GazeEventReceived aArgs)
        {
            //iPointers.feed(aArgs);
            iUIContext.Send(new SendOrPostCallback((target) => {
                PointF gazePoint = aArgs.payload.Location;
                if (iPlugins.feedReceivedPoint(aArgs.from, ref gazePoint))
                    iPointers.movePointer(aArgs.from, gazePoint);
            }), null);
        }

        private void WebSocketClient_OnCommand(object aSender, WebSocket.CommandReceived aArgs)
        {
            iUIContext.Send(new SendOrPostCallback((target) => {
                IPlugin plugin = iPlugins[aArgs.payload.target];
                if (plugin != null && plugin.Enabled)
                    plugin.command(aArgs.payload.command, aArgs.payload.value);
            }), null);
        }

        private void Plugin_Req(object aSender, RequestArgs aArgs)
        {
            switch (aArgs.Type)
            {
                case RequestType.Stop:
                    toggleConnection();
                    break;
                case RequestType.Send:
                    SendPluginRequest((aArgs as SendCommandRequestArgs).Command);
                    break;
                case RequestType.SetConfig:
                    {
                        ContainerConfig config = (aArgs as SetContainerConfigRequestArgs).Config;
                        if (config.PointerVisisble != iPointers.Visible)
                            togglePointersVisibility();
                        iPointers.scale(Utils.Sizing.getScale(config.ScreenSize, config.Distance));
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private void SendPluginRequest(Command aCommand)
        {
            iWebSocketClient.send(aCommand);
        }

        private void Plugin_Log(object aSender, string aArgs)
        {
            System.Diagnostics.Debug.WriteLine(aArgs);
        }

        private void GazeParser_OnNewGazePoint(object aSender, Processor.GazePoint aArgs)
        {
            Processor.GazePoint pt = iPlugins.feedOwnPoint(aArgs);
            iWebSocketClient.send(new WebSocket.GazeEvent(pt.LocationF));

            if (iOwnPointer.Visible)
            {
                iOwnPointer.moveTo(pt.LocationF);
            }
        }

        private void Shortcut_TrackingNext()
        {
            bool requiresTracking = (iWebSocketClient.Config.Role & WebSocket.ClientRole.Source) > 0;

            if (requiresTracking && State == TrackingState.Disconnected)
                showETUDOptions();
            else if (requiresTracking && State == TrackingState.Connected)
                calibrate();
            else if ((!requiresTracking || State == TrackingState.Calibrated) && !iWebSocketClient.Connected )
                toggleConnection();
            else if (iWebSocketClient.Connected)
                toggleConnection();
        }

        #endregion
    }
}
