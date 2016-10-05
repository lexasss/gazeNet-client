namespace GazeNetClient.Plugins.Scaler
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
            this.label1 = new System.Windows.Forms.Label();
            this.nudLeft = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nudRight = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nudTop = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nudBottom = new System.Windows.Forms.NumericUpDown();
            this.cmbAppliesTo = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBottom)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Left";
            // 
            // nudLeft
            // 
            this.nudLeft.Location = new System.Drawing.Point(65, 31);
            this.nudLeft.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudLeft.Name = "nudLeft";
            this.nudLeft.Size = new System.Drawing.Size(102, 20);
            this.nudLeft.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Right";
            // 
            // nudRight
            // 
            this.nudRight.Location = new System.Drawing.Point(65, 57);
            this.nudRight.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudRight.Name = "nudRight";
            this.nudRight.Size = new System.Drawing.Size(102, 20);
            this.nudRight.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Top";
            // 
            // nudTop
            // 
            this.nudTop.Location = new System.Drawing.Point(65, 83);
            this.nudTop.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudTop.Name = "nudTop";
            this.nudTop.Size = new System.Drawing.Size(102, 20);
            this.nudTop.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Bottom";
            // 
            // nudBottom
            // 
            this.nudBottom.Location = new System.Drawing.Point(65, 109);
            this.nudBottom.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudBottom.Name = "nudBottom";
            this.nudBottom.Size = new System.Drawing.Size(102, 20);
            this.nudBottom.TabIndex = 1;
            // 
            // cmbAppliesTo
            // 
            this.cmbAppliesTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAppliesTo.FormattingEnabled = true;
            this.cmbAppliesTo.Location = new System.Drawing.Point(65, 4);
            this.cmbAppliesTo.Name = "cmbAppliesTo";
            this.cmbAppliesTo.Size = new System.Drawing.Size(102, 21);
            this.cmbAppliesTo.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(2, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Applies to";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(173, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "gaze point";
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbAppliesTo);
            this.Controls.Add(this.nudBottom);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nudTop);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nudRight);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudLeft);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Name = "Options";
            this.Size = new System.Drawing.Size(994, 609);
            ((System.ComponentModel.ISupportInitialize)(this.nudLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBottom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.NumericUpDown nudLeft;
        internal System.Windows.Forms.NumericUpDown nudRight;
        internal System.Windows.Forms.NumericUpDown nudTop;
        internal System.Windows.Forms.NumericUpDown nudBottom;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        internal System.Windows.Forms.ComboBox cmbAppliesTo;
    }
}
