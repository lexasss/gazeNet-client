using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin
{
    public enum Request
    {
        Stop
    }

    public interface Plugin
    {
        Dictionary<string, EventHandler> MenuItems { get; }

        event EventHandler<string> Log;
        event EventHandler<Request> Req;

        void start();
        void finilize();
        void feed(float aX, float aY);
    }
}
