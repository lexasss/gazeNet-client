using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Web.Script.Serialization;

namespace GazeNetClient.Experiment.OinQs
{
    public partial class MainForm : Form
    {
        private const string MSG_INTERNAL_ERROR = "Internal error: {0}";
        private const string MSG_CONNECTION_LOST = "The connection was lost";
        private const string MSG_DATA_LOSS_WARNING = "Data will be lost if you close the application before it is finished. Do you want to exit now?";

        private const string KEY_FOUND = "ENTER";
        private const string KEY_NOT_FOUND = "SPACE";
        private const string KEY_CONTINUE = "SPACE";

        private const string INSTRUCTION_INITIAL = "Search for \"{0}\". Press {1} when found, or {2} if is not displayed.";
        private const string INSTRUCTION_CONTINUE = "Press {0} to continue.";
        private const string INSTRUCTION_FEEDBACK = "Response: \"{0}\".   {1}.";
        private const string INSTRUCTION_FEEDBACK_FOUND = "FOUND";
        private const string INSTRUCTION_FEEDBACK_NOT_FOUND = "NOT FOUND";
        private const string INSTRUCTION_FEEDBACK_CORRECT = "Correct";
        private const string INSTRUCTION_FEEDBACK_INCORRECT = "NOT correct";
        private const string INSTRUCTION_TIMEOUT = "Timeout.";
        private const string INSTRUCTION_TIMEOUT_CONTINUATION = " Try to be faster.";
        private const string INSTRUCTION_FINISHED = "Finished. Thank you!";
        private const string INSTRUCTION_FOCUS = "+";
        private const string INSTRUCTION_VERTICAL_SPACE = "\n\n";
        private const string INSTRUCTION_FAILURE = "SETUP FAILURE.\nPlease wait...";

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

            Utils.Sizing.getScale(iConfig.ScreenSize, iConfig.Distance);

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
            nudDistance.Value = iConfig.Distance;

            Utils.Sizing.ScreenSize = iConfig.ScreenSize;
            Utils.Sizing.ScreenDistance = iConfig.Distance;

            iUIContext = WindowsFormsSynchronizationContext.Current;
            if (iUIContext == null)
            {
                throw new Exception(string.Format(MSG_INTERNAL_ERROR, "no UI ocntext"));
            }
        }

        private void StartSession()
        {
            string payload;

            string instruction = 
                string.Format(INSTRUCTION_INITIAL, Plugins.OinQs.LayoutItemText.Target, KEY_FOUND, KEY_NOT_FOUND) +
                INSTRUCTION_VERTICAL_SPACE + 
                string.Format(INSTRUCTION_CONTINUE, KEY_CONTINUE);

            payload = iJSON.Serialize(new Plugins.OinQs.Instruction(instruction, 0));
            iWebSocketClient.send(new Plugin.Command(Plugins.OinQs.OinQs.NAME, Plugins.OinQs.Command.INSTRUCTION, payload));

            payload = iJSON.Serialize(new Plugin.ContainerConfig(
                iConfig.IsPointerVisible,
                iConfig.ScreenSize,
                iConfig.Distance
            ));
            iWebSocketClient.send(new Plugin.Command(Plugins.OinQs.OinQs.NAME, Plugins.OinQs.Command.CONFIG, payload));
            /*
            Utils.DelayedAction.Execute(5000, () =>
            {
                StartTrial();
            });*/
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
                payload = iJSON.Serialize(new Plugins.OinQs.Instruction(INSTRUCTION_FAILURE, 4000));
                iWebSocketClient.send(new Plugin.Command(Plugins.OinQs.OinQs.NAME, Plugins.OinQs.Command.INSTRUCTION, payload));

                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            payload = iJSON.Serialize(items);
            iWebSocketClient.send(new Plugin.Command(Plugins.OinQs.OinQs.NAME, Plugins.OinQs.Command.ADD_RANGE, payload));

            Utils.DelayedAction.Execute(500, () =>
            {
                payload = iJSON.Serialize(new Plugins.OinQs.Instruction(INSTRUCTION_FOCUS, 0));
                iWebSocketClient.send(new Plugin.Command(Plugins.OinQs.OinQs.NAME, Plugins.OinQs.Command.INSTRUCTION, payload));
            });

            Utils.DelayedAction.Execute(3000, () =>
            {
                iSession.startTrial();
                iReplyTimeout.Start();

                payload = iJSON.Serialize(new Plugins.OinQs.TrialConfig(iSession.TrialCondition.ToString()));
                iWebSocketClient.send(new Plugin.Command(Plugins.OinQs.OinQs.NAME, Plugins.OinQs.Command.DISPLAY, payload));
            });
        }

        private void FinishTrial(string aParticipant, TrialResult aResult)
        {
            bool sessionFinished = iSession.finishTrial(aParticipant, aResult);

            ShowFeedback(aResult, sessionFinished);

            if (sessionFinished)
            {
                iWebSocketClient.send(new Plugin.Command(Plugins.OinQs.OinQs.NAME, Plugins.OinQs.Command.FINISHED, ""));
                iWebSocketClient.stop();
            }
        }

        private void ShowFeedback(TrialResult aResult, bool aIsSessionFinished)
        {
            string instruction;
            if (aResult == TrialResult.Timeout)
            {
                instruction = INSTRUCTION_TIMEOUT;
                if (!aIsSessionFinished)
                    instruction += INSTRUCTION_TIMEOUT_CONTINUATION;
            }
            else
            {
                instruction = string.Format(INSTRUCTION_FEEDBACK, aResult == TrialResult.Found ?
                    INSTRUCTION_FEEDBACK_FOUND : INSTRUCTION_FEEDBACK_NOT_FOUND,
                    iSession.isResultCorrect(aResult) ? INSTRUCTION_FEEDBACK_CORRECT : INSTRUCTION_FEEDBACK_INCORRECT);
            }

            int displayInterval = 0;
            if (aIsSessionFinished)
            {
                displayInterval = 3000;
                instruction += INSTRUCTION_VERTICAL_SPACE + INSTRUCTION_FINISHED;
            }

            string payload = iJSON.Serialize(new Plugins.OinQs.Instruction(instruction, displayInterval));
            iWebSocketClient.send(new Plugin.Command(Plugins.OinQs.OinQs.NAME, Plugins.OinQs.Command.INSTRUCTION, payload));

            if (!aIsSessionFinished)
            {
                Utils.DelayedAction.Execute(2000, () =>
                {
                    instruction = string.Format(INSTRUCTION_CONTINUE, KEY_CONTINUE);
                    payload = iJSON.Serialize(new Plugins.OinQs.Instruction(instruction, 0));
                    iWebSocketClient.send(new Plugin.Command(Plugins.OinQs.OinQs.NAME, Plugins.OinQs.Command.INSTRUCTION, payload));
                });
            }
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
                    MessageBox.Show(MSG_CONNECTION_LOST, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Utils.Storage.saveData((fileName) =>
                {
                    iSession.save(fileName);
                    iSession = null;
                }, (iConfig.IsPointerVisible ? "gz" : "ng") + "_summary");
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
                    e.Cancel = MessageBox.Show(MSG_DATA_LOSS_WARNING, Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No;
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
