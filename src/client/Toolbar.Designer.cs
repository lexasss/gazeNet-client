namespace GazeNetClient
{
    partial class Toolbar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Toolbar));
            this.tstToolbar = new System.Windows.Forms.ToolStrip();
            this.lblMinimize = new System.Windows.Forms.Label();
            this.lblHideToTray = new System.Windows.Forms.Label();
            this.lblClose = new System.Windows.Forms.Label();
            this.flpWindowControls = new System.Windows.Forms.FlowLayoutPanel();
            this.flpWindowControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // tstToolbar
            // 
            this.tstToolbar.Dock = System.Windows.Forms.DockStyle.None;
            this.tstToolbar.GripMargin = new System.Windows.Forms.Padding(0);
            this.tstToolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tstToolbar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tstToolbar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tstToolbar.Location = new System.Drawing.Point(6, 28);
            this.tstToolbar.Name = "tstToolbar";
            this.tstToolbar.Padding = new System.Windows.Forms.Padding(0);
            this.tstToolbar.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tstToolbar.Size = new System.Drawing.Size(31, 19);
            this.tstToolbar.TabIndex = 0;
            // 
            // lblMinimize
            // 
            this.lblMinimize.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblMinimize.AutoSize = true;
            this.lblMinimize.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMinimize.ForeColor = System.Drawing.Color.LightGray;
            this.lblMinimize.Location = new System.Drawing.Point(0, 0);
            this.lblMinimize.Margin = new System.Windows.Forms.Padding(0);
            this.lblMinimize.MinimumSize = new System.Drawing.Size(24, 0);
            this.lblMinimize.Name = "lblMinimize";
            this.lblMinimize.Padding = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.lblMinimize.Size = new System.Drawing.Size(32, 19);
            this.lblMinimize.TabIndex = 1;
            this.lblMinimize.Text = "___";
            this.lblMinimize.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblMinimize.Click += new System.EventHandler(this.lblMinimize_Click);
            this.lblMinimize.MouseLeave += new System.EventHandler(this.WindowControl_MouseLeave);
            this.lblMinimize.MouseHover += new System.EventHandler(this.WindowControl_MouseHover);
            // 
            // lblHideToTray
            // 
            this.lblHideToTray.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblHideToTray.AutoSize = true;
            this.lblHideToTray.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHideToTray.ForeColor = System.Drawing.Color.LightGray;
            this.lblHideToTray.Location = new System.Drawing.Point(32, 0);
            this.lblHideToTray.Margin = new System.Windows.Forms.Padding(0);
            this.lblHideToTray.MinimumSize = new System.Drawing.Size(24, 0);
            this.lblHideToTray.Name = "lblHideToTray";
            this.lblHideToTray.Padding = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.lblHideToTray.Size = new System.Drawing.Size(31, 19);
            this.lblHideToTray.TabIndex = 1;
            this.lblHideToTray.Text = " v ";
            this.lblHideToTray.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblHideToTray.Click += new System.EventHandler(this.lblHideToTray_Click);
            this.lblHideToTray.MouseLeave += new System.EventHandler(this.WindowControl_MouseLeave);
            this.lblHideToTray.MouseHover += new System.EventHandler(this.WindowControl_MouseHover);
            // 
            // lblClose
            // 
            this.lblClose.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblClose.AutoSize = true;
            this.lblClose.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClose.ForeColor = System.Drawing.Color.LightGray;
            this.lblClose.Location = new System.Drawing.Point(63, 0);
            this.lblClose.Margin = new System.Windows.Forms.Padding(0);
            this.lblClose.MinimumSize = new System.Drawing.Size(24, 0);
            this.lblClose.Name = "lblClose";
            this.lblClose.Padding = new System.Windows.Forms.Padding(3, 0, 3, 2);
            this.lblClose.Size = new System.Drawing.Size(26, 19);
            this.lblClose.TabIndex = 1;
            this.lblClose.Text = " x";
            this.lblClose.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblClose.Click += new System.EventHandler(this.lblClose_Click);
            this.lblClose.MouseLeave += new System.EventHandler(this.WindowControl_MouseLeave);
            this.lblClose.MouseHover += new System.EventHandler(this.WindowControl_MouseHover);
            // 
            // flpWindowControls
            // 
            this.flpWindowControls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flpWindowControls.AutoSize = true;
            this.flpWindowControls.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpWindowControls.Controls.Add(this.lblMinimize);
            this.flpWindowControls.Controls.Add(this.lblHideToTray);
            this.flpWindowControls.Controls.Add(this.lblClose);
            this.flpWindowControls.Location = new System.Drawing.Point(275, 5);
            this.flpWindowControls.Name = "flpWindowControls";
            this.flpWindowControls.Size = new System.Drawing.Size(89, 19);
            this.flpWindowControls.TabIndex = 3;
            this.flpWindowControls.WrapContents = false;
            // 
            // Toolbar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(371, 78);
            this.Controls.Add(this.flpWindowControls);
            this.Controls.Add(this.tstToolbar);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "Toolbar";
            this.Opacity = 0.8D;
            this.Text = "GazeNet";
            this.Activated += new System.EventHandler(this.Toolbar_Activated);
            this.Deactivate += new System.EventHandler(this.Toolbar_Deactivate);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Toolbar_FormClosed);
            this.Shown += new System.EventHandler(this.Toolbar_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Toolbar_Paint);
            this.flpWindowControls.ResumeLayout(false);
            this.flpWindowControls.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tstToolbar;
        private System.Windows.Forms.Label lblMinimize;
        private System.Windows.Forms.Label lblHideToTray;
        private System.Windows.Forms.Label lblClose;
        private System.Windows.Forms.FlowLayoutPanel flpWindowControls;
    }
}