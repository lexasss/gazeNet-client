using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace GazeNetClient.WebSocket
{
    public enum MessageType : int
    {
        GazeEvent = 0,
        Command = 1
    }

    public interface IReceived
    {
        string from { get; set; }
    }

    public interface ISent
    {
        MessageType type { get; set; }
        string topic { get; set; }
    }

    public class MessageSent<Payload> : ISent
    {
        public MessageType type { get; set; }
        public string topic { get; set; }
        public Payload payload { get; set; }
        public MessageSent() { }
        public MessageSent(string aTopic, Payload aPayload) { topic = aTopic; payload = aPayload; }
    }

    public class MessageReceived<Payload> : IReceived
    {
        public string from { get; set; }
        public Payload payload { get; set; }
        public MessageReceived() { }
        public MessageReceived(string aFrom, Payload aPayload) { from = aFrom; payload = aPayload; }
    }

    public class MessageReceived : IReceived
    {
        public string from { get; set; }
        public MessageType type { get; set; }
        public string payload { get; set; }

        public GazeEventReceived toGazeEvent()
        {
            if (type != MessageType.GazeEvent)
                return null;

            JavaScriptSerializer json = new JavaScriptSerializer();
            return new GazeEventReceived(from, json.Deserialize<GazeEvent>(payload));
        }

        public CommandReceived toCommand()
        {
            if (type != MessageType.Command)
                return null;

            JavaScriptSerializer json = new JavaScriptSerializer();
            return new CommandReceived(from, json.Deserialize<Plugin.Command>(payload));
        }
    }
}
