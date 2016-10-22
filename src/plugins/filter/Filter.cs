using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GazeNetClient.Plugin;

namespace GazeNetClient.Plugins.Filter
{
    public partial class Filter : IPlugin
    {
        private Config iConfig;
        private Options iOptions;
        private Rectangle iDisplaySize;

        public string Name { get; } = "filter";
        public string DisplayName { get; } = "Name filter";
        public bool IsExclusive { get; } = false;
        public Dictionary<string, Utils.UIAction> MenuItems { get; } = null; // new Dictionary<string, Utils.UIAction>();
        public OptionsWidget Options { get { return iOptions; } }

        public bool Enabled
        {
            get { return iConfig.Enabled; }
            set { iConfig.Enabled = value; }
        }

        public event EventHandler<string> Log = delegate { };
        public event EventHandler<RequestArgs> Req = delegate { };

        public Filter()
        {
            iConfig = Utils.Storage<Config>.load();

            iOptions = new Options();
            /*
            Bitmap bmp1 = new Bitmap(24, 24);
            Graphics g1 = Graphics.FromImage(bmp1);
            g1.FillRectangle(Brushes.Azure, new RectangleF(new Point(0, 0), bmp1.Size));

            Bitmap bmp2 = new Bitmap(24, 24);
            Graphics g2 = Graphics.FromImage(bmp2);
            g2.FillRectangle(Brushes.BurlyWood, new RectangleF(new Point(0, 0), bmp2.Size));

            MenuItems.Add("1", new Utils.UIAction("Filter item 1", new System.Action(() => { MessageBox.Show("Hello 1"); }), bmp1));
            MenuItems.Add("2", new Utils.UIAction("Filter item 2", new System.Action(() => { MessageBox.Show("Hello 2"); }), bmp2));
            */
        }

        ~Filter()
        {
            Utils.Storage<Config>.save(iConfig);
        }

        public void command(string aCommand, string aValue) { }

        public void start()
        {
            iDisplaySize = Screen.PrimaryScreen.Bounds;
        }

        public void finilize() { }

        public void displayOptions()
        {
            iOptions.lsbNames.Items.Clear();
            iOptions.lsbNames.Items.AddRange(iConfig.Names.ToArray());

            iOptions.cmbAction.SelectedIndex = (int)iConfig.Action;
        }

        public void acceptOptions()
        {
            iConfig.Names.Clear();
            foreach (object item in iOptions.lsbNames.Items)
            {
                iConfig.Names.Add((string)item);
            }
            iConfig.Action = (Action)iOptions.cmbAction.SelectedIndex;
        }

        public Processor.GazePoint feedOwnPoint(Processor.GazePoint aSample)
        {
            return aSample;
        }

        public bool feedReceivedPoint(string aFrom, ref PointF aPoint)
        {
            if (iConfig.Action == Action.Pass)
                return iConfig.Names.Contains(aFrom);
            else if (iConfig.Action == Action.Block)
                return !iConfig.Names.Contains(aFrom);
            else
                throw new NotImplementedException();
        }
    }
}
