namespace GazeNetClient
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
            this.label1 = new System.Windows.Forms.Label();
            this.cmbPointerAppearance = new System.Windows.Forms.ComboBox();
            this.trbPointerOpacity = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.lblOpacity = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblSize = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.trbPointerSize = new System.Windows.Forms.TrackBar();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbpConnect = new System.Windows.Forms.TabPage();
            this.cmbRole = new System.Windows.Forms.ComboBox();
            this.nudServerPort = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txbTopic = new System.Windows.Forms.TextBox();
            this.txbUserName = new System.Windows.Forms.TextBox();
            this.txbServerHost = new System.Windows.Forms.TextBox();
            this.tbpPointer = new System.Windows.Forms.TabPage();
            this.pcbPointer = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.nudFilterFixationThreshold = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.nudFilterWindowSize = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudFilterTHigh = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nudFilterTLow = new System.Windows.Forms.NumericUpDown();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.nudPointerNoDataVisibilityDuration = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.nudPointerFadingInterval = new System.Windows.Forms.NumericUpDown();
            this.chkAutoStarterEnabled = new System.Windows.Forms.CheckBox();
            this.imlIcons = new System.Windows.Forms.ImageList(this.components);
            this.Port = new System.Windows.Forms.Label();
            this.chkAutoRestartOnDisconnection = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.trbPointerOpacity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbPointerSize)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tbpConnect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudServerPort)).BeginInit();
            this.tbpPointer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbPointer)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFilterFixationThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFilterWindowSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFilterTHigh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFilterTLow)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPointerNoDataVisibilityDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPointerFadingInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Appearance";
            // 
            // cmbPointerAppearance
            // 
            this.cmbPointerAppearance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPointerAppearance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPointerAppearance.FormattingEnabled = true;
            this.cmbPointerAppearance.Location = new System.Drawing.Point(75, 6);
            this.cmbPointerAppearance.Name = "cmbPointerAppearance";
            this.cmbPointerAppearance.Size = new System.Drawing.Size(90, 21);
            this.cmbPointerAppearance.TabIndex = 0;
            this.cmbPointerAppearance.SelectedIndexChanged += new System.EventHandler(this.cmbAppearance_SelectedIndexChanged);
            // 
            // trbPointerOpacity
            // 
            this.trbPointerOpacity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trbPointerOpacity.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.trbPointerOpacity.Location = new System.Drawing.Point(75, 33);
            this.trbPointerOpacity.Minimum = 1;
            this.trbPointerOpacity.Name = "trbPointerOpacity";
            this.trbPointerOpacity.Size = new System.Drawing.Size(90, 45);
            this.trbPointerOpacity.TabIndex = 1;
            this.trbPointerOpacity.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trbPointerOpacity.Value = 9;
            this.trbPointerOpacity.ValueChanged += new System.EventHandler(this.trbOpacity_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Opacity";
            // 
            // lblOpacity
            // 
            this.lblOpacity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOpacity.AutoSize = true;
            this.lblOpacity.Location = new System.Drawing.Point(171, 47);
            this.lblOpacity.Name = "lblOpacity";
            this.lblOpacity.Size = new System.Drawing.Size(13, 13);
            this.lblOpacity.TabIndex = 4;
            this.lblOpacity.Text = "0";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(32, 184);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(114, 184);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblSize
            // 
            this.lblSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(171, 98);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(13, 13);
            this.lblSize.TabIndex = 9;
            this.lblSize.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Size";
            // 
            // trbPointerSize
            // 
            this.trbPointerSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trbPointerSize.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.trbPointerSize.Location = new System.Drawing.Point(75, 84);
            this.trbPointerSize.Maximum = 30;
            this.trbPointerSize.Minimum = 3;
            this.trbPointerSize.Name = "trbPointerSize";
            this.trbPointerSize.Size = new System.Drawing.Size(90, 45);
            this.trbPointerSize.TabIndex = 2;
            this.trbPointerSize.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trbPointerSize.Value = 9;
            this.trbPointerSize.ValueChanged += new System.EventHandler(this.trbSize_ValueChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tbpConnect);
            this.tabControl1.Controls.Add(this.tbpPointer);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(218, 166);
            this.tabControl1.TabIndex = 10;
            // 
            // tbpConnect
            // 
            this.tbpConnect.Controls.Add(this.chkAutoRestartOnDisconnection);
            this.tbpConnect.Controls.Add(this.cmbRole);
            this.tbpConnect.Controls.Add(this.nudServerPort);
            this.tbpConnect.Controls.Add(this.Port);
            this.tbpConnect.Controls.Add(this.label11);
            this.tbpConnect.Controls.Add(this.label13);
            this.tbpConnect.Controls.Add(this.label12);
            this.tbpConnect.Controls.Add(this.label10);
            this.tbpConnect.Controls.Add(this.txbTopic);
            this.tbpConnect.Controls.Add(this.txbUserName);
            this.tbpConnect.Controls.Add(this.txbServerHost);
            this.tbpConnect.Location = new System.Drawing.Point(4, 22);
            this.tbpConnect.Name = "tbpConnect";
            this.tbpConnect.Padding = new System.Windows.Forms.Padding(3);
            this.tbpConnect.Size = new System.Drawing.Size(210, 140);
            this.tbpConnect.TabIndex = 3;
            this.tbpConnect.Text = "Connection";
            this.tbpConnect.UseVisualStyleBackColor = true;
            // 
            // cmbRole
            // 
            this.cmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRole.FormattingEnabled = true;
            this.cmbRole.Items.AddRange(new object[] {
            "observer",
            "source",
            "both"});
            this.cmbRole.Location = new System.Drawing.Point(50, 111);
            this.cmbRole.Name = "cmbRole";
            this.cmbRole.Size = new System.Drawing.Size(154, 21);
            this.cmbRole.TabIndex = 4;
            // 
            // nudServerPort
            // 
            this.nudServerPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudServerPort.Location = new System.Drawing.Point(153, 7);
            this.nudServerPort.Maximum = new decimal(new int[] {
            65355,
            0,
            0,
            0});
            this.nudServerPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudServerPort.Name = "nudServerPort";
            this.nudServerPort.Size = new System.Drawing.Size(51, 20);
            this.nudServerPort.TabIndex = 1;
            this.nudServerPort.Value = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 114);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 13);
            this.label11.TabIndex = 1;
            this.label11.Text = "Role";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 88);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 13);
            this.label13.TabIndex = 1;
            this.label13.Text = "Topic";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 62);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 13);
            this.label12.TabIndex = 1;
            this.label12.Text = "User";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Server";
            // 
            // txbTopic
            // 
            this.txbTopic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbTopic.Location = new System.Drawing.Point(50, 85);
            this.txbTopic.Name = "txbTopic";
            this.txbTopic.Size = new System.Drawing.Size(154, 20);
            this.txbTopic.TabIndex = 3;
            // 
            // txbUserName
            // 
            this.txbUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbUserName.Location = new System.Drawing.Point(50, 59);
            this.txbUserName.Name = "txbUserName";
            this.txbUserName.Size = new System.Drawing.Size(154, 20);
            this.txbUserName.TabIndex = 2;
            // 
            // txbServerHost
            // 
            this.txbServerHost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbServerHost.Location = new System.Drawing.Point(50, 6);
            this.txbServerHost.Name = "txbServerHost";
            this.txbServerHost.Size = new System.Drawing.Size(81, 20);
            this.txbServerHost.TabIndex = 0;
            this.txbServerHost.Text = "localhost";
            // 
            // tbpPointer
            // 
            this.tbpPointer.Controls.Add(this.pcbPointer);
            this.tbpPointer.Controls.Add(this.label1);
            this.tbpPointer.Controls.Add(this.lblSize);
            this.tbpPointer.Controls.Add(this.cmbPointerAppearance);
            this.tbpPointer.Controls.Add(this.label4);
            this.tbpPointer.Controls.Add(this.trbPointerOpacity);
            this.tbpPointer.Controls.Add(this.trbPointerSize);
            this.tbpPointer.Controls.Add(this.label2);
            this.tbpPointer.Controls.Add(this.lblOpacity);
            this.tbpPointer.Location = new System.Drawing.Point(4, 22);
            this.tbpPointer.Name = "tbpPointer";
            this.tbpPointer.Padding = new System.Windows.Forms.Padding(3);
            this.tbpPointer.Size = new System.Drawing.Size(210, 140);
            this.tbpPointer.TabIndex = 0;
            this.tbpPointer.Text = "Pointer";
            this.tbpPointer.UseVisualStyleBackColor = true;
            // 
            // pcbPointer
            // 
            this.pcbPointer.Location = new System.Drawing.Point(171, 6);
            this.pcbPointer.Name = "pcbPointer";
            this.pcbPointer.Size = new System.Drawing.Size(33, 21);
            this.pcbPointer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcbPointer.TabIndex = 10;
            this.pcbPointer.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.nudFilterFixationThreshold);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.nudFilterWindowSize);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.nudFilterTHigh);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.nudFilterTLow);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(210, 140);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Smoothing";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 86);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Fixation threshold";
            // 
            // nudFilterFixationThreshold
            // 
            this.nudFilterFixationThreshold.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudFilterFixationThreshold.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudFilterFixationThreshold.Location = new System.Drawing.Point(123, 84);
            this.nudFilterFixationThreshold.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudFilterFixationThreshold.Name = "nudFilterFixationThreshold";
            this.nudFilterFixationThreshold.Size = new System.Drawing.Size(81, 20);
            this.nudFilterFixationThreshold.TabIndex = 6;
            this.nudFilterFixationThreshold.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Window size (ms)";
            // 
            // nudFilterWindowSize
            // 
            this.nudFilterWindowSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudFilterWindowSize.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudFilterWindowSize.Location = new System.Drawing.Point(123, 58);
            this.nudFilterWindowSize.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nudFilterWindowSize.Minimum = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.nudFilterWindowSize.Name = "nudFilterWindowSize";
            this.nudFilterWindowSize.Size = new System.Drawing.Size(81, 20);
            this.nudFilterWindowSize.TabIndex = 4;
            this.nudFilterWindowSize.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "T (saccade)";
            // 
            // nudFilterTHigh
            // 
            this.nudFilterTHigh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudFilterTHigh.Location = new System.Drawing.Point(123, 32);
            this.nudFilterTHigh.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudFilterTHigh.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFilterTHigh.Name = "nudFilterTHigh";
            this.nudFilterTHigh.Size = new System.Drawing.Size(81, 20);
            this.nudFilterTHigh.TabIndex = 2;
            this.nudFilterTHigh.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "T (fixation)";
            // 
            // nudFilterTLow
            // 
            this.nudFilterTLow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudFilterTLow.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudFilterTLow.Location = new System.Drawing.Point(123, 6);
            this.nudFilterTLow.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudFilterTLow.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudFilterTLow.Name = "nudFilterTLow";
            this.nudFilterTLow.Size = new System.Drawing.Size(81, 20);
            this.nudFilterTLow.TabIndex = 0;
            this.nudFilterTLow.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.nudPointerNoDataVisibilityDuration);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.nudPointerFadingInterval);
            this.tabPage3.Controls.Add(this.chkAutoStarterEnabled);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(210, 140);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Misc";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 57);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(108, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Data-less visibility, ms";
            // 
            // nudPointerNoDataVisibilityDuration
            // 
            this.nudPointerNoDataVisibilityDuration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudPointerNoDataVisibilityDuration.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudPointerNoDataVisibilityDuration.Location = new System.Drawing.Point(140, 55);
            this.nudPointerNoDataVisibilityDuration.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.nudPointerNoDataVisibilityDuration.Minimum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.nudPointerNoDataVisibilityDuration.Name = "nudPointerNoDataVisibilityDuration";
            this.nudPointerNoDataVisibilityDuration.Size = new System.Drawing.Size(64, 20);
            this.nudPointerNoDataVisibilityDuration.TabIndex = 4;
            this.nudPointerNoDataVisibilityDuration.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(128, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Pointer fading interval, ms";
            // 
            // nudPointerFadingInterval
            // 
            this.nudPointerFadingInterval.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudPointerFadingInterval.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudPointerFadingInterval.Location = new System.Drawing.Point(140, 29);
            this.nudPointerFadingInterval.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudPointerFadingInterval.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudPointerFadingInterval.Name = "nudPointerFadingInterval";
            this.nudPointerFadingInterval.Size = new System.Drawing.Size(64, 20);
            this.nudPointerFadingInterval.TabIndex = 2;
            this.nudPointerFadingInterval.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // chkAutoStarterEnabled
            // 
            this.chkAutoStarterEnabled.AutoSize = true;
            this.chkAutoStarterEnabled.Location = new System.Drawing.Point(6, 6);
            this.chkAutoStarterEnabled.Name = "chkAutoStarterEnabled";
            this.chkAutoStarterEnabled.Size = new System.Drawing.Size(150, 17);
            this.chkAutoStarterEnabled.TabIndex = 0;
            this.chkAutoStarterEnabled.Text = "Auto-start tracking on start";
            this.chkAutoStarterEnabled.UseVisualStyleBackColor = true;
            // 
            // imlIcons
            // 
            this.imlIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlIcons.ImageStream")));
            this.imlIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imlIcons.Images.SetKeyName(0, "initial");
            this.imlIcons.Images.SetKeyName(1, "connected");
            this.imlIcons.Images.SetKeyName(2, "connected-as-observer");
            // 
            // Port
            // 
            this.Port.AutoSize = true;
            this.Port.Location = new System.Drawing.Point(137, 9);
            this.Port.Name = "Port";
            this.Port.Size = new System.Drawing.Size(10, 13);
            this.Port.TabIndex = 1;
            this.Port.Text = ":";
            // 
            // chkAutoRestartOnDisconnection
            // 
            this.chkAutoRestartOnDisconnection.AutoSize = true;
            this.chkAutoRestartOnDisconnection.Location = new System.Drawing.Point(9, 32);
            this.chkAutoRestartOnDisconnection.Name = "chkAutoRestartOnDisconnection";
            this.chkAutoRestartOnDisconnection.Size = new System.Drawing.Size(205, 17);
            this.chkAutoRestartOnDisconnection.TabIndex = 5;
            this.chkAutoRestartOnDisconnection.Text = "Auto-restart tracking on disconnection";
            this.chkAutoRestartOnDisconnection.UseVisualStyleBackColor = true;
            // 
            // Options
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(242, 219);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Options";
            this.Text = "GazeNetClient Options";
            ((System.ComponentModel.ISupportInitialize)(this.trbPointerOpacity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbPointerSize)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tbpConnect.ResumeLayout(false);
            this.tbpConnect.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudServerPort)).EndInit();
            this.tbpPointer.ResumeLayout(false);
            this.tbpPointer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbPointer)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFilterFixationThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFilterWindowSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFilterTHigh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFilterTLow)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPointerNoDataVisibilityDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPointerFadingInterval)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbPointerAppearance;
        private System.Windows.Forms.TrackBar trbPointerOpacity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblOpacity;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar trbPointerSize;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbpPointer;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudFilterTHigh;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudFilterTLow;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudFilterWindowSize;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudFilterFixationThreshold;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox chkAutoStarterEnabled;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudPointerFadingInterval;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown nudPointerNoDataVisibilityDuration;
        private System.Windows.Forms.TabPage tbpConnect;
        private System.Windows.Forms.NumericUpDown nudServerPort;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txbServerHost;
        private System.Windows.Forms.ImageList imlIcons;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txbTopic;
        private System.Windows.Forms.TextBox txbUserName;
        private System.Windows.Forms.ComboBox cmbRole;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.PictureBox pcbPointer;
        private System.Windows.Forms.CheckBox chkAutoRestartOnDisconnection;
        private System.Windows.Forms.Label Port;
    }
}

