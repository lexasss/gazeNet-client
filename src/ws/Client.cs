using System;
using System.Windows.Forms;
using System.Web.Script.Serialization;

namespace GazeNetClient.WebSocket
{
    [Serializable]
    public class Client : IDisposable
    {
        #region Internal members

        private WebSocketSharp.WebSocket iWS = null;
        private JavaScriptSerializer iJSON = new JavaScriptSerializer();
        private bool iDisposed = false;
        private bool iNeedsRestart = false;

        private string iHost = "gazenet.sis.uta.fi";
        private ushort iPort = 80;
        private Config iConfig = new Config();

        #endregion

        #region Events

        public event EventHandler OnConnected = delegate { };
        public event EventHandler OnClosed = delegate { };
        public event EventHandler<GazeEventReceived> OnSample = delegate { };
        public event EventHandler<CommandReceived> OnCommand = delegate { };

        #endregion

        #region Properties

        public bool Connected { get { return iWS != null ? iWS.ReadyState != WebSocketSharp.WebSocketState.Closed : false; } }

        public bool NeedsRestart { get { return (iNeedsRestart || iConfig.NeedsRestart) && Connected; } }

        public string Host
        {
            get { return iHost; }
            set
            {
                if (value != iHost)
                {
                    iNeedsRestart = true;
                    iHost = value;
                }
            }
        }

        public ushort Port
        {
            get { return iPort; }
            set
            {
                if (value != iPort)
                {
                    iNeedsRestart = true;
                    iPort = value;
                }
            }
        }

        public Config Config
        {
            get { return iConfig; }
            set
            {
                if (value != iConfig)
                {
                    iNeedsRestart = true;
                    iConfig = value;
                }
            }
        }

        public bool Verbatime { get; set; } = false;

        #endregion

        #region Public methods

        public Client()
        {
//            Start();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void start()
        {
            if (Connected)
                return;

            iWS = new WebSocketSharp.WebSocket(String.Format("ws://{0}:{1}", Host, Port));

            iWS.OnOpen += (sender, e) =>
            {
                OnConnected(this, new EventArgs());
                iWS.Send(Config.ToString());
            };

            iWS.OnMessage += (sender, e) =>
            {
                if (e.IsPing || !e.IsText)
                    return;

                MessageReceived msg = iJSON.Deserialize<MessageReceived>(e.Data);
                switch (msg.type)
                {
                    case MessageType.GazeEvent:
                        OnSample(this, msg.toGazeEvent());
                        break;
                    case MessageType.Command:
                        OnCommand(this, msg.toCommand());
                        break;
                    default:
                        throw new NotImplementedException(string.Format("Message type #{0} is unknown", msg.type));
                }
            };

            iWS.OnError += (sender, e) =>
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            };

            iWS.OnClose += (sender, e) => OnClosed(this, new EventArgs());

            iWS.Connect();

            iNeedsRestart = false;
            iConfig.NeedsRestart = false;

            if (!Connected)
            {
               MessageBox.Show(String.Format("Cannot connect to the server ({0}:{1})", Host, Port),
                    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                iWS = null;
            }
        }

        public void stop()
        {
            if (iWS != null)
            {
                iWS.Close();
                iWS = null;
            }
        }

        public void restart()
        {
            stop();
            start();
        }

        public void send(GazeEvent aGazeEvent)
        {
            if (iWS != null)
            {
                string text = iJSON.Serialize(new GazeEventSent(Config.Topics, aGazeEvent));
                iWS.Send(text);
            }
        }

        public void send(Plugin.Command aCommand)
        {
            if (iWS != null)
            {
                string text = iJSON.Serialize(new CommandSent(Config.Topics, aCommand));
                System.Diagnostics.Debug.WriteLineIf(Verbatime, text);
                iWS.Send(text);
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
                stop();
            }

            // Free any unmanaged objects here.

            iDisposed = true;
        }

        #endregion
    }
}