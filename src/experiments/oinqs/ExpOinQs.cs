using System;
using System.Drawing;
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

            iReplyTimeout.Interval = 1000 * iConfig.Timeout;
            iReplyTimeout.Tick += ReplyTimeout_Tick;

            txbTopic.Text = iWebSocketClient.Config.Topics;
            chkPointerVisibility.Checked = iConfig.IsPointerVisible;
            nudRepetitions.Value = iConfig.Repetitions;
            nudScreenSizeWidth.Value = iConfig.ScreenSize.Width;
            nudScreenSizeHeight.Value = iConfig.ScreenSize.Height;
            nudScreenResolutionWidth.Value = iConfig.ScreenResolution.Width;
            nudScreenResolutionHeight.Value = iConfig.ScreenResolution.Height;
            nudDistance.Value = iConfig.Distance;

            Utils.Sizing.ScreenSize = iConfig.ScreenSize;
            Utils.Sizing.ScreenResolution = iConfig.ScreenResolution;
            Utils.Sizing.ScreenDistance = iConfig.Distance;

            iUIContext = WindowsFormsSynchronizationContext.Current;
            if (iUIContext == null)
            {
                throw new Exception("Internal error: no UI ocntext");
            }
        }

        private void StartSession()
        {
            string payload;

            string instruction = string.Format("Search for \"{0}\". Press ENTER when found, or SPACE if is not displayed", Plugins.OinQs.LayoutItemText.Target);
            payload = iJSON.Serialize(new Plugins.OinQs.Instruction(instruction, 0));
            iWebSocketClient.send(new Plugin.Command(Plugins.OinQs.OinQs.NAME, Plugins.OinQs.Command.INSTRUCTION, payload));

            payload = iJSON.Serialize(new Plugin.ExternalConfig(iConfig.IsPointerVisible));
            iWebSocketClient.send(new Plugin.Command(Plugins.OinQs.OinQs.NAME, Plugins.OinQs.Command.CONFIG, payload));

            Utils.DelayedAction.Execute(5000, () =>
            {
                StartTrial();
            });
        }

        private void StartTrial()
        {
            string payload;
            Plugins.OinQs.LayoutItem[] items;

            try
            {
                items = iSession.createTrial();
            }
            catch (Exception ex)
            {
                string instruction = "SETUP FAILURE.\nPlease wait...";
                payload = iJSON.Serialize(new Plugins.OinQs.Instruction(instruction, 4000));
                iWebSocketClient.send(new Plugin.Command(Plugins.OinQs.OinQs.NAME, Plugins.OinQs.Command.INSTRUCTION, payload));

                MessageBox.Show(ex.Message, "O-in-Qs", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            payload = iJSON.Serialize(items);
            iWebSocketClient.send(new Plugin.Command(Plugins.OinQs.OinQs.NAME, Plugins.OinQs.Command.ADD_RANGE, payload));

            Utils.DelayedAction.Execute(500, () =>
            {
                string instruction = "+";
                payload = iJSON.Serialize(new Plugins.OinQs.Instruction(instruction, 0));
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

            ShowFeedback(aResult, sessionFinished);

            if (sessionFinished)
            {
                iWebSocketClient.stop();
            }
        }

        private void ShowFeedback(TrialResult aResult, bool aIsSessionFinished)
        {
            string instruction;
            if (aResult == TrialResult.Timeout)
            {
                instruction = "Timeout.";
                if (!aIsSessionFinished)
                    instruction += " Try to be faster.";
            }
            else
            {
                instruction = iSession.isResultCorrect(aResult) ? "Well done!" : "The target " +
                    (aResult == TrialResult.Found ? "WASN'T" : "WAS") + " there.";
            }

            instruction += aIsSessionFinished ? "\n\nFinished! Thank you!" : "\n\nPress SPACE to continue.";

            string payload = iJSON.Serialize(new Plugins.OinQs.Instruction(instruction, 2000));
            iWebSocketClient.send(new Plugin.Command(Plugins.OinQs.OinQs.NAME, Plugins.OinQs.Command.INSTRUCTION, payload));
        }

        #region Event handlers

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
                    iSession = null;
                }
            }), null);
        }

        private void WebSocketClient_OnCommand(object aSender, WebSocket.CommandReceived aArgs)
        {
            if (aArgs.payload.target != Plugins.OinQs.OinQs.NAME)
                return;

            iUIContext.Send(new SendOrPostCallback((target) => {
                if (aArgs.payload.command == Plugins.OinQs.Command.RESULT)
                {
                    if (!iReplyTimeout.Enabled)
                        return;

                    iReplyTimeout.Stop();
                    Plugins.OinQs.SearchResult result = iJSON.Deserialize<Plugins.OinQs.SearchResult>(aArgs.payload.value);
                    FinishTrial(aArgs.from, result.found ? TrialResult.Found : TrialResult.NotFound);
                }
                else if (aArgs.payload.command == Plugins.OinQs.Command.NEXT)
                {
                    if (iReplyTimeout.Enabled || iSession == null || iSession.Active)
                        return;

                    StartTrial();
                }
            }), null);
        }

        private void ReplyTimeout_Tick(object sender, EventArgs e)
        {
            iReplyTimeout.Stop();
            iWebSocketClient.send(new Plugin.Command(Plugins.OinQs.OinQs.NAME, Plugins.OinQs.Command.RESULT, ""));
            FinishTrial("", TrialResult.Timeout);
        }

        #endregion

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (iWebSocketClient != null)
            {
                if (iWebSocketClient.Connected)
                {
                    e.Cancel = MessageBox.Show("Data will be lost if you close the application before it is finished. Do you want to exit now?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No;
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

        private void nudRepetitions_ValueChanged(object sender, EventArgs e)
        {
            iConfig.Repetitions = (int)nudRepetitions.Value;
        }

        private void nudScreenSizeWidth_ValueChanged(object sender, EventArgs e)
        {
            iConfig.ScreenSize = new Size((int)nudScreenSizeWidth.Value, iConfig.ScreenSize.Height);
            Utils.Sizing.ScreenSize = iConfig.ScreenSize;
        }

        private void nudScreenSizeHeight_ValueChanged(object sender, EventArgs e)
        {
            iConfig.ScreenSize = new Size(iConfig.ScreenSize.Width, (int)nudScreenSizeHeight.Value);
            Utils.Sizing.ScreenSize = iConfig.ScreenSize;
        }

        private void nudScreenResolutionWidth_ValueChanged(object sender, EventArgs e)
        {
            iConfig.ScreenResolution = new Size((int)nudScreenResolutionWidth.Value, iConfig.ScreenResolution.Height);
            Utils.Sizing.ScreenResolution = iConfig.ScreenResolution;
        }

        private void nudScreenResolutionHeight_ValueChanged(object sender, EventArgs e)
        {
            iConfig.ScreenResolution = new Size(iConfig.ScreenResolution.Width, (int)nudScreenResolutionHeight.Value);
            Utils.Sizing.ScreenResolution = iConfig.ScreenResolution;
        }

        private void nudDistance_ValueChanged(object sender, EventArgs e)
        {
            iConfig.Distance = (int)nudDistance.Value;
            Utils.Sizing.ScreenDistance = iConfig.Distance;
        }

        private void chkPointerVisibility_CheckedChanged(object sender, EventArgs e)
        {
            iConfig.IsPointerVisible = chkPointerVisibility.Checked;
        }
    }
}
