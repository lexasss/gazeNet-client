using System;
using System.Xml.Serialization;

namespace GazeNetClient.Plugins.Scaler
{
    [Serializable]
    public enum ScalingTarget : int
    {
        Own,
        Recevied
    }

    [Serializable]
    [XmlType(Namespace = "GazeNetClient.Plugins", TypeName = "ScalerConfig")]
    public class Config : Plugin.Config
    {
        public ScalingTarget ScalingTarget { get; set; } = ScalingTarget.Own;

        public int Left { get; set; } = 0;
        public int Top { get; set; } = 0;
        public int Right { get; set; } = 640;
        public int Bottom { get; set; } = 480;

        public int Width { get { return Right - Left; } }
        public int Height { get { return Bottom - Top; } }
    }
}
