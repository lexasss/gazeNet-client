namespace GazeNetClient.Plugins.Filter
{
    public partial class Options : Plugin.OptionsWidget
    {
        public Options()
        {
            InitializeComponent();

            cmbAction.Items.AddRange(typeof(Action).GetEnumNames());
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            lsbNames.Items.Add(txbName.Text);
        }

        private void btnRemove_Click(object sender, System.EventArgs e)
        {
            lsbNames.Items.RemoveAt(lsbNames.SelectedIndex);
        }

        private void lsbNames_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            btnRemove.Enabled = lsbNames.SelectedIndex >= 0;
        }

        private void txbName_TextChanged(object sender, System.EventArgs e)
        {
            btnAdd.Enabled = txbName.Text.Length > 0;
        }
    }
}
