using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GazeNetClient.Plugin;

namespace GazeNetClient
{
    internal class Menu
    {
        #region Internal members

        private ContextMenuStrip cmsMenu;
        private UIActions iActions;

        #endregion

        #region Properties

        public ContextMenuStrip Strip { get { return cmsMenu; } }

        #endregion

        #region Public methods

        public Menu(UIActions aActions)
        {
            cmsMenu = new ContextMenuStrip();

            iActions = aActions;
            foreach (KeyValuePair<string, Utils.UIAction> actionItem in iActions.Items)
            {
                AddItem(actionItem.Value);
            }
        }

        public void update()
        {
            foreach (ToolStripItem item in cmsMenu.Items)
            {
                Utils.UIAction action = (Utils.UIAction)item.Tag;
                item.Text = action.Text;
                //item.Image = action.Image;
                item.Enabled = action.Enabled;
                item.Visible = action.Visible;
                ToolStripMenuItem menuItem = item as ToolStripMenuItem;
                if (menuItem != null)
                {
                    menuItem.Checked = action.Checked;
                }
            }
        }

        public void addPlugins(IPlugin[] aPlugins)
        {
            foreach (IPlugin plugin in aPlugins)
            {
                IDictionary<string, Utils.UIAction> items = plugin.MenuItems;
                if (items?.Count > 0)
                {
                    int index = 0;
                    AddItem(new Utils.UIAction("-"), index);
                    foreach (KeyValuePair<string, Utils.UIAction> actionItem in items)
                    {
                        AddItem(actionItem.Value, index++);
                    }
                }
            }
        }

        private void AddItem(Utils.UIAction aAction, int aIndex = -1)
        {
            if (!aAction.Set.HasFlag(Utils.UIActionSet.Menu))
                return;

            ToolStripItem item = aAction.Action != null ?
                (ToolStripItem)new ToolStripMenuItem(aAction.Text) :
                (ToolStripItem)new ToolStripSeparator();

            item.Tag = aAction;
            item.Click += new EventHandler((s, e) => aAction?.Action());

            if (aIndex < 0)
                cmsMenu.Items.Add(item);
            else
                cmsMenu.Items.Insert(aIndex, item);
        }

        #endregion
    }
}
