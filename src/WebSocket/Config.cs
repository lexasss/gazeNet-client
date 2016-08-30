using System;
using System.Web.Script.Serialization;

namespace GazeNetClient.WebSocket
{
    public enum ClientRole
    {
        None = 0,
        Listener = 1,
        Source = 2,
        SourceAndListener = 3
    }

    public class Config
    {
        private JavaScriptSerializer iJSON = new JavaScriptSerializer();

        public string Sources { get; set; } = "";
        public ClientRole Role { get; set; } = ClientRole.Source;

        public static readonly Config Default = new Config("default", ClientRole.SourceAndListener);

        public Config(string aSources, ClientRole aRole)
        {
            Sources = aSources;
            Role = aRole;
        }

        public override string ToString()
        {
            return iJSON.Serialize(new {
                config = new
                {
                    role = (int)Role,
                    sources = Sources
                }
            });
        }
    }
}
