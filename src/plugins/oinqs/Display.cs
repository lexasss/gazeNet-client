using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GazeNetClient.Plugins.OinQs
{
    internal partial class Display : Form
    {
        private List<Control> iItems = new List<Control>();
        private Cursor iEmptyCursor;

        public event EventHandler OnFound = delegate { };
        public event EventHandler OnNotFound = delegate { };
        public event EventHandler OnNext = delegate { };
        public event EventHandler OnRequestExit = delegate { };
        public event EventHandler OnRequestSave = delegate { };

        public PointF StimuliScale { get; set; } = new PointF(1f, 1f);

        public bool IsDisplayingItems { get { return iItems.Count > 0 && iItems[0].Visible; } }
        public bool IsBlank { get { return !(IsDisplayingItems || lblInstruction.Visible); } }

        public Display()
        {
            InitializeComponent();

            iEmptyCursor = new Cursor("EmptyCursor.cur");
        }

        public void clear()
        {
            lblInstruction.Hide();

            foreach (Control item in iItems)
                Controls.Remove(item);

            iItems.Clear();
            Cursor = Cursors.Default;
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

            Cursor = iEmptyCursor;
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
            else
            {
                BringToFront();
                Activate();
            }
        }

        private bool DoesInstructionMentionSpaceKey()
        {
            return lblInstruction.Text.Contains("SPACE");
        }

        private void Display_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    if (!IsDisplayingItems)
                        OnRequestExit(this, new EventArgs());
                    break;
                case Keys.Enter:
                    if (IsDisplayingItems)
                        OnFound(this, new EventArgs());
                    break;
                case Keys.Space:
                    if (IsDisplayingItems)
                        OnNotFound(this, new EventArgs());
                    else if (DoesInstructionMentionSpaceKey())
                        OnNext(this, new EventArgs());
                    break;
                case Keys.S:
                    if (e.Control && !IsDisplayingItems)
                        OnRequestSave(this, new EventArgs());
                    break;
            }
        }

        private void tmrInstructionHide_Tick(object sender, EventArgs e)
        {
            tmrInstructionHide.Stop();
            lblInstruction.Hide();
        }
    }
}
