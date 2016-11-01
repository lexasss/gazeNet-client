namespace GazeNetClient.Plugins.VNC
{
    partial class Options
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lsvViewers = new System.Windows.Forms.ListView();
            this.colIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txbName = new System.Windows.Forms.TextBox();
            this.txbIP = new System.Windows.Forms.TextBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.gpbViewers = new System.Windows.Forms.GroupBox();
            this.chkLaunchViewerOnStart = new System.Windows.Forms.CheckBox();
            this.chkViewOnly = new System.Windows.Forms.CheckBox();
            this.chkViewersEnabled = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txbUVNCInstallationFolder = new System.Windows.Forms.TextBox();
            this.lblUVNCNotFound = new System.Windows.Forms.Label();
            this.btnBrowseUVNCFolder = new System.Windows.Forms.Button();
            this.fbdFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.chkServerEnabled = new System.Windows.Forms.CheckBox();
            this.gpbViewers.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(131, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Name";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "IP";
            // 
            // lsvViewers
            // 
            this.lsvViewers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsvViewers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colIP,
            this.colName});
            this.lsvViewers.FullRowSelect = true;
            this.lsvViewers.Location = new System.Drawing.Point(6, 19);
            this.lsvViewers.MultiSelect = false;
            this.lsvViewers.Name = "lsvViewers";
            this.lsvViewers.ShowGroups = false;
            this.lsvViewers.Size = new System.Drawing.Size(249, 133);
            this.lsvViewers.TabIndex = 9;
            this.lsvViewers.UseCompatibleStateImageBehavior = false;
            this.lsvViewers.View = System.Windows.Forms.View.Details;
            this.lsvViewers.SelectedIndexChanged += new System.EventHandler(this.lsvViewers_SelectedIndexChanged);
            // 
            // colIP
            // 
            this.colIP.Text = "IP";
            this.colIP.Width = 114;
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 108;
            // 
            // txbName
            // 
            this.txbName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txbName.Location = new System.Drawing.Point(172, 158);
            this.txbName.Name = "txbName";
            this.txbName.Size = new System.Drawing.Size(83, 20);
            this.txbName.TabIndex = 1;
            this.txbName.TextChanged += new System.EventHandler(this.NewViewerData_TextChanged);
            // 
            // txbIP
            // 
            this.txbIP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txbIP.Location = new System.Drawing.Point(23, 158);
            this.txbIP.Name = "txbIP";
            this.txbIP.Size = new System.Drawing.Size(102, 20);
            this.txbIP.TabIndex = 0;
            this.txbIP.TextChanged += new System.EventHandler(this.NewViewerData_TextChanged);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemove.Enabled = false;
            this.btnRemove.Location = new System.Drawing.Point(131, 184);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(124, 23);
            this.btnRemove.TabIndex = 3;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.Enabled = false;
            this.btnAdd.Location = new System.Drawing.Point(3, 184);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(122, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // gpbViewers
            // 
            this.gpbViewers.Controls.Add(this.label5);
            this.gpbViewers.Controls.Add(this.chkLaunchViewerOnStart);
            this.gpbViewers.Controls.Add(this.label4);
            this.gpbViewers.Controls.Add(this.lsvViewers);
            this.gpbViewers.Controls.Add(this.txbName);
            this.gpbViewers.Controls.Add(this.txbIP);
            this.gpbViewers.Controls.Add(this.chkViewOnly);
            this.gpbViewers.Controls.Add(this.btnRemove);
            this.gpbViewers.Controls.Add(this.btnAdd);
            this.gpbViewers.Controls.Add(this.chkViewersEnabled);
            this.gpbViewers.Location = new System.Drawing.Point(0, 67);
            this.gpbViewers.Name = "gpbViewers";
            this.gpbViewers.Size = new System.Drawing.Size(258, 259);
            this.gpbViewers.TabIndex = 5;
            this.gpbViewers.TabStop = false;
            // 
            // chkLaunchViewerOnStart
            // 
            this.chkLaunchViewerOnStart.AutoSize = true;
            this.chkLaunchViewerOnStart.Location = new System.Drawing.Point(6, 236);
            this.chkLaunchViewerOnStart.Name = "chkLaunchViewerOnStart";
            this.chkLaunchViewerOnStart.Size = new System.Drawing.Size(214, 17);
            this.chkLaunchViewerOnStart.TabIndex = 5;
            this.chkLaunchViewerOnStart.Text = "launch the viewrs upon connection only";
            this.chkLaunchViewerOnStart.UseVisualStyleBackColor = true;
            // 
            // chkViewOnly
            // 
            this.chkViewOnly.AutoSize = true;
            this.chkViewOnly.Location = new System.Drawing.Point(6, 213);
            this.chkViewOnly.Name = "chkViewOnly";
            this.chkViewOnly.Size = new System.Drawing.Size(70, 17);
            this.chkViewOnly.TabIndex = 4;
            this.chkViewOnly.Text = "view only";
            this.chkViewOnly.UseVisualStyleBackColor = true;
            // 
            // chkViewersEnabled
            // 
            this.chkViewersEnabled.AutoSize = true;
            this.chkViewersEnabled.Location = new System.Drawing.Point(6, 0);
            this.chkViewersEnabled.Name = "chkViewersEnabled";
            this.chkViewersEnabled.Size = new System.Drawing.Size(87, 17);
            this.chkViewersEnabled.TabIndex = 8;
            this.chkViewersEnabled.Text = "Start viewers";
            this.chkViewersEnabled.UseVisualStyleBackColor = true;
            this.chkViewersEnabled.CheckedChanged += new System.EventHandler(this.chkViewersEnabled_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "UltraVNC installation folder";
            // 
            // txbUVNCInstallationFolder
            // 
            this.txbUVNCInstallationFolder.Location = new System.Drawing.Point(3, 18);
            this.txbUVNCInstallationFolder.Name = "txbUVNCInstallationFolder";
            this.txbUVNCInstallationFolder.ReadOnly = true;
            this.txbUVNCInstallationFolder.Size = new System.Drawing.Size(221, 20);
            this.txbUVNCInstallationFolder.TabIndex = 2;
            this.txbUVNCInstallationFolder.Text = ":";
            this.txbUVNCInstallationFolder.TextChanged += new System.EventHandler(this.txbUVNCInstallationFolder_TextChanged);
            // 
            // lblUVNCNotFound
            // 
            this.lblUVNCNotFound.AutoSize = true;
            this.lblUVNCNotFound.ForeColor = System.Drawing.Color.Red;
            this.lblUVNCNotFound.Location = new System.Drawing.Point(165, 2);
            this.lblUVNCNotFound.Name = "lblUVNCNotFound";
            this.lblUVNCNotFound.Size = new System.Drawing.Size(99, 13);
            this.lblUVNCNotFound.TabIndex = 11;
            this.lblUVNCNotFound.Text = "UltraVNC not found";
            this.lblUVNCNotFound.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblUVNCNotFound.Visible = false;
            // 
            // btnBrowseUVNCFolder
            // 
            this.btnBrowseUVNCFolder.Location = new System.Drawing.Point(230, 16);
            this.btnBrowseUVNCFolder.Name = "btnBrowseUVNCFolder";
            this.btnBrowseUVNCFolder.Size = new System.Drawing.Size(28, 23);
            this.btnBrowseUVNCFolder.TabIndex = 3;
            this.btnBrowseUVNCFolder.Text = "...";
            this.btnBrowseUVNCFolder.UseVisualStyleBackColor = true;
            this.btnBrowseUVNCFolder.Click += new System.EventHandler(this.btnBrowseUVNCFolder_Click);
            // 
            // fbdFolderBrowser
            // 
            this.fbdFolderBrowser.Description = "Please select the UltraVNC installation folder";
            this.fbdFolderBrowser.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.fbdFolderBrowser.ShowNewFolderButton = false;
            // 
            // chkServerEnabled
            // 
            this.chkServerEnabled.AutoSize = true;
            this.chkServerEnabled.Location = new System.Drawing.Point(6, 44);
            this.chkServerEnabled.Name = "chkServerEnabled";
            this.chkServerEnabled.Size = new System.Drawing.Size(80, 17);
            this.chkServerEnabled.TabIndex = 4;
            this.chkServerEnabled.Text = "Start server";
            this.chkServerEnabled.UseVisualStyleBackColor = true;
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnBrowseUVNCFolder);
            this.Controls.Add(this.chkServerEnabled);
            this.Controls.Add(this.lblUVNCNotFound);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txbUVNCInstallationFolder);
            this.Controls.Add(this.gpbViewers);
            this.Name = "Options";
            this.Size = new System.Drawing.Size(1033, 526);
            this.gpbViewers.ResumeLayout(false);
            this.gpbViewers.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txbIP;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox gpbViewers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblUVNCNotFound;
        private System.Windows.Forms.Button btnBrowseUVNCFolder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ColumnHeader colIP;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.TextBox txbName;
        private System.Windows.Forms.FolderBrowserDialog fbdFolderBrowser;
        internal System.Windows.Forms.CheckBox chkViewersEnabled;
        internal System.Windows.Forms.TextBox txbUVNCInstallationFolder;
        internal System.Windows.Forms.CheckBox chkViewOnly;
        internal System.Windows.Forms.CheckBox chkLaunchViewerOnStart;
        internal System.Windows.Forms.ListView lsvViewers;
        internal System.Windows.Forms.CheckBox chkServerEnabled;
    }
}
