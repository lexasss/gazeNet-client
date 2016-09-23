using System;
using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace GazeNetClient.WebSocket
{
    public enum ClientRole
    {
        None = 0,
        Observer = 1,
        Source = 2,
        Both = 3
    }

    [Serializable]
    public class Config
    {
        private const string DEFAULT_TOPIC = "default";

        private JavaScriptSerializer iJSON = new JavaScriptSerializer();

        private string iTopics = DEFAULT_TOPIC;
        private string iUserName = "";
        private ClientRole iRole = ClientRole.Both;

        [XmlIgnore]
        public bool NeedsRestart { get; set; } = false;

        public string Topics
        {
            get { return iTopics; }
            set
            {
                string newTopic = !String.IsNullOrEmpty(value) ? value : DEFAULT_TOPIC;
                if (iTopics != newTopic)
                {
                    NeedsRestart = true;
                    iTopics = newTopic;
                }
            }
        }

        public string UserName
        {
            get { return iUserName; }
            set
            {
                if (value != iUserName)
                {
                    NeedsRestart = true;
                    iUserName = value;
                }
            }
        }

        public ClientRole Role
        {
            get { return iRole; }
            set
            {
                if (value != iRole)
                {
                    NeedsRestart = true;
                    iRole = value;
                }
            }
        }

        public Config() { }

        public override string ToString()
        {
            return iJSON.Serialize(new {
                config = new
                {
                    role = (int)Role,
                    topics = Topics,
                    name = UserName
                }
            });
        }
    }
}
