namespace GazeNetClient.WebSocket
{
    public class CommandSent : MessageSent<Plugin.Command>
    {
        public new MessageType type { get; set; } = MessageType.Command;
        public CommandSent() : base() { }
        public CommandSent(string aTopic, Plugin.Command aPayload) : base(aTopic, aPayload) { }
    }

    public class CommandReceived : MessageReceived<Plugin.Command>
    {
        public CommandReceived() { }
        public CommandReceived(string aFrom, Plugin.Command aPayload) : base(aFrom, aPayload) { }
    }
}
