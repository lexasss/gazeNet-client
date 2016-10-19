using System;
using System.Windows.Forms;

namespace GazeNetClient.Plugins.Scaler
{
    public partial class Options : Plugin.OptionsWidget
    {
        public class Range
        {
            private NumericUpDown iFrom;
            private NumericUpDown iTo;
            private bool iIsSettingTheRange = false;

            public int From
            {
                get { return (int)iFrom.Value; }
                set { iFrom.Value = value; }
            }
            public int To
            {
                get { return (int)iTo.Value; }
                set { iTo.Value = value; }
            }
            public int Value { get { return (int)(iTo.Value - iFrom.Value); } }

            public Range(NumericUpDown aFrom, NumericUpDown aTo)
            {
                iFrom = aFrom;
                iTo = aTo;

                iFrom.ValueChanged += From_ValueChanged;
                iTo.ValueChanged += To_ValueChanged;
            }

            public void set(int aFrom, int aTo)
            {
                iIsSettingTheRange = true;

                iFrom.Value = aFrom < aTo ? aFrom : aTo;
                iTo.Value = aTo > aFrom ? aTo : aFrom;

                iIsSettingTheRange = false;

                From_ValueChanged(null, null);
                To_ValueChanged(null, null);
            }

            private void From_ValueChanged(object sender, EventArgs e)
            {
                if (!iIsSettingTheRange)
                    iTo.Minimum = iFrom.Value;
            }

            private void To_ValueChanged(object sender, EventArgs e)
            {
                if (!iIsSettingTheRange)
                    iFrom.Maximum = iTo.Value;
            }
        }

        public Range OwnHorizontalRange { get; }
        public Range OwnVerticalRange { get; }
        public Range ReceivedHorizontalRange { get; }
        public Range ReceivedVerticalRange { get; }

        public Options()
        {
            InitializeComponent();

            OwnHorizontalRange = new Range(nudOwnLeft, nudOwnRight);
            OwnVerticalRange = new Range(nudOwnTop, nudOwnBottom);
            ReceivedHorizontalRange = new Range(nudReceivedLeft, nudReceivedRight);
            ReceivedVerticalRange = new Range(nudReceivedTop, nudReceivedBottom);
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
