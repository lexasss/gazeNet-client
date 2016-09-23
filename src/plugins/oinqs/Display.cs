using System;
using System.Windows.Forms;

namespace GazeNetClient.Plugins.OinQs
{
    public partial class Display : Form
    {
        public event EventHandler OnFound = delegate { };
        public event EventHandler OnStopped = delegate { };

        public Display()
        {
            InitializeComponent();
        }

        private void Display_KeyDown(object sender, KeyEventArgs e)
        {
            if (!Enabled)
                return;

            if (e.KeyCode == Keys.Space)
            {
                OnFound(this, new EventArgs());
            }
            else if (e.KeyCode == Keys.Escape)
            {
                OnStopped(this, new EventArgs());
            }
        }
    }
}
