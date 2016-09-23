namespace GazeNetClient.Plugin
{
    public class Command
    {
        public string target { get; set; }
        public string command { get; set; }
        public string value { get; set; }

        public Command() { }
        public Command(string aTarget, string aCommand, string aValue)
        {
            target = aTarget;
            command = aCommand;
            value = aValue;
        }
    }

    public class SendCommandRequestArgs : RequestArgs
    {
        public Command Command { get; private set; }
        public SendCommandRequestArgs(Command aCommand) : base(RequestType.Send)
        {
            Command = aCommand;
        }
    }
}
