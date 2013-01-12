namespace Visualisator
{
    partial class StationInfo
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblAssociatedAP = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnScan = new System.Windows.Forms.Button();
            this.lblMac = new System.Windows.Forms.Label();
            this.btnConnectToBSS = new System.Windows.Forms.Button();
            this.cmbAPList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDumpAll = new System.Windows.Forms.Button();
            this.txtDumpAll = new System.Windows.Forms.TextBox();
            this.tmrGUI = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblAssociatedAP);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnScan);
            this.groupBox1.Controls.Add(this.lblMac);
            this.groupBox1.Controls.Add(this.btnConnectToBSS);
            this.groupBox1.Controls.Add(this.cmbAPList);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(10, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(562, 220);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cennect Info";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // lblAssociatedAP
            // 
            this.lblAssociatedAP.AutoSize = true;
            this.lblAssociatedAP.Location = new System.Drawing.Point(28, 111);
            this.lblAssociatedAP.Name = "lblAssociatedAP";
            this.lblAssociatedAP.Size = new System.Drawing.Size(35, 13);
            this.lblAssociatedAP.TabIndex = 7;
            this.lblAssociatedAP.Text = "label3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "label2";
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(390, 50);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(92, 24);
            this.btnScan.TabIndex = 5;
            this.btnScan.Text = "Scan";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // lblMac
            // 
            this.lblMac.AutoSize = true;
            this.lblMac.Location = new System.Drawing.Point(28, 61);
            this.lblMac.Name = "lblMac";
            this.lblMac.Size = new System.Drawing.Size(35, 13);
            this.lblMac.TabIndex = 4;
            this.lblMac.Text = "label2";
            // 
            // btnConnectToBSS
            // 
            this.btnConnectToBSS.Location = new System.Drawing.Point(390, 23);
            this.btnConnectToBSS.Name = "btnConnectToBSS";
            this.btnConnectToBSS.Size = new System.Drawing.Size(92, 21);
            this.btnConnectToBSS.TabIndex = 3;
            this.btnConnectToBSS.Text = "Connect";
            this.btnConnectToBSS.UseVisualStyleBackColor = true;
            this.btnConnectToBSS.Click += new System.EventHandler(this.btnConnectToBSS_Click);
            // 
            // cmbAPList
            // 
            this.cmbAPList.FormattingEnabled = true;
            this.cmbAPList.Location = new System.Drawing.Point(229, 24);
            this.cmbAPList.Name = "cmbAPList";
            this.cmbAPList.Size = new System.Drawing.Size(126, 21);
            this.cmbAPList.TabIndex = 2;
            this.cmbAPList.SelectedIndexChanged += new System.EventHandler(this.cmbAPList_SelectedIndexChanged);
            this.cmbAPList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmbAPList_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(21, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Choose BSS[AP] to connect";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDumpAll);
            this.groupBox2.Controls.Add(this.txtDumpAll);
            this.groupBox2.Location = new System.Drawing.Point(12, 234);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(559, 288);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Send Data";
            // 
            // btnDumpAll
            // 
            this.btnDumpAll.Location = new System.Drawing.Point(70, 0);
            this.btnDumpAll.Name = "btnDumpAll";
            this.btnDumpAll.Size = new System.Drawing.Size(103, 23);
            this.btnDumpAll.TabIndex = 1;
            this.btnDumpAll.Text = "DumpAll";
            this.btnDumpAll.UseVisualStyleBackColor = true;
            this.btnDumpAll.Click += new System.EventHandler(this.btnDumpAll_Click);
            // 
            // txtDumpAll
            // 
            this.txtDumpAll.Location = new System.Drawing.Point(6, 29);
            this.txtDumpAll.Multiline = true;
            this.txtDumpAll.Name = "txtDumpAll";
            this.txtDumpAll.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDumpAll.Size = new System.Drawing.Size(547, 252);
            this.txtDumpAll.TabIndex = 0;
            this.txtDumpAll.TextChanged += new System.EventHandler(this.txtDumpAll_TextChanged);
            // 
            // tmrGUI
            // 
            this.tmrGUI.Enabled = true;
            this.tmrGUI.Interval = 20;
            this.tmrGUI.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // StationInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 527);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "StationInfo";
            this.Text = "StationInfo";
            this.Load += new System.EventHandler(this.StationInfo_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbAPList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnConnectToBSS;
        private System.Windows.Forms.Label lblMac;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Timer tmrGUI;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDumpAll;
        private System.Windows.Forms.TextBox txtDumpAll;
        private System.Windows.Forms.Label lblAssociatedAP;
    }
}