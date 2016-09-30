using System;
using System.Collections.Generic;

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

        event EventHandler<string> Log;
        event EventHandler<RequestArgs> Req;

        void start();
        void finilize();
        void feed(Processor.GazePoint aSample);
        void command(string aCommand, string aValue);
    }
}
