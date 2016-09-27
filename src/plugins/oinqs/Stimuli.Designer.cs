namespace GazeNetClient.Plugins.OinQs
{
    partial class Stimuli
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Stimuli));
            this.lblText = new System.Windows.Forms.Label();
            this.imlQs = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // lblText
            // 
            this.lblText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblText.Font = new System.Drawing.Font("DejaVu Sans Condensed", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblText.ImageKey = "Q0";
            this.lblText.ImageList = this.imlQs;
            this.lblText.Location = new System.Drawing.Point(0, 0);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(34, 34);
            this.lblText.TabIndex = 0;
            this.lblText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // imlQs
            // 
            this.imlQs.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlQs.ImageStream")));
            this.imlQs.TransparentColor = System.Drawing.Color.Transparent;
            this.imlQs.Images.SetKeyName(0, "O0");
            this.imlQs.Images.SetKeyName(1, "O90");
            this.imlQs.Images.SetKeyName(2, "Q0");
            this.imlQs.Images.SetKeyName(3, "Q90");
            this.imlQs.Images.SetKeyName(4, "Q180");
            this.imlQs.Images.SetKeyName(5, "Q270");
            // 
            // Stimuli
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.lblText);
            this.Name = "Stimuli";
            this.Size = new System.Drawing.Size(34, 34);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.ImageList imlQs;
    }
}
