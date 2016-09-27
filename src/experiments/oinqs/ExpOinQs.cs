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
        private Session iSession;

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
            nudOsPerQs.Value = iConfig.OsPerQs;

            iUIContext = WindowsFormsSynchronizationContext.Current;
            if (iUIContext == null)
            {
                throw new Exception("Internal error: no UI ocntext");
            }
        }

        private void StartSession()
        {
            string instruction = string.Format("Search for \"{0}\". Press ENTER when found, or SPACE if is not displayed", Plugins.OinQs.LayoutItemText.Target);
            string payload = iJSON.Serialize(new Plugins.OinQs.Instruction(instruction, 4500));
            iWebSocketClient.send(new Plugin.Command(Plugins.OinQs.OinQs.NAME, Plugins.OinQs.Command.INSTRUCTION, payload));

            Utils.DelayedAction.Execute(5000, () =>
            {
                StartTrial();
            });
        }

        private void StartTrial()
        {
            Plugins.OinQs.LayoutItem[] items = iSession.createTrial();
            string payload = iJSON.Serialize(items);
            iWebSocketClient.send(new Plugin.Command(Plugins.OinQs.OinQs.NAME, Plugins.OinQs.Command.ADD_RANGE, payload));

            Utils.DelayedAction.Execute(500, () =>
            {
                string instruction = "+";
                payload = iJSON.Serialize(new Plugins.OinQs.Instruction(instruction, 2000));
                iWebSocketClient.send(new Plugin.Command(Plugins.OinQs.OinQs.NAME, Plugins.OinQs.Command.INSTRUCTION, payload));
            });

            Utils.DelayedAction.Execute(3000, () =>
            {
                iSession.startTrial();
                iReplyTimeout.Start();
                iWebSocketClient.send(new Plugin.Command(Plugins.OinQs.OinQs.NAME, Plugins.OinQs.Command.DISPLAY, ""));
            });
        }

        private void FinishTrial(string aParticipant, TrialResult aResult)
        {
            bool sessionFinished = iSession.finishTrial(aParticipant, aResult);
            if (sessionFinished)
            {
                string payload = iJSON.Serialize(new Plugins.OinQs.Instruction("Thank you!", 2000));
                iWebSocketClient.send(new Plugin.Command(Plugins.OinQs.OinQs.NAME, Plugins.OinQs.Command.INSTRUCTION, payload));
                iWebSocketClient.stop();
            }
            else
            {
                StartTrial();
            }
        }

        private void WebSocketClient_OnConnected(object aSender, EventArgs aArgs)
        {
            iUIContext.Send(new SendOrPostCallback((target) => {
                btnStart.Enabled = false;
                Utils.DelayedAction.Execute(1000, () => { StartSession(); });
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
                    iSession.save(svdSession.FileName);
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
                    FinishTrial(aArgs.from, result.found ? TrialResult.Found : TrialResult.NotFound);
                }
            }), null);
        }

        private void ReplyTimeout_Tick(object sender, EventArgs e)
        {
            iReplyTimeout.Stop();
            iWebSocketClient.send(new Plugin.Command(Plugins.OinQs.OinQs.NAME, Plugins.OinQs.Command.RESULT, ""));
            FinishTrial("", TrialResult.Timeout);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (iWebSocketClient != null)
            {
                if (iWebSocketClient.Connected)
                {
                    e.Cancel = MessageBox.Show("Data will be lost if you exit the experiment before it is finished. Do you want to exit now?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No;
                    if (e.Cancel)
                    {
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
            iSession = new Session(iConfig);
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

        private void nudOsPerQs_ValueChanged(object sender, EventArgs e)
        {
            iConfig.OsPerQs = (int)nudOsPerQs.Value;
        }
    }
}
