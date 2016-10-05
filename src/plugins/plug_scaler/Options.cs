using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GazeNetClient.Plugins.Scaler
{
    public partial class Options : Plugin.OptionsWidget
    {
        public Options()
        {
            InitializeComponent();

            foreach (string scalingTarget in Enum.GetNames(typeof(ScalingTarget)))
            {
                cmbAppliesTo.Items.Add(scalingTarget.ToLower());
            }
        }
    }
}
