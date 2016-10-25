using System;
using System.Windows.Forms;

namespace GazeNetClient.Plugins.Scaler
{
    public class Range
    {
        private static int MAX_VALUE = 7000;

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

            iFrom.Minimum = -MAX_VALUE;
            iFrom.Maximum = MAX_VALUE;
            iFrom.Value = aFrom < aTo ? aFrom : aTo;

            iTo.Minimum = -MAX_VALUE;
            iTo.Maximum = MAX_VALUE;
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
}
