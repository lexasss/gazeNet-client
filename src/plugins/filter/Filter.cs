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
        public Dictionary<string, EventHandler> MenuItems { get; } = null;
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
