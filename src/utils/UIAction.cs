using System;
using System.Drawing;

namespace GazeNetClient.Utils
{
    [Flags]
    public enum UIActionSet
    {
        Menu = 1,
        Toolbar = 2
    }

    public class UIAction
    {
        private string iText;
        private Image iImage = null;
        private string iAltText = null;
        private Image iAltImage = null;

        public string Text { get { return UseAltView && !string.IsNullOrEmpty(iAltText) ? iAltText : iText; } }
        public Image Image { get { return UseAltView && iAltImage != null ? iAltImage : iImage; } }
        public Action Action { get; }
        public UIActionSet Set { get; }
        public bool Toggable { get; }

        public bool UseAltView { get; set; } = false;

        public bool Checked { get { return Toggable && UseAltView; } }
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
}
