using System.Drawing;
using System.Windows.Forms;

namespace GazeNetClient.Plugin
{
    public partial class OptionsWidget : UserControl
    {
        public OptionsWidget()
        {
            InitializeComponent();

            if (!this.DesignMode)
                this.Dock = DockStyle.Fill;
        }

        public void makeEmpty()
        {
            Controls.Clear();

            Label lbl = new Label();
            lbl.Text = "No options to display";
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            lbl.Dock = DockStyle.Fill;

            Controls.Add(lbl);
        }
    }
}
