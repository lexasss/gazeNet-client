using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using GazeNetClient.Utils;

namespace GazeNetClient.Plugins.Scaler
{
    public partial class Options : Plugin.OptionsWidget
    {
        private delegate bool IsConditionMet(Control aControl);

        private List<WinData> iWindows = new List<WinData>();

        private struct WinData
        {
            public readonly IntPtr Handle;
            public readonly string Title;
            public readonly IntPtr Icon;

            public WinData(IntPtr aHandle, string aTitle, IntPtr aIcon)
            {
                Handle = aHandle;
                Title = aTitle;
                Icon = aIcon;
            }

            public override string ToString()
            {
                return Title;
            }
        }

        public Range OwnHorizontalRange { get; }
        public Range OwnVerticalRange { get; }
        public Range ReceivedHorizontalRange { get; }
        public Range ReceivedVerticalRange { get; }

        public string Window
        {
            get { return cmbReceived.SelectedIndex < 1 ? "" : cmbReceived.SelectedItem.ToString(); }
            set
            {
                if (cmbReceived.Items.Count == 0)
                    UpdateWindowsList();

                cmbReceived.SelectedIndex = 0;
                if (string.IsNullOrEmpty(value))
                    return;

                for (int i = 1; i < cmbReceived.Items.Count; i++)
                {
                    string window = cmbReceived.Items[i].ToString();
                    if (window == value)
                    {
                        cmbReceived.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        public IntPtr WindowPtr
        {
            get
            {
                if (cmbReceived.SelectedIndex < 1 || !chkReceived.Checked)
                    return IntPtr.Zero;

                return iWindows[cmbReceived.SelectedIndex - 1].Handle;
            }
        }

        public Options()
        {
            InitializeComponent();

            OwnHorizontalRange = new Range(nudOwnLeft, nudOwnRight);
            OwnVerticalRange = new Range(nudOwnTop, nudOwnBottom);
            ReceivedHorizontalRange = new Range(nudReceivedLeft, nudReceivedRight);
            ReceivedVerticalRange = new Range(nudReceivedTop, nudReceivedBottom);
        }

        public void updateVisibility()
        {
            UpdateWindowsList();
            UpdateVisibility();
        }

        public static IntPtr windowTitleToHandle(string aTitle)
        {
            IntPtr result = IntPtr.Zero;

            List<WinData> windows = EnumTopLevelWindows();
            foreach (WinData window in windows)
            {
                if (window.Title == aTitle)
                {
                    result = window.Handle;
                    break;
                }
            }

            return result;
        }

        private void UpdateVisibility()
        {
            SetGroupEnabling(chkOwn.Parent, new IsConditionMet[] { ctrl => ctrl == chkOwn || chkOwn.Checked });
            SetGroupEnabling(chkReceived.Parent, new IsConditionMet[] {
                ctrl =>
                    ctrl == chkReceived ||
                    (ctrl == cmbReceived && chkReceived.Checked) ||
                    (chkReceived.Checked && cmbReceived.SelectedIndex == 0)
            });
        }

        private void UpdateWindowsList()
        {
            string currentWindow = Window;

            iWindows.Clear();

            cmbReceived.Items.Clear();
            cmbReceived.Items.Add(new WinData(IntPtr.Zero, "fixed area", IntPtr.Zero));

            iWindows = EnumTopLevelWindows();
            foreach (WinData window in iWindows)
            {
                cmbReceived.Items.Add(window);
            }

            Window = currentWindow;
        }

        private void SetGroupEnabling(Control aContainer, IsConditionMet[] aConditions)
        {
            foreach (Control ctrl in aContainer.Controls)
            {
                bool enabled = true;
                foreach (IsConditionMet condition in aConditions)
                    enabled = enabled && condition(ctrl);

                ctrl.Enabled = enabled;
            }
        }

        private void SetReceivedGroupEnabling(CheckBox aCheckBox)
        {
            SetGroupEnabling(chkReceived.Parent, new IsConditionMet[] { ctrl => ctrl == aCheckBox || aCheckBox.Checked });
        }

        private static List<WinData> EnumTopLevelWindows()
        {
            List<WinData> windows = new List<WinData>();
            GCHandle handles = GCHandle.Alloc(windows);

            WinAPI.EnumWindows(HandleWindowEnumeration, (IntPtr)handles);

            return windows;
        }

        private static bool HandleWindowEnumeration(IntPtr hWnd, IntPtr lParam)
        {
            WinAPI.RECT winRect;
            if (WinAPI.GetWindowRect(hWnd, out winRect))
            {
                Rectangle fullScreenRect = Screen.PrimaryScreen.Bounds;
                if (winRect.Left == fullScreenRect.Left && winRect.Right == fullScreenRect.Right &&
                    winRect.Top == fullScreenRect.Top && winRect.Bottom == fullScreenRect.Bottom)
                    return true;
            }

            int size = WinAPI.GetWindowTextLength(hWnd);
            if (size++ > 0 && WinAPI.IsWindowVisible(hWnd) && !WinAPI.IsIconic(hWnd))
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder(size);
                WinAPI.GetWindowText(hWnd, sb, size);
                string title = sb.ToString();

                if (title != "GazeNet")
                {
                    GCHandle handles = (GCHandle)lParam;
                    List<WinData> windows = (handles.Target as List<WinData>);

                    WinAPI.WINDOWINFO wi;
                    if (WinAPI.GetWindowInfo(hWnd, out wi))
                    {
                        IntPtr icon = WinAPI.GetClassLongPtr(hWnd, WinAPI.GCL.HICON);
                        windows.Add(new WinData(hWnd, title, icon));
                    }
                    else
                        windows.Add(new WinData(hWnd, title, IntPtr.Zero));
                }
            }

            return true;
        }

        private void chkGazePoint_CheckedChanged(object aSender, EventArgs aArgs)
        {
            UpdateVisibility();
        }

        private void cmbReceived_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateVisibility();

            IntPtr window = WindowPtr;
            if (WinAPI.IsWindow(window))
            {
                WinAPI.RECT winRect;
                if (WinAPI.GetWindowRect(window, out winRect))
                {
                    ReceivedHorizontalRange.set(winRect.Left, winRect.Right);
                    ReceivedVerticalRange.set(winRect.Top, winRect.Bottom);
                }
            }
        }

        private void cmbReceived_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;

            WinData winData = (WinData)cmbReceived.Items[e.Index];

            e.DrawBackground();

            if (!e.State.HasFlag(DrawItemState.ComboBoxEdit))
            {
                Icon icon;
                if (winData.Icon != IntPtr.Zero)
                    icon = Icon.FromHandle(winData.Icon);
                else
                    icon = Icon.FromHandle(WinAPI.LoadIcon(IntPtr.Zero, (IntPtr)WinAPI.IDI.WINLOGO));
                e.Graphics.DrawIcon(icon, 2, e.Bounds.Top + 2);
            }

            Rectangle rect = e.Bounds;
            rect.Width -= 16;

            if (!e.State.HasFlag(DrawItemState.ComboBoxEdit))
                rect.X += 36;

            e.Graphics.DrawString(winData.Title, e.Font, new SolidBrush(e.ForeColor), rect);
        }

        private void cmbReceived_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index < 0)
                return;

            WinData winData = (WinData)cmbReceived.Items[e.Index];
            SizeF textSize = e.Graphics.MeasureString(winData.Title, cmbReceived.Font, cmbReceived.Width - 36);
            e.ItemHeight = Math.Max((int)textSize.Height, 36);
        }

        private void cmbReceived_DropDown(object sender, EventArgs e)
        {
            UpdateWindowsList();
        }
    }
}
