using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GazeNetClient.Plugin
{
    public class Plugins
    {
        private List<IPlugin> iItems = new List<IPlugin>(); 

        public IPlugin[] Items { get { return iItems.ToArray(); } }

        public IPlugin this[string aName]
        {
            get
            {
                if (string.IsNullOrEmpty(aName))
                    return null;

                foreach (IPlugin plugin in iItems)
                    if (plugin.Name == aName)
                        return plugin;

                return null;
            }
        }

        /// <summary>
        /// Loads plugins ([NAMESPACE].Plugins.*.dll) from the specified folder
        /// </summary>
        /// <param name="aFolder">The folder to load plugins from. Can be absolute, or relative to the 
        /// folder of the currently executing application (default). </param>
        /// <returns></returns>
        public static Plugins load(string aFolder = "")
        {
            Plugins result = new Plugins();

            string folder;
            if (aFolder != null && aFolder.Length > 1 && aFolder[1] == Path.VolumeSeparatorChar)
            {
                folder = aFolder;
            }
            else
            {
                folder = System.Windows.Forms.Application.StartupPath;
                if (!string.IsNullOrEmpty(aFolder))
                {
                    if (aFolder[0] != Path.DirectorySeparatorChar)
                        folder += Path.DirectorySeparatorChar;
                    folder += aFolder;
                }
            }

            string pluginFileNamePattern = string.Format("{0}s.*.dll", typeof(Plugins).Namespace);
            string[] fileNames = Directory.GetFiles(folder, pluginFileNamePattern);

            foreach (string pluginFileName in fileNames)
            {
                string[] typeParts = Path.GetFileNameWithoutExtension(pluginFileName).Split('.');
                string plugintTypeName = string.Join(".", typeParts) + "." + typeParts.Last<string>();
                Type pluginType = Type.GetType(plugintTypeName);
                if (pluginType == null)
                {
                    Assembly pluginAssembly = Assembly.LoadFrom(pluginFileName);
                    pluginType = pluginAssembly.GetType(plugintTypeName);
                }

                if (pluginType != null)
                {
                    result.iItems.Add(Activator.CreateInstance(pluginType) as IPlugin);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Cannot load the plugin {0}", plugintTypeName);
                }
            }

            return result;
        }

        public Plugins(IPlugin[] aItems = null)
        {
            if (aItems != null)
                foreach (IPlugin plugin in aItems)
                    iItems.Add(plugin);
       }

        public void start()
        {
            foreach (IPlugin plugin in iItems)
                if (plugin.Enabled)
                    plugin.start();
        }

        public void finilize()
        {
            foreach (IPlugin plugin in iItems)
                if (plugin.Enabled)
                    plugin.finilize();
        }

        public void enableMenuItems(bool aEnabled)
        {
            foreach (IPlugin plugin in iItems)
                if (plugin.MenuItems != null)
                    foreach (Utils.UIAction actionItem in plugin.MenuItems.Values)
                        actionItem.Enabled = aEnabled;
        }

        public Processor.GazePoint feedOwnPoint(Processor.GazePoint aSample)
        {
            Processor.GazePoint gp = aSample;
            foreach (IPlugin plugin in iItems)
                if (plugin.Enabled)
                    gp = plugin.feedOwnPoint(gp);
            return gp;
        }

        public bool feedReceivedPoint(string aFrom, ref PointF aPoint)
        {
            bool result = true;
            foreach (IPlugin plugin in iItems)
                if (plugin.Enabled)
                    if (!plugin.feedReceivedPoint(aFrom, ref aPoint))
                    {
                        result = false;
                        break;
                    }
            return result;
        }

        public void command(string aCommand, string aValue)
        {
            foreach (IPlugin plugin in iItems)
                if (plugin.Enabled)
                    plugin.command(aCommand, aValue);
        }

        public void showOptions()
        {
            Options options = new Options();
            options.list(Items);
            if (options.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                options.save();
            }
        }
    }
}
