using System.Drawing;

namespace GazeNetClient.Plugin
{
    public class ContainerConfig
    {
        public bool PointerVisisble { get; set; }
        public Size ScreenSize { get; set; }
        public int Distance { get; set; }

        public ContainerConfig() { }
        public ContainerConfig(bool aPointerVisisble, Size aScreenSize, int aDistance)
        {
            PointerVisisble = aPointerVisisble;
            ScreenSize = aScreenSize;
            Distance = aDistance;
        }
    }

    public class SetContainerConfigRequestArgs : RequestArgs
    {
        public ContainerConfig Config { get; private set; }
        public SetContainerConfigRequestArgs(ContainerConfig aExternalConfig) : base(RequestType.SetConfig)
        {
            Config = aExternalConfig;
        }
    }
}
