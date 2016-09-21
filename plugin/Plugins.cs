using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plugin
{
    public class Plugins : Plugin
    {
        public Dictionary<string, EventHandler> MenuItems { get { return null; } }
        public Plugin[] Items { get; private set; }
        public event EventHandler<string> Log = delegate { };
        public event EventHandler<Request> Req = delegate { };

        public Plugins(Plugin[] aItems)
        {
            Items = aItems;
        }

        public void feed(float aX, float aY)
        {
            foreach (Plugin plugin in Items)
                plugin.feed(aX, aY);
        }

        public void finilize()
        {
            foreach (Plugin plugin in Items)
                plugin.finilize();
        }

        public void start()
        {
            foreach (Plugin plugin in Items)
                plugin.start();
        }
    }
}
