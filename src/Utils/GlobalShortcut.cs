using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace GazeNetClient.Utils
{
    public class GlobalShortcut
    {
        #region Internal members

        private static WinAPI.LowLevelKeyboardProc iProc = HookCallback;
        private static IntPtr iHookID = IntPtr.Zero;
        private static List<Shortcut> iShortcuts = new List<Shortcut>();
        private static List<int> iPressedKeys = new List<int>();

        #endregion

        #region Public methods

        public static void init()
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                iHookID = WinAPI.SetWindowsHookEx(WinAPI.WH.KEYBOARD_LL, iProc,
                    WinAPI.GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        public static void close()
        {
            WinAPI.UnhookWindowsHookEx(iHookID);
        }

        public static void add(Shortcut aShortcut)
        {
            lock (iShortcuts)
            {
                iShortcuts.Add(aShortcut);
            }
        }

        public static void clear(string aGroup = "")
        {
            lock (iShortcuts)
            {
                if (String.IsNullOrEmpty(aGroup))
                {
                    iShortcuts.Clear();
                }
                else
                {
                    List<Shortcut> shortcuts = new List<Shortcut>();
                    foreach (Shortcut shortcut in iShortcuts)
                    {
                        if (shortcut.Group != aGroup)
                        {
                            shortcuts.Add(shortcut);
                        }
                    }
                    iShortcuts = shortcuts;
                }
            }
        }

        #endregion

        #region Internal methods

        private GlobalShortcut() { }

        private static IntPtr HookCallback(int aCode, IntPtr aWParam, IntPtr aLParam)
        {
            if (aCode >= 0)
            {
                bool isKeyDown = (uint)aWParam == WinAPI.WM.KEYDOWN;
                bool isKeyUp = (uint)aWParam == WinAPI.WM.KEYUP;
                if (isKeyDown || isKeyUp)
                {
                    int vkCode = Marshal.ReadInt32(aLParam);

                    bool canContinue = isKeyUp || (isKeyDown && !iPressedKeys.Contains(vkCode));
                    if (canContinue)
                    {
                        if (isKeyDown)
                            iPressedKeys.Add(vkCode);
                        else
                            iPressedKeys.Remove(vkCode);

                        lock (iShortcuts)
                        {
                            foreach (Shortcut shortcut in iShortcuts)
                            {
                                if (shortcut.isPressed(vkCode, (uint)aWParam == WinAPI.WM.KEYUP))
                                {
                                    shortcut.Callback();
                                }
                            }
                        }
                    }
                }
            }
            return WinAPI.CallNextHookEx(iHookID, aCode, aWParam, aLParam);
        }

        #endregion
    }
}