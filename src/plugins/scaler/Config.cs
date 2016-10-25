using System;
using System.Drawing;
using System.Xml.Serialization;

namespace GazeNetClient.Plugins.Scaler
{
    [Serializable]
    [XmlType(Namespace = "GazeNetClient.Plugins", TypeName = "ScalerConfig")]
    public class Config : Plugin.Config
    {
        public bool OwnEnabled { get; set; } = true;
        public Rectangle Own { get; set; } = new Rectangle(0, 0, 640, 480);
        public bool ReceivedEnabled { get; set; } = false;
        public Rectangle Received { get; set; } = new Rectangle(0, 0, 640, 480);
        public string Window { get; set; } = "";
    }
}
