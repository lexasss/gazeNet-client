using System;
using System.Windows.Forms;

namespace GazeNetClient.Utils
{
    public class Shortcut
    {
        public string Group { get; private set; }
        public Action Callback { get; private set; }
        public Keys Key { get; private set; }
        public Keys[] Modifiers { get; private set; }
        public bool IsOnBothUpDown { get; private set; } = false;

        public Shortcut(string aGroup, Action aCallback, Keys aKey, params Keys[] aModifiers)
        {
            Group = aGroup;
            Callback = aCallback;
            Key = aKey;
            Modifiers = aModifiers;
        }

        public Shortcut(string aGroup, Action aCallback, Keys aKey, bool aIsOnBothUpDown)
        {
            Group = aGroup;
            Callback = aCallback;
            Key = aKey;
            Modifiers = new Keys[] { };
            IsOnBothUpDown = aIsOnBothUpDown;
        }

        public bool isPressed(int aVirtualKey, bool aIsUp)
        {
            if (aIsUp && !IsOnBothUpDown)
                return false;

            if (Key != (Keys)aVirtualKey)
                return false;

            Keys pressedModifiers = Control.ModifierKeys;
            foreach (Keys modifier in Modifiers)
            {
                if (!pressedModifiers.HasFlag(modifier))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
