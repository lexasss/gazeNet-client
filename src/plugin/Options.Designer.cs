namespace GazeNetClient.Plugin
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Exclusive", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Inclusive", System.Windows.Forms.HorizontalAlignment.Center);
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lsvPlugins = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gpbPluginOptionsContainer = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(198, 357);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(279, 357);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lsvPlugins
            // 
            this.lsvPlugins.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsvPlugins.CheckBoxes = true;
            this.lsvPlugins.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lsvPlugins.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            listViewGroup3.Header = "Exclusive";
            listViewGroup3.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup3.Name = "exclusive";
            listViewGroup4.Header = "Inclusive";
            listViewGroup4.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup4.Name = "inclusive";
            this.lsvPlugins.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup3,
            listViewGroup4});
            this.lsvPlugins.Location = new System.Drawing.Point(12, 12);
            this.lsvPlugins.Name = "lsvPlugins";
            this.lsvPlugins.Size = new System.Drawing.Size(261, 339);
            this.lsvPlugins.TabIndex = 2;
            this.lsvPlugins.UseCompatibleStateImageBehavior = false;
            this.lsvPlugins.View = System.Windows.Forms.View.Details;
            this.lsvPlugins.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lsvPlugins_ItemChecked);
            this.lsvPlugins.SelectedIndexChanged += new System.EventHandler(this.lsvPlugins_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Plugin";
            this.columnHeader1.Width = 220;
            // 
            // gpbPluginOptionsContainer
            // 
            this.gpbPluginOptionsContainer.Location = new System.Drawing.Point(279, 12);
            this.gpbPluginOptionsContainer.Name = "gpbPluginOptionsContainer";
            this.gpbPluginOptionsContainer.Size = new System.Drawing.Size(266, 339);
            this.gpbPluginOptionsContainer.TabIndex = 4;
            this.gpbPluginOptionsContainer.TabStop = false;
            this.gpbPluginOptionsContainer.Text = "Select a plugin to display its options";
            // 
            // Options
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(557, 392);
            this.Controls.Add(this.gpbPluginOptionsContainer);
            this.Controls.Add(this.lsvPlugins);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Options";
            this.Text = "Plugin manager";
            this.Shown += new System.EventHandler(this.Options_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListView lsvPlugins;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.GroupBox gpbPluginOptionsContainer;
    }
}