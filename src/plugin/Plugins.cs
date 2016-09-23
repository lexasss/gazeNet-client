using System;
using System.Collections.Generic;

namespace GazeNetClient.Plugin
{
    public class Plugins : IPlugin
    {
        public Dictionary<string, EventHandler> MenuItems { get { return null; } }
        public string Name { get; } = "";
        public IPlugin[] Items { get; private set; }
        public event EventHandler<string> Log = delegate { };
        public event EventHandler<RequestArgs> Req = delegate { };

        public IPlugin this[string aName]
        {
            get
            {
                if (string.IsNullOrEmpty(aName))
                    return this;

                foreach (IPlugin plugin in Items)
                    if (plugin.Name == aName)
                        return plugin;

                return null;
            }
        }

        public Plugins(IPlugin[] aItems)
        {
            Items = aItems;
        }

        public void feed(float aX, float aY)
        {
            foreach (IPlugin plugin in Items)
                plugin.feed(aX, aY);
        }

        public void finilize()
        {
            foreach (IPlugin plugin in Items)
                plugin.finilize();
        }

        public void start()
        {
            foreach (IPlugin plugin in Items)
                plugin.start();
        }

        public void command(string aCommand, string aValue)
        {
            foreach (IPlugin plugin in Items)
                plugin.command(aCommand, aValue);
        }
    }
}
