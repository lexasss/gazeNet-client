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
            this.SuspendLayout();
            // 
            // tstToolbar
            // 
            this.tstToolbar.Dock = System.Windows.Forms.DockStyle.None;
            this.tstToolbar.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tstToolbar.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.tstToolbar.Location = new System.Drawing.Point(9, 28);
            this.tstToolbar.Name = "tstToolbar";
            this.tstToolbar.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.tstToolbar.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tstToolbar.Size = new System.Drawing.Size(33, 19);
            this.tstToolbar.TabIndex = 0;
            this.tstToolbar.Text = "toolStrip1";
            // 
            // Toolbar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(371, 78);
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
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Toolbar_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tstToolbar;
    }
}