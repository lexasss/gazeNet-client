using System.Collections.Generic;
using GazeNetClient.Utils;

// Icons by Gregor Cresnar, downloaded from FlatIcon.com

namespace GazeNetClient
{
    public struct InternalState
    {
        public bool IsShowingOptions;
        public bool IsEyeTrackingRequired;
        public bool IsServerConnected;
        public bool ArePointersVisible;
        public bool IsOwnPointerVisible;
        public bool HasTrackingDevices;
        public bool IsTrackerConnected;
        public bool IsTrackerCalibrated;
        public bool IsTrackingGaze;
    }

    internal class UIActions
    {
        public Dictionary<string, UIAction> Items { get; } = new Dictionary<string, UIAction>();

        public static UIActions create(GazeNetClient aClient)
        {
            UIActions actions = new UIActions();

            actions.Items.Add("Options", new UIAction("Options", aClient.showOptions, Properties.Resources.actionOptions));
            actions.Items.Add("Plugins", new UIAction("Plugins", aClient.showPluginOptions, Properties.Resources.actionPlugins));
            actions.Items.Add("sep1", new UIAction("-"));
            actions.Items.Add("ETUDOptions", new UIAction("Eye Tracker", aClient.showETUDOptions, Properties.Resources.actionEtudriver));
            actions.Items.Add("ETUDCalibrate", new UIAction("Calibrate", aClient.calibrate, Properties.Resources.actionCalibrate));
            actions.Items.Add("ETUDSep", new UIAction("-"));
            actions.Items.Add("Connection", new UIAction("Connect", aClient.toggleConnection, Properties.Resources.actionConnect, "Disconnect", Properties.Resources.actionDisconnect));
            actions.Items.Add("Pointers", new UIAction("Show remote pointers", aClient.togglePointersVisibility, Properties.Resources.actionPointers, true));
            actions.Items.Add("OwnPointer", new UIAction("Show own pointer", aClient.toggleOwnPointerVisibility, Properties.Resources.actionPointer, true));
            actions.Items.Add("sep2", new UIAction("-", aSet: UIActionSet.Menu));
            //actions.Items.Add("HideToolbar", new UIAction("Hide toolbar", aClient.hideToolbarToTray, Properties.Resources.actionHideToolbar, UIActionSet.Toolbar));
            actions.Items.Add("ShowToolbar", new UIAction("Show toolbar", aClient.showToolbar, Properties.Resources.actionShowToolbar, UIActionSet.Menu));
            actions.Items.Add("Exit", new UIAction("Exit", aClient.exit, Properties.Resources.actionExit, UIActionSet.Menu));

            return actions;
        }

        public void update(InternalState aState, bool aIsConnecting)
        {
            Items["Options"].Enabled = !aIsConnecting && !aState.IsShowingOptions;
            Items["Plugins"].Enabled = !aIsConnecting && !aState.IsShowingOptions && !aState.IsTrackingGaze;
            Items["Connection"].Enabled = !aIsConnecting && (!aState.IsEyeTrackingRequired || aState.IsTrackerCalibrated);
            if (aIsConnecting)
            {
                //Items["Connection"].Text = "Connecting...";
            }
            else
            {
                Items["Connection"].UseAltView = aState.IsServerConnected;
            }
            Items["Pointers"].Enabled = !aIsConnecting;
            Items["Pointers"].UseAltView = aState.ArePointersVisible;
            Items["OwnPointer"].Enabled = !aIsConnecting;
            Items["OwnPointer"].UseAltView = aState.IsOwnPointerVisible;
            Items["ETUDOptions"].Enabled = !aIsConnecting && !aState.IsShowingOptions && aState.HasTrackingDevices && !aState.IsTrackingGaze;
            Items["ETUDCalibrate"].Enabled = !aIsConnecting && !aState.IsShowingOptions && aState.IsTrackerConnected && !aState.IsTrackingGaze;
            Items["Exit"].Enabled = !aState.IsShowingOptions;

            Items["ETUDOptions"].Visible = aState.IsEyeTrackingRequired;
            Items["ETUDCalibrate"].Visible = aState.IsEyeTrackingRequired;
            Items["ETUDSep"].Visible = aState.IsEyeTrackingRequired;
        }
    }
}
