using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GazeNetClient.Plugin;

namespace GazeNetClient
{
    internal partial class Toolbar : Form
    {
        private readonly string SIGN_MINIMIZE = "__";
        private readonly string SIGN_HIDE = "\u2335";
        private readonly string SIGN_CLOSE = "\u2715";

        private readonly string SETTINGS_NAME = "toolbar";

        private GazeNetClient iClient;
        private UIActions iActions;

        public Toolbar(GazeNetClient aClient, UIActions aActions)
        {
            InitializeComponent();

            iClient = aClient;
            iActions = aActions;

            foreach (KeyValuePair<string, Utils.UIAction> actionItem in iActions.Items)
            {
                AddItem(actionItem.Value);
            }

            lblMinimize.Text = SIGN_MINIMIZE;
            lblHideToTray.Text = SIGN_HIDE;
            lblClose.Text = SIGN_CLOSE;

            UpdateSize();
        }

        public void update()
        {
            foreach (ToolStripItem item in tstToolbar.Items)
            {
                Utils.UIAction action = (Utils.UIAction)item.Tag;
                //item.Text = action.Text;
                item.Image = action.Image;
                item.Enabled = action.Enabled;
                item.Visible = action.Visible;
                ToolStripButton button = item as ToolStripButton;
                if (button != null)
                {
                    button.Checked = action.Checked;
                    button.BackColor = button.Checked ? Color.Red : Color.Transparent;
                }
            }

            UpdateSize();
            Invalidate();
        }

        public void addPlugins(IPlugin[] aPlugins)
        {
            foreach (IPlugin plugin in aPlugins)
            {
                IDictionary<string, Utils.UIAction> items = plugin.MenuItems;
                if (items?.Count > 0)
                {
                    AddItem(new Utils.UIAction("-"));
                    foreach (KeyValuePair<string, Utils.UIAction> actionItem in items)
                    {
                        AddItem(actionItem.Value);
                    }
                }
            }

            UpdateSize();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == Utils.WinAPI.WM.NCHITTEST)
                m.Result = (IntPtr)(Utils.WinAPI.HT.CAPTION);
        }

        protected override Size SizeFromClientSize(Size clientSize)
        {
            Size result = base.SizeFromClientSize(clientSize);
            result.Width += 5;
            result.Height += 5;
            return result;
        }

        private void AddItem(Utils.UIAction aAction)
        {
            if (!aAction.Set.HasFlag(Utils.UIActionSet.Toolbar))
                return;

            ToolStripItem item;
            if (aAction.Action != null)
            {
                item = aAction.Toggable ?
                    (ToolStripItem)new ToolStripButton(aAction.Image) :
                    (ToolStripItem)new ToolStripMenuItem(aAction.Image);
                item.Click += new EventHandler((s, e) => aAction.Action());
            }
            else
            {
                ToolStripSeparator separator = new ToolStripSeparator();
                separator.AutoSize = true;
                item = separator;
            }

            item.ToolTipText = aAction.Text;
            item.Tag = aAction;

            tstToolbar.Items.Add(item);
        }

        private void UpdateSize()
        {
            tstToolbar.PerformLayout();
            Width = 2 * tstToolbar.Left + tstToolbar.Width;
            Height = tstToolbar.Top + tstToolbar.Height + tstToolbar.Left;
        }

        private void Toolbar_Shown(object sender, EventArgs e)
        {
            Location = Utils.Storage<Point>.load(SETTINGS_NAME);
        }

        private void Toolbar_FormClosed(object sender, FormClosedEventArgs e)
        {
            Utils.Storage<Point>.save(Location, SETTINGS_NAME);
        }

        private void Toolbar_Paint(object sender, PaintEventArgs e)
        {
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), new Rectangle(3, 3, Width - 6, Font.Height), format);
        }

        private void Toolbar_Activated(object sender, EventArgs e)
        {
            this.Opacity = 1.0;
        }

        private void Toolbar_Deactivate(object sender, EventArgs e)
        {
            this.Opacity = 0.7;
        }

        private void WindowControl_MouseHover(object sender, EventArgs e)
        {
            Control ctrl = sender as Control;
            ctrl.BackColor = ctrl == lblClose ? Color.FromArgb(255,90,90) : SystemColors.MenuHighlight;
            ctrl.ForeColor = Color.Black;
        }

        private void WindowControl_MouseLeave(object sender, EventArgs e)
        {
            Control ctrl = sender as Control;
            ctrl.BackColor = this.BackColor;
            ctrl.ForeColor = Color.LightGray;
        }

        private void lblMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void lblHideToTray_Click(object sender, EventArgs e)
        {
            iClient.hideToolbarToTray();
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            iClient.exit();
        }
    }
}
