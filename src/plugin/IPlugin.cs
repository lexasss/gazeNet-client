using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GazeNetClient.Plugin
{
    public enum RequestType
    {
        Stop,
        Send,
        SetConfig
    }

    public class RequestArgs : EventArgs
    {
        public RequestType Type { get; private set; }
        public RequestArgs(RequestType aType) : base()
        {
            Type = aType;
        }
    }

    public interface IPlugin
    {
        Dictionary<string, EventHandler> MenuItems { get; }
        string Name { get; }
        string DisplayName { get; }
        bool IsExclusive { get; }
        bool Enabled { get; set; }
        OptionsWidget Options { get; }

        event EventHandler<string> Log;
        event EventHandler<RequestArgs> Req;

        void displayOptions();
        void acceptOptions();

        void start();
        void finilize();
        void feed(Processor.GazePoint aSample);
        void command(string aCommand, string aValue);
    }
}
