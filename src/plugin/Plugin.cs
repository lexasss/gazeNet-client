using System;
using System.Collections.Generic;

namespace GazeNetClient.Plugin
{
    public enum RequestType
    {
        Stop,
        Send
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
        void feed(float aX, float aY);
        void command(string aCommand, string aValue);
    }
}
