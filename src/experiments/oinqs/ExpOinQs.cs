using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Web.Script.Serialization;

namespace GazeNetClient.Experiment.OinQs
{
    public partial class MainForm : Form
    {
        private Config iConfig;
        private WebSocket.Client iWebSocketClient = null;
        JavaScriptSerializer iJSON = new JavaScriptSerializer();
        private SynchronizationContext iUIContext;

        private System.Windows.Forms.Timer iReplyTimeout = new System.Windows.Forms.Timer();
        private Logger iLogger = new Logger();

        public MainForm()
        {
            InitializeComponent();

            iConfig = Utils.Storage<Config>.load();

            iWebSocketClient = Utils.Storage<WebSocket.Client>.load();
            iWebSocketClient.Config.Role = WebSocket.ClientRole.Observer;
            iWebSocketClient.Config.UserName = "experimenter";
            iWebSocketClient.OnConnected += WebSocketClient_OnConnected;
            iWebSocketClient.OnClosed += WebSocketClient_OnClosed;
            iWebSocketClient.OnCommand += WebSocketClient_OnCommand;

            iReplyTimeout.Interval = 10000;
            iReplyTimeout.Tick += ReplyTimeout_Tick;

            txbTopic.Text = iWebSocketClient.Config.Topics;
            nudObjectCount.Value = iConfig.ObjectCount;
            nudTrialCount.Value = iConfig.TrialCount;

            iUIContext = WindowsFormsSynchronizationContext.Current;
            if (iUIContext == null)
            {
                throw new Exception("Internal error: no UI ocntext");
            }
        }

        private void StartTrial()
        {
            LayoutItem[] items = RandomLayout.create(Screen.PrimaryScreen.Bounds.Size, iConfig.ObjectCount);
            string payload = iJSON.Serialize(items);
            iWebSocketClient.send(new Plugin.Command(Plugins.OinQs.OinQs.NAME, Plugins.OinQs.Command.ADD_RANGE, payload));

            new Utils.DelayedAction(1000, () =>
            {
                iLogger.startTrial();
                iReplyTimeout.Start();
                iWebSocketClient.send(new Plugin.Command(Plugins.OinQs.OinQs.NAME, Plugins.OinQs.Command.DISPLAY, ""));
            });
        }

        private void Finish()
        {
        }

        private void WebSocketClient_OnConnected(object aSender, EventArgs aArgs)
        {
            iUIContext.Send(new SendOrPostCallback((target) => {
                btnStart.Enabled = false;
                new Utils.DelayedAction(1000, () => { StartTrial(); });
            }), null);
        }

        private void WebSocketClient_OnClosed(object aSender, EventArgs aArgs)
        {
            bool isConnectionLost = iUIContext != SynchronizationContext.Current;
            iUIContext.Send(new SendOrPostCallback((target) => {
                btnStart.Enabled = true;
                if (isConnectionLost)
                {
                    MessageBox.Show("The connection was lost", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (svdSession.ShowDialog() == DialogResult.OK)
                {
                    iLogger.save(svdSession.FileName);
                }
            }), null);
        }

        private void WebSocketClient_OnCommand(object aSender, WebSocket.CommandReceived aArgs)
        {
            iUIContext.Send(new SendOrPostCallback((target) => {
                if (aArgs.payload.target == Plugins.OinQs.OinQs.NAME && aArgs.payload.command == Plugins.OinQs.Command.RESULT)
                {
                    iReplyTimeout.Stop();
                    Plugins.OinQs.SearchResult result = iJSON.Deserialize<Plugins.OinQs.SearchResult>(aArgs.payload.value);

                    Console.WriteLine("FROM: {0}, VAL: {1}", aArgs.from, result.found);

                    if (iLogger.finishTrial(aArgs.from, result.found ? TrialResult.Found : TrialResult.NotFound))
                    {
                        iWebSocketClient.stop();
                    }
                    else
                    {
                        StartTrial();
                    }
                }
            }), null);
        }

        private void ReplyTimeout_Tick(object sender, EventArgs e)
        {
            iReplyTimeout.Stop();

            iWebSocketClient.send(new Plugin.Command(Plugins.OinQs.OinQs.NAME, Plugins.OinQs.Command.RESULT, ""));

            if (iLogger.finishTrial("", TrialResult.Timeout))
            {
                iWebSocketClient.stop();
            }
            else
            {
                StartTrial();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (iWebSocketClient != null)
            {
                if (iWebSocketClient.Connected)
                {
                    if (MessageBox.Show("Data will be lost if you exit the experiment before it is finished. Do you want to exit now?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        e.Cancel = true;
                        return;
                    }
                    else
                    {
                        iWebSocketClient.OnClosed -= WebSocketClient_OnClosed;
                        iWebSocketClient.stop();
                    }
                }

                Utils.Storage<WebSocket.Client>.save(iWebSocketClient);
                iWebSocketClient.Dispose();
            }

            Utils.Storage<Config>.save(iConfig);
        }

        private void btnStart_Click(object aSender, EventArgs aArgs)
        {
            iLogger.initialize((int)nudTrialCount.Value);
            iWebSocketClient.start();
        }

        private void txbTopic_TextChanged(object sender, EventArgs e)
        {
            iWebSocketClient.Config.Topics = txbTopic.Text;
        }

        private void nudObjectCount_ValueChanged(object sender, EventArgs e)
        {
            iConfig.ObjectCount = (int)nudObjectCount.Value;
        }

        private void nudTrialCount_ValueChanged(object sender, EventArgs e)
        {
            iConfig.TrialCount = (int)nudTrialCount.Value;
        }
    }
}
