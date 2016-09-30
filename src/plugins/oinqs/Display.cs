using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GazeNetClient.Plugins.OinQs
{
    public partial class Display : Form
    {
        private List<Control> iItems = new List<Control>();

        public event EventHandler OnFound = delegate { };
        public event EventHandler OnNotFound = delegate { };
        public event EventHandler OnNext = delegate { };
        public event EventHandler OnRequestExit = delegate { };

        public PointF StimuliScale { get; set; } = new PointF(1f, 1f);

        public bool IsDisplayingItems { get { return iItems.Count > 0 && iItems[0].Visible; } }
        public bool IsBlank { get { return !(IsDisplayingItems || lblInstruction.Visible); } }

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

        private bool DoesInstructionMentionSpaceKey()
        {
            return lblInstruction.Text.Contains("SPACE");
        }

        private void Display_KeyDown(object sender, KeyEventArgs e)
        {
            if (!IsDisplayingItems && e.KeyCode == Keys.Escape)
            {
                OnRequestExit(this, new EventArgs());
            }
            else if (IsDisplayingItems && e.KeyCode == Keys.Enter)
            {
                OnFound(this, new EventArgs());
            }
            else if (e.KeyCode == Keys.Space)
            {
                if (IsDisplayingItems)
                    OnNotFound(this, new EventArgs());
                else if (DoesInstructionMentionSpaceKey())
                    OnNext(this, new EventArgs());
            }
        }

        private void tmrInstructionHide_Tick(object sender, EventArgs e)
        {
            tmrInstructionHide.Stop();
            lblInstruction.Hide();
        }
    }
}
