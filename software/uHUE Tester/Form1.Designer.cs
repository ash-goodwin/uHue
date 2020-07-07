namespace uHUE_Tester
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing && (components != null) ) {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label label1;
            this.comboPorts = new System.Windows.Forms.ComboBox();
            this.btnRefreshPorts = new System.Windows.Forms.Button();
            this.btnRed = new System.Windows.Forms.Button();
            this.btnGreen = new System.Windows.Forms.Button();
            this.btnBlue = new System.Windows.Forms.Button();
            this.btnOff = new System.Windows.Forms.Button();
            this.radSolid = new System.Windows.Forms.RadioButton();
            this.btnOpenPort = new System.Windows.Forms.Button();
            this.radBlink = new System.Windows.Forms.RadioButton();
            this.radFade = new System.Windows.Forms.RadioButton();
            this.numCycleTimeMs = new System.Windows.Forms.NumericUpDown();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnVersion = new System.Windows.Forms.Button();
            this.btnWhite = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numCycleTimeMs)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(436, 88);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(80, 13);
            label1.TabIndex = 11;
            label1.Text = "Cycle time (ms):";
            // 
            // comboPorts
            // 
            this.comboPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPorts.FormattingEnabled = true;
            this.comboPorts.Location = new System.Drawing.Point(12, 12);
            this.comboPorts.Name = "comboPorts";
            this.comboPorts.Size = new System.Drawing.Size(156, 21);
            this.comboPorts.TabIndex = 0;
            // 
            // btnRefreshPorts
            // 
            this.btnRefreshPorts.Location = new System.Drawing.Point(174, 10);
            this.btnRefreshPorts.Name = "btnRefreshPorts";
            this.btnRefreshPorts.Size = new System.Drawing.Size(75, 23);
            this.btnRefreshPorts.TabIndex = 1;
            this.btnRefreshPorts.Text = "Refresh";
            this.btnRefreshPorts.UseVisualStyleBackColor = true;
            this.btnRefreshPorts.Click += new System.EventHandler(this.btnRefreshPorts_Click);
            // 
            // btnRed
            // 
            this.btnRed.Location = new System.Drawing.Point(12, 88);
            this.btnRed.Name = "btnRed";
            this.btnRed.Size = new System.Drawing.Size(75, 53);
            this.btnRed.TabIndex = 2;
            this.btnRed.Text = "Red";
            this.btnRed.UseVisualStyleBackColor = true;
            // 
            // btnGreen
            // 
            this.btnGreen.Location = new System.Drawing.Point(93, 88);
            this.btnGreen.Name = "btnGreen";
            this.btnGreen.Size = new System.Drawing.Size(75, 53);
            this.btnGreen.TabIndex = 3;
            this.btnGreen.Text = "Green";
            this.btnGreen.UseVisualStyleBackColor = true;
            // 
            // btnBlue
            // 
            this.btnBlue.Location = new System.Drawing.Point(174, 88);
            this.btnBlue.Name = "btnBlue";
            this.btnBlue.Size = new System.Drawing.Size(75, 53);
            this.btnBlue.TabIndex = 4;
            this.btnBlue.Text = "Blue";
            this.btnBlue.UseVisualStyleBackColor = true;
            // 
            // btnOff
            // 
            this.btnOff.Location = new System.Drawing.Point(336, 88);
            this.btnOff.Name = "btnOff";
            this.btnOff.Size = new System.Drawing.Size(75, 53);
            this.btnOff.TabIndex = 5;
            this.btnOff.Text = "Off";
            this.btnOff.UseVisualStyleBackColor = true;
            // 
            // radSolid
            // 
            this.radSolid.AutoSize = true;
            this.radSolid.Checked = true;
            this.radSolid.Location = new System.Drawing.Point(12, 49);
            this.radSolid.Name = "radSolid";
            this.radSolid.Size = new System.Drawing.Size(48, 17);
            this.radSolid.TabIndex = 6;
            this.radSolid.TabStop = true;
            this.radSolid.Text = "Solid";
            this.radSolid.UseVisualStyleBackColor = true;
            // 
            // btnOpenPort
            // 
            this.btnOpenPort.Location = new System.Drawing.Point(255, 10);
            this.btnOpenPort.Name = "btnOpenPort";
            this.btnOpenPort.Size = new System.Drawing.Size(75, 23);
            this.btnOpenPort.TabIndex = 7;
            this.btnOpenPort.Text = "Open";
            this.btnOpenPort.UseVisualStyleBackColor = true;
            // 
            // radBlink
            // 
            this.radBlink.AutoSize = true;
            this.radBlink.Location = new System.Drawing.Point(93, 49);
            this.radBlink.Name = "radBlink";
            this.radBlink.Size = new System.Drawing.Size(48, 17);
            this.radBlink.TabIndex = 8;
            this.radBlink.Text = "Blink";
            this.radBlink.UseVisualStyleBackColor = true;
            // 
            // radFade
            // 
            this.radFade.AutoSize = true;
            this.radFade.Location = new System.Drawing.Point(174, 49);
            this.radFade.Name = "radFade";
            this.radFade.Size = new System.Drawing.Size(49, 17);
            this.radFade.TabIndex = 9;
            this.radFade.Text = "Fade";
            this.radFade.UseVisualStyleBackColor = true;
            // 
            // numCycleTimeMs
            // 
            this.numCycleTimeMs.Location = new System.Drawing.Point(439, 106);
            this.numCycleTimeMs.Maximum = new decimal(new int[] {
            25600,
            0,
            0,
            0});
            this.numCycleTimeMs.Name = "numCycleTimeMs";
            this.numCycleTimeMs.Size = new System.Drawing.Size(120, 20);
            this.numCycleTimeMs.TabIndex = 10;
            this.numCycleTimeMs.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.Location = new System.Drawing.Point(12, 156);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(776, 151);
            this.txtLog.TabIndex = 12;
            // 
            // btnVersion
            // 
            this.btnVersion.Location = new System.Drawing.Point(336, 10);
            this.btnVersion.Name = "btnVersion";
            this.btnVersion.Size = new System.Drawing.Size(75, 23);
            this.btnVersion.TabIndex = 13;
            this.btnVersion.Text = "Version";
            this.btnVersion.UseVisualStyleBackColor = true;
            // 
            // btnWhite
            // 
            this.btnWhite.Location = new System.Drawing.Point(255, 88);
            this.btnWhite.Name = "btnWhite";
            this.btnWhite.Size = new System.Drawing.Size(75, 53);
            this.btnWhite.TabIndex = 14;
            this.btnWhite.Text = "White";
            this.btnWhite.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 319);
            this.Controls.Add(this.btnWhite);
            this.Controls.Add(this.btnVersion);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(label1);
            this.Controls.Add(this.numCycleTimeMs);
            this.Controls.Add(this.radFade);
            this.Controls.Add(this.radBlink);
            this.Controls.Add(this.btnOpenPort);
            this.Controls.Add(this.radSolid);
            this.Controls.Add(this.btnOff);
            this.Controls.Add(this.btnBlue);
            this.Controls.Add(this.btnGreen);
            this.Controls.Add(this.btnRed);
            this.Controls.Add(this.btnRefreshPorts);
            this.Controls.Add(this.comboPorts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "uHUE Tester";
            ((System.ComponentModel.ISupportInitialize)(this.numCycleTimeMs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboPorts;
        private System.Windows.Forms.Button btnRefreshPorts;
        private System.Windows.Forms.Button btnRed;
        private System.Windows.Forms.Button btnGreen;
        private System.Windows.Forms.Button btnBlue;
        private System.Windows.Forms.Button btnOff;
        private System.Windows.Forms.RadioButton radSolid;
        private System.Windows.Forms.Button btnOpenPort;
        private System.Windows.Forms.RadioButton radBlink;
        private System.Windows.Forms.RadioButton radFade;
        private System.Windows.Forms.NumericUpDown numCycleTimeMs;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnVersion;
        private System.Windows.Forms.Button btnWhite;
    }
}

