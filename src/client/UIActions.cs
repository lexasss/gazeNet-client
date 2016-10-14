using System;
using System.Collections.Generic;
using System.Drawing;

// Icons by Gregor Cresnar, downloaded from FlatIcon.com

namespace GazeNetClient
{
    [Flags]
    internal enum UIActionSet
    {
        Menu = 1,
        Toolbar = 2
    }

    internal struct InternalState
    {
        public bool IsShowingOptions;
        public bool IsEyeTrackingRequired;
        public bool IsServerConnected;
        public bool IsPointerVisible;
        public bool HasTrackingDevices;
        public bool IsTrackerConnected;
        public bool IsTrackerCalibrated;
        public bool IsTrackingGaze;
    }

    internal class UIAction
    {
        private string iText;
        private Image iImage = null;
        private string iAltText = null;
        private Image iAltImage = null;

        public string Text { get { return UseAlt && !string.IsNullOrEmpty(iAltText) ? iAltText : iText; } }
        public Image Image { get { return UseAlt && iAltImage != null ? iAltImage : iImage; } }
        public Action Action { get; }
        public UIActionSet Set { get; }
        public bool Toggable { get; }

        public bool UseAlt { get; set; } = false;

        public bool Checked { get { return Toggable && UseAlt; } }
        public bool Enabled { get; set; } = true;
        public bool Visible { get; set; } = true;

        public UIAction(string aText, Action aAction = null, Image aImage = null, UIActionSet aSet = UIActionSet.Menu | UIActionSet.Toolbar, 
            bool aToggable = false, string aAltText = null, Image aAltImage = null)
        {
            iText = aText;
            Action = aAction;
            iImage = aImage;
            Set = aSet;
            Toggable = aToggable;
            iAltText = aAltText;
            iAltImage = aAltImage;
        }

        public UIAction(string aText, Action aAction, Image aImage, bool aToggable) :
            this(aText, aAction, aImage, UIActionSet.Menu | UIActionSet.Toolbar, aToggable)
        {
        }

        public UIAction(string aText, Action aAction, Image aImage, string aAltText, Image aAltImage) :
            this(aText, aAction, aImage, UIActionSet.Menu | UIActionSet.Toolbar, false, aAltText, aAltImage)
        {
        }
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
            actions.Items.Add("Connection", new UIAction("Connect", aClient.toggleConnection, Properties.Resources.actionConnect, "Disconnect", Properties.Resources.actionDisconnect));
            actions.Items.Add("Pointers", new UIAction("Show pointers", aClient.togglePointersVisibility, Properties.Resources.actionPointer, true));
            actions.Items.Add("sep2", new UIAction("-"));
            actions.Items.Add("ETUDOptions", new UIAction("ETU-Driver", aClient.showETUDOptions, Properties.Resources.actionEtudriver));
            actions.Items.Add("ETUDCalibrate", new UIAction("Calibrate", aClient.calibrate, Properties.Resources.actionCalibrate));
            actions.Items.Add("ETUDSep", new UIAction("-"));
            actions.Items.Add("HideToolbar", new UIAction("Hide toolbar", aClient.hideToolbar, Properties.Resources.actionHideToolbar, UIActionSet.Toolbar));
            actions.Items.Add("ShowToolbar", new UIAction("Show toolbar", aClient.showToolbar, Properties.Resources.actionShowToolbar, UIActionSet.Menu));
            actions.Items.Add("Exit", new UIAction("Exit", aClient.exit, Properties.Resources.actionExit));

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
                Items["Connection"].UseAlt = aState.IsServerConnected;
            }
            Items["Pointers"].Enabled = !aIsConnecting;
            Items["Pointers"].UseAlt = aState.IsPointerVisible;
            Items["ETUDOptions"].Enabled = !aIsConnecting && !aState.IsShowingOptions && aState.HasTrackingDevices && !aState.IsTrackingGaze;
            Items["ETUDCalibrate"].Enabled = !aIsConnecting && !aState.IsShowingOptions && aState.IsTrackerConnected && !aState.IsTrackingGaze;
            Items["Exit"].Enabled = !aState.IsShowingOptions;

            Items["ETUDOptions"].Visible = aState.IsEyeTrackingRequired;
            Items["ETUDCalibrate"].Visible = aState.IsEyeTrackingRequired;
            Items["ETUDSep"].Visible = aState.IsEyeTrackingRequired;
        }
    }
}
