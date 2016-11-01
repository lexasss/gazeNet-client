using System;
using System.Collections.Generic;
using System.Drawing;

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
        // Items requirements:
        //      must be enabled; they will be disabled automatically when gaze data flow starts
        //      images 24x24
        Dictionary<string, Utils.UIAction> MenuItems { get; }

        string Name { get; }
        string DisplayName { get; }
        bool IsExclusive { get; }
        bool Enabled { get; set; }
        OptionsWidget Options { get; }

        event EventHandler<string> Log;
        event EventHandler<RequestArgs> Req;

        void displayOptions();
        void acceptOptions();
        void updateMenuItems(InternalState aInternalState);

        void command(string aCommand, string aValue);

        void start();
        void finilize();

        Processor.GazePoint feedOwnPoint(Processor.GazePoint aSample);
        bool feedReceivedPoint(string aFrom, ref PointF aLocation);
    }
}
