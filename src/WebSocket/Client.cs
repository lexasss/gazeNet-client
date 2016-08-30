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
        private string iSource;

        public event EventHandler OnClosed = delegate { };
        public event EventHandler<GazeEventReceived> OnSample = delegate { };

        public bool Connected { get { return iWS.ReadyState != WebSocketSharp.WebSocketState.Closed; } }

        public Client(Config aConfig)
        {
            iSource = aConfig.Sources;

            iWS = new WebSocketSharp.WebSocket(url: String.Format("ws://{0}:{1}", Client.Host, Client.Port),
                onOpen: () =>
                {
                    return iWS.Send(aConfig.ToString());
                },
                onMessage: (e) =>
                {
                    string text = e.Text.ReadToEnd();
                    GazeEventReceived evt = iJSON.Deserialize<GazeEventReceived>(text);
                    OnSample(this, evt);
                    return null;
                },
                onError: (e) =>
                {
                    Console.WriteLine(e.Message);
                    return null;
                },
                onClose: (e) =>
                {
                    OnClosed(this, new EventArgs());
                    return null;
                }
            );

            /*
            iWS.OnOpen += (sender, e) => iWS.Send(aConfig.ToString());

            iWS.OnMessage += (sender, e) =>
            {
                if (e.IsPing)
                    return;

                GazeEventReceived evt = iJSON.Deserialize<GazeEventReceived>(e.Data);
                OnSample(this, evt);
            };

            iWS.OnError += (sender, e) => Console.WriteLine(e.Message);

            iWS.OnClose += (sender, e) => OnClosed(this, new EventArgs());
            */

            iWS.Connect();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Send(GazeEvent aGazeEvent)
        {
            if (iWS != null)
            {
                string text = iJSON.Serialize(new GazeEventSent(iSource, aGazeEvent));
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
                iWS.Close();
                iWS = null;
            }

            // Free any unmanaged objects here.
            
            iDisposed = true;
        }
    }
}