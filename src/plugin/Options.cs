using System;
using System.Windows.Forms;

namespace GazeNetClient.Plugin
{
    public partial class Options : Form
    {
        private bool iIsCheckControlAllowed = false;
         
        public Options()
        {
            InitializeComponent();
        }

        public void list(IPlugin[] aItems)
        {
            foreach (IPlugin plugin in aItems)
            {
                ListViewItem lvi = new ListViewItem(plugin.DisplayName);
                lvi.Checked = plugin.Enabled;
                lvi.Group = plugin.IsExclusive ? lsvPlugins.Groups["exclusive"] : lsvPlugins.Groups["inclusive"];
                lvi.Tag = plugin;
                lsvPlugins.Items.Add(lvi);
                plugin.displayOptions();
            }
        }

        public void save()
        {
            foreach (ListViewItem lvi in lsvPlugins.Items)
            {
                IPlugin plugin = (IPlugin)lvi.Tag;
                plugin.Enabled = lvi.Checked;
                plugin.acceptOptions();
            }
        }

        private void lsvPlugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            gpbPluginOptionsContainer.Controls.Clear();
            gpbPluginOptionsContainer.Text = "Select a plugin to display its options";

            ListView lsv = sender as ListView;
            if (lsv.SelectedItems.Count > 0)
            {
                IPlugin plugin = (IPlugin)lsv.SelectedItems[0].Tag;
                if (plugin != null)
                {
                    gpbPluginOptionsContainer.Controls.Add(plugin.Options);
                    gpbPluginOptionsContainer.Text = plugin.DisplayName;
                }
            }
        }

        private void lsvPlugins_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (!iIsCheckControlAllowed)
                return;

            iIsCheckControlAllowed = false;

            ListViewGroup group = e.Item.Group;
            if (e.Item.Checked && group == lsvPlugins.Groups["exclusive"])
            {
                foreach (ListViewItem lvi in group.Items)
                {
                    if (lvi != e.Item)
                        lvi.Checked = false;
                }
            }

            if (e.Item.Checked)
                e.Item.Selected = true;

            iIsCheckControlAllowed = true;
        }

        private void Options_Shown(object sender, EventArgs e)
        {
            iIsCheckControlAllowed = true;
        }
    }
}
