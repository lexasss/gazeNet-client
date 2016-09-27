using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GazeNetClient.Plugins.OinQs
{
    public partial class Display : Form
    {
        private List<Control> iItems = new List<Control>();

        public event EventHandler OnFound = delegate { };
        public event EventHandler OnStopped = delegate { };
        public event EventHandler OnRequestExit = delegate { };

        public bool IsDisplayingItems { get; set; } = false;

        public Display()
        {
            InitializeComponent();
        }

        public void clear()
        {
            lblInstruction.Hide();

            foreach (Control item in iItems)
                Controls.Remove(item);

            iItems.Clear();
            IsDisplayingItems = false;
        }

        public void addItem(Control aItem)
        {
            iItems.Add(aItem);
            Controls.Add(aItem);
        }

        public void showItems()
        {
            lblInstruction.Hide();

            foreach (Control ctrl in iItems)
                ctrl.Visible = true;

            IsDisplayingItems = true;

            BringToFront();
            Activate();
        }

        public void showInstruction(Instruction aInstruction)
        {
            lblInstruction.Text = aInstruction.text;
            lblInstruction.Show();

            if (aInstruction.time > 0)
            {
                tmrInstructionHide.Interval = aInstruction.time;
                tmrInstructionHide.Start();
            }
        }

        private void Display_KeyDown(object sender, KeyEventArgs e)
        {
            if (!IsDisplayingItems && e.KeyCode == Keys.Escape)
            {
                OnRequestExit(this, new EventArgs());
                return;
            }

            if (e.KeyCode == Keys.Enter)
            {
                OnFound(this, new EventArgs());
            }
            else if (e.KeyCode == Keys.Space)
            {
                OnStopped(this, new EventArgs());
            }
        }

        private void tmrInstructionHide_Tick(object sender, EventArgs e)
        {
            tmrInstructionHide.Stop();
            lblInstruction.Hide();
        }
    }
}
