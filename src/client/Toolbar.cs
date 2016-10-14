using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GazeNetClient.Plugin;

namespace GazeNetClient
{
    internal partial class Toolbar : Form
    {
        private UIActions iActions;

        public Toolbar(UIActions aActions)
        {
            InitializeComponent();

            iActions = aActions;
            foreach (KeyValuePair<string, UIAction> actionItem in iActions.Items)
            {
                UIAction action = actionItem.Value;
                if (!action.Set.HasFlag(UIActionSet.Toolbar))
                    continue;

                ToolStripItem item = action.Action != null ?
                    (action.Toggable ?
                        (ToolStripItem)new ToolStripButton(action.Image) :
                        (ToolStripItem)new ToolStripMenuItem(action.Image)) :
                    (ToolStripItem)new ToolStripSeparator();

                item.ToolTipText = action.Text;
                item.Tag = action;
                item.Click += new EventHandler((s, e) => action?.Action());

                tstToolbar.Items.Add(item);
            }
        }

        public void update()
        {
            foreach (ToolStripItem item in tstToolbar.Items)
            {
                UIAction action = (UIAction)item.Tag;
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

            Invalidate();
        }

        public void addPlugins(IPlugin[] aPlugins)
        {
            foreach (IPlugin plugin in aPlugins)
            {
                IDictionary<string, EventHandler> items = plugin.MenuItems;
                if (items?.Count > 0)
                {
                    tstToolbar.Items.Insert(0, new ToolStripSeparator());
                    int index = 0;
                    foreach (KeyValuePair<string, EventHandler> k in items)
                    {
                        ToolStripMenuItem item = new ToolStripMenuItem(k.Key);
                        item.Click += k.Value;
                        tstToolbar.Items.Insert(index++, item);
                    }
                }
            }
        }

        private const int WM_NCHITTEST = 0x84;
        private const int HT_CAPTION = 0x2;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }

        protected override Size SizeFromClientSize(Size clientSize)
        {
            Size result = base.SizeFromClientSize(clientSize);
            result.Width += 5;
            result.Height += 5;
            return result;
        }

        private void Toolbar_Paint(object sender, PaintEventArgs e)
        {
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), new Rectangle(3, 3, Width - 6, Font.Height), format);
        }
    }
}
