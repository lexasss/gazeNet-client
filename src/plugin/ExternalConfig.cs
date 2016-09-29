namespace GazeNetClient.Plugin
{
    public class ExternalConfig
    {
        public bool PointerVisisble { get; set; }
        public ExternalConfig() { }
        public ExternalConfig(bool aPointerVisisble)
        {
            PointerVisisble = aPointerVisisble;
        }
    }

    public class SetExternalConfigRequestArgs : RequestArgs
    {
        public ExternalConfig ExternalConfig { get; private set; }
        public SetExternalConfigRequestArgs(ExternalConfig aExternalConfig) : base(RequestType.SetConfig)
        {
            ExternalConfig = aExternalConfig;
        }
    }
}
