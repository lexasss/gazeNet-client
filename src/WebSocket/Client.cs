using System;
using System.Web.Script.Serialization;
using System.Threading.Tasks;

namespace GazeNetClient.WebSocket
{
    public class Client : IDisposable
    {
        public static string Host { get; set; } = "localhost";
        public static ushort Port { get; set; } = 8080;

        private WebSocketSharp.WebSocket iWS = null;
        private JavaScriptSerializer iJSON = new JavaScriptSerializer();
        private bool iDisposed = false;
        private Config iConfig;

        public event EventHandler OnClosed = delegate { };
        public event EventHandler<GazeEventReceived> OnSample = delegate { };

        public bool Connected { get { return iWS.ReadyState != WebSocketSharp.WebSocketState.Closed; } }

        public Client(Config aConfig)
        {
            iConfig = aConfig;
            Start();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void restart()
        {
            Stop();
            Start();
        }

        public void send(GazeEvent aGazeEvent)
        {
            if (iWS != null)
            {
                string text = iJSON.Serialize(new GazeEventSent(iConfig.Sources, aGazeEvent));
                iWS.Send(text);
            }
        }

        protected virtual void Dispose(bool aDisposing)
        {
            if (iDisposed)
                return;

            if (aDisposing)
            {
                // Free any other managed objects here.
                Stop();
            }

            // Free any unmanaged objects here.

            iDisposed = true;
        }

        private void Start()
        {
            iWS = new WebSocketSharp.WebSocket(String.Format("ws://{0}:{1}", Client.Host, Client.Port));

            iWS.OnOpen += (sender, e) => iWS.Send(iConfig.ToString());

            iWS.OnMessage += (sender, e) =>
            {
                if (e.IsPing || !e.IsText)
                    return;

                GazeEventReceived evt = iJSON.Deserialize<GazeEventReceived>(e.Data);
                OnSample(this, evt);
            };

            iWS.OnError += (sender, e) => Console.WriteLine(e.Message);

            iWS.OnClose += (sender, e) => OnClosed(this, new EventArgs());

            iWS.Connect();
        }

        private void Stop()
        {
            if (iWS != null)
            {
                iWS.Close();
                iWS = null;
            }
        }
    }
}