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
            foreach (KeyValuePair<string, UIAction> actionItem in iActions.Items)
            {
                UIAction action = actionItem.Value;
                if (!action.Set.HasFlag(UIActionSet.Menu))
                    continue;

                ToolStripItem item = action.Action != null ? 
                    (ToolStripItem)new ToolStripMenuItem(action.Text) :
                    (ToolStripItem)new ToolStripSeparator();

                item.Tag = action;
                item.Click += new EventHandler((s, e) => action?.Action());

                cmsMenu.Items.Add(item);
            }
        }

        public void update()
        {
            foreach (ToolStripItem item in cmsMenu.Items)
            {
                UIAction action = (UIAction)item.Tag;
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
                IDictionary<string, EventHandler> items = plugin.MenuItems;
                if (items?.Count > 0)
                {
                    cmsMenu.Items.Insert(0, new ToolStripSeparator());
                    int index = 0;
                    foreach (KeyValuePair<string, EventHandler> k in items)
                    {
                        ToolStripMenuItem item = new ToolStripMenuItem(k.Key);
                        item.Click += k.Value;
                        cmsMenu.Items.Insert(index++, item);
                    }
                }
            }
        }

        #endregion
    }
}
