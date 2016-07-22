namespace GoBot
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.tStats = new System.Windows.Forms.Timer(this.components);
            this.cTabControl1 = new cTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cbAuthType = new BoosterComboBox();
            this.chkGetForts = new cCheckBox();
            this.chkCatchPokes = new cCheckBox();
            this.btnStop = new cButton();
            this.txtPass = new cTextBox();
            this.txtUser = new cTextBox();
            this.btnStart = new cButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.txtAltitude = new cTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtWalkSpeed = new cTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLng = new cTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtLat = new cTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtProbability = new cTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.chkInverseBerries = new cCheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.clbBerries = new System.Windows.Forms.CheckedListBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtEvolveCp = new cTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCatchCp = new cTextBox();
            this.chkInverseTransfer = new cCheckBox();
            this.chkInverseCatch = new cCheckBox();
            this.chkInverseEvolve = new cCheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBelowCp = new cTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.clbTransfer = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.clbCatch = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.clbEvolve = new System.Windows.Forms.CheckedListBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.label24 = new System.Windows.Forms.Label();
            this.txtRB = new cTextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.txtMaxRevive = new cTextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtRevive = new cTextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtMP = new cTextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtHP = new cTextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtSP = new cTextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtP = new cTextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtUPB = new cTextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtGPB = new cTextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtPB = new cTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.lvWalkLog = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.btnResetSettings = new cButton();
            this.lblDump = new System.Windows.Forms.Label();
            this.lblXP = new System.Windows.Forms.Label();
            this.lblStardust = new System.Windows.Forms.Label();
            this.lblTransferred = new System.Windows.Forms.Label();
            this.lblFound = new System.Windows.Forms.Label();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtCatchIV = new cTextBox();
            this.txtTransferIV = new cTextBox();
            this.txtEvolveIV = new cTextBox();
            this.cTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.SuspendLayout();
            // 
            // tStats
            // 
            this.tStats.Interval = 500;
            this.tStats.Tick += new System.EventHandler(this.tStats_Tick);
            // 
            // cTabControl1
            // 
            this.cTabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.cTabControl1.Controls.Add(this.tabPage1);
            this.cTabControl1.Controls.Add(this.tabPage2);
            this.cTabControl1.Controls.Add(this.tabPage3);
            this.cTabControl1.Controls.Add(this.tabPage6);
            this.cTabControl1.Controls.Add(this.tabPage4);
            this.cTabControl1.Controls.Add(this.tabPage5);
            this.cTabControl1.Controls.Add(this.tabPage7);
            this.cTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cTabControl1.ItemSize = new System.Drawing.Size(40, 170);
            this.cTabControl1.Location = new System.Drawing.Point(0, 0);
            this.cTabControl1.Multiline = true;
            this.cTabControl1.Name = "cTabControl1";
            this.cTabControl1.SelectedIndex = 0;
            this.cTabControl1.Size = new System.Drawing.Size(914, 594);
            this.cTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.cTabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(43)))));
            this.tabPage1.Controls.Add(this.cbAuthType);
            this.tabPage1.Controls.Add(this.chkGetForts);
            this.tabPage1.Controls.Add(this.chkCatchPokes);
            this.tabPage1.Controls.Add(this.btnStop);
            this.tabPage1.Controls.Add(this.txtPass);
            this.tabPage1.Controls.Add(this.txtUser);
            this.tabPage1.Controls.Add(this.btnStart);
            this.tabPage1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabPage1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tabPage1.Location = new System.Drawing.Point(174, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(736, 586);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Bot Settings";
            // 
            // cbAuthType
            // 
            this.cbAuthType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbAuthType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAuthType.FormattingEnabled = true;
            this.cbAuthType.ItemHeight = 20;
            this.cbAuthType.Items.AddRange(new object[] {
            "Ptc",
            "Google"});
            this.cbAuthType.Location = new System.Drawing.Point(265, 137);
            this.cbAuthType.Name = "cbAuthType";
            this.cbAuthType.Size = new System.Drawing.Size(207, 26);
            this.cbAuthType.TabIndex = 19;
            // 
            // chkGetForts
            // 
            this.chkGetForts.AutoSize = true;
            this.chkGetForts.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkGetForts.Location = new System.Drawing.Point(391, 241);
            this.chkGetForts.Name = "chkGetForts";
            this.chkGetForts.Size = new System.Drawing.Size(81, 19);
            this.chkGetForts.TabIndex = 18;
            this.chkGetForts.Text = "PokeStops";
            this.chkGetForts.UseVisualStyleBackColor = true;
            // 
            // chkCatchPokes
            // 
            this.chkCatchPokes.AutoSize = true;
            this.chkCatchPokes.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkCatchPokes.Location = new System.Drawing.Point(264, 241);
            this.chkCatchPokes.Name = "chkCatchPokes";
            this.chkCatchPokes.Size = new System.Drawing.Size(111, 19);
            this.chkCatchPokes.TabIndex = 17;
            this.chkCatchPokes.Text = "Catch Pokemon";
            this.chkCatchPokes.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(381, 266);
            this.btnStop.Name = "btnStop";
            this.btnStop.Scheme = cButton.Schemes.Red;
            this.btnStop.Size = new System.Drawing.Size(91, 23);
            this.btnStop.TabIndex = 9;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(265, 205);
            this.txtPass.MaxLength = 32767;
            this.txtPass.Name = "txtPass";
            this.txtPass.ReadOnly = false;
            this.txtPass.Scheme = cTextBox.Schemes.Black;
            this.txtPass.Size = new System.Drawing.Size(207, 30);
            this.txtPass.TabIndex = 8;
            this.txtPass.UseSystemPasswordChar = true;
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(265, 169);
            this.txtUser.MaxLength = 32767;
            this.txtUser.Name = "txtUser";
            this.txtUser.ReadOnly = false;
            this.txtUser.Scheme = cTextBox.Schemes.Black;
            this.txtUser.Size = new System.Drawing.Size(207, 30);
            this.txtUser.TabIndex = 7;
            this.txtUser.UseSystemPasswordChar = false;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(265, 266);
            this.btnStart.Name = "btnStart";
            this.btnStart.Scheme = cButton.Schemes.Green;
            this.btnStart.Size = new System.Drawing.Size(110, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(43)))));
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.txtAltitude);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.txtWalkSpeed);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.txtLng);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.txtLat);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabPage2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tabPage2.Location = new System.Drawing.Point(174, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(736, 586);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Walk Settings";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(43, 135);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(49, 15);
            this.label11.TabIndex = 8;
            this.label11.Text = "Altitude";
            // 
            // txtAltitude
            // 
            this.txtAltitude.Location = new System.Drawing.Point(129, 128);
            this.txtAltitude.MaxLength = 32767;
            this.txtAltitude.Name = "txtAltitude";
            this.txtAltitude.ReadOnly = false;
            this.txtAltitude.Scheme = cTextBox.Schemes.Black;
            this.txtAltitude.Size = new System.Drawing.Size(169, 30);
            this.txtAltitude.TabIndex = 7;
            this.txtAltitude.Text = "50";
            this.txtAltitude.UseSystemPasswordChar = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(126, 185);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 15);
            this.label7.TabIndex = 6;
            this.label7.Text = "Walk Speed (km/h)";
            // 
            // txtWalkSpeed
            // 
            this.txtWalkSpeed.Location = new System.Drawing.Point(129, 203);
            this.txtWalkSpeed.MaxLength = 32767;
            this.txtWalkSpeed.Name = "txtWalkSpeed";
            this.txtWalkSpeed.ReadOnly = false;
            this.txtWalkSpeed.Scheme = cTextBox.Schemes.Black;
            this.txtWalkSpeed.Size = new System.Drawing.Size(169, 30);
            this.txtWalkSpeed.TabIndex = 5;
            this.txtWalkSpeed.Text = "20";
            this.txtWalkSpeed.UseSystemPasswordChar = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(43, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 15);
            this.label6.TabIndex = 4;
            this.label6.Text = "Longitude";
            // 
            // txtLng
            // 
            this.txtLng.Location = new System.Drawing.Point(129, 92);
            this.txtLng.MaxLength = 32767;
            this.txtLng.Name = "txtLng";
            this.txtLng.ReadOnly = false;
            this.txtLng.Scheme = cTextBox.Schemes.Black;
            this.txtLng.Size = new System.Drawing.Size(169, 30);
            this.txtLng.TabIndex = 3;
            this.txtLng.Text = "138.599732";
            this.txtLng.UseSystemPasswordChar = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(43, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 15);
            this.label5.TabIndex = 2;
            this.label5.Text = "Latitude";
            // 
            // txtLat
            // 
            this.txtLat.Location = new System.Drawing.Point(129, 56);
            this.txtLat.MaxLength = 32767;
            this.txtLat.Name = "txtLat";
            this.txtLat.ReadOnly = false;
            this.txtLat.Scheme = cTextBox.Schemes.Black;
            this.txtLat.Size = new System.Drawing.Size(169, 30);
            this.txtLat.TabIndex = 1;
            this.txtLat.Text = "-34.92577";
            this.txtLat.UseSystemPasswordChar = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "Start Location";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(43)))));
            this.tabPage3.Controls.Add(this.txtEvolveIV);
            this.tabPage3.Controls.Add(this.txtTransferIV);
            this.tabPage3.Controls.Add(this.txtCatchIV);
            this.tabPage3.Controls.Add(this.txtProbability);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.chkInverseBerries);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.clbBerries);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.txtEvolveCp);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.txtCatchCp);
            this.tabPage3.Controls.Add(this.chkInverseTransfer);
            this.tabPage3.Controls.Add(this.chkInverseCatch);
            this.tabPage3.Controls.Add(this.chkInverseEvolve);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.txtBelowCp);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.clbTransfer);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.clbCatch);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.clbEvolve);
            this.tabPage3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabPage3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tabPage3.Location = new System.Drawing.Point(174, 4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(736, 586);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Pokemon Settings";
            // 
            // txtProbability
            // 
            this.txtProbability.Location = new System.Drawing.Point(26, 542);
            this.txtProbability.MaxLength = 32767;
            this.txtProbability.Name = "txtProbability";
            this.txtProbability.ReadOnly = false;
            this.txtProbability.Scheme = cTextBox.Schemes.Black;
            this.txtProbability.Size = new System.Drawing.Size(207, 30);
            this.txtProbability.TabIndex = 19;
            this.txtProbability.Text = "40";
            this.txtProbability.UseSystemPasswordChar = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(25, 524);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(116, 15);
            this.label12.TabIndex = 18;
            this.label12.Text = "Probabilty Less Than";
            // 
            // chkInverseBerries
            // 
            this.chkInverseBerries.AutoSize = true;
            this.chkInverseBerries.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkInverseBerries.Location = new System.Drawing.Point(26, 493);
            this.chkInverseBerries.Name = "chkInverseBerries";
            this.chkInverseBerries.Size = new System.Drawing.Size(63, 19);
            this.chkInverseBerries.TabIndex = 17;
            this.chkInverseBerries.Text = "Inverse";
            this.chkInverseBerries.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(23, 304);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(84, 15);
            this.label13.TabIndex = 16;
            this.label13.Text = "Use Berries on:";
            // 
            // clbBerries
            // 
            this.clbBerries.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(43)))));
            this.clbBerries.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.clbBerries.CheckOnClick = true;
            this.clbBerries.ForeColor = System.Drawing.Color.Gainsboro;
            this.clbBerries.FormattingEnabled = true;
            this.clbBerries.Location = new System.Drawing.Point(26, 322);
            this.clbBerries.Name = "clbBerries";
            this.clbBerries.Size = new System.Drawing.Size(207, 162);
            this.clbBerries.TabIndex = 15;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(23, 238);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 15);
            this.label10.TabIndex = 14;
            this.label10.Text = "Over (CP/IV)";
            // 
            // txtEvolveCp
            // 
            this.txtEvolveCp.Location = new System.Drawing.Point(26, 256);
            this.txtEvolveCp.MaxLength = 32767;
            this.txtEvolveCp.Name = "txtEvolveCp";
            this.txtEvolveCp.ReadOnly = false;
            this.txtEvolveCp.Scheme = cTextBox.Schemes.Black;
            this.txtEvolveCp.Size = new System.Drawing.Size(116, 30);
            this.txtEvolveCp.TabIndex = 13;
            this.txtEvolveCp.Text = "50";
            this.txtEvolveCp.UseSystemPasswordChar = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(260, 238);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(175, 15);
            this.label9.TabIndex = 12;
            this.label9.Text = "Over (CP/IV) - Will Transfer Rest";
            // 
            // txtCatchCp
            // 
            this.txtCatchCp.Location = new System.Drawing.Point(263, 256);
            this.txtCatchCp.MaxLength = 32767;
            this.txtCatchCp.Name = "txtCatchCp";
            this.txtCatchCp.ReadOnly = false;
            this.txtCatchCp.Scheme = cTextBox.Schemes.Black;
            this.txtCatchCp.Size = new System.Drawing.Size(85, 30);
            this.txtCatchCp.TabIndex = 11;
            this.txtCatchCp.Text = "50";
            this.txtCatchCp.UseSystemPasswordChar = false;
            // 
            // chkInverseTransfer
            // 
            this.chkInverseTransfer.AutoSize = true;
            this.chkInverseTransfer.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkInverseTransfer.Location = new System.Drawing.Point(506, 207);
            this.chkInverseTransfer.Name = "chkInverseTransfer";
            this.chkInverseTransfer.Size = new System.Drawing.Size(63, 19);
            this.chkInverseTransfer.TabIndex = 10;
            this.chkInverseTransfer.Text = "Inverse";
            this.chkInverseTransfer.UseVisualStyleBackColor = true;
            // 
            // chkInverseCatch
            // 
            this.chkInverseCatch.AutoSize = true;
            this.chkInverseCatch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkInverseCatch.Location = new System.Drawing.Point(263, 207);
            this.chkInverseCatch.Name = "chkInverseCatch";
            this.chkInverseCatch.Size = new System.Drawing.Size(63, 19);
            this.chkInverseCatch.TabIndex = 9;
            this.chkInverseCatch.Text = "Inverse";
            this.chkInverseCatch.UseVisualStyleBackColor = true;
            // 
            // chkInverseEvolve
            // 
            this.chkInverseEvolve.AutoSize = true;
            this.chkInverseEvolve.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkInverseEvolve.Location = new System.Drawing.Point(26, 207);
            this.chkInverseEvolve.Name = "chkInverseEvolve";
            this.chkInverseEvolve.Size = new System.Drawing.Size(63, 19);
            this.chkInverseEvolve.TabIndex = 8;
            this.chkInverseEvolve.Text = "Inverse";
            this.chkInverseEvolve.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(503, 238);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 15);
            this.label8.TabIndex = 7;
            this.label8.Text = "Below (CP/IV)";
            // 
            // txtBelowCp
            // 
            this.txtBelowCp.Location = new System.Drawing.Point(506, 256);
            this.txtBelowCp.MaxLength = 32767;
            this.txtBelowCp.Name = "txtBelowCp";
            this.txtBelowCp.ReadOnly = false;
            this.txtBelowCp.Scheme = cTextBox.Schemes.Black;
            this.txtBelowCp.Size = new System.Drawing.Size(97, 30);
            this.txtBelowCp.TabIndex = 6;
            this.txtBelowCp.Text = "100";
            this.txtBelowCp.UseSystemPasswordChar = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(503, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Transfer Selected:";
            // 
            // clbTransfer
            // 
            this.clbTransfer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(43)))));
            this.clbTransfer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.clbTransfer.CheckOnClick = true;
            this.clbTransfer.ForeColor = System.Drawing.Color.Gainsboro;
            this.clbTransfer.FormattingEnabled = true;
            this.clbTransfer.Location = new System.Drawing.Point(506, 33);
            this.clbTransfer.Name = "clbTransfer";
            this.clbTransfer.Size = new System.Drawing.Size(207, 162);
            this.clbTransfer.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(260, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Catch Selected:";
            // 
            // clbCatch
            // 
            this.clbCatch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(43)))));
            this.clbCatch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.clbCatch.CheckOnClick = true;
            this.clbCatch.ForeColor = System.Drawing.Color.Gainsboro;
            this.clbCatch.FormattingEnabled = true;
            this.clbCatch.Location = new System.Drawing.Point(263, 33);
            this.clbCatch.Name = "clbCatch";
            this.clbCatch.Size = new System.Drawing.Size(207, 162);
            this.clbCatch.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Evolve Selected:";
            // 
            // clbEvolve
            // 
            this.clbEvolve.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(43)))));
            this.clbEvolve.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.clbEvolve.CheckOnClick = true;
            this.clbEvolve.ForeColor = System.Drawing.Color.Gainsboro;
            this.clbEvolve.FormattingEnabled = true;
            this.clbEvolve.Location = new System.Drawing.Point(26, 33);
            this.clbEvolve.Name = "clbEvolve";
            this.clbEvolve.Size = new System.Drawing.Size(207, 162);
            this.clbEvolve.TabIndex = 0;
            // 
            // tabPage6
            // 
            this.tabPage6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(43)))));
            this.tabPage6.Controls.Add(this.label24);
            this.tabPage6.Controls.Add(this.txtRB);
            this.tabPage6.Controls.Add(this.label23);
            this.tabPage6.Controls.Add(this.txtMaxRevive);
            this.tabPage6.Controls.Add(this.label22);
            this.tabPage6.Controls.Add(this.txtRevive);
            this.tabPage6.Controls.Add(this.label21);
            this.tabPage6.Controls.Add(this.txtMP);
            this.tabPage6.Controls.Add(this.label20);
            this.tabPage6.Controls.Add(this.txtHP);
            this.tabPage6.Controls.Add(this.label19);
            this.tabPage6.Controls.Add(this.txtSP);
            this.tabPage6.Controls.Add(this.label18);
            this.tabPage6.Controls.Add(this.txtP);
            this.tabPage6.Controls.Add(this.label17);
            this.tabPage6.Controls.Add(this.txtUPB);
            this.tabPage6.Controls.Add(this.label16);
            this.tabPage6.Controls.Add(this.txtGPB);
            this.tabPage6.Controls.Add(this.label15);
            this.tabPage6.Controls.Add(this.txtPB);
            this.tabPage6.Controls.Add(this.label14);
            this.tabPage6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabPage6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tabPage6.Location = new System.Drawing.Point(174, 4);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(736, 586);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Item Settings";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(81, 216);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(65, 15);
            this.label24.TabIndex = 22;
            this.label24.Text = "RazzBerries";
            // 
            // txtRB
            // 
            this.txtRB.Location = new System.Drawing.Point(167, 208);
            this.txtRB.MaxLength = 32767;
            this.txtRB.Name = "txtRB";
            this.txtRB.ReadOnly = false;
            this.txtRB.Scheme = cTextBox.Schemes.Black;
            this.txtRB.Size = new System.Drawing.Size(169, 30);
            this.txtRB.TabIndex = 21;
            this.txtRB.Text = "50";
            this.txtRB.UseSystemPasswordChar = false;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(81, 313);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(66, 15);
            this.label23.TabIndex = 20;
            this.label23.Text = "Max Revive";
            // 
            // txtMaxRevive
            // 
            this.txtMaxRevive.Location = new System.Drawing.Point(167, 305);
            this.txtMaxRevive.MaxLength = 32767;
            this.txtMaxRevive.Name = "txtMaxRevive";
            this.txtMaxRevive.ReadOnly = false;
            this.txtMaxRevive.Scheme = cTextBox.Schemes.Black;
            this.txtMaxRevive.Size = new System.Drawing.Size(169, 30);
            this.txtMaxRevive.TabIndex = 19;
            this.txtMaxRevive.Text = "25";
            this.txtMaxRevive.UseSystemPasswordChar = false;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(81, 277);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(41, 15);
            this.label22.TabIndex = 18;
            this.label22.Text = "Revive";
            // 
            // txtRevive
            // 
            this.txtRevive.Location = new System.Drawing.Point(167, 269);
            this.txtRevive.MaxLength = 32767;
            this.txtRevive.Name = "txtRevive";
            this.txtRevive.ReadOnly = false;
            this.txtRevive.Scheme = cTextBox.Schemes.Black;
            this.txtRevive.Size = new System.Drawing.Size(169, 30);
            this.txtRevive.TabIndex = 17;
            this.txtRevive.Text = "10";
            this.txtRevive.UseSystemPasswordChar = false;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(364, 216);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(50, 15);
            this.label21.TabIndex = 16;
            this.label21.Text = "Max Pot";
            // 
            // txtMP
            // 
            this.txtMP.Location = new System.Drawing.Point(450, 208);
            this.txtMP.MaxLength = 32767;
            this.txtMP.Name = "txtMP";
            this.txtMP.ReadOnly = false;
            this.txtMP.Scheme = cTextBox.Schemes.Black;
            this.txtMP.Size = new System.Drawing.Size(169, 30);
            this.txtMP.TabIndex = 15;
            this.txtMP.Text = "30";
            this.txtMP.UseSystemPasswordChar = false;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(364, 180);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(60, 15);
            this.label20.TabIndex = 14;
            this.label20.Text = "Hyper Pot";
            // 
            // txtHP
            // 
            this.txtHP.Location = new System.Drawing.Point(450, 172);
            this.txtHP.MaxLength = 32767;
            this.txtHP.Name = "txtHP";
            this.txtHP.ReadOnly = false;
            this.txtHP.Scheme = cTextBox.Schemes.Black;
            this.txtHP.Size = new System.Drawing.Size(169, 30);
            this.txtHP.TabIndex = 13;
            this.txtHP.Text = "20";
            this.txtHP.UseSystemPasswordChar = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(364, 144);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(58, 15);
            this.label19.TabIndex = 12;
            this.label19.Text = "Super Pot";
            // 
            // txtSP
            // 
            this.txtSP.Location = new System.Drawing.Point(450, 136);
            this.txtSP.MaxLength = 32767;
            this.txtSP.Name = "txtSP";
            this.txtSP.ReadOnly = false;
            this.txtSP.Scheme = cTextBox.Schemes.Black;
            this.txtSP.Size = new System.Drawing.Size(169, 30);
            this.txtSP.TabIndex = 11;
            this.txtSP.Text = "10";
            this.txtSP.UseSystemPasswordChar = false;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(364, 108);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(42, 15);
            this.label18.TabIndex = 10;
            this.label18.Text = "Potion";
            // 
            // txtP
            // 
            this.txtP.Location = new System.Drawing.Point(450, 100);
            this.txtP.MaxLength = 32767;
            this.txtP.Name = "txtP";
            this.txtP.ReadOnly = false;
            this.txtP.Scheme = cTextBox.Schemes.Black;
            this.txtP.Size = new System.Drawing.Size(169, 30);
            this.txtP.TabIndex = 9;
            this.txtP.Text = "10";
            this.txtP.UseSystemPasswordChar = false;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(81, 180);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(49, 15);
            this.label17.TabIndex = 8;
            this.label17.Text = "Ultra PB";
            // 
            // txtUPB
            // 
            this.txtUPB.Location = new System.Drawing.Point(167, 172);
            this.txtUPB.MaxLength = 32767;
            this.txtUPB.Name = "txtUPB";
            this.txtUPB.ReadOnly = false;
            this.txtUPB.Scheme = cTextBox.Schemes.Black;
            this.txtUPB.Size = new System.Drawing.Size(169, 30);
            this.txtUPB.TabIndex = 7;
            this.txtUPB.Text = "75";
            this.txtUPB.UseSystemPasswordChar = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(81, 144);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(52, 15);
            this.label16.TabIndex = 6;
            this.label16.Text = "Great PB";
            // 
            // txtGPB
            // 
            this.txtGPB.Location = new System.Drawing.Point(167, 136);
            this.txtGPB.MaxLength = 32767;
            this.txtGPB.Name = "txtGPB";
            this.txtGPB.ReadOnly = false;
            this.txtGPB.Scheme = cTextBox.Schemes.Black;
            this.txtGPB.Size = new System.Drawing.Size(169, 30);
            this.txtGPB.TabIndex = 5;
            this.txtGPB.Text = "50";
            this.txtGPB.UseSystemPasswordChar = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(81, 108);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(52, 15);
            this.label15.TabIndex = 4;
            this.label15.Text = "PokeBall";
            // 
            // txtPB
            // 
            this.txtPB.Location = new System.Drawing.Point(167, 100);
            this.txtPB.MaxLength = 32767;
            this.txtPB.Name = "txtPB";
            this.txtPB.ReadOnly = false;
            this.txtPB.Scheme = cTextBox.Schemes.Black;
            this.txtPB.Size = new System.Drawing.Size(169, 30);
            this.txtPB.TabIndex = 3;
            this.txtPB.Text = "100";
            this.txtPB.UseSystemPasswordChar = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(62, 47);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(85, 15);
            this.label14.TabIndex = 0;
            this.label14.Text = "Item Recycling";
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(43)))));
            this.tabPage4.Controls.Add(this.lvWalkLog);
            this.tabPage4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabPage4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tabPage4.Location = new System.Drawing.Point(174, 4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(736, 586);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Event Log";
            // 
            // lvWalkLog
            // 
            this.lvWalkLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvWalkLog.FullRowSelect = true;
            this.lvWalkLog.GridLines = true;
            this.lvWalkLog.Location = new System.Drawing.Point(50, 54);
            this.lvWalkLog.Name = "lvWalkLog";
            this.lvWalkLog.Size = new System.Drawing.Size(637, 491);
            this.lvWalkLog.TabIndex = 0;
            this.lvWalkLog.UseCompatibleStateImageBehavior = false;
            this.lvWalkLog.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Sender";
            this.columnHeader1.Width = 167;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Message";
            this.columnHeader2.Width = 445;
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(43)))));
            this.tabPage5.Controls.Add(this.btnResetSettings);
            this.tabPage5.Controls.Add(this.lblDump);
            this.tabPage5.Controls.Add(this.lblXP);
            this.tabPage5.Controls.Add(this.lblStardust);
            this.tabPage5.Controls.Add(this.lblTransferred);
            this.tabPage5.Controls.Add(this.lblFound);
            this.tabPage5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabPage5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tabPage5.Location = new System.Drawing.Point(174, 4);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(736, 586);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Statistics";
            // 
            // btnResetSettings
            // 
            this.btnResetSettings.Location = new System.Drawing.Point(625, 555);
            this.btnResetSettings.Name = "btnResetSettings";
            this.btnResetSettings.Scheme = cButton.Schemes.Black;
            this.btnResetSettings.Size = new System.Drawing.Size(108, 23);
            this.btnResetSettings.TabIndex = 5;
            this.btnResetSettings.Text = "Reset Settings";
            this.btnResetSettings.UseVisualStyleBackColor = true;
            this.btnResetSettings.Click += new System.EventHandler(this.btnResetSettings_Click);
            // 
            // lblDump
            // 
            this.lblDump.AutoSize = true;
            this.lblDump.Location = new System.Drawing.Point(62, 235);
            this.lblDump.MaximumSize = new System.Drawing.Size(250, 0);
            this.lblDump.Name = "lblDump";
            this.lblDump.Size = new System.Drawing.Size(12, 15);
            this.lblDump.TabIndex = 4;
            this.lblDump.Text = "-";
            // 
            // lblXP
            // 
            this.lblXP.AutoSize = true;
            this.lblXP.Location = new System.Drawing.Point(62, 165);
            this.lblXP.Name = "lblXP";
            this.lblXP.Size = new System.Drawing.Size(47, 15);
            this.lblXP.TabIndex = 3;
            this.lblXP.Text = "XP/H: 0";
            // 
            // lblStardust
            // 
            this.lblStardust.AutoSize = true;
            this.lblStardust.Location = new System.Drawing.Point(62, 131);
            this.lblStardust.Name = "lblStardust";
            this.lblStardust.Size = new System.Drawing.Size(62, 15);
            this.lblStardust.TabIndex = 2;
            this.lblStardust.Text = "Stardust: 0";
            // 
            // lblTransferred
            // 
            this.lblTransferred.AutoSize = true;
            this.lblTransferred.Location = new System.Drawing.Point(62, 97);
            this.lblTransferred.Name = "lblTransferred";
            this.lblTransferred.Size = new System.Drawing.Size(133, 15);
            this.lblTransferred.TabIndex = 1;
            this.lblTransferred.Text = "Pokemon Transferred: 0";
            // 
            // lblFound
            // 
            this.lblFound.AutoSize = true;
            this.lblFound.Location = new System.Drawing.Point(62, 62);
            this.lblFound.Name = "lblFound";
            this.lblFound.Size = new System.Drawing.Size(107, 15);
            this.lblFound.TabIndex = 0;
            this.lblFound.Text = "Pokemon Found: 0";
            // 
            // tabPage7
            // 
            this.tabPage7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(43)))));
            this.tabPage7.Controls.Add(this.textBox1);
            this.tabPage7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabPage7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tabPage7.Location = new System.Drawing.Point(174, 4);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(736, 586);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "Credits";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(43)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.ForeColor = System.Drawing.Color.Gainsboro;
            this.textBox1.Location = new System.Drawing.Point(229, 156);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(278, 167);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCatchIV
            // 
            this.txtCatchIV.Location = new System.Drawing.Point(354, 256);
            this.txtCatchIV.MaxLength = 32767;
            this.txtCatchIV.Name = "txtCatchIV";
            this.txtCatchIV.ReadOnly = false;
            this.txtCatchIV.Scheme = cTextBox.Schemes.Black;
            this.txtCatchIV.Size = new System.Drawing.Size(85, 30);
            this.txtCatchIV.TabIndex = 20;
            this.txtCatchIV.Text = "50";
            this.txtCatchIV.UseSystemPasswordChar = false;
            // 
            // txtTransferIV
            // 
            this.txtTransferIV.Location = new System.Drawing.Point(607, 256);
            this.txtTransferIV.MaxLength = 32767;
            this.txtTransferIV.Name = "txtTransferIV";
            this.txtTransferIV.ReadOnly = false;
            this.txtTransferIV.Scheme = cTextBox.Schemes.Black;
            this.txtTransferIV.Size = new System.Drawing.Size(85, 30);
            this.txtTransferIV.TabIndex = 21;
            this.txtTransferIV.Text = "25";
            this.txtTransferIV.UseSystemPasswordChar = false;
            // 
            // txtEvolveIV
            // 
            this.txtEvolveIV.Location = new System.Drawing.Point(148, 256);
            this.txtEvolveIV.MaxLength = 32767;
            this.txtEvolveIV.Name = "txtEvolveIV";
            this.txtEvolveIV.ReadOnly = false;
            this.txtEvolveIV.Scheme = cTextBox.Schemes.Black;
            this.txtEvolveIV.Size = new System.Drawing.Size(85, 30);
            this.txtEvolveIV.TabIndex = 22;
            this.txtEvolveIV.Text = "25";
            this.txtEvolveIV.UseSystemPasswordChar = false;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 594);
            this.Controls.Add(this.cTabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BasicGo - Small Pokemon Go Bot";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.cTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            this.tabPage7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private cTabControl cTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.CheckedListBox clbEvolve;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox clbCatch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckedListBox clbTransfer;
        private System.Windows.Forms.Label label6;
        private cTextBox txtLng;
        private System.Windows.Forms.Label label5;
        private cTextBox txtLat;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private cTextBox txtWalkSpeed;
        private System.Windows.Forms.Label label8;
        private cTextBox txtBelowCp;
        private cButton btnStart;
        private cButton btnStop;
        private cTextBox txtPass;
        private cTextBox txtUser;
        private cCheckBox chkInverseTransfer;
        private cCheckBox chkInverseCatch;
        private cCheckBox chkInverseEvolve;
        private System.Windows.Forms.ListView lvWalkLog;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private cCheckBox chkGetForts;
        private cCheckBox chkCatchPokes;
        private System.Windows.Forms.Label label10;
        private cTextBox txtEvolveCp;
        private System.Windows.Forms.Label label9;
        private cTextBox txtCatchCp;
        private System.Windows.Forms.Label label11;
        private cTextBox txtAltitude;
        private cCheckBox chkInverseBerries;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckedListBox clbBerries;
        private cTextBox txtProbability;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Timer tStats;
        private System.Windows.Forms.Label lblTransferred;
        private System.Windows.Forms.Label lblFound;
        private System.Windows.Forms.Label lblStardust;
        private System.Windows.Forms.Label lblXP;
        private System.Windows.Forms.Label lblDump;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Label label16;
        private cTextBox txtGPB;
        private System.Windows.Forms.Label label15;
        private cTextBox txtPB;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label21;
        private cTextBox txtMP;
        private System.Windows.Forms.Label label20;
        private cTextBox txtHP;
        private System.Windows.Forms.Label label19;
        private cTextBox txtSP;
        private System.Windows.Forms.Label label18;
        private cTextBox txtP;
        private System.Windows.Forms.Label label17;
        private cTextBox txtUPB;
        private System.Windows.Forms.Label label24;
        private cTextBox txtRB;
        private System.Windows.Forms.Label label23;
        private cTextBox txtMaxRevive;
        private System.Windows.Forms.Label label22;
        private cTextBox txtRevive;
        private BoosterComboBox cbAuthType;
        private cButton btnResetSettings;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.TextBox textBox1;
        private cTextBox txtCatchIV;
        private cTextBox txtTransferIV;
        private cTextBox txtEvolveIV;
    }
}

