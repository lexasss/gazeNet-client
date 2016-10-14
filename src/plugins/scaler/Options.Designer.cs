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
            this.nudOwnLeft = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.nudOwnRight = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nudOwnTop = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nudOwnBottom = new System.Windows.Forms.NumericUpDown();
            this.gpbOwn = new System.Windows.Forms.GroupBox();
            this.chkOwn = new System.Windows.Forms.CheckBox();
            this.gpbReceived = new System.Windows.Forms.GroupBox();
            this.chkReceived = new System.Windows.Forms.CheckBox();
            this.nudReceivedBottom = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nudReceivedLeft = new System.Windows.Forms.NumericUpDown();
            this.nudReceivedTop = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.nudReceivedRight = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudOwnLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOwnRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOwnTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOwnBottom)).BeginInit();
            this.gpbOwn.SuspendLayout();
            this.gpbReceived.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudReceivedBottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReceivedLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReceivedTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReceivedRight)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Left";
            // 
            // nudOwnLeft
            // 
            this.nudOwnLeft.Location = new System.Drawing.Point(73, 23);
            this.nudOwnLeft.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudOwnLeft.Name = "nudOwnLeft";
            this.nudOwnLeft.Size = new System.Drawing.Size(102, 20);
            this.nudOwnLeft.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Right";
            // 
            // nudOwnRight
            // 
            this.nudOwnRight.Location = new System.Drawing.Point(73, 49);
            this.nudOwnRight.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudOwnRight.Name = "nudOwnRight";
            this.nudOwnRight.Size = new System.Drawing.Size(102, 20);
            this.nudOwnRight.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Top";
            // 
            // nudOwnTop
            // 
            this.nudOwnTop.Location = new System.Drawing.Point(73, 75);
            this.nudOwnTop.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudOwnTop.Name = "nudOwnTop";
            this.nudOwnTop.Size = new System.Drawing.Size(102, 20);
            this.nudOwnTop.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Bottom";
            // 
            // nudOwnBottom
            // 
            this.nudOwnBottom.Location = new System.Drawing.Point(73, 101);
            this.nudOwnBottom.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudOwnBottom.Name = "nudOwnBottom";
            this.nudOwnBottom.Size = new System.Drawing.Size(102, 20);
            this.nudOwnBottom.TabIndex = 4;
            // 
            // gpbOwn
            // 
            this.gpbOwn.Controls.Add(this.chkOwn);
            this.gpbOwn.Controls.Add(this.nudOwnBottom);
            this.gpbOwn.Controls.Add(this.label1);
            this.gpbOwn.Controls.Add(this.label4);
            this.gpbOwn.Controls.Add(this.nudOwnLeft);
            this.gpbOwn.Controls.Add(this.nudOwnTop);
            this.gpbOwn.Controls.Add(this.label2);
            this.gpbOwn.Controls.Add(this.label3);
            this.gpbOwn.Controls.Add(this.nudOwnRight);
            this.gpbOwn.Location = new System.Drawing.Point(3, 3);
            this.gpbOwn.Name = "gpbOwn";
            this.gpbOwn.Size = new System.Drawing.Size(189, 131);
            this.gpbOwn.TabIndex = 3;
            this.gpbOwn.TabStop = false;
            this.gpbOwn.Text = "groupBox1";
            // 
            // chkOwn
            // 
            this.chkOwn.AutoSize = true;
            this.chkOwn.Location = new System.Drawing.Point(6, 0);
            this.chkOwn.Name = "chkOwn";
            this.chkOwn.Size = new System.Drawing.Size(100, 17);
            this.chkOwn.TabIndex = 0;
            this.chkOwn.Text = "Own gaze point";
            this.chkOwn.UseVisualStyleBackColor = true;
            this.chkOwn.CheckedChanged += new System.EventHandler(this.chkGazePoint_CheckedChanged);
            // 
            // gpbReceived
            // 
            this.gpbReceived.Controls.Add(this.chkReceived);
            this.gpbReceived.Controls.Add(this.nudReceivedBottom);
            this.gpbReceived.Controls.Add(this.label5);
            this.gpbReceived.Controls.Add(this.label6);
            this.gpbReceived.Controls.Add(this.nudReceivedLeft);
            this.gpbReceived.Controls.Add(this.nudReceivedTop);
            this.gpbReceived.Controls.Add(this.label7);
            this.gpbReceived.Controls.Add(this.label8);
            this.gpbReceived.Controls.Add(this.nudReceivedRight);
            this.gpbReceived.Location = new System.Drawing.Point(3, 140);
            this.gpbReceived.Name = "gpbReceived";
            this.gpbReceived.Size = new System.Drawing.Size(189, 131);
            this.gpbReceived.TabIndex = 3;
            this.gpbReceived.TabStop = false;
            this.gpbReceived.Text = "groupBox1";
            // 
            // chkReceived
            // 
            this.chkReceived.AutoSize = true;
            this.chkReceived.Location = new System.Drawing.Point(6, 0);
            this.chkReceived.Name = "chkReceived";
            this.chkReceived.Size = new System.Drawing.Size(124, 17);
            this.chkReceived.TabIndex = 0;
            this.chkReceived.Text = "Received gaze point";
            this.chkReceived.UseVisualStyleBackColor = true;
            this.chkReceived.CheckedChanged += new System.EventHandler(this.chkGazePoint_CheckedChanged);
            // 
            // nudReceivedBottom
            // 
            this.nudReceivedBottom.Location = new System.Drawing.Point(73, 101);
            this.nudReceivedBottom.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudReceivedBottom.Name = "nudReceivedBottom";
            this.nudReceivedBottom.Size = new System.Drawing.Size(102, 20);
            this.nudReceivedBottom.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Left";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Bottom";
            // 
            // nudReceivedLeft
            // 
            this.nudReceivedLeft.Location = new System.Drawing.Point(73, 23);
            this.nudReceivedLeft.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudReceivedLeft.Name = "nudReceivedLeft";
            this.nudReceivedLeft.Size = new System.Drawing.Size(102, 20);
            this.nudReceivedLeft.TabIndex = 1;
            // 
            // nudReceivedTop
            // 
            this.nudReceivedTop.Location = new System.Drawing.Point(73, 75);
            this.nudReceivedTop.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudReceivedTop.Name = "nudReceivedTop";
            this.nudReceivedTop.Size = new System.Drawing.Size(102, 20);
            this.nudReceivedTop.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Right";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 77);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Top";
            // 
            // nudReceivedRight
            // 
            this.nudReceivedRight.Location = new System.Drawing.Point(73, 49);
            this.nudReceivedRight.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudReceivedRight.Name = "nudReceivedRight";
            this.nudReceivedRight.Size = new System.Drawing.Size(102, 20);
            this.nudReceivedRight.TabIndex = 2;
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gpbReceived);
            this.Controls.Add(this.gpbOwn);
            this.Name = "Options";
            this.Size = new System.Drawing.Size(1036, 609);
            ((System.ComponentModel.ISupportInitialize)(this.nudOwnLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOwnRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOwnTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOwnBottom)).EndInit();
            this.gpbOwn.ResumeLayout(false);
            this.gpbOwn.PerformLayout();
            this.gpbReceived.ResumeLayout(false);
            this.gpbReceived.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudReceivedBottom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReceivedLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReceivedTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudReceivedRight)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        internal System.Windows.Forms.NumericUpDown nudOwnLeft;
        internal System.Windows.Forms.NumericUpDown nudOwnRight;
        internal System.Windows.Forms.NumericUpDown nudOwnTop;
        internal System.Windows.Forms.NumericUpDown nudOwnBottom;
        private System.Windows.Forms.GroupBox gpbOwn;
        private System.Windows.Forms.GroupBox gpbReceived;
        internal System.Windows.Forms.NumericUpDown nudReceivedBottom;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        internal System.Windows.Forms.NumericUpDown nudReceivedLeft;
        internal System.Windows.Forms.NumericUpDown nudReceivedTop;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        internal System.Windows.Forms.NumericUpDown nudReceivedRight;
        internal System.Windows.Forms.CheckBox chkOwn;
        internal System.Windows.Forms.CheckBox chkReceived;
    }
}
