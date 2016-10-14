using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GazeNetClient.Plugins.Filter
{
    public enum Action : int
    {
        Pass,
        Block
    }

    [Serializable]
    [XmlType(Namespace = "GazeNetClient.Plugins", TypeName = "FilterConfig")]
    public class Config : Plugin.Config
    {
        public List<string> Names { get; set; } = new List<string>();
        public Action Action { get; set; } = Action.Pass;
    }
}
