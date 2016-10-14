using System;
using System.Windows.Forms;

namespace GazeNetClient.Plugins.Scaler
{
    public partial class Options : Plugin.OptionsWidget
    {
        public Options()
        {
            InitializeComponent();
        }

        public void updateVisibility()
        {
            SetGroupEnabling(chkOwn);
            SetGroupEnabling(chkReceived);
        }

        private void SetGroupEnabling(CheckBox aCheckBox)
        {
            foreach (Control ctrl in aCheckBox.Parent.Controls)
            {
                if (ctrl != aCheckBox)
                {
                    ctrl.Enabled = aCheckBox.Checked;
                }
            }
        }

        private void chkGazePoint_CheckedChanged(object aSender, EventArgs aArgs)
        {
            SetGroupEnabling((CheckBox)aSender);
        }
    }
}
