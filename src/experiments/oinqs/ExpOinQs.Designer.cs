namespace GazeNetClient.Experiment.OinQs
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txbTopic = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nudObjectCount = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nudTrialCount = new System.Windows.Forms.NumericUpDown();
            this.svdSession = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.nudObjectCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTrialCount)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(12, 111);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(166, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Topic";
            // 
            // txbTopic
            // 
            this.txbTopic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbTopic.Location = new System.Drawing.Point(86, 12);
            this.txbTopic.Name = "txbTopic";
            this.txbTopic.Size = new System.Drawing.Size(92, 20);
            this.txbTopic.TabIndex = 2;
            this.txbTopic.TextChanged += new System.EventHandler(this.txbTopic_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Object count";
            // 
            // nudObjectCount
            // 
            this.nudObjectCount.Location = new System.Drawing.Point(86, 39);
            this.nudObjectCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudObjectCount.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudObjectCount.Name = "nudObjectCount";
            this.nudObjectCount.Size = new System.Drawing.Size(92, 20);
            this.nudObjectCount.TabIndex = 3;
            this.nudObjectCount.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudObjectCount.ValueChanged += new System.EventHandler(this.nudObjectCount_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Trial count";
            // 
            // nudTrialCount
            // 
            this.nudTrialCount.Location = new System.Drawing.Point(86, 65);
            this.nudTrialCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTrialCount.Name = "nudTrialCount";
            this.nudTrialCount.Size = new System.Drawing.Size(92, 20);
            this.nudTrialCount.TabIndex = 3;
            this.nudTrialCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTrialCount.ValueChanged += new System.EventHandler(this.nudTrialCount_ValueChanged);
            // 
            // svdSession
            // 
            this.svdSession.DefaultExt = "txt";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(190, 146);
            this.Controls.Add(this.nudTrialCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nudObjectCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txbTopic);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "O-in-Qs";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.nudObjectCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTrialCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbTopic;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudObjectCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudTrialCount;
        private System.Windows.Forms.SaveFileDialog svdSession;
    }
}

