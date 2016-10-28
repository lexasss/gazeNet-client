using System.Windows.Forms;
using GazeNetClient.Utils;

namespace GazeNetClient.Plugins.VNC
{
    public partial class Options : Plugin.OptionsWidget
    {
        public Options()
        {
            InitializeComponent();
        }

        private void UpdateVisibility()
        {
            UI.setGroupEnabling(gpbViewers, new UI.IsConditionMet[] {
                ctrl => {
                    if (lblUVNCNotFound.Visible)
                        return false;
                    if (ctrl == chkViewersEnabled) 
                        return true;
                    if (ctrl == btnAdd)
                        return chkViewersEnabled.Checked && CanAddNViewer();
                    if (ctrl == btnRemove)
                        return chkViewersEnabled.Checked && CanDeleteViewer();
                    return chkViewersEnabled.Checked;
                }
            });
        }

        private bool CanAddNViewer()
        {
            return IsValidIP(txbIP.Text) && IsValidName(txbName.Text);
        }

        private bool CanDeleteViewer()
        {
            return lsvViewers.SelectedIndices.Count > 0;
        }

        private bool IsValidIP(string aText)
        {
            if (string.IsNullOrEmpty(aText))
                return false;

            string[] bytes = aText.Split('.');
            if (bytes.Length != 4)
                return false;

            foreach (string b in bytes)
            {
                int value;
                if (!int.TryParse(b, out value))
                    return false;
                if (value < 1 || value > 254)
                    return false;
            }

            foreach (ListViewItem lvi in lsvViewers.Items)
                if (lvi.SubItems[0].Text == aText)
                    return false;

            return true;
        }

        private bool IsValidName(string aText)
        {
            if (string.IsNullOrEmpty(aText))
                return false;

            foreach (ListViewItem lvi in lsvViewers.Items)
                if (lvi.SubItems[1].Text == aText)
                    return false;

            return true;
        }

        private void txbUVNCInstallationFolder_TextChanged(object sender, System.EventArgs e)
        {
            lblUVNCNotFound.Visible = !Config.isVNCInstalledInto(txbUVNCInstallationFolder.Text);
            UpdateVisibility();
        }

        private void btnBrowseUVNCFolder_Click(object sender, System.EventArgs e)
        {
            fbdFolderBrowser.SelectedPath = txbUVNCInstallationFolder.Text;
            if (fbdFolderBrowser.ShowDialog() == DialogResult.OK)
                txbUVNCInstallationFolder.Text = fbdFolderBrowser.SelectedPath + @"\";
        }

        private void chkServer_CheckedChanged(object sender, System.EventArgs e)
        {
            UpdateVisibility();
        }

        private void chkViewersEnabled_CheckedChanged(object sender, System.EventArgs e)
        {
            UpdateVisibility();
        }

        private void lsvViewers_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            btnRemove.Enabled = CanDeleteViewer();
        }

        private void NewViewerData_TextChanged(object sender, System.EventArgs e)
        {
            btnAdd.Enabled = CanAddNViewer();
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            ListViewItem lvi = new ListViewItem(new string[] { txbIP.Text, txbName.Text });
            lsvViewers.Items.Add(lvi);
            txbIP.Text = "";
            txbName.Text = "";
        }

        private void btnRemove_Click(object sender, System.EventArgs e)
        {
            lsvViewers.Items.RemoveAt(lsvViewers.SelectedIndices[0]);
        }
    }
}
