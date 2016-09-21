using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace OinQs
{
    public class OinQs : Plugin.Plugin
    {
        private Display iDisplay = new Display();

        public Dictionary<string, EventHandler> MenuItems { get { return null; } }

        public event EventHandler<string> Log = delegate { };
        public event EventHandler<Plugin.Request> Req = delegate { };

        public OinQs()
        {
            iDisplay.Visible = false;
        }

        public void feed(float aX, float aY)
        {
            
        }

        public void finilize()
        {
            iDisplay.Hide();
        }

        public void start()
        {
            iDisplay.Controls.Clear();

            Letter[] letters = Layout.create(Screen.PrimaryScreen.Bounds.Size, 10);
            iDisplay.Controls.AddRange(letters);

            iDisplay.Show();

            var t = new System.Windows.Forms.Timer();
            t.Interval = 3000;
            t.Tick += new EventHandler((s, e) => {
                t.Stop();
                Req(this, Plugin.Request.Stop);
            });
            t.Start();
        }
    }
}
