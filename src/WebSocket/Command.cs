using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazeNetClient.WebSocket
{
    public class Command
    {
        public string command { get; set; }
        public string value { get; set; }
    }

    public class CommandReceived : Command
    {
        public string from { get; set; }

        public CommandReceived() { }
        public CommandReceived(string aFrom, string aCommand, string aValue)
        {
            from = aFrom;
            command = aCommand;
            value = aValue;
        }
    }

    public class CommandSent : Command
    {
        public string topic { get; set; }

        public CommandSent() { }
        public CommandSent(string aTopic, string aCommand, string aValue)
        {
            topic = aTopic;
            command = aCommand;
            value = aValue;
        }
    }
}
